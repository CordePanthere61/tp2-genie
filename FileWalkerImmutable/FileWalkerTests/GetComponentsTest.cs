
using FileWalkerImmutable;

namespace FileWalkerTests;

[TestClass]
public class GetComponentsTest
{
    private FileSystemFacade _fileSystem;
    private IComponent _rootFolder;
    private IComponent _subFolder;
    private IComponent _textFile;
    

    [TestInitialize]
    public void Initialize()
    {
        _fileSystem = new FileSystemFacade();
        _rootFolder = _fileSystem.CreateFolder("root");
        _subFolder = _fileSystem.CreateFolder("subFolder");
        _textFile = _fileSystem.CreateFile("file.txt", 3, "123");
        
        _fileSystem.AddChildren(_rootFolder, _subFolder);
        _fileSystem.AddChildren(_subFolder, _textFile);
    }
    
    [TestMethod]
    public void TestGetFileFromNestedFolder()
    {
        var fileId = _fileSystem.GetComponentByPath(_rootFolder, "subFolder", "file.txt").ID;
        Assert.AreEqual(_textFile.ID, fileId);
    }

    [TestMethod]
    public void TestGetInvalidFileFromNestedFolder()
    {
        var file = _fileSystem.GetComponentByPath(_rootFolder, "subFolder", "toto.txt");
        Assert.IsNull(file);
    }

    [TestMethod]
    public void TestGetFolderInRoot()
    {
        var folderId = _fileSystem.GetComponentByPath(_rootFolder, "subFolder").ID;
        Assert.AreEqual(_subFolder.ID, folderId);
    }

    [TestMethod]
    public void TestGetInvalidFolderInRoot()
    {
        var folder = _fileSystem.GetComponentByPath(_rootFolder, "invalidFolder");
        Assert.IsNull(folder);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestGetFileInNonExistentFolder()
    {
        _fileSystem.GetComponentByPath(_rootFolder, "invalidFolder", "text.txt");
    }
}