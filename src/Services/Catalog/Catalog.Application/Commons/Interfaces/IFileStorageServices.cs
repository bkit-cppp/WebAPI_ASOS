namespace Catalog.Application.Commons.Interfaces;

public interface IFileStorageServices
{
	List<string> GetFiles(string path, string searchPattern = "*.*");

	Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

	Task DeleteFileAsync(string fileName);
}
