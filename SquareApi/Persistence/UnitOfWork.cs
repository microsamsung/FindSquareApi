using Microsoft.EntityFrameworkCore;
using SquareApi.Core;
using SquareApi.Core.Service;
using SquareApi.Persistence.Repositories;
using SquareApi.Persitence;

namespace SquareApi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private SquareApiContext _context;

        public IPointService PointService { get; private set; }

        public UnitOfWork(SquareApiContext context)
        {
            _context = context;

            PointService = new PointRepository(_context);
        }


        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
