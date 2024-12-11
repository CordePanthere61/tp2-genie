using FileWalkerImmutable;


namespace FileWalkerTests;

[TestClass]
public class RenameComponentsTest
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
    public void TestRenameFile()
    {
        var file = _fileSystem.GetComponentByPath(_root, "rootFile");
        
        _fileSystem.Rename(file, "newRootFile");
        
        var newTextFile = _fileSystem.GetComponentByPath(_root, "newRootFile");
        Assert.IsNotNull(newTextFile);
    }

    [TestMethod]
    public void TestRenameFolder()
    {
        var folder = _fileSystem.GetComponentByPath(_root, "folder");
        
        _fileSystem.Rename(folder, "newFolder");
        
        var newFolder = _fileSystem.GetComponentByPath(_root, "newFoler");
        Assert.IsNotNull(newFolder);
    }

    [TestMethod]
    public void TestRenameComponentIsChangingInstance()
    {
        var component = _fileSystem.GetComponentByPath(_root, "rootFile");
        
        _fileSystem.Rename(component, "rootFile2");
        
        var newComponent = _fileSystem.GetComponentByPath(_root, "rootFile2");
        Assert.AreNotEqual(component, newComponent);
    }


    [TestMethod]
    public void TestRenameFileKeepsContent()
    {
        var file = _fileSystem.GetComponentByPath(_root, "rootFile");
        
        _fileSystem.Rename(file, "newRootFile");
        
        var newFile = _fileSystem.GetComponentByPath(_root, "newRootFile");
        Assert.AreEqual((file as IFile).Content, (newFile as IFile).Content);
    }

    [TestMethod]
    public void TestRenameComponentKeepsId()
    {
        var component = _fileSystem.GetComponentByPath(_root, "rootFile");
        
        _fileSystem.Rename(component, "rootFile2");
        
        var renamedComponent = _fileSystem.GetComponentByPath(_root, "rootFile2");
        Assert.AreEqual(component.ID, renamedComponent.ID);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))] // NameAlreadyExistsException ?
    // This should not be permitted.
    public void TestRenameComponentToExistingName()
    {
        var folder = _fileSystem.GetComponentByPath(_root, "folder");
        var existingFolder = _fileSystem.GetComponentByPath(_root, "emptyFolder");
        _fileSystem.Rename(folder, existingFolder.Name);
    }
}