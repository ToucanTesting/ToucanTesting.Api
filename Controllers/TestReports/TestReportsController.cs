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

namespace ToucanTesting.Controllers.TestReports
{
    [Route("/test-reports/{testRunId}")]
    public class TestModulesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        // private readonly ITestReportRepository _repository;

        // public TestModulesController(IMapper mapper, IUnitOfWork unitOfWork, ITestReportRepository repository)
        // {
        //     this._repository = repository;
        //     this._unitOfWork = unitOfWork;
        //     this._mapper = mapper;
        // }

        // [HttpGet]
        // public async Task<IActionResult> GetTestReport(long testSuiteId, [FromQuery]DateTime? beforeDate, [FromQuery]bool? isReport)
        // {
        //     try
        //     {
        //         var testModules = await _repository.GetAll(testSuiteId, beforeDate, isReport);
        //         return Ok(_mapper.Map<List<TestModule>, List<TestModuleResource>>(testModules));
        //     } 
        //     catch (Exception err) {
        //         return BadRequest(err);
        //     }
        // }
    }
}