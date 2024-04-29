using Microsoft.AspNetCore.Mvc;
using SquareApi.Core;
using SquareApi.Core.Business;

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
        /// HttpGet methods to Retrive all the points from DB and return the possible squares can be drwan
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            //Get Points
            var points = await _unitOfWork.PointService.GetAll();

            //Ask Manager to build squres
            var manager = new SquareManager();
            var result = manager.IdentifySquares(points.ToList());

            //return count
            return Ok(result.ToList().Count);
        }

    }
}
