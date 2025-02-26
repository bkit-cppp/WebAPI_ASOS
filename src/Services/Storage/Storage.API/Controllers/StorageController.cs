using BuildingBlock.Core.Result;
using BuildingBlock.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.API.Dtos;
using Storage.API.Interfaces;

namespace Storage.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class StorageController : BaseController
	{
		private readonly IStorageService _storageService;
		public StorageController(IStorageService storageService)
		{
			_storageService = storageService;
		}

		[HttpPost("upload")]
		public async Task<IActionResult> Upload(List<IFormFile> files)
		{
			var data = await _storageService.Upload(files);
			var response = Result<List<FileDto>>.Success(data);
			return Ok(response);
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> Delete([FromBody] List<string> ids)
		{
			var data = await _storageService.Delete(ids);
			var response = Result<bool>.Success(data);
			return Ok(response);
		}
	}
}
