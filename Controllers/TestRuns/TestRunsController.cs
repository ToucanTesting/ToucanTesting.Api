using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToucanTesting.Models;
using ToucanTesting.Data;

namespace ToucanTesting.Controllers.TestSuites
{
    [Route("/test-runs")]
    public class TestRunsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITestRunRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TestRunsController(IMapper mapper, ITestRunRepository repository, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<List<TestRunResource>> GetTestRuns([FromQuery]int pageNumber, [FromQuery]int pageSize)
        {
            var pageCount = await _repository.GetPageCount(pageSize);
            var testRuns = await _repository.GetPage(pageNumber, pageSize);
            HttpContext.Response.Headers.Add("totalPages", (pageCount >= 1) ? pageCount.ToString() : "1");

            return _mapper.Map<List<TestRun>, List<TestRunResource>>(testRuns);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestRun(long id, [FromQuery]bool results = false)
        {
            var testRun = await _repository.Get(id, results);

            if (testRun == null)
            {
                return NotFound();
            }

            var testRunResource = _mapper.Map<TestRun, TestRunResource>(testRun);
            return Ok(testRunResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestRun([FromBody] TestRunResource resource)
        {
            var testRun = _mapper.Map<TestRunResource, TestRun>(resource);
            _repository.Add(testRun);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<TestRun, TestRunResource>(testRun);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTestRun([FromBody] TestRunResource runResource)
        {
            var testRun = _mapper.Map<TestRunResource, TestRun>(runResource);
            _repository.Update(testRun);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<TestRun, TestRunResource>(testRun);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestRun(long id)
        {
            var testRun = await _repository.Get(id, includeRelated: false);

            if (testRun == null)
            {
                return NotFound();
            }

            _repository.Remove(testRun);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}