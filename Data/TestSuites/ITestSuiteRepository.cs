using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Controllers.TestSuites;
using ToucanTesting.Models;

namespace ToucanTesting.Data
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