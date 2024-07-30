using Microsoft.AspNetCore.Mvc;
using SquareApi.Core.Business;
using SquareApi.Persistence.UOW;
using Swashbuckle.Swagger.Annotations;

namespace SquareApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SquareController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SquareController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all the points from the database and returns the count of possible squares that can be drawn.
        /// </summary>
        /// <returns>The count of possible squares.</returns>
        /// <response code="200">Returns the count of possible squares</response>
        /// <response code="400">If there is an error in retrieving the points</response>
        [HttpGet("GetAllSquares")]
        [SwaggerResponse(200, "Retrived All possible squares from the database")]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> Get()
        {
            //Get Points
            var points = await _unitOfWork.PointService.GetAllAsync();

            //Ask Manager to build squres
            var manager = new SquareManager();
            var result = manager.IdentifySquares(points.ToList());

            //return count
            return Ok(result.ToList().Count);
        }

    }
}
