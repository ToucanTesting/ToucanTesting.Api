using System.Collections.Generic;
using System.Threading.Tasks;
using TucanTesting.Controllers.TestSuites;
using TucanTesting.Models;

namespace TucanTesting.Data
{
    public interface ITestSuiteRepository
    {
        Task<TestSuite> GetSuite(long id);
        Task<List<TestSuite>> GetSuites();
        void Add(TestSuite testSuite);
        void Update(TestSuite testSuite);
        void Remove(TestSuite testSuite);
    }
}