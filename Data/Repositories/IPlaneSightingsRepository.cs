using System;
using Rusada.Data.Models.DTOs;

namespace Rusada.Data.Repositories
{
	public interface IPlaneSightingsRepository
	{
        /// <summary>
        /// Creates a Plane Sighting
        /// </summary>
        /// <param name="planeSightingCreateDataDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
		Task CreateAsync(
            PlaneSightingCreateDataDTO planeSightingCreateDataDTO,
            string userId);

        /// <summary>
        /// Edit/Update the specific plane sight
        /// </summary>
        /// <param name="planeSightingEditDataDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task EditAsync(
            PlaneSightingEditDataDTO planeSightingEditDataDTO,
            string userId);

        /// <summary>
        /// Delete the plane sight by the given id
        /// </summary>
        /// <param name="planeSightingId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteAsync(int planeSightingId, string userId);

        /// <summary>
        /// Get details of the plane sight by the given id
        /// </summary>
        /// <param name="planeSightingId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<PlaneSightingDataDTO> GetDetailsAsync(
            int planeSightingId,
            string userId);

        /// <summary>
        /// List plane sights on search (optional) with pagination
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<PlaneSightingDataDTO>> GetListAsync(
            string searchText,
            int pageNumber,
            int pageSize,
            string userId);
    }
}

