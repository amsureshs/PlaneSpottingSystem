using System;
using Rusada.Domain.Models.DTOs;

namespace Rusada.Domain.Interfaces
{
	public interface IPlaneSightingsService
	{
        /// <summary>
        /// Creates a Plane Sighting
        /// </summary>
        /// <param name="planeSightingCreateDomainDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateAsync(
            PlaneSightingCreateDomainDTO planeSightingCreateDomainDTO,
            string userId);

        /// <summary>
        /// Edit/Update the specific plane sight
        /// </summary>
        /// <param name="planeSightingEditDomainDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task EditAsync(
            PlaneSightingEditDomainDTO planeSightingEditDomainDTO,
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
        Task<PlaneSightingDomainDTO> GetDetailsAsync(
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
        Task<List<PlaneSightingDomainDTO>> GetListAsync(
            string searchText,
            int pageNumber,
            int pageSize,
            string userId);
    }
}

