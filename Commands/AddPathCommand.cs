using FileTransfer.Interfaces;
using FileTransfer.Utility;

namespace FileTransfer.Commands
{
    using System;
    class AddPathCommand : ICommand
    {
        private readonly CsvUtility _csvUtility;

        public AddPathCommand(string csvFilePath)
        {
            _csvUtility = new CsvUtility(csvFilePath);
        }

        public void Execute(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: --add_path requires at least a path argument.");
                return;
            }
            
            if (args.Length > 3)
            {
                Console.WriteLine("Error: --add_path requires only a path argument and a row_number.");
                return;
            }

            string path = args[1];
            int rowNumber = 0;

            if (args.Length > 2 && int.TryParse(args[2], out int inputRowNumber))
            {
                rowNumber = inputRowNumber;
            }

            try
            {
                int nextEmpty = 1;
                bool flagPathAlreadyExists = false;
                var data = _csvUtility.ReadCsv();

                foreach (var item in data)
                {
                    if (item.Value == path)
                    {
                        flagPathAlreadyExists = true;
                        rowNumber = item.Key;
                        break;
                    }
                    if (!data.ContainsKey(item.Key + 1))
                    {
                        nextEmpty = item.Key + 1;
                    }
                }

                if (flagPathAlreadyExists)
                {
                    Console.WriteLine($"Path '{path}' already exists at row {rowNumber}.");
                }
                else
                {
                    bool flagAddPath = true;

                    if (data.ContainsKey(rowNumber))
                    {
                        Console.WriteLine($"Do you want to change the path on row {rowNumber} from {data[rowNumber]} to {path}? (y/n)");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        char response = keyInfo.KeyChar;
                        Console.WriteLine();
                        if (response == 'y' || response == 'Y')
                        {
                            Console.WriteLine($"Path at row {rowNumber} will be overwritten.");
                        }
                        else if (response == 'n' || response == 'N')
                        {
                            flagAddPath = false;
                            Console.WriteLine($"Path at row {rowNumber} will stay {data[rowNumber]}.");
                        }
                        else
                        {
                            flagAddPath = false;
                            Console.WriteLine("Invalid response.");
                        }
                    }
                    else
                    {
                        rowNumber = nextEmpty;
                    }

                    if (flagAddPath)
                    {
                        data[rowNumber] = path;
                        _csvUtility.WriteCsv(data);
                        Console.WriteLine($"Path '{path}' was added successfully at row {rowNumber}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding path: {ex.Message}");
            }
        }
    }
}