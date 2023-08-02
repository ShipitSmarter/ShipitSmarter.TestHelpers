using System.IO.Abstractions;
using Moq;

namespace ShipitSmarter.TestHelpers;

/// <summary>
/// Class to help testing <see cref="IFileSystem"/>
/// </summary>
public class TestFileSystem : IFileSystem
{
#pragma warning disable CS1591
    public readonly Mock<IDirectory> DirectoryMock = new();
    public readonly Mock<IDirectoryInfoFactory> DirectoryInfoMock = new();
    public readonly Mock<IDriveInfoFactory> DriveInfoMock = new();
    public readonly Mock<IFile> FileMock = new();
    public readonly Mock<IFileInfoFactory> FileInfoMock = new();
    public readonly Mock<IFileStreamFactory> FileStreamMock = new();
    public readonly Mock<IFileSystemWatcherFactory> FileSystemWatcherMock = new();
    public readonly Mock<IPath> PathMock = new();


    public IDirectory Directory => DirectoryMock.Object;
    public IDirectoryInfoFactory DirectoryInfo => DirectoryInfoMock.Object;
    public IDriveInfoFactory DriveInfo => DriveInfoMock.Object;
    public IFile File => FileMock.Object;
    public IFileInfoFactory FileInfo => FileInfoMock.Object;
    public IFileStreamFactory FileStream => FileStreamMock.Object;
    public IFileSystemWatcherFactory FileSystemWatcher => FileSystemWatcherMock.Object;
    public IPath Path => PathMock.Object;
#pragma warning restore CS1591
}