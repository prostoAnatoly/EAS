namespace Shared.Common.Extensions;

public static class StreamExtensions
{

    public static async IAsyncEnumerable<TChunk> GetChunks<TChunk, TMeta>(this Stream stream, TMeta meta,
        Func<byte[], TMeta?, TChunk> createChunk)
        where TChunk : class, new()
        where TMeta : class
    {
        const int chunkSize = 4096;

        var buffer = new byte[chunkSize];
        int read;
        var numChunk = 0;

        while ((read = await stream.ReadAsync(buffer)) > 0)
        {
            numChunk++;

            if (read < chunkSize)
            {
                Array.Resize(ref buffer, read);
            }

            yield return createChunk(buffer, numChunk == 1 ? meta : null);
        }
    }
}
