using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TucanTesting.Models;

namespace TucanTesting.Data
{
    public interface ITestResultRepository
    {
        Task<TestResult> Get (long id);
        Task<List<TestResult>> GetAll(long testRunId);
        void Add (TestResult testResult);
        void Update(TestResult testResult);
        void UpdateTestRun(long testRunId);
    }
}