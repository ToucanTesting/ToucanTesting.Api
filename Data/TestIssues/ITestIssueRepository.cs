using System.Collections.Generic;
using System.Threading.Tasks;
using TucanTesting.Models;

namespace TucanTesting.Data
{
    public interface ITestIssueRepository
    {
        Task<TestIssue> Get(long testIssueId);
        Task<List<TestIssue>> GetAll();
        void Add(TestIssue testIssue);

        void Update(TestIssue testIssue);
        void Remove(TestIssue testIssue);
    }
}