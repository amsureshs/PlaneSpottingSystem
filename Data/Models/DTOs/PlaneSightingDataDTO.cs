using System;
namespace Rusada.Data.Models.DTOs
{
    public class PlaneSightingDataDTO
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; }

        public string UserId { get; set; }

        public ICollection<PlanePictureDataDTO> PlanePictures { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }

    public class PlaneSightingCreateDataDTO
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; }

        public PlanePictureCreateDataDTO PlanePicture { get; set; }
    }

    public class PlaneSightingEditDataDTO
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; }

        public PlanePictureCreateDataDTO PlanePicture { get; set; }
    }
}

