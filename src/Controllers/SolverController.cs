using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sudokusolver.Converters;
using sudokusolver.Solver;

namespace sudokusolver.Controllers
{
    [Route("api/solve")]
    [ApiController]
    public class SolverController : ControllerBase
    {

        private readonly ILogger<SolverController> _logger;

        public SolverController(ILogger<SolverController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Consumes("text/plain")]
        public ActionResult<string> Post([FromBody] string body, [FromQuery(Name = "rd")] string rowdelimiter)
        {
            if (String.IsNullOrWhiteSpace(body))
            {
                return this.BadRequest("you must provide a body containing the 9x9 puzzle to be solved");
            }

            Grid grid = null;
            try
            {
                var puzzle = Converter.From(body, rowdelimiter);
                grid = Grid.From(puzzle);
            }
            catch (FormatException fe)
            {
                return this.BadRequest("the supplied puzzle contained non-numeric values");
            }
            catch (ArgumentException ae)
            {
                return this.BadRequest(ae.Message);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return this.Problem("an error occurred while parsing the supplied puzzle", statusCode: 500);
            }

            try
            {
                var solver = new GridSolver(grid);
                var result = solver.Solve();
                if (result)
                {
                    return this.Ok(grid.ToString());
                }
                else
                {
                    return this.UnprocessableEntity("the puzzle was unsolvable");
                }
            }
            catch (ArgumentException ae)
            {
                return this.BadRequest(ae.Message);
            } 
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return this.Problem("an error occurred while solving the supplied puzzle", statusCode: 500);

            }

        }

    }
}
