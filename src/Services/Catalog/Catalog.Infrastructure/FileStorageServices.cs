using Microsoft.AspNetCore.Hosting;
namespace Catalog.Infrastructure;

public class FileStorageServices : IFileStorageServices
{
	private readonly string _userContentFolder;
	public FileStorageServices(IWebHostEnvironment webHostEnvironment)
	{
		_userContentFolder = webHostEnvironment.WebRootPath;
	}

	public async Task DeleteFileAsync(string fileName)
	{
		var filePath = Path.Combine(_userContentFolder, fileName);
		if (File.Exists(filePath))
		{
			await Task.Run(() => File.Delete(filePath));
		}
	}

	public List<string> GetFiles(string path, string searchPattern = "*.*")
	{
		var originalFolderPath = Path.Combine(_userContentFolder, path);
		return Directory.GetFiles(originalFolderPath, searchPattern, SearchOption.AllDirectories).ToList();
	}

	public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
	{
		var filePath = Path.Combine(_userContentFolder, fileName);
		using var output = new FileStream(filePath, FileMode.Create);
		await mediaBinaryStream.CopyToAsync(output);
	}
}
