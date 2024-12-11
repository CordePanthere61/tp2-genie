
using FileWalkerImmutable;

namespace FileWalkerTests;

[TestClass]
public class GetComponentsTest
{
    private FileSystemFacade _fileSystem;
    private IComponent _root;

    private IComponent _folder;
    private IComponent _emptyFolder;
    private IComponent _subFolder;

    private IComponent _rootFile;
    private IComponent _file;
    private IComponent _subFile;

    [TestInitialize]
    public void Initialize()
    {
        // FileSystem & Root
        _fileSystem = new FileSystemFacade();
        _root = _fileSystem.CreateFolder("root");
        // Folders
        _folder = _fileSystem.CreateFolder("folder");
        _emptyFolder = _fileSystem.CreateFolder("emptyFolder");
        _subFolder = _fileSystem.CreateFolder("subFolder");
        // Files
        _rootFile = _fileSystem.CreateFile("rootFile", 1, "1");
        _file = _fileSystem.CreateFile("file", 2, "12");
        _subFile = _fileSystem.CreateFile("subFile", 3, "123");

        // Arrange structure
        _fileSystem.AddChildren(_root, _folder, _emptyFolder, _rootFile);
        _fileSystem.AddChildren(_folder, _subFolder, _file);
        _fileSystem.AddChildren(_subFolder, _subFile);
    }

    [TestMethod]
    public void TestGetRootOnly()
    {
        var rootId = _fileSystem.GetComponentByPath(_root).ID;
        Assert.AreEqual(_root.ID, rootId);
    }

    [TestMethod]
    // Method throws. It shouldn't in my opinion. The facade should deal with the exception and return null I guess.
    // Right now it throws NullReferenceException
    public void TestGetComponentWithInvalidOrder()
    {
        var component = _fileSystem.GetComponentByPath(_root, "file2", "folder");
        Assert.IsNull(component);
    }

    [TestMethod]
    public void TestGetComponentInRoot()
    {
        var componentId = _fileSystem.GetComponentByPath(_root, "rootFile").ID;
        Assert.AreEqual(_rootFile.ID, componentId);
    }

    [TestMethod]
    public void TestGetComponentFromNestedFolder()
    {
        var componentId = _fileSystem.GetComponentByPath(_root, "folder", "subFolder", "subFile").ID;
        Assert.AreEqual(_subFile.ID, componentId);
    }

    [TestMethod]
    public void TestGetInvalidComponentFromNestedFolder()
    {
        var component = _fileSystem.GetComponentByPath(_root, "emptyFolder", "file");
        Assert.IsNull(component);
    }

    [TestMethod]
    public void TestGetInvalidComponentInRoot()
    {
        var component = _fileSystem.GetComponentByPath(_root, "not_a_folder");
        Assert.IsNull(component);
    }

    [TestMethod]
    // Method throws. It shouldn't in my opinion. The facade should deal with the exception and return null I guess.
    // Right now it throws NullReferenceException
    public void TestGetFileInNonExistentFolder()
    {
        _fileSystem.GetComponentByPath(_root, "not_a_folder", "file1");
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))] //NameAlreadyExistsException ?
    // This should not be permitted.
    // The creation of a new file with the same name should throw a NameAlreadyExistsException IMO
    public void TestCreateComponentWithSameName()
    {
        var componentA = _fileSystem.CreateFile(_rootFile.Name, 5, "12345");
        _fileSystem.AddChildren(_root, componentA);
    }
}