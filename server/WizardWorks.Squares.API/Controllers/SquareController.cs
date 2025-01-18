using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WizardWorks.Squares.API.Services;
using static WizardWorks.Squares.API.Models.SquareModel;

namespace WizardWorks.Squares.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SquaresController : ControllerBase
    {
        private readonly ISquareService _squareService;
        private readonly ILogger<SquaresController> _logger;

        public SquaresController(ISquareService squareService, ILogger<SquaresController> logger)
        {
            _squareService = squareService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Square>>> GetSquares()
        {
            try
            {
                var squares = await _squareService.GetSquaresAsync();
                return Ok(squares);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving squares");
                return StatusCode(500, "An error occurred while retrieving squares");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Square>> AddSquare()
        {
            try
            {
                var square = await _squareService.AddSquareAsync();
                return CreatedAtAction(nameof(GetSquares), new { id = square.Id }, square);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new square");
                return StatusCode(500, "An error occurred while adding a new square");
            }
        }
    }
}