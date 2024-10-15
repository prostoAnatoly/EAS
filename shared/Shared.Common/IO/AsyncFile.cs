namespace Shared.Common.IO;

public class AsyncFile<TProps>
    where TProps : class
{

    public TProps Props { get; }

    public AsyncStream Stream { get; }

    public AsyncFile(TProps props, AsyncStream stream)
    {
        Props = props;
        Stream = stream;
    }
}
