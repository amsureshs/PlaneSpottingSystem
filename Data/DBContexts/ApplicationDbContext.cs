using System;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Rusada.Data.Models.Entities;

namespace Rusada.Data.DBContexts
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {
        public DbSet<PlaneSighting> PlaneSightings { get; set; }
        public DbSet<PlanePicture> PlanePictures { get; set; }

        private readonly string dbPath;
        private readonly bool isSQLIteDbInUse;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            IConfiguration configuration)
            : base(options, operationalStoreOptions)
        {
            this.isSQLIteDbInUse = configuration.GetValue<bool>("UseSQLiteDatabase");

            if (isSQLIteDbInUse)
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                dbPath = System.IO.Path.Join(path, "RusadaDb_5.db");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (isSQLIteDbInUse)
            {
                options.UseSqlite($"Data Source={dbPath}");
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(true);
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            AuditEntities();
            return await base.SaveChangesAsync(
                acceptAllChangesOnSuccess,
                cancellationToken);
        }

        public override int SaveChanges()
        {
            AuditEntities();
            return base.SaveChanges();
        }

        private void AuditEntities()
        {
            var modifiedEntities = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added
                || e.State == EntityState.Modified);

            foreach (var entity in modifiedEntities)
            {
                entity.Property("ModifiedDateTime").CurrentValue = DateTime.UtcNow;
                if (entity.State == EntityState.Added)
                {
                    entity.Property("CreatedDateTime").CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}

