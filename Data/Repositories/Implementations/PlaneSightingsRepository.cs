using System;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rusada.Data.DBContexts;
using Rusada.Data.Models.DTOs;
using Rusada.Data.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Rusada.Data.Repositories.Implementations
{
	public class PlaneSightingsRepository : IPlaneSightingsRepository
    {
        private readonly ILogger<PlaneSightingsRepository> logger;
        private readonly ApplicationDbContext dbContext;

        public PlaneSightingsRepository(
            ILogger<PlaneSightingsRepository> logger,
            ApplicationDbContext dbContext)
		{
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(
            PlaneSightingCreateDataDTO planeSightingCreateDataDTO,
            string userId)
        {
            try
            {
                var planeSighting = planeSightingCreateDataDTO.Adapt<PlaneSighting>();
                planeSighting.UserId = userId;

                if (planeSightingCreateDataDTO.PlanePicture != null)
                {
                    planeSighting.PlanePictures = new List<PlanePicture>
                    {
                        planeSightingCreateDataDTO.PlanePicture.Adapt<PlanePicture>(),
                    };
                }

                await dbContext.PlaneSightings.AddAsync(planeSighting);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error on creating new plane sighting", ex);
                throw;
            }
        }

        public async Task DeleteAsync(int planeSightingId, string userId)
        {
            try
            {
                //assumed - only created user can delete item
                var planeSighting = await dbContext.PlaneSightings
                    .FirstOrDefaultAsync(ps => ps.Id == planeSightingId && ps.UserId == userId);

                if (planeSighting == null)
                {
                    //need to imprpove by introducing app specific exceptions
                    throw new Exception("Plane sighting not found");
                }

                dbContext.PlaneSightings.Remove(planeSighting);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error on deleting the plane sighting", ex);
                throw;
            }
        }

        public async Task EditAsync(PlaneSightingEditDataDTO planeSightingEditDataDTO, string userId)
        {
            try
            {
                var planeSighting = await dbContext.PlaneSightings
                    .Include(ps => ps.PlanePictures)
                    .FirstOrDefaultAsync(ps => ps.Id == planeSightingEditDataDTO.Id && ps.UserId == userId);

                if (planeSighting == null)
                {
                    throw new Exception("Plane sighting not found");
                }

                planeSighting.Make = planeSightingEditDataDTO.Make;
                planeSighting.Model = planeSightingEditDataDTO.Model;
                planeSighting.Registration = planeSightingEditDataDTO.Registration;
                planeSighting.Location = planeSightingEditDataDTO.Location;
                planeSighting.SeenDateTime = planeSightingEditDataDTO.SeenDateTime;

                if (planeSightingEditDataDTO.PlanePicture != null
                    && planeSightingEditDataDTO.PlanePicture.IsNewFile)
                {
                    //new picture
                    planeSighting.PlanePictures = new List<PlanePicture>
                    {
                        planeSightingEditDataDTO.PlanePicture.Adapt<PlanePicture>(),
                    };
                }
                else if (planeSightingEditDataDTO.PlanePicture == null)
                {
                    //if exist, remove the existing picture
                    planeSighting.PlanePictures = null;
                }

                dbContext.PlaneSightings.Update(planeSighting);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error on updating the plane sighting", ex);
                throw;
            }
        }

        public async Task<List<PlaneSightingDataDTO>> GetListAsync(
            string searchText,
            int pageNumber,
            int pageSize,
            string userId)
        {
            try
            {
                //assumed - user can list his own items only
                var queryable = dbContext.PlaneSightings
                    .Include(ps => ps.PlanePictures)
                    .Where(ps => ps.UserId == userId);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    //queryable = queryable
                    //    .Where(ps => EF.Functions.Contains(ps.SearchField, $"\"{searchText}\""));

                    queryable = queryable
                        .Where(ps => ps.SearchField.Contains(searchText));
                }

                if (pageNumber > 0 && pageSize > 0)
                {
                    queryable = queryable
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);
                }

                var planeSightings = await queryable
                    .ToListAsync();

                return planeSightings.Adapt<List<PlaneSightingDataDTO>>();
            }
            catch (Exception ex)
            {
                logger.LogError("Error on get list of the plane sightings", ex);
                throw;
            }
        }

        public async Task<PlaneSightingDataDTO> GetDetailsAsync(
            int planeSightingId,
            string userId)
        {
            try
            {
                //assumed - user can view his own items only
                var planeSighting = await dbContext.PlaneSightings
                    .FirstOrDefaultAsync(ps => ps.Id == planeSightingId && ps.UserId == userId);

                if (planeSighting == null)
                {
                    throw new Exception("Plane sighting not found");
                }

                return planeSighting.Adapt<PlaneSightingDataDTO>();
            }
            catch (Exception ex)
            {
                logger.LogError("Error on get details of the plane sighting", ex);
                throw;
            }
        }
    }
}

