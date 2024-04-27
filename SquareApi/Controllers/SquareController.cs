using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using SquareApi.Core;
using SquareApi.Core.Business;
using SquareApi.Core.Dto;
using SquareApi.Core.Model;
using SquareApi.Persitence;

namespace SquareApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SquareController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SquareController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        //[Authorize]
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
