using FileWalkerImmutable;

namespace FileWalkerTests;

[TestClass]
public class NotificationsTest
{
    private FileSystemFacade _fileSystem;
    private IComponent _rootFolder;
    private IComponent _folder1;
    private IComponent _file1;
    
    [TestInitialize]
    public void Initialize()
    {
        _fileSystem = new FileSystemFacade();
        _rootFolder = _fileSystem.CreateFolder("root");
        _folder1 = _fileSystem.CreateFolder("folder1");
        _file1 = _fileSystem.CreateFile("file1", 2, "123");
        
        _fileSystem.AddChildren(_rootFolder, _folder1, _file1);
    }

    [TestMethod]
    public void TestAddComponentObserver()
    {
        _fileSystem.NotifyOnChange(_folder1);
        _fileSystem.Rename(_folder1, "new name");
        
        Assert.AreEqual(1, _fileSystem.NotificationLog.Count);
    }


    // It should not fail. If observer already exists, it should be overridden.
    [TestMethod]
    public void TestComponentShouldNotBeAddedTwice()
    {
        _fileSystem.NotifyOnChange(_folder1);
        _fileSystem.NotifyOnChange(_folder1);
        _fileSystem.Rename(_folder1, "new name");
        
        Assert.AreEqual(1, _fileSystem.NotificationLog.Count);
    }

    [TestMethod]
    public void TestRenameNotification()
    {
        _fileSystem.NotifyOnChange(_folder1);

        _fileSystem.Rename(_folder1, "new folder");
        
        Assert.AreEqual("folder1 was renamed to new folder.", _fileSystem.NotificationLog.Peek());
    }

    [TestMethod]
    public void TestDeleteNotification()
    {
        _fileSystem.NotifyOnChange(_folder1);

        _fileSystem.Delete(_folder1);
        
        Assert.AreEqual("folder1 was deleted.", _fileSystem.NotificationLog.Peek()); 
    }

}