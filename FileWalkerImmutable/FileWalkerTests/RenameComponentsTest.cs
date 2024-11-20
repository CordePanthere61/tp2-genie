using FileWalkerImmutable;


namespace FileWalkerTests;

[TestClass]
public class RenameComponentsTest
{
    private FileSystemFacade _fileSystem;
    private IComponent _rootFolder;
    private IComponent _folder1;
    private IComponent _folder2;
    private IComponent _file1;
    private IComponent _file2;

    [TestInitialize]
    public void Initialize()
    {
        _fileSystem = new FileSystemFacade();
        _rootFolder = _fileSystem.CreateFolder("root");
        _folder1 = _fileSystem.CreateFolder("folder1");
        _folder2 = _fileSystem.CreateFolder("folder2");
        _file1 = _fileSystem.CreateFile("file1", 3, "123");
        _file2 = _fileSystem.CreateFile("file2", 4, "1234");

        _fileSystem.AddChildren(_rootFolder, _folder1, _file1, _file2);
    }

    [TestMethod]
    public void TestRenameFile()
    {
        _fileSystem.Rename(_file1, "newfile1");
        var newTextFile = _fileSystem.GetComponentByPath(_rootFolder, "newfile1");

        Assert.IsNotNull(newTextFile);
    }

    [TestMethod]
    public void TestRenameFolder()
    {
        _fileSystem.Rename(_folder1, "newFolder1");
        var newFolder = _fileSystem.GetComponentByPath(_rootFolder, "newFolder1");

        Assert.IsNotNull(newFolder);
    }

    [TestMethod]
    public void TestRenameFileIsChangingInstance()
    {
        _fileSystem.Rename(_file1, "newfile1");
        var newTextFile = _fileSystem.GetComponentByPath(_rootFolder, "newfile1");

        Assert.AreNotEqual(_file1, newTextFile);
    }

    [TestMethod]
    public void TestRenameFolderIsChangingInstance()
    {
        _fileSystem.Rename(_folder1, "newFolder1");
        var newFolder = _fileSystem.GetComponentByPath(_rootFolder, "newFolder1");

        Assert.AreNotEqual(_folder1, newFolder);
    }

    [TestMethod]
    public void TestRenameFileKeepsContent()
    {
        _fileSystem.Rename(_file1, "newfile1");
        var newFile = _fileSystem.GetComponentByPath(_rootFolder, "newfile1");

        Assert.AreEqual((_file1 as IFile).Content, (newFile as IFile).Content);
    }

    [TestMethod]
    public void TestRenameFileKeepsId()
    {
        _fileSystem.Rename(_file1, "newfile1");
        var newFile = _fileSystem.GetComponentByPath(_rootFolder, "newfile1");

        Assert.AreEqual(_file1.ID, newFile.ID);
    }

    [TestMethod]
    public void TestRenameFolderKeepsId()
    {
        _fileSystem.Rename(_folder1, "newFolder1");
        var newFolder = _fileSystem.GetComponentByPath(_rootFolder, "newFolder1");

        Assert.AreEqual(_folder1.ID, newFolder.ID);
    }

    [TestMethod]
    public void TestRenameFileToExistingName()
    {
        _fileSystem.Rename(_file1, _file2.Name);
    }
    
    [TestMethod]
    public void TestRenameFolderToExistingName()
    {
        _fileSystem.Rename(_folder1, _folder2.Name);
    }
}