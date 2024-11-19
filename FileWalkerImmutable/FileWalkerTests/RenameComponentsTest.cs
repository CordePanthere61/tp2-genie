
using FileWalkerImmutable;


namespace FileWalkerTests;

[TestClass]
public class RenameComponentsTest
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
        
        _fileSystem.AddChildren(_rootFolder, _subFolder, _textFile);
    }

    [TestMethod]
    public void TestRenameFile()
    {
        _fileSystem.Rename(_textFile, "newFile.txt");
        var newTextFile = _fileSystem.GetComponentByPath(_rootFolder, "newFile.txt");
 
        Assert.IsNotNull(newTextFile);
    }

    [TestMethod]
    public void TestRenameFolder()
    {
        _fileSystem.Rename(_subFolder, "newSubFolder");
        var newFolder = _fileSystem.GetComponentByPath(_rootFolder, "newSubFolder");
        
        Assert.IsNotNull(newFolder);
    }

    [TestMethod]
    public void TestRenameFileIsChangingInstance()
    {
        
        _fileSystem.Rename(_textFile, "newFile.txt");
        var newTextFile = _fileSystem.GetComponentByPath(_rootFolder, "newFile.txt");
        
        Assert.AreNotEqual(_textFile, newTextFile);
    }

    [TestMethod]
    public void TestRenameFolderIsChangingInstance()
    {
        
        _fileSystem.Rename(_subFolder, "newSubFolder");
        var newFolder = _fileSystem.GetComponentByPath(_rootFolder, "newSubFolder");
        
        Assert.AreNotEqual(_subFolder, newFolder);
    }

    [TestMethod]
    public void TestRenameFileKeepsContent()
    {
        _fileSystem.Rename(_textFile, "newFile.txt");
        var newFile = _fileSystem.GetComponentByPath(_rootFolder, "newFile.txt");
        
        Assert.AreEqual((_textFile as IFile).Content, (newFile as IFile).Content);
    }


    [TestMethod]
    public void TestRenameFileKeepsId()
    {
        _fileSystem.Rename(_textFile, "newFile.txt");
        var newFile = _fileSystem.GetComponentByPath(_rootFolder, "newFile.txt");
        
        Assert.AreEqual(_textFile.ID, newFile.ID);
    }

    [TestMethod]
    public void TestRenameFolderKeepsId()
    {
        _fileSystem.Rename(_subFolder, "newSubFolder");
        var newFolder = _fileSystem.GetComponentByPath(_rootFolder, "newSubFolder");

        Assert.AreEqual(_subFolder.ID, newFolder.ID);
    }

}