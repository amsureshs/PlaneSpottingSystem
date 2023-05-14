using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Rusada.Domain.Interfaces;
using Rusada.Domain.Models.DTOs;
using Rusada.Web.Models.APIViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rusada.Web.Controllers
{
    public class PlaneSightingsController : BaseController
    {
        private readonly IPlaneSightingsService planeSightingsService;

        public PlaneSightingsController(IPlaneSightingsService planeSightingsService)
        {
            this.planeSightingsService = planeSightingsService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task CreateAsync([FromBody] PlaneSightingCreateVM planeSightingCreateVM)
        {
            var planeSighting = planeSightingCreateVM.Adapt<PlaneSightingCreateDomainDTO>();
            await planeSightingsService.CreateAsync(planeSighting, UserId);
        }

        [HttpPut]
        public async Task EditAsync([FromBody] PlaneSightingEditVM planeSightingEditVM)
        {
            var planeSighting = planeSightingEditVM.Adapt<PlaneSightingEditDomainDTO>();
            await planeSightingsService.EditAsync(planeSighting, UserId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] int id)
        {
            await planeSightingsService.DeleteAsync(id, UserId);
        }

        [HttpGet("{id}")]
        public async Task<PlaneSightingVM> GetDetailsAsync([FromRoute] int id)
        {
            var planeSighting = await planeSightingsService.GetDetailsAsync(id, UserId);
            return planeSighting.Adapt<PlaneSightingVM>();
        }

        [HttpGet("list")]
        public async Task<List<PlaneSightingVM>> GetListAsync(
            [FromQuery] string searchText,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            //TODO sinitize searchText 
            var planeSightings = await planeSightingsService.GetListAsync(
                searchText,
                pageNumber,
                pageSize,
                UserId);
            return planeSightings.Adapt<List<PlaneSightingVM>>();
        }
    }
}

