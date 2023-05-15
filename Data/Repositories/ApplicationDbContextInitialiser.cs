using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rusada.Data.DBContexts;

namespace Rusada.Data.Repositories
{
	public class ApplicationDbContextInitialiser
	{
        private readonly ILogger<ApplicationDbContextInitialiser> logger;
        private readonly ApplicationDbContext dbContext;

        public ApplicationDbContextInitialiser(
			ILogger<ApplicationDbContextInitialiser> logger,
            ApplicationDbContext dbContext)
		{
			this.logger = logger;
			this.dbContext = dbContext;

        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (dbContext.Database.IsSqlServer())
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }
    }
}

