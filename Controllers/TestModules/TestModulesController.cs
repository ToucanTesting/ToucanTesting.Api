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

namespace ToucanTesting.Controllers.TestModules
{
    [Route("/test-suites/{testSuiteId}/test-modules")]
    public class TestModulesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITestModuleRepository _repository;

        public TestModulesController(IMapper mapper, IUnitOfWork unitOfWork, ITestModuleRepository repository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestModule([FromBody] TestModuleResource resource)
        {
            try
            {
                var testModule = _mapper.Map<TestModuleResource, TestModule>(resource);
                _repository.Add(testModule);
                await _unitOfWork.CompleteAsync();

                var result = _mapper.Map<TestModule, TestModuleResource>(testModule);

                return Ok(result);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTestModules(long testSuiteId, [FromQuery]DateTime? beforeDate, [FromQuery]bool? isReport)
        {
            try
            {
                var testModules = await _repository.GetAll(testSuiteId, beforeDate, isReport);
                return Ok(_mapper.Map<List<TestModule>, List<TestModuleResource>>(testModules));
            } 
            catch (Exception err) {
                return BadRequest(err);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestModule(long id)
        {
            var testModule = await _repository.Get(id);

            if (testModule == null)
            {
                return NotFound();
            }

            var componentResource = _mapper.Map<TestModule, TestModuleResource>(testModule);
            return Ok(componentResource);
        }

        [HttpPut("{id}")]
        [ValidateModelIdFilter("id", "moduleResource")]
        public async Task<IActionResult> UpdateTestModule(long id, [FromBody] TestModuleResource moduleResource)
        {
            var testModule = await _repository.Get(id);

            if (testModule == null)
                return NotFound();

            var result = _mapper.Map<TestModuleResource, TestModule>(moduleResource, testModule);
            await _unitOfWork.CompleteAsync();
            return Ok(result);
        }

        [HttpPut("{targetId}/sort")]
        public async Task<IActionResult> SortTestAction(long targetId, [FromBody] TestModuleResource moduleResource)
        {
            var fromModule = _mapper.Map<TestModuleResource, TestModule>(moduleResource);
            List<TestModule> result = await _repository.Sort(fromModule, targetId);
            return Ok(_mapper.Map<List<TestModule>, List<TestModuleResource>>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestModule(long id)
        {
            var testModule = await _repository.Get(id);

            if (testModule == null)
                return NotFound();

            _repository.Remove(testModule);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}