using FileTransfer.Interfaces;
using FileTransfer.Utility;

namespace FileTransfer.Commands
{
    using System;
    using System.IO;

    class MoveFileCommand : ICommand
    {
        private readonly CsvUtility _csvUtility;

        public MoveFileCommand(string csvFilePath)
        {
            _csvUtility = new CsvUtility(csvFilePath);
        }

        public void Execute(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Error: --move_file requires a file path and a destination folder number");
                return;
            }

            string sourceFile = args[1];
            int rowNumber = 0;


            if (int.TryParse(args[2], out int inputRowNumber))
            {
                rowNumber = inputRowNumber;
            }

            Console.WriteLine($"{sourceFile} and {rowNumber}");

            try
            {
                var data = _csvUtility.ReadCsv();
                string destinationFolder = data[rowNumber];
                File.Move(sourceFile, destinationFolder);
                Console.WriteLine($"File moved to {destinationFolder}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error moving file: {ex.Message}");
            }
        }
    }
}