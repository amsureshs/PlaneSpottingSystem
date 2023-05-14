using System;
namespace Rusada.Domain.Interfaces
{
	public interface IPlanePicturePersist
	{
		/// <summary>
		/// Save plane picture represent in base64 string to a file with given file name
		/// </summary>
		/// <param name="pictureContent"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		Task SavePictureAsync(string pictureContent, string name);
	}
}

