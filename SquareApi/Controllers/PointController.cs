using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    }
}
