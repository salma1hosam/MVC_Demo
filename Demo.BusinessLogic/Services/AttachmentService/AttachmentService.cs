using Microsoft.AspNetCore.Http;


namespace Demo.BusinessLogic.Services.AttachmentService
{
	public class AttachmentService : IAttachmentService
	{
		List<string> allowedExtensions = [".png", ".jpg", ".jpeg"];

		const int maxFileSize = 2_097_152;  //1024 * 1024 * 2 = 2MB
		public string? Upload(IFormFile file, string folderName)
		{
			//1.Check Extension
			var extension = Path.GetExtension(file.FileName);  //GetExtension() here will split the name of the file (or the specified passed parameter [path]) till the period and return the extenssion
			if (!allowedExtensions.Contains(extension)) return null;

			//2.Check Size
			if (file.Length == 0 || file.Length > maxFileSize)
				return null;

			//3.Get Located Folder Path
			//D:\Route\Back-end Asp.net\07 MVC\MVC Demo Project\DemoMvcSolution\Demo.Presentation\wwwroot\Files\Images
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

			//4.Make Attachment Name Unique-- GUID
			var fileName = $"{Guid.NewGuid()}_{file.FileName}";

			//5.Get File Path
			var filePath = Path.Combine(folderPath, fileName);

			//6.Create File Stream To Copy File[Unmanaged]
			using FileStream fileStream = new FileStream(filePath, FileMode.Create);

			//7.Use Stream To Copy File
			file.CopyTo(fileStream);
			//File.Create(filePath);

			//8.Return FileName To Store In Database
			return fileName;
		}

		public bool Delete(string filePath)
		{
			if (!File.Exists(filePath)) return false;
			else
			{
				File.Delete(filePath);
				return true;
			}
		}

	}
}
