using Microsoft.AspNetCore.Http;

namespace BuildingBlock.Logging;

public interface ILoggerService
{
	Task WriteErrorLogAsync(Exception exception, HttpRequest request = null, string functionName = "", object requestObject = null);
	void WriteErrorLog(Exception exception, HttpRequest request = null, string functionName = "");
	void WriteInfoLog(string logMessage, HttpRequest request = null, string functionName = "");
	void WriteWarningLog(string logMessage, HttpRequest request = null, string functionName = "");
}
