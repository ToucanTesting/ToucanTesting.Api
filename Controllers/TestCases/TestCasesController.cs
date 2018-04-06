using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TucanTesting.Models;
using TucanTesting.Data;
using System.Linq;
using TucanTesting.Filters;
using TucanTesting.Api.Controllers.TestIssues;

namespace TucanTesting.Controllers.TestCases
{
    public class TestCasesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestCaseRepository _repository;

        public TestCasesController(IMapper mapper, IUnitOfWork unitOfWork, ITestCaseRepository repository)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        [Route("/test-cases")]
        public async Task<IActionResult> CreateTestCase([FromBody] TestCaseResource resource)
        {
            var testCase = _mapper.Map<TestCaseResource, TestCase>(resource);
            _repository.Add(testCase);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<TestCase, TestCaseResource>(testCase);

            return Ok(result);
        }

        [HttpPost]
        [Route("/test-cases/{testCaseId}")]
        public async Task<IActionResult> DuplicateTestCase(long testCaseId)
        {
            var testCase = await _repository.Duplicate(testCaseId);
            _repository.Add(testCase);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<TestCase, TestCaseResource>(testCase);

            return Ok(result);
        }

        [HttpGet]
        [Route("/test-suites/{testSuiteId}/test-modules/{testModuleId}/test-cases")]
        public async Task<List<TestCaseResource>> GetTestCases(long testModuleId, [FromQuery]DateTime? beforeDate)
        {

            var testCases = await _repository.GetAll(testModuleId, beforeDate);
            return _mapper.Map<List<TestCase>, List<TestCaseResource>>(testCases);
        }

        [HttpGet]
        [Route("/test-cases/{testCaseId}/test-issues/")]
        public async Task<List<TestIssueResource>> GetTestCases(long testCaseId)
        {
            var testIssues = await _repository.GetIssues(testCaseId);
            return _mapper.Map<List<TestIssue>, List<TestIssueResource>>(testIssues);
        }

        [HttpPut]
        [Route("/test-cases/{testCaseId}")]
        [ValidateModelIdFilter("testCaseId", "testCaseResource")]
        public async Task<IActionResult> UpdateTestCase(long testCaseId, [FromBody] TestCaseResource testCaseResource)
        {
            var testCase = await _repository.Get(testCaseId);

            if (testCase == null)
                return NotFound();

            var result = _mapper.Map<TestCaseResource, TestCase>(testCaseResource, testCase);
            _repository.Update(result);
            await _unitOfWork.CompleteAsync();
            return Ok(result);
        }

        [HttpDelete]
        [Route("/test-cases/{testCaseId}")]
        public async Task<IActionResult> DeleteTestCase(long testCaseId)
        {
            var testCase = await _repository.Get(testCaseId, includeRelated: false);

            if (testCase == null)
                return NotFound();

            _repository.Remove(testCase);
            await _unitOfWork.CompleteAsync();

            return Ok(testCaseId);
        }
    }
}