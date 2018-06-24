using AutoMapper;
using ToucanTesting.Controllers.TestModules;
using ToucanTesting.Controllers.TestSuites;
using ToucanTesting.Controllers.TestCases;
using ToucanTesting.Controllers.TestActions;
using ToucanTesting.Controllers.TestConditions;
using ToucanTesting.Controllers.TestResults;
using ToucanTesting.Models;
using ToucanTesting.Controllers.ExpectedResults;
using ToucanTesting.Api.Controllers.TestIssues;

namespace ToucanTesting.Mapping
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
            CreateMap<TestModuleResource, TestModule>();

            CreateMap<TestCase, TestCaseResource>();
            CreateMap<TestCaseResource, TestCase>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.TestIssues, opt => opt.Ignore())
                .ForMember(c => c.TestActions, opt => opt.Ignore())
                .ForMember(c => c.ExpectedResults, opt => opt.Ignore())
                .ForMember(c => c.TestConditions, opt => opt.Ignore());

            CreateMap<TestAction, TestActionResource>();
            CreateMap<TestActionResource, TestAction>();

            CreateMap<ExpectedResult, ExpectedResultResource>();
            CreateMap<ExpectedResultResource, ExpectedResult>();

            CreateMap<TestCondition, TestConditionResource>();
            CreateMap<TestConditionResource, TestCondition>();

            CreateMap<TestResult, TestResultResource>();
            CreateMap<TestResultResource, TestResult>();
            
            CreateMap<TestIssue, TestIssueResource>();
            CreateMap<TestIssueResource, TestIssue>();

        }
    }
}