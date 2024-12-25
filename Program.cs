using FileTransfer.Interfaces;
using FileTransfer.Commands;

namespace FileTransfer
{
    using System;
    using System.Collections.Generic;

    
    class Program
    {
        static void Main(string[] args)
        {
            string csvFilePath = "paths.csv";

            var commands = new Dictionary<string, ICommand>
            {
                { "--move_file", new MoveFileCommand(csvFilePath) },
                { "--move_folder", new MoveFileCommand(csvFilePath) },
                { "--add_path", new AddPathCommand(csvFilePath) },
                { "--show_paths", new ShowPathsCommand(csvFilePath) },
                { "--help", new HelpCommand() }
            };

            if (args.Length > 0){

                if (commands.ContainsKey(args[0].ToLower()))
                {
                    commands[args[0].ToLower()].Execute(args.Skip(0).ToArray());
                }
                else
                {
                    Console.WriteLine($"Unknown command: {args[0]}");
                    commands["--help"].Execute(args.Skip(0).ToArray());
                }
            }
            else
            { 
                Console.WriteLine($"To run the application at least one parameter as a command is required."); 
            }
        }
    }
}