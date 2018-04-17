using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Controllers.TestSuites;
using ToucanTesting.Models;

namespace ToucanTesting.Data
{
    public interface ITestRunRepository
    {
        Task<TestRun> Get(long testRunId, bool includeRelated);
        Task<List<TestRun>> GetAll();
        void Add(TestRun testRun);
        void Update(TestRun testRun);
        void Remove(TestRun testRun);
    }
}