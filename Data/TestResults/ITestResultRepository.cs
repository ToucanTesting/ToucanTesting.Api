using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Models;

namespace ToucanTesting.Data
{
    public interface ITestResultRepository
    {
        Task<TestResult> Get (long id);
        Task<List<TestResult>> GetAll(long testRunId);
        Task<List<TestResult>> GetAll(long testRunId, long testModuleId);
        void Add (TestResult testResult);
        void Update(TestResult testResult);
    }
}