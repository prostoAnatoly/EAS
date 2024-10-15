using System.Net;

namespace Shared.Rest.Common;

public interface IHttpStatusCodeDefiner
{

    HttpStatusCode GetStatusCodeByException(Exception exception);
}
