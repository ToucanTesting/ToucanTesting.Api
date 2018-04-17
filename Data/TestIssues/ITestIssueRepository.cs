using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Models;

namespace ToucanTesting.Data
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