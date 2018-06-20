using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToucanTesting.Data;
using ToucanTesting.Filters;
using ToucanTesting.Models;

namespace ToucanTesting.Api.Controllers.TestIssues
{
    [Route("/test-issues")]
    public class TestIssuesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestIssueRepository _repository;

        public TestIssuesController(IMapper mapper, IUnitOfWork unitOfWork, ITestIssueRepository repository)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestCondition([FromBody] TestIssueResource testIssueResource)
        {
            var testIssue = _mapper.Map<TestIssueResource, TestIssue>(testIssueResource);
            _repository.Add(testIssue);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<TestIssue, TestIssueResource>(testIssue);

            return Ok(result);
        }

        [HttpGet]
        public async Task<List<TestIssueResource>> GetAll()
        {
            var testIssues = await _repository.GetAll();
            return _mapper.Map<List<TestIssue>, List<TestIssueResource>>(testIssues);
        }

        [HttpPut]
        [Route("/test-issues/{testIssueId}")]
        [ValidateModelIdFilter("testIssueId", "testIssueResource")]
        public async Task<IActionResult> UpdateTestCase(long testIssueId, [FromBody] TestIssueResource testIssueResource)
        {
            var testIssue = await _repository.Get(testIssueId);

            if (testIssue == null)
                return NotFound();

            var result = _mapper.Map<TestIssueResource, TestIssue>(testIssueResource, testIssue);
            _repository.Update(result);
            await _unitOfWork.CompleteAsync();
            return Ok(result);
        }

        [HttpDelete("{testIssueId}")]
        public async Task<IActionResult> Delete(long testIssueId)
        {
            var testIssue = await _repository.Get(testIssueId);

            if (testIssue == null)
                return NotFound();

            _repository.Remove(testIssue);
            await _unitOfWork.CompleteAsync();

            return Ok(testIssue);
        }
    }
}
