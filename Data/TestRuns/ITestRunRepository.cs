using System.Collections.Generic;
using System.Threading.Tasks;
using TucanTesting.Controllers.TestSuites;
using TucanTesting.Models;

namespace TucanTesting.Data
{
    public interface ITestRunRepository
    {
        Task<TestRun> Get(long testRunId, bool includeRelated = true);
        Task<List<TestRun>> GetAll();
        void Add(TestRun testRun);
        void Update(TestRun testRun);
        void Remove(TestRun testRun);
    }
}