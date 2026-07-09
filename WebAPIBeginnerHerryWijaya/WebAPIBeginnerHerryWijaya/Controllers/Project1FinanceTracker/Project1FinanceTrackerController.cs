using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPIBeginnerHerryWijaya.Data;
using WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker;
using WebAPIBeginnerHerryWijaya.Repositories;
using WebAPIBeginnerHerryWijaya.Services;

namespace WebAPIBeginnerHerryWijaya.Controllers.Project1FinanceTracker
{
    [Route("api/[controller]")]
    [ApiController]
    public class Project1FinanceTrackerController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFinanceTrackerService _financeTrackerService;
        private readonly IFinanceTrackerRepository _financeTrackerRepository;

        public Project1FinanceTrackerController(ApplicationDbContext dbContext ,IFinanceTrackerService financeTrackerService,IFinanceTrackerRepository financeTrackerRepository )
        {
            _dbContext = dbContext;
            _financeTrackerService = financeTrackerService;
            _financeTrackerRepository = financeTrackerRepository;
        }
        [HttpGet("dashboard/monthly")]
        public async Task<ActionResult<MonthlyDashboardDto>> GetMonthlyDashboard(
    int year,
    int month)
        {
            var dashboard = await _financeTrackerService.GetMonthlyDashboardAsync(year, month);
            return Ok(dashboard);
        }
        [HttpGet("dashboard/yearly")]
        public async Task<ActionResult<YearlyDashboardDto>> GetYearlyDashboard(
    int year)
        {
            var dashboard = await _financeTrackerService.GetYearlyDashboardAsync(year   );
            return Ok(dashboard);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinance(int id)
        {
            var deleted=  await _financeTrackerRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFinances(int page = 1,
    int pageSize = 20)
        {
            var finances = await _financeTrackerRepository.GetAllAsync(page, pageSize);
            return Ok(finances);
        }
        [HttpPost("generateRandom")]
        public async Task<IActionResult> GenerateRandomFinances([FromQuery]int count)
        {
            if (count <= 0)
                return BadRequest("Count must be greater than 0.");

            if (count > 1000)
                return BadRequest("Count cannot exceed 1000.");
            var generatedFinances =await  _financeTrackerService.SeedFinance(count);

            return Ok(generatedFinances);
        }
    }
}
