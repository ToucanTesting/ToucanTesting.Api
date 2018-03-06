using System.Collections.Generic;
using System.Threading.Tasks;
using TucanTesting.Models;

namespace TucanTesting.Data
{
    public interface ITestActionRepository
    {
        Task<TestAction> Get (long id);
        Task<List<TestAction>> GetAll();
        void Add (TestAction testAction);

        void Update (TestAction testAction);
        void Remove (TestAction testAction);
    }
}