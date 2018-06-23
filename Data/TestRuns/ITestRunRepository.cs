using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Api.Interfaces;
using ToucanTesting.Controllers.TestSuites;
using ToucanTesting.Models;

namespace ToucanTesting.Data
{
    public interface ITestRunRepository : IPageable
    {
        Task<TestRun> Get(long testRunId, bool includeRelated);
        Task<List<TestRun>> GetPage(int pageNumber, int pageSize);
        void Add(TestRun testRun);
        void Update(TestRun testRun);
        void Remove(TestRun testRun);
    }
}