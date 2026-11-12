namespace Fuse.compute;

public interface IBufferInput<T>
{
	BufferType BufferType { get; set; }

	string ID { get; }

	string TypeName();
}
