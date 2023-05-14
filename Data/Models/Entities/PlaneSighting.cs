using System;
using System.ComponentModel.DataAnnotations;

namespace Rusada.Data.Models.Entities
{
	public class PlaneSighting : BaseEntity
	{
        [MaxLength(128), Required]
        public string Make { get; set; }

        [MaxLength(128), Required]
        public string Model { get; set; }

        [MaxLength(8), Required]
        public string Registration { get; set; }

        [MaxLength(255), Required]
        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; } = DateTime.UtcNow;

        public string SearchField
        {
            get
            {
                return $"{Make} {Model} {Registration}";
            }
        }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<PlanePicture> PlanePictures { get; set; }
    }
}

