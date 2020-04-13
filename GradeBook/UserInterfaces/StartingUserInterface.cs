﻿using GradeBook.Enums;
using GradeBook.GradeBooks;
using System;

namespace GradeBook.UserInterfaces
{
    public static class StartingUserInterface
    {
        public static bool Quit = false;
        public static void CommandLoop()
        {
            while (!Quit)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(">> What would you like to do?");
                var command = Console.ReadLine().ToLower();
                CommandRoute(command);
            }
        }

        public static void CommandRoute(string command)
        {
            if (command.StartsWith("create"))
                CreateCommand(command);
            else if (command.StartsWith("load"))
                LoadCommand(command);
            else if (command == "help")
                HelpCommand();
            else if (command == "quit")
                Quit = true;
            else
                Console.WriteLine("{0} was not recognized, please try again.", command);
        }

        public static void CreateCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 3)
            {
                Console.WriteLine("Command not valid, Create requires a name and type of gradebook.");
                return;
            }
            var name = parts[2];
            string sType;
            
            if (name == Enum.GetName(typeof(GradeBookType), 0))
            {
                sType = Enum.GetName(typeof(GradeBookType), 0);
                StandardGradeBook gradeBook = new StandardGradeBook(name);
                GradeBookUserInterface.CommandLoop(gradeBook);
            }
            else if (name == Enum.GetName(typeof(GradeBookType), 1))
            {
                sType = Enum.GetName(typeof(GradeBookType), 1);
                RankedGradeBook gradeBook = new RankedGradeBook(name);
                GradeBookUserInterface.CommandLoop(gradeBook);
            }
            else 
            {
                Console.WriteLine($"{name}  is not a supported type of gradebook, please try again");
                return;
            }

            Console.WriteLine($"Create {name} {sType} - Creates a new gradebook where 'Name' is the name of the gradebook and 'Type' is what type of grading it should use.");

            //BaseGradeBook gradeBook = new BaseGradeBook(name);
        }

        public static void LoadCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Command not valid, Load requires a name.");
                return;
            }
            var name = parts[1];
            var gradeBook = BaseGradeBook.Load(name);

            if (gradeBook == null)
                return;

            GradeBookUserInterface.CommandLoop(gradeBook);
        }

        public static void HelpCommand()
        {
            Console.WriteLine();
            Console.WriteLine("GradeBook accepts the following commands:");
            Console.WriteLine();
            Console.WriteLine("Create 'Name' - Creates a new gradebook where 'Name' is the name of the gradebook.");
            Console.WriteLine();
            Console.WriteLine("Load 'Name' - Loads the gradebook with the provided 'Name'.");
            Console.WriteLine();
            Console.WriteLine("Help - Displays all accepted commands.");
            Console.WriteLine();
            Console.WriteLine("Quit - Exits the application");
        }
    }
}
