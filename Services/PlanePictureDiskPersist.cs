using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Rusada.Common.Exceptions;
using Rusada.Domain.Interfaces;

namespace Rusada.Services
{
	public class PlanePictureDiskPersist : IPlanePicturePersist
    {
        private readonly ILogger<PlanePictureDiskPersist> logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PlanePictureDiskPersist(
            ILogger<PlanePictureDiskPersist> logger,
            IWebHostEnvironment webHostEnvironment)
		{
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task SavePictureAsync(string pictureContent, string name)
        {
            try
            {
                var folderPath = Path.Combine(
                    webHostEnvironment.WebRootPath,
                    "images",
                    "plane-pictures");

                Directory.CreateDirectory(folderPath);

                var imagePath = Path.Combine(folderPath, name);

                //remove data:image/png;base64, from string content
                var imgContent = pictureContent.Substring(pictureContent.LastIndexOf(',') + 1);
                //base64 image to bytes
                var imageDataBytes = Convert.FromBase64String(imgContent);

                //write to the file
                await File.WriteAllBytesAsync(imagePath, imageDataBytes);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured when saving plane picture", ex);
                throw new ServerErrorException("Error occured when saving plane picture", ex);
            }
        }
    }
}

