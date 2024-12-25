using FileTransfer.Interfaces;
using FileTransfer.Utility;

namespace FileTransfer.Commands
{
    using System;
    using System.IO;

    class MoveFolderCommand : ICommand
    {
        private readonly CsvUtility _csvUtility;

        public MoveFolderCommand(string csvFilePath)
        {
            _csvUtility = new CsvUtility(csvFilePath);
        }

        public void Execute(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Error: --move_folder requires a sourse folder path and a destination folder number");
                return;
            }

            string sourceFolder = args[1];
            int rowNumber = 0;


            if (int.TryParse(args[2], out int inputRowNumber))
            {
                rowNumber = inputRowNumber;
            }

            Console.WriteLine($"{sourceFolder} and {rowNumber}");

            try
            {
                var data = _csvUtility.ReadCsv();
                string destinationFolder = data[rowNumber];
                Directory.Move(sourceFolder, destinationFolder);
                Console.WriteLine($"Folder moved to {destinationFolder}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving file: {ex.Message}");
            }

        }
    }
}