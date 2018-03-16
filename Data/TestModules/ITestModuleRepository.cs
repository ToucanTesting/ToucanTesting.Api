using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TucanTesting.Models;

namespace TucanTesting.Data
{
    public interface ITestModuleRepository
    {
        Task<TestModule> Get (long testModuleId);
        Task<List<TestModule>> GetAll(long testSuiteId, DateTime? beforeDate);
        void Update(TestModule testModule);
        void Add (TestModule testModule);
        void Remove (TestModule testModule);
    }
}