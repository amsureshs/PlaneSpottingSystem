using System;
namespace Rusada.Web.Models.APIViewModels
{
	public class PlanePictureVM
	{
        public int Id { get; set; }

        public string FileKey { get; set; }

        public string FileName { get; set; }
    }

    public class PlanePictureCreateVM
    {
        public string FileContent { get; set; }
    }

    public class PlanePictureEditVM
    {
        public string FileKey { get; set; }

        public string FileContent { get; set; }
    }
}

