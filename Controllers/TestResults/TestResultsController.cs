using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToucanTesting.Models;
using ToucanTesting.Data;
using System.Linq;
using ToucanTesting.Filters;

namespace ToucanTesting.Controllers.TestResults
{
    [Route("/test-results")]
    public class TestResultsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestResultRepository _repository;

        public TestResultsController(IMapper mapper, IUnitOfWork unitOfWork, ITestResultRepository repository)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestResult([FromBody] TestResultResource testResultResource)
        {
            var testResult = _mapper.Map<TestResultResource, TestResult>(testResultResource);
            var existsResult = await _repository.GetIfExists(testResult);

            if (existsResult == null)
            {
                _repository.Add(testResult);
            }
            else
            {
                testResult = _mapper.Map<TestResultResource, TestResult>(testResultResource, existsResult);
                _repository.Update(testResult);
            }

            var result = _mapper.Map<TestResult, TestResultResource>(testResult);
            await _unitOfWork.CompleteAsync();
            return Ok(result);

        }

        [HttpGet]
        public async Task<List<TestResultResource>> GetTestResults([FromQuery] long testRunId, [FromQuery] long testModuleId = -1)
        {
            var testResults = (testModuleId > -1 ) ? await _repository.GetAll(testRunId) : await _repository.GetAll(testRunId, testModuleId);
            return _mapper.Map<List<TestResult>, List<TestResultResource>>(testResults);
        }

        [HttpPut("{id}")]
        [ValidateModelIdFilter("id", "testResultResource")]
        public async Task<IActionResult> UpdateTestResult(long id, [FromBody] TestResultResource testResultResource)
        {
            var testResult = await _repository.Get(id);

            if (testResult == null)
                return NotFound();

            var result = _mapper.Map<TestResultResource, TestResult>(testResultResource, testResult);
            await _unitOfWork.CompleteAsync();
            return Ok(result);
        }
    }
}