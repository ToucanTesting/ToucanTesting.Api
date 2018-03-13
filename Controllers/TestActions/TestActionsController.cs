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
using Microsoft.AspNetCore.Authorization;

namespace TucanTesting.Controllers.TestActions
{
    [Route("/test-actions")]
    [Authorize]
    public class TestActionsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestActionRepository _repository;

        public TestActionsController(IMapper mapper, IUnitOfWork unitOfWork, ITestActionRepository repository)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestAction([FromBody] TestActionResource resource)
        {
            var testAction = _mapper.Map<TestActionResource, TestAction>(resource);
            _repository.Add(testAction);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<TestAction, TestActionResource>(testAction);

            return Ok(result);
        }

        [HttpGet]
        [Route("/test-suites/{testSuiteId}/test-modules/{testModuleId}/test-cases/{testCaseId}/test-actions")]
        public async Task<List<TestActionResource>> GetTestActions(long testCaseId)
        {
            var testActions = await _repository.GetAll(testCaseId);
            return _mapper.Map<List<TestAction>, List<TestActionResource>>(testActions);
        }

        [HttpPut("{id}")]
        [ValidateModelIdFilter("id", "testActionResource")]
        public async Task<IActionResult> UpdateTestAction(long id, [FromBody] TestActionResource testActionResource)
        {
            var testAction = await _repository.Get(id);

            if (testAction == null)
                return NotFound();

            var result = _mapper.Map<TestActionResource, TestAction>(testActionResource, testAction);
            await _unitOfWork.CompleteAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestAction(long id)
        {
            var testAction = await _repository.Get(id);

            if (testAction == null)
                return NotFound();

            _repository.Remove(testAction);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}