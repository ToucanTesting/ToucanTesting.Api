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
using ToucanTesting.Api.Controllers.TestIssues;

namespace ToucanTesting.Controllers.TestCases
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
        [Route("/test-cases/{testCaseId}")]
        public async Task<TestCaseResource> GetTestCases(long testCaseId)
        {
            var testCase = await _repository.Get(testCaseId);
            return _mapper.Map<TestCase, TestCaseResource>(testCase);
        }

        [HttpGet]
        [Route("/test-cases")]
        public async Task<List<TestCaseResource>> GetTestCases([FromQuery]string searchText)
        {

            var testCases = await _repository.GetContains(searchText);
            return _mapper.Map<List<TestCase>, List<TestCaseResource>>(testCases);
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
        public async Task<List<TestIssueResource>> GetTestIssues(long testCaseId)
        {
            var testIssues = await _repository.GetIssues(testCaseId);
            return _mapper.Map<List<TestIssue>, List<TestIssueResource>>(testIssues);
        }

        [HttpPut]
        [Route("/test-cases/{testCaseId}")]
        [ValidateModelIdFilter("testCaseId", "testCaseResource")]
        public async Task<IActionResult> UpdateTestCase(long testCaseId, [FromBody] TestCaseResource testCaseResource)
        {
            try
            {
                if (!string.IsNullOrEmpty(testCaseResource.AutomationId))
                {
                    var automationIdInUse = await _repository.CheckAutomationId(testCaseId, testCaseResource.AutomationId);
                }
                var testCase = await _repository.Get(testCaseId);

                if (testCase == null)
                {
                    return NotFound();
                }

                var result = _mapper.Map<TestCaseResource, TestCase>(testCaseResource, testCase);
                _repository.Update(result);
                await _unitOfWork.CompleteAsync();
                return Ok(result);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }


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