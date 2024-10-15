using Grpc.Core;

namespace Shared.Grpc;

public interface IGrpcStatusCodeDefiner
{

    StatusCode GetStatusCodeByException(Exception exception);
}
