using System;

namespace Rusada.Data.Models.DTOs
{
	public class PlanePictureDataDTO
	{
        public int Id { get; set; }

        public string FileKey { get; set; }

        public string FileName { get; set; }

        public int PlaneSightingId { get; set; }

        public PlaneSightingDataDTO PlaneSightingDataDTO { get; set; }
    }

    public class PlanePictureCreateDataDTO
    {
        public string FileKey { get; set; }

        public string FileName { get; set; }

        public bool IsNewFile
        {
            get
            {
                return !string.IsNullOrEmpty(FileKey) && !string.IsNullOrEmpty(FileName);
            }
        }
    }
}

