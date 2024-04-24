// namespace fileMover;

// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Hello, World!");
//     }
// }
using System;
using System.IO;

class Program
{
    static string sourceDirectory = @"C:\Source";
    static string destinationDirectory = @"C:\Destination";
    static string logFile = @"C:\Logs\file_mover.log";

    static void Main()
    {
        // Ensure directories exist
        Directory.CreateDirectory(sourceDirectory);
        Directory.CreateDirectory(destinationDirectory);
        Directory.CreateDirectory(Path.GetDirectoryName(logFile));

        // Start monitoring the source directory
        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = sourceDirectory;
        watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
        watcher.Filter = "*.*";
        watcher.Created += OnCreated;
        watcher.EnableRaisingEvents = true;

        Console.WriteLine("File Mover is running. Press any key to exit.");
        Console.ReadKey();
    }

    static void OnCreated(object sender, FileSystemEventArgs e)
    {
        try
        {
            // Move the file to the destination directory
            string sourceFile = e.FullPath;
            string fileName = Path.GetFileName(sourceFile);
            string destFile = Path.Combine(destinationDirectory, fileName);
            File.Move(sourceFile, destFile);

            // Log if it's a text file
            if (Path.GetExtension(fileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
            {
                LogFirstLine(destFile);
            }
        }
        catch (Exception ex)
        {
            LogError($"Error moving file {e.Name}: {ex.Message}");
        }
    }

    static void LogFirstLine(string filePath)
    {
        try
        {
            string firstLine = File.ReadLines(filePath).FirstOrDefault();
            File.AppendAllText(logFile, $"First line of {Path.GetFileName(filePath)}: {firstLine}\n");
        }
        catch (Exception ex)
        {
            LogError($"Error logging first line of {filePath}: {ex.Message}");
        }
    }

    static void LogError(string message)
    {
        File.AppendAllText(logFile, $"Error: {message}\n");
    }
}
