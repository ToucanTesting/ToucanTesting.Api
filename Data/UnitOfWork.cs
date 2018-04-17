using System.Threading.Tasks;

namespace ToucanTesting.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToucanDbContext _context;

        public UnitOfWork(ToucanDbContext context)
        {
            this._context = context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}