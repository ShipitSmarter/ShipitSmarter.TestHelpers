using System;
using System.IO;
using System.Text;

namespace ShipitSmarter.TestHelpers;

public static class TestFilesReader
{
    public static string FileContent(string testFileName)
    {
        return FileContent(testFileName, "TestFiles");
    }
    
    public static string FileContent(string testFileName, string baseDirectory)
    {
        var binFolder = AppDomain.CurrentDomain.BaseDirectory;
        return File.ReadAllText(Path.Combine(binFolder, baseDirectory, testFileName));
    }

    public static StreamReader CreateReader(string testFileName)
    {
        var content = FileContent(testFileName);
        return new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(content)));
    }
}