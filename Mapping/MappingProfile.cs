using AutoMapper;
using TucanTesting.Controllers.TestModules;
using TucanTesting.Controllers.TestSuites;
using TucanTesting.Controllers.TestCases;
using TucanTesting.Controllers.TestActions;
using TucanTesting.Controllers.TestConditions;
using TucanTesting.Controllers.TestResults;
using TucanTesting.Models;
using TucanTesting.Controllers.ExpectedResults;

namespace TucanTesting.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TestSuite, TestSuiteResource>();
            CreateMap<TestSuiteResource, TestSuite>();
            CreateMap<TestRun, TestRunResource>();
            CreateMap<TestRunResource, TestRun>();
            CreateMap<TestModule, TestModuleResource>();
            CreateMap<TestModuleResource, TestModule>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<TestCase, TestCaseResource>();
            CreateMap<TestCaseResource, TestCase>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<TestAction, TestActionResource>();
            CreateMap<TestActionResource, TestAction>();
            CreateMap<ExpectedResult, ExpectedResultResource>();
            CreateMap<ExpectedResultResource, ExpectedResult>();
            CreateMap<TestCondition, TestConditionResource>();
            CreateMap<TestConditionResource, TestCondition>();
            CreateMap<TestResult, TestResultResource>();
            CreateMap<TestResultResource, TestResult>();

        }
    }
}