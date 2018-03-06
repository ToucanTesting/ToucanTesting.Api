using System.Threading.Tasks;

namespace TucanTesting.Data
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}