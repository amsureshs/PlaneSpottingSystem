using System;
using System.ComponentModel.DataAnnotations;

namespace Rusada.Data.Models.Entities
{
	public class PlanePicture : BaseEntity
	{
        [MaxLength(256), Required]
        public string FileKey { get; set; }

        [MaxLength(256), Required]
        public string FileName { get; set; }

        public int PlaneSightingId { get; set; }

        public PlaneSighting PlaneSighting { get; set; }
    }
}

