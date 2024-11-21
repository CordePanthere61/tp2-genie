using FileWalkerImmutable;
using IComponent = FileWalkerImmutable.IComponent;

namespace FileWalkerTests;

[TestClass]
public class DeleteComponentsTest
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
    public void TestDeleteComponentInRoot()
    {
        var component = _fileSystem.GetComponentByPath(_root, "rootFile");
        
        _fileSystem.Delete(component);
        
        var deletedComponent = _fileSystem.GetComponentByPath(_root, "rootFile");
        Assert.IsNull(deletedComponent);
    }

    [TestMethod]
    public void TestDeleteNestedComponent()
    {
        var component = _fileSystem.GetComponentByPath(_root, "folder", "file");
        
        _fileSystem.Delete(component);

        var deletedComponent = _fileSystem.GetComponentByPath(_root, "folder", "file");
        Assert.IsNull(deletedComponent);
    }

    [TestMethod]
    // Method throws. It shouldn't in my opinion. The facade should deal with the exception and return null I guess.
    // Right now it throws NullReferenceException
    public void TestDeleteFolderCascade()
    {
        var folder = _fileSystem.GetComponentByPath(_root, "folder");
        
        _fileSystem.Delete(folder);
        
        var component = _fileSystem.GetComponentByPath(_root, "folder", "subFolder", "subFile");
        Assert.IsNull(component);
    }
}