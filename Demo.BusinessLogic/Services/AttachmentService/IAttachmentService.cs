using Microsoft.AspNetCore.Http;

namespace Demo.BusinessLogic.Services.AttachmentService
{
	public interface IAttachmentService
	{
		string? Upload(IFormFile file, string folderName);
		bool Delete(string filePath);
	}
}
