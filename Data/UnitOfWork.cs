using System.Threading.Tasks;

namespace TucanTesting.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TucanDbContext _context;

        public UnitOfWork(TucanDbContext context)
        {
            this._context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}