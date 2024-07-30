using Microsoft.AspNetCore.Mvc;
using SquareApi.Core.Dto;
using SquareApi.Core.Model;
using SquareApi.Persistence.UOW;
using Swashbuckle.Swagger.Annotations;




namespace SquareApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PointController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds points to the database.
        /// </summary>
        /// <param name="points">List of points to add.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="201">Points were successfully added.</response>
        /// <response code="400">Invalid input received.</response>
        [HttpPost("AddPoints")]
        [SwaggerOperation("Adds points to the database operation.")]
        [SwaggerResponse(200, "Points were successfully added")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task Add(IEnumerable<PointDto> points)
        {
            foreach (var point in points)
            {
                await _unitOfWork.PointService.AddAsync(new Point()
                {
                    X = point.X,
                    Y = point.Y
                });
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a point from the database.
        /// </summary>
        /// <param name="id">ID of the point to delete.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Point was successfully deleted.</response>
        /// <response code="404">Point with the specified ID was not found.</response>
        [HttpDelete("DeletePoint")]
        [SwaggerResponse(200, "Deletes point from the database")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(500, "Internal server error")]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.PointService.DeleteAsync(id);

            if (!result)
                return NotFound();
            else
                return Ok("Point Deleted");
        }

        /// <summary>
        /// Retrieves all points from the database.
        /// </summary>
        /// <returns>List of points.</returns>
        /// <response code="200">Successfully retrieved points.</response>
        /// <response code="400">An error occurred while retrieving points.</response>
        [HttpGet("GetAllPoints")]
        [SwaggerResponse(200, "Successfully retrieved points")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(500, "Internal server error")]
        //[Authorize]
        public async Task<List<Point>> GetAll()
        {
            var result = await _unitOfWork.PointService.GetAllAsync();

            return result.ToList();

        }


        /// <summary>
        /// Deletes All points from the database.
        /// </summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Point was successfully deleted.</response>
        /// <response code="404">Point with the specified ID was not found.</response>
        [HttpDelete("DeleteAllPoint")]
        [SwaggerResponse(200, "Deletes All points from the database")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(500, "Internal server error")]
        //[Authorize]
        public async Task<IActionResult> DeleteAll()
        {
            var result = await _unitOfWork.PointService.DeleteAllAsync();

            if (!result)
                return NotFound();
            else
                return Ok("All Points Deleted");
        }
    }
}
