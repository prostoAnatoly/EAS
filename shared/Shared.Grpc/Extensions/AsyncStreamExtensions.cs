using Shared.Common.IO;
using Shared.Grpc.Models;

namespace Shared.Grpc.Extensions
{
    public static class AsyncStreamExtensions
    {

        public static async IAsyncEnumerable<StreamChunk<T>> ToGrpcChankedStream<T>(this AsyncStream stream, T meta)
            where T : class
        {
            var i = -1;

            await foreach (var contentChunk in stream.Content)
            {
                i++;
                yield return new StreamChunk<T>()
                {
                    Chunk = contentChunk,
                    Meta = i == 0 ? meta : null,
                };
            }
        }

        public static async Task<AsyncFile<T>?> ReadGrpcChunkedStream<T>(this IAsyncEnumerable<StreamChunk<T>> chunkedStream,
            CancellationToken cancellationToken = default)
            where T : class
        {
            var enumerator = chunkedStream.GetAsyncEnumerator(cancellationToken);
            var isMoveNext = await enumerator.MoveNextAsync();
            if (!isMoveNext) { return null; }

            var chunkFirst = enumerator.Current;
            var asyncStream = GetAsyncStream(chunkFirst, enumerator);

            return new AsyncFile<T>(chunkFirst.Meta!, asyncStream);
        }

        private static AsyncStream GetAsyncStream<T>(StreamChunk<T> chunkFirst, IAsyncEnumerator<StreamChunk<T>> enumerator)
            where T : class
        {
            return new AsyncStream(GetContent(chunkFirst, enumerator));

            static async IAsyncEnumerable<byte[]> GetContent<TChunk>(StreamChunk<TChunk> chunkFirst, IAsyncEnumerator<StreamChunk<TChunk>> enumerator)
                where TChunk : class
            {
                yield return chunkFirst.Chunk;
                while (await enumerator.MoveNextAsync())
                {
                    yield return enumerator.Current.Chunk;
                }
            }
        }
    }
}