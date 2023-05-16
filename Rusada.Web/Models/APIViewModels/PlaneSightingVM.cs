using System;
namespace Rusada.Web.Models.APIViewModels
{
	public class PlaneSightingVM
	{
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; }

        public PlanePictureVM Picture { get; set; }
    }

    public class PlaneSightingCreateVM
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; }

        public PlanePictureCreateVM Picture { get; set; }
    }

    public class PlaneSightingEditVM
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Registration { get; set; }

        public string Location { get; set; }

        public DateTime SeenDateTime { get; set; }

        public PlanePictureEditVM Picture { get; set; }
    }
}

