using System;
namespace Rusada.Domain.Models.DTOs
{
    public class PlaneSightingDomainDTO
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; }

        public string UserId { get; set; }

        public ICollection<PlanePictureDomainDTO> PlanePictures { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public PlanePictureDomainDTO Picture
        {
            get
            {
                return PlanePictures?.FirstOrDefault() ?? null;
            }
        }
    }

    public class PlaneSightingCreateDomainDTO
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; }

        public PlanePictureCreateDomainDTO Picture { get; set; }
    }

    public class PlaneSightingEditDomainDTO
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; }

        public PlanePictureEditDomainDTO Picture { get; set; }
    }
}

