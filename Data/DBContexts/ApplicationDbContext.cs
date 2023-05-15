using System;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rusada.Data.Models.Entities;

namespace Rusada.Data.DBContexts
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {
        public DbSet<PlaneSighting> PlaneSightings { get; set; }
        public DbSet<PlanePicture> PlanePictures { get; set; }

        public string DbPath { get; }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "RusadaDb_2.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

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

