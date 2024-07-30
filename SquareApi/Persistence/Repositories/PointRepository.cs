using Microsoft.EntityFrameworkCore;
using SquareApi.Core.Model;
using SquareApi.Core.Service;
using SquareApi.Persitence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquareApi.Persistence.Repositories
{
    /// <summary>
    /// Repository for managing point entities in the database.
    /// </summary>
    public class PointRepository : IPointService
    {
        private readonly SquareApiContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public PointRepository(SquareApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new point to the database.
        /// </summary>
        /// <param name="point">The point to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while adding the point.</exception>
        public async Task AddAsync(Point point)
        {
            try
            {
                await _context.Point.AddAsync(point);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception (implement a logging mechanism)
                // Example: _logger.LogError(ex, "An error occurred while adding a point.");
                throw new Exception("An error occurred while adding the point.", ex);
            }
        }

        /// <summary>
        /// Deletes a point from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the point to delete.</param>
        /// <returns>A task representing the asynchronous operation. Returns true if the point was deleted; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while deleting the point.</exception>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var point = await _context.Point.SingleOrDefaultAsync(p => p.Id == id);
                if (point == null)
                    return false;

                _context.Point.Remove(point);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log exception (implement a logging mechanism)
                // Example: _logger.LogError(ex, "An error occurred while deleting the point.");
                throw new Exception("An error occurred while deleting the point.", ex);
            }
        }

        /// <summary>
        /// Deletes all points from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. Returns true if all points were deleted; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while deleting all points.</exception>
        public async Task<bool> DeleteAllAsync()
        {
            try
            {
                var points = await _context.Point.ToListAsync();
                if (points == null || !points.Any())
                    return false;

                _context.Point.RemoveRange(points);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log exception (implement a logging mechanism)
                // Example: _logger.LogError(ex, "An error occurred while deleting all points.");
                throw new Exception("An error occurred while deleting all points.", ex);
            }
        }

        /// <summary>
        /// Retrieves all points from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing a collection of points.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving points.</exception>
        public async Task<IEnumerable<Point>> GetAllAsync()
        {
            try
            {
                return await _context.Point.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (implement a logging mechanism)
                // Example: _logger.LogError(ex, "An error occurred while retrieving points.");
                throw new Exception("An error occurred while retrieving points.", ex);
            }
        }
    }
}
