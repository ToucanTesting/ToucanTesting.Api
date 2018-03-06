using System.Collections.Generic;
using System.Threading.Tasks;
using TucanTesting.Models;

namespace TucanTesting.Data
{
    public interface ITestConditionRepository
    {
        Task<TestCondition> Get (long id);
        Task<List<TestCondition>> GetAll();
        void Add (TestCondition testCondition);

        void Update (TestCondition testCondition);
        void Remove (TestCondition testCondition);
    }
}