
using FileWalkerImmutable;

namespace FileWalkerTests;

[TestClass]
public class GetComponentsTest
{
    private FileSystemFacade _fileSystem;
    private IComponent _root;
    private IComponent _folder;
    private IComponent _file2;
    private IComponent _file1;

    [TestInitialize]
    public void Initialize()
    {
        _fileSystem = new FileSystemFacade();
        _root = _fileSystem.CreateFolder("root");
        _file1 = _fileSystem.CreateFile("file1", 1, "1");
        _folder = _fileSystem.CreateFolder("folder1");
        _file2 = _fileSystem.CreateFile("file2", 2, "12");
        
        _fileSystem.AddChildren(_root, _folder);
        _fileSystem.AddChildren(_root, _file1);
        _fileSystem.AddChildren(_folder, _file2);
    }

    [TestMethod]
    public void TestGetRootOnly()
    {
        var rootId = _fileSystem.GetComponentByPath(_root).ID;
        Assert.AreEqual(_root.ID, rootId);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestGetPathWithInvalidOrder()
    {
        _fileSystem.GetComponentByPath(_root, "file2", "folder");
    }

    [TestMethod]
    public void TestGetFileInRoot()
    {
        var componentId = _fileSystem.GetComponentByPath(_root, "file1").ID;
        Assert.AreEqual(_file1.ID, componentId);
    }
    
    [TestMethod]
    public void TestGetFileFromNestedFolder()
    {
        var componentId = _fileSystem.GetComponentByPath(_root, "folder1", "file2").ID;
        Assert.AreEqual(_file2.ID, componentId);
    }

    [TestMethod]
    public void TestGetInvalidFileFromNestedFolder()
    {
        var component = _fileSystem.GetComponentByPath(_root, "folder1", "not_a_file");
        Assert.IsNull(component);
    }

    [TestMethod]
    public void TestGetFolderInRoot()
    {
        var componentId = _fileSystem.GetComponentByPath(_root, "folder1").ID;
        Assert.AreEqual(_folder.ID, componentId);
    }

    [TestMethod]
    public void TestGetInvalidFolderInRoot()
    {
        var component = _fileSystem.GetComponentByPath(_root, "not_a_folder");
        Assert.IsNull(component);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void TestGetFileInNonExistentFolder()
    {
        _fileSystem.GetComponentByPath(_root, "not_a_folder", "file1");
    }
}