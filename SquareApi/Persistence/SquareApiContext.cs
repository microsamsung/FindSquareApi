using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SquareApi.Core.Model;

namespace SquareApi.Persitence
{
    public class SquareApiContext : DbContext
    {
        public SquareApiContext (DbContextOptions<SquareApiContext> options)
            : base(options)
        {
        }

        public DbSet<SquareApi.Core.Model.Square> Square { get; set; } = default!;
        public DbSet<SquareApi.Core.Model.Point> Point { get; set; } = default!;
    }
}
