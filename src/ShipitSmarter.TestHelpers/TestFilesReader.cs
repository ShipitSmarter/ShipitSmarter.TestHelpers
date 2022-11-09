using System.Text;

namespace ShipitSmarter.TestHelpers;

/// <summary>
/// Class that reads testing files
/// </summary>
public static class TestFilesReader
{
    /// <summary>
    /// Reads a file in the "TestFiles" directory
    /// </summary>
    /// <param name="testFileName">name of the test file</param>
    /// <returns>returns the full content of the file as a string</returns>
    public static string FileContent(string testFileName)
    {
        return FileContent(testFileName, "TestFiles");
    }
    
    /// <summary>
    /// Reads a file in the specified directory
    /// </summary>
    /// <param name="testFileName">Name of the test file</param>
    /// <param name="directory">Directory location where the files are located</param>
    /// <returns>returns the full content of the file as a string</returns>
    public static string FileContent(string testFileName, string directory)
    {
        var binFolder = AppDomain.CurrentDomain.BaseDirectory;
        return File.ReadAllText(Path.Combine(binFolder, directory, testFileName));
    }

    /// <summary>
    /// Reads a file in the specified directory
    /// </summary>
    /// <param name="testFileName">Name of the test file</param>
    /// <returns>File content as a stream reader</returns>
    public static StreamReader CreateReader(string testFileName)
    {
        var content = FileContent(testFileName);
        return new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(content)));
    }
}