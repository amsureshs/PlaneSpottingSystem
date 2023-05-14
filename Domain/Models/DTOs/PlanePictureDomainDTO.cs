using System;
namespace Rusada.Domain.Models.DTOs
{
	public class PlanePictureDomainDTO
	{
        public int Id { get; set; }

        public string FileKey { get; set; }

        public string FileName { get; set; }
    }

    public class PlanePictureCreateDomainDTO
    {
        public string FileContent { get; set; }
    }

    public class PlanePictureEditDomainDTO
    {
        public string FileKey { get; set; }

        public string FileContent { get; set; }
    }
}

