namespace Shared.Common.IO;

public class AsyncStream
{
    public static readonly AsyncStream Empty = new(AsyncEnumerable.Empty<byte[]>());

    private readonly IAsyncEnumerable<byte[]> content;

    public AsyncStream(IAsyncEnumerable<byte[]> content)
    {
        this.content = content;
    }

    public IAsyncEnumerable<byte[]> Content => content;
}