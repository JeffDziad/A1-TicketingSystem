using System;
using System.IO;
using System.Collections.Generic;
using ConsoleTables;

namespace A1_TicketingSystem
{
    class Program
    {
        public static string file = "Tickets.csv";
        public static bool isError = false;
        public static string error;

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            if(isError)
            {
                isError = false;
                Console.WriteLine(error);
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine("|     Ticketing System     |");
            Console.WriteLine("----------------------------\n");
            Console.WriteLine("1. Read data from file.");
            Console.WriteLine("2. Create file from data.");
            Console.WriteLine("Enter any other key to exit.");
            Console.Write(": ");

            string input = Console.ReadLine();

            handleInput(input);
        }

        static void handleInput(String i)
        {
            switch(i)
            {
                case "1":
                    handleFileRead();
                    break;
                case "2":
                    checkFile();
                    break;
                default:
                    break;
            }
        }

        static void handleFileRead()
        {
            if(File.Exists(file))
            {
                StreamReader reader = new StreamReader(file);
                var table = new ConsoleTable("TicketID", "Summary", "Status", "Priority", "Submitter", "Assigned", "Watching");
                reader.ReadLine();
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] lineArray = line.Split(",");
                    string[] watchingArray = lineArray[6].Split("|");
                    string watchingString = String.Join(", ", watchingArray);
                    table.AddRow(lineArray[0], lineArray[1], lineArray[2], lineArray[3], lineArray[4], lineArray[5], watchingString);
                }
                table.Write();
                Console.Write("Press any key to return to the menu...");
                Console.ReadLine();
                reader.Close();
                Console.Clear();
                MainMenu();
            } 
            else
            {
                error = "File does not exist!";
                isError = true;
                Console.Clear();
                MainMenu();
            }
        }

        static void checkFile()
        {
            string input;
            bool isError = false;
            string error = "";
            if(isError)
            {
                Console.WriteLine(error);
            }
            if(File.Exists(file))
            {
                Console.Clear();
                Console.Write("The file {0} exists. Would you like to add a new line? (Y/N): ", file);
                input = Console.ReadLine();
                switch(input.ToUpper())
                {
                    case "Y":
                    handleFileEdit();
                        break;
                    case "N":
                    Console.Clear();
                    MainMenu();
                        break;
                    default:
                        isError = true;
                        error = "Please enter Y or N.";
                        Console.Clear();
                        checkFile();
                        break;
                }
            }else
            {
                handleFileCreate();
            }
        }

        static void handleFileEdit()
        {
            string[] input = new string[7];

            int idNumber = getLineAmount();

            StreamWriter writer = new StreamWriter(file, append: true);
            input[0] = idNumber.ToString();
            input[1] = getSummary();
            input[2] = getStatus();
            input[3] = getPriority();
            input[4] = getSubmitter();
            input[5] = getAssigned();
            input[6] = getWatching();
            idNumber++;
            writer.WriteLine(String.Join(",", input));
            writer.Close();
            MainMenu();
        }

        static void handleFileCreate()
        {
            string[] input = new string[7];
            int idNumber = 0;
            StreamWriter writer = new StreamWriter(file);
            if(idNumber == 0)
            {
                writer.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching");
                idNumber++;
            }
            input[0] = idNumber.ToString();
            input[1] = getSummary();
            input[2] = getStatus();
            input[3] = getPriority();
            input[4] = getSubmitter();
            input[5] = getAssigned();
            input[6] = getWatching();
            idNumber++;
            writer.WriteLine(String.Join(",", input));
            writer.Close();
            MainMenu();
        }

        static string getSummary()
        {
            string input = "";
            bool isError = false;
            bool notFinished = true;
            string error = "";
            Console.Clear();
            while(notFinished)
            {
                if(isError)
                {
                    Console.WriteLine(error);
                }
                Console.Write("Enter a summary for the ticket: ");
                input = Console.ReadLine();
                if(input.ToUpper() == "")
                {
                    error = "Summary can't be empty...";
                    isError = true;
                    Console.Clear();
                }
                else
                {
                    notFinished = false;
                    isError = false;
                    Console.Clear();
                }
            }
            notFinished = true;
            return input;
        }

        static string getStatus()
        {
            string input = "";
            bool isError = false;
            bool notFinished = true;
            string error = "";
            Console.Clear();
            while(notFinished)
            {
                if(isError)
                {
                    Console.WriteLine(error);
                }
                Console.Write("Enter a status for the ticket (Open/Closed): ");
                input = Console.ReadLine();
                if(input.ToUpper() == "OPEN" || input.ToUpper() == "CLOSED")
                {
                    isError = false;
                    notFinished = false;
                    Console.Clear();
                }
                else
                {
                    error = "Please enter a valid status...";
                    isError = true;
                    Console.Clear();
                }
            }
            notFinished = true;
            return input;
        }

        static string getPriority()
        {
            string input = "";
            bool isError = false;
            bool notFinished = true;
            string error = "";
            Console.Clear();
            while(notFinished)
            {
                if(isError)
                {
                    Console.WriteLine(error);
                }
                Console.Write("Enter a priority for the ticket (High/Medium/Low): ");
                input = Console.ReadLine();
                if(input.ToUpper() == "HIGH" || input.ToUpper() == "MEDIUM" || input.ToUpper() == "LOW")
                {
                    isError = false;
                    notFinished = false;
                    Console.Clear();
                }
                else
                {
                    error = "Please enter a valid priority...";
                    isError = true;
                    Console.Clear();
                }
            }
            notFinished = true;
            return input;
        }

        static string getSubmitter()
        {
            string input = "";
            bool isError = false;
            bool notFinished = true;
            string error = "";
            Console.Clear();
            while(notFinished)
            {
                if(isError)
                {
                    Console.WriteLine(error);
                }
                Console.Write("Enter a submitter name: ");
                input = Console.ReadLine();
                if(input.ToUpper() == "")
                {
                    error = "Submitter name can't be empty...";
                    isError = true;
                    Console.Clear();
                }
                else
                {
                    isError = false;
                    notFinished = false;
                    Console.Clear();
                }
            }
            notFinished = true;
            return input;
        }

        static string getAssigned()
        {
            string input = "";
            bool isError = false;
            bool notFinished = true;
            string error = "";
            Console.Clear();
            while(notFinished)
            {
                if(isError)
                {
                    Console.WriteLine(error);
                }
                Console.Write("Enter assigned name: ");
                input = Console.ReadLine();
                if(input.ToUpper() == "")
                {
                    error = "Assigned name can't be empty...";
                    isError = true;
                    Console.Clear();
                }
                else
                {
                    isError = false;
                    notFinished = false;
                    Console.Clear();
                }
            }
            notFinished = true;
            return input;
        }

        static string getWatching()
        {
            string input = "";
            List<string> watching = new List<string>();
            string error = "";
            int numWatching = 0;
            bool notFinished = true;
            bool isError = false;
            while(notFinished)
            {
                if(isError)
                {
                    Console.WriteLine(error);
                }
                Console.Write("Enter watching persons name: ");
                input = Console.ReadLine();
                if(input.ToUpper() == "" && numWatching == 0)
                {
                    error = "There must be at least one person watching...";
                    isError = true;
                }
                else
                {
                    watching.Add(input);
                    Console.Write("Enter another name? (Y/Press any other key): ");
                    input = Console.ReadLine();
                    if(input.ToUpper() == "Y")
                    {
                        numWatching++;
                    }
                    else
                    {
                        notFinished = false;
                        isError = false;
                    }
                }
            }
            numWatching = 0;
            input = String.Join("|", watching.ToArray());
            return input;
        }

        static int getLineAmount()
        {
            StreamReader reader = new StreamReader(file);
            int lineNum = 0;
            while(!reader.EndOfStream)
            {
                reader.ReadLine();
                lineNum++;
            }
            reader.Close();
            return lineNum;
        }
    }
}
