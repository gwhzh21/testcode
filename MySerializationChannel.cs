using DotNext.Threading.Channels;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppNext
{
    public sealed class MySerializationChannel<T> : PersistentChannel<T, T>
    {
        private readonly IFormatter formatter;
        public MySerializationChannel(PersistentChannelOptions options) : base(options)
        {
            formatter = new BinaryFormatter();
        }

        protected override ValueTask<T> DeserializeAsync(Stream input, CancellationToken token)
        {
            return new ValueTask<T>((T)formatter.Deserialize(input));
        }

        protected override ValueTask SerializeAsync(T input, Stream output, CancellationToken token)
        {
            formatter.Serialize(output, input);
            return new ValueTask();
        }
    }
}
