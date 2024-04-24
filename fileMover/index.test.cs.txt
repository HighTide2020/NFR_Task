using System;
using System.IO;
using NUnit.Framework;

[TestFixture]
public class FileMoverTests
{
    private string sourceDirectory = Path.Combine(Path.GetTempPath(), "SourceTest");
    private string destinationDirectory = Path.Combine(Path.GetTempPath(), "DestinationTest");
    private string logFile = Path.Combine(Path.GetTempPath(), "Logs", "file_mover_test.log");

    [SetUp]
    public void Setup()
    {
        // Create temporary directories
        Directory.CreateDirectory(sourceDirectory);
        Directory.CreateDirectory(destinationDirectory);
        Directory.CreateDirectory(Path.GetDirectoryName(logFile));
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up temporary directories
        Directory.Delete(sourceDirectory, true);
        Directory.Delete(destinationDirectory, true);
        File.Delete(logFile);
    }

    [Test]
    public void TestFileMover()
    {
        // Create a test text file in source directory
        string sourceFilePath = Path.Combine(sourceDirectory, "test.txt");
        File.WriteAllText(sourceFilePath, "Test content");

        // Trigger OnCreated event manually
        Program.OnCreated(null, new FileSystemEventArgs(WatcherChangeTypes.Created, sourceDirectory, "test.txt"));

        // Assert file moved to destination directory
        Assert.IsTrue(File.Exists(Path.Combine(destinationDirectory, "test.txt")));

        // Assert log file contains correct log entry
        string[] logLines = File.ReadAllLines(logFile);
        Assert.AreEqual(2, logLines.Length); // One log for moving file, another for logging first line
        Assert.IsTrue(logLines[1].Contains("First line of test.txt: Test content"));
    }
}
