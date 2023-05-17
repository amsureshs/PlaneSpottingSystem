using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rusada.Domain.Interfaces;
using Rusada.Domain.Models.DTOs;
using Rusada.Web.Models.APIViewModels;

namespace Rusada.Web.Controllers
{
    [Authorize]
    public class PlaneSightingsController : BaseController
    {
        private readonly IPlaneSightingsService planeSightingsService;
        private readonly IValidator<PlaneSightingCreateVM> planeSightCreateValidator;
        private readonly IValidator<PlaneSightingEditVM> planeSightEditValidator;

        public PlaneSightingsController(
            IPlaneSightingsService planeSightingsService,
            IValidator<PlaneSightingCreateVM> createValidator,
            IValidator<PlaneSightingEditVM> editValidator)
        {
            this.planeSightingsService = planeSightingsService;
            this.planeSightCreateValidator = createValidator;
            this.planeSightEditValidator = editValidator;
        }
        //TODO annotate with http response codes
        [HttpPost]
        public async Task CreateAsync([FromBody] PlaneSightingCreateVM planeSightingCreateVM)
        {
            var result = await planeSightCreateValidator.ValidateAsync(planeSightingCreateVM);
            if (!result.IsValid)
            {
                throw new Rusada.Common.Exceptions.ValidationException(result.Errors);
            }

            var planeSighting = planeSightingCreateVM.Adapt<PlaneSightingCreateDomainDTO>();
            await planeSightingsService.CreateAsync(planeSighting, UserId);
        }

        [HttpPut("{id}")]
        public async Task EditAsync([FromRoute] int id, [FromBody] PlaneSightingEditVM planeSightingEditVM)
        {
            var result = await planeSightEditValidator.ValidateAsync(planeSightingEditVM);
            if (!result.IsValid)
            {
                throw new Rusada.Common.Exceptions.ValidationException(result.Errors);
            }

            var planeSighting = planeSightingEditVM.Adapt<PlaneSightingEditDomainDTO>();
            planeSighting.Id = id;
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

        [HttpGet]
        public async Task<PageListVM<PlaneSightingVM>> GetListAsync(
            [FromQuery] string searchText,
            [FromQuery] int pageNumber = 0,
            [FromQuery] int pageSize = 0)
        {
            //TODO sinitize searchText 
            var planeSightings = await planeSightingsService.GetListAsync(
                searchText,
                pageNumber,
                pageSize,
                UserId);
            return planeSightings.Adapt<PageListVM<PlaneSightingVM>>();
        }
    }
}

