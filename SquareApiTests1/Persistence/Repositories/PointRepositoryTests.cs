using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SquareApi.Core.Model;
using SquareApi.Persistence.Repositories;
using SquareApi.Persitence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquareApi.Persistence.Tests
{
    [TestClass]
    public class PointRepositoryTests
    {
        private PointRepository _repository;
        private DbContextOptions<SquareApiContext> _contextOptions;

        [TestInitialize]
        public void Setup()
        {
            _contextOptions = new DbContextOptionsBuilder<SquareApiContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new SquareApiContext(_contextOptions))
            {
                context.Database.EnsureCreated();
            }

            _repository = new PointRepository(new SquareApiContext(_contextOptions));
        }

        [TestMethod]
        public async Task AddAsync_ShouldAddPoint()
        {
            // Arrange
            var point = new Point { X = 1, Y = 1 };

            // Act
            await _repository.AddAsync(point);

            // Assert
            using (var context = new SquareApiContext(_contextOptions))
            {
                var addedPoint = await context.Point.SingleOrDefaultAsync(p => p.X == 1 && p.Y == 1);
                Assert.IsNotNull(addedPoint);
                Assert.AreEqual(1, addedPoint.X);
                Assert.AreEqual(1, addedPoint.Y);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldRemovePoint()
        {
            // Arrange
            var point = new Point { X = 1, Y = 1 };
            using (var context = new SquareApiContext(_contextOptions))
            {
                context.Point.Add(point);
                await context.SaveChangesAsync();
            }

            // Act
            var isDeleted = await _repository.DeleteAsync(point.Id);

            // Assert
            Assert.IsTrue(isDeleted);
            using (var context = new SquareApiContext(_contextOptions))
            {
                var deletedPoint = await context.Point.SingleOrDefaultAsync(p => p.X == 1 && p.Y == 1);
                Assert.IsNull(deletedPoint);
            }
        }

        [TestMethod]
        public async Task DeleteAllAsync_ShouldRemoveAllPoints()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { X = 1, Y = 1 },
                new Point { X = 2, Y = 2 }
            };
            using (var context = new SquareApiContext(_contextOptions))
            {
                context.Point.AddRange(points);
                await context.SaveChangesAsync();
            }

            // Act
            var isDeleted = await _repository.DeleteAllAsync();

            // Assert
            Assert.IsTrue(isDeleted);
            using (var context = new SquareApiContext(_contextOptions))
            {
                var remainingPoints = await context.Point.ToListAsync();
                Assert.AreEqual(0, remainingPoints.Count);
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllPoints()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { X = 1, Y = 1 },
                new Point { X = 2, Y = 2 }
            };
            using (var context = new SquareApiContext(_contextOptions))
            {
                context.Point.AddRange(points);
                await context.SaveChangesAsync();
            }

            // Act
            var allPoints = await _repository.GetAllAsync();

            // Assert
            Assert.AreEqual(2, allPoints.Count());
            Assert.IsTrue(allPoints.Any(p => p.X == 1 && p.Y == 1));
            Assert.IsTrue(allPoints.Any(p => p.X == 2 && p.Y == 2));
        }
    }
}
