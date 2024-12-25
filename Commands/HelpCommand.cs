using FileTransfer.Interfaces;

namespace FileTransfer.Commands
{
    using System;
    class HelpCommand : ICommand
    {
        public void Execute(string[] args)
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("  --move_file <path> [row_number]     Moves a source file to a destination folder saved in the CSV file.");
            Console.WriteLine("  --move_folder <path> [row_number]   Moves a source folder to a destination folder saved in the CSV file.");
            Console.WriteLine("  --add_path <path> [row_number]      Adds a path to the specified row in the CSV file (default: next available row).");
            Console.WriteLine("  --show_paths                        Displays all stored paths from the CSV file.");
            Console.WriteLine("  --help                              Displays this help message.");
        }
    }
}