using Microsoft.AspNetCore.Mvc;
using SquareApi.Core;
using SquareApi.Core.Dto;
using SquareApi.Core.Model;

namespace SquareApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PointController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// POST method to Add points to DB
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        [HttpPost(Name = "Add")]
        //[Authorize]
        public async Task Add(IEnumerable<PointDto> points)
        {
            foreach (var point in points)
            {
                await _unitOfWork.PointService.Add(new Point()
                {
                    X = point.X,
                    Y = point.Y
                });
            }

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Delete method to remove points from DB
        /// </summary>
        /// <param name="id">ID of point record</param>
        /// <returns></returns>
        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.PointService.Delete(id);

            if (!result)
                return NotFound();
            else
                return Ok("Point Deleted");
        }

        /// <summary>
        /// Get Methods to retrieve all the points from DB
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Get")]
        //[Authorize]
        public async Task<List<Point>> GetAll()
        {
            var result = await _unitOfWork.PointService.GetAll();

            return result.ToList();

        }
    }
}
