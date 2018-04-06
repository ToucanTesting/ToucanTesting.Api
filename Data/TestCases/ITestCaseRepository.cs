using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TucanTesting.Models;

namespace TucanTesting.Data
{
    public interface ITestCaseRepository
    {
        Task<TestCase> Get (long id, bool includeRelated = true);
        Task<List<TestCase>> GetAll(long testModuleId, DateTime? beforeDate);
        Task<List<TestIssue>> GetIssues(long testCaseId);
        void Add (TestCase testCase);
        Task<TestCase> Duplicate (long testCaseId);
        void Update (TestCase testCase);
        void Remove (TestCase testCase);
    }
}