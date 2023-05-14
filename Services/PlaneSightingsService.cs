using System;
using Mapster;
using Microsoft.Extensions.Logging;
using Rusada.Data.Models.DTOs;
using Rusada.Data.Repositories;
using Rusada.Domain.Interfaces;
using Rusada.Domain.Models.DTOs;

namespace Rusada.Services
{
	public class PlaneSightingsService : IPlaneSightingsService
    {
        private readonly ILogger<PlaneSightingsService> logger;
        private readonly IPlaneSightingsRepository planeSightingsRepository;
        private readonly IPlanePicturePersist planePicturePersist;

        public PlaneSightingsService(
            ILogger<PlaneSightingsService> logger,
            IPlaneSightingsRepository planeSightingsRepository,
            IPlanePicturePersist planePicturePersist)
		{
            this.logger = logger;
            this.planeSightingsRepository = planeSightingsRepository;
            this.planePicturePersist = planePicturePersist;
        }

        public async Task CreateAsync(
            PlaneSightingCreateDomainDTO planeSightingCreateDomainDTO,
            string userId)
        {
            try
            {
                var planeSighting = planeSightingCreateDomainDTO
                    .Adapt<PlaneSightingCreateDataDTO>();

                if (planeSightingCreateDomainDTO.Picture != null
                    && !string.IsNullOrWhiteSpace(planeSightingCreateDomainDTO.Picture.FileContent))
                {
                    //a picture file exist
                    var fileKey = Guid.NewGuid().ToString();
                    var fileName = $"{fileKey}.jpg";
                    await planePicturePersist
                        .SavePictureAsync(planeSightingCreateDomainDTO.Picture.FileContent, fileName);
                    planeSighting.PlanePicture = new PlanePictureCreateDataDTO
                    {
                        FileKey = fileKey,
                        FileName = fileName
                    };
                }

                await planeSightingsRepository.CreateAsync(
                    planeSighting,
                    userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int planeSightingId, string userId)
        {
            try
            {
                await planeSightingsRepository.DeleteAsync(planeSightingId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EditAsync(
            PlaneSightingEditDomainDTO planeSightingEditDomainDTO,
            string userId)
        {
            try
            {
                var planeSighting = planeSightingEditDomainDTO
                    .Adapt<PlaneSightingEditDataDTO>();

                if (planeSightingEditDomainDTO.Picture != null
                    && !string.IsNullOrWhiteSpace(planeSightingEditDomainDTO.Picture.FileContent)
                    && string.IsNullOrWhiteSpace(planeSightingEditDomainDTO.Picture.FileKey))
                {
                    //if there is file conetent and no fileKey, then it is a new file
                    var fileKey = Guid.NewGuid().ToString();
                    var fileName = $"{fileKey}.jpg";
                    await planePicturePersist
                        .SavePictureAsync(planeSightingEditDomainDTO.Picture.FileContent, fileName);
                    planeSighting.PlanePicture = new PlanePictureCreateDataDTO
                    {
                        FileKey = fileKey,
                        FileName = fileName
                    };
                }
                else if (planeSightingEditDomainDTO.Picture != null
                    && string.IsNullOrWhiteSpace(planeSightingEditDomainDTO.Picture.FileKey))
                {
                    //if there is a fileKey, then it is considered as an exist file
                    planeSighting.PlanePicture = new PlanePictureCreateDataDTO
                    {
                        FileKey = planeSightingEditDomainDTO.Picture.FileKey
                    };
                }

                await planeSightingsRepository.EditAsync(
                    planeSighting,
                    userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PlaneSightingDomainDTO>> GetListAsync(
            string searchText,
            int pageNumber,
            int pageSize,
            string userId)
        {
            try
            {
                var planeSightings = await planeSightingsRepository
                    .GetListAsync(searchText, pageNumber, pageSize, userId);

                return planeSightings.Adapt<List<PlaneSightingDomainDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PlaneSightingDomainDTO> GetDetailsAsync(
            int planeSightingId,
            string userId)
        {
            try
            {
                var planeSightings = await planeSightingsRepository
                    .GetDetailsAsync(planeSightingId, userId);

                return planeSightings.Adapt<PlaneSightingDomainDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

