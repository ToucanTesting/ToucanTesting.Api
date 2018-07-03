using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Models;

namespace ToucanTesting.Data
{
    public interface ITestCaseRepository
    {
        Task<TestCase> Get (long id, bool includeRelated = true);
        Task<List<TestCase>> GetAll(long testModuleId, DateTime? beforeDate);
        Task<List<TestCase>> GetContains(string searchText);
        Task<List<TestIssue>> GetIssues(long testCaseId);
        Task<Boolean> CheckAutomationId(long testCaseId, string automationId);
        void Add (TestCase testCase);
        Task<TestCase> Duplicate (long testCaseId);
        void Update (TestCase testCase);
        void Remove (TestCase testCase);
    }
}