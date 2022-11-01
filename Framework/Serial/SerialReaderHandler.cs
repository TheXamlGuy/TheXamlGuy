using System.Buffers;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Serial;

public abstract class SerialReaderHandler<TRead> : IMediatorAsyncHandler<TRead, ReadOnlySequence<byte>> where TRead : class
{
    public abstract Task<TRead> Handle(ReadOnlySequence<byte> request, CancellationToken cancellationToken = default);
}
