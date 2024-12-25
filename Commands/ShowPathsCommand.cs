using FileTransfer.Interfaces;

namespace FileTransfer.Commands
{
    using System;
    using System.IO;
    class ShowPathsCommand : ICommand
    {
        private readonly string _csvFilePath;

        public ShowPathsCommand(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public void Execute(string[] args)
        {
            try
            {
                if (File.Exists(_csvFilePath))
                {
                    var data = File.ReadAllLines(_csvFilePath);

                    if (data.Length == 0)
                    {
                        Console.WriteLine("No paths are stored in the file.");
                    }
                    else
                    {
                        Console.WriteLine("Stored destination paths:");
                        foreach (var item in data)
                        {
                            var parts = item.Split(',');
                            Console.WriteLine($"{parts[0]}. {parts[1]}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"File '{_csvFilePath}' does not exist. No paths to show.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading paths: {ex.Message}");
            }
        }
    }
}