using System;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

string path = @"C:\Users\NO ONE\AppData\Local\vvvv\gamma\nugets\VL.Fuse.1.0.3-beta08\lib\net8.0\Fuse.dll";

using var stream = File.OpenRead(path);
using var pe = new PEReader(stream);
var md = pe.GetMetadataReader();

Console.WriteLine("=== Fuse 类型定义 ===");

foreach (var handle in md.TypeDefinitions)
{
    var type = md.GetTypeDefinition(handle);
    string ns = md.GetString(type.Namespace);
    string name = md.GetString(type.Name);

    Console.WriteLine(
        string.IsNullOrEmpty(ns) ? name : ns + "." + name
    );
}
