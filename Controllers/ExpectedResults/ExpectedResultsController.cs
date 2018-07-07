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

namespace ToucanTesting.Controllers.ExpectedResults
{
    [Route("/expected-results")]
    public class ExpectedResultsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpectedResultRepository _repository;

        public ExpectedResultsController(IMapper mapper, IUnitOfWork unitOfWork, IExpectedResultRepository repository)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpectedResult([FromBody] ExpectedResultResource resource)
        {
            var expectedResult = _mapper.Map<ExpectedResultResource, ExpectedResult>(resource);
            _repository.Add(expectedResult);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<ExpectedResult, ExpectedResultResource>(expectedResult);

            return Ok(result);
        }

        [HttpGet]
        [Route("/test-cases/{testCaseId}/expected-results")]
        public async Task<List<ExpectedResultResource>> GetExpectedResults(long testCaseId)
        {
            var expectedResults = await _repository.GetAll(testCaseId);
            return _mapper.Map<List<ExpectedResult>, List<ExpectedResultResource>>(expectedResults);
        }

        [HttpPut("{id}")]
        [ValidateModelIdFilter("id", "expectedResultResource")]
        public async Task<IActionResult> UpdateExpectedResult(long id, [FromBody] ExpectedResultResource expectedResultResource)
        {
            var expectedResult = await _repository.Get(id);

            if (expectedResult == null)
                return NotFound();

            var result = _mapper.Map<ExpectedResultResource, ExpectedResult>(expectedResultResource, expectedResult);
            await _unitOfWork.CompleteAsync();
            return Ok(result);
        }

        [HttpPut("{targetId}/sort")]
        public async Task<IActionResult> SortExpectedResults(long targetId, [FromBody] ExpectedResultResource expectedResultResource)
        {
            var origin = _mapper.Map<ExpectedResultResource, ExpectedResult>(expectedResultResource);
            List<ExpectedResult> result = await _repository.Sort(origin, targetId);
            return Ok(_mapper.Map<List<ExpectedResult>, List<ExpectedResultResource>>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpectedResult(long id)
        {
            var expectedResult = await _repository.Get(id);

            if (expectedResult == null)
                return NotFound();

            _repository.Remove(expectedResult);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
