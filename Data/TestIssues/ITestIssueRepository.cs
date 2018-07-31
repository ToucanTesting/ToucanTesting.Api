using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Api.Interfaces;
using ToucanTesting.Models;

namespace ToucanTesting.Data
{
    public interface ITestIssueRepository : IPageable
    {
        Task<TestIssue> Get(long testIssueId);
        Task<List<TestIssue>> GetAll();
        Task<List<TestIssue>> GetPage(int pageNumber, int pageSize, string searchText);
        void Add(TestIssue testIssue);
        void Update(TestIssue testIssue);
        void Remove(TestIssue testIssue);
    }
}