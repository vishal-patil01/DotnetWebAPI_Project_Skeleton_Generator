using System.Diagnostics;
using ProjectCreator.MultiLayerProject;
using ProjectCreator.SingleLayerProject;

namespace ProjectCreator
{
    class Program
    {
        static char DotNetVersionFirstChar
        {
            get
            {
                string version = GetDotNetVersion();
                return version[0];
            }
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string projectCreator = @"
┌─────────────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                             │
│     _   _      _     _____           _           _      _____                _              │
│    | \ | |    | |   |  __ \         (_)         | |    / ____|              | |             │
│    |  \| | ___| |_  | |__) | __ ___  _  ___  ___| |_  | |     _ __ ___  __ _| |_ ___  _ __  │
│    | . ` |/ _ \ __| |  ___/ '__/ _ \| |/ _ \/ __| __| | |    | '__/ _ \/ _` | __/ _ \| '__| │
│   _| |\  |  __/ |_  | |   | | | (_) | |  __/ (__| |_  | |____| | |  __/ (_| | || (_) | |    │
│  (_)_| \_|\___|\__| |_|   |_|  \___/| |\___|\___|\__|  \_____|_|  \___|\__,_|\__\___/|_|    │
│                                    _/ |                                                     │
│                                   |__/                                                      │
│                                                                          - By Vishal Rajput │
└─────────────────────────────────────────────────────────────────────────────────────────────┘";
            string projectName = string.Empty;
            Console.WriteLine($"{projectCreator} \n");
            Console.ForegroundColor = ConsoleColor.Gray;
            int choice;
            do
            {
                Console.WriteLine($"Please enter project name [dont add api word in project name]\n");
                projectName = Console.ReadLine();
                Console.WriteLine("======================================");
                Console.WriteLine("Please choose Project type:");
                Console.WriteLine("1 Multilayer Web API Project ");
                Console.WriteLine("2 Singlelayer Web API Project ");
                Console.WriteLine("0 For Exit");
                Console.WriteLine("======================================");

                var input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                {
                    if (choice == 0)
                    {
                        Environment.Exit(0);
                    }

                    switch (choice)
                    {
                        case 1:
                            MultiLayerProjectCreator.CreateMultiLayerProject(projectName, DotNetVersionFirstChar);
                            break;
                        case 2:
                            SingleLayerProjectCreator.CreateSingleLayerProjectCreator(projectName, DotNetVersionFirstChar);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter a valid option.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid option.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.ReadKey();
            } while (string.IsNullOrEmpty(projectName));
        }

        static string GetDotNetVersion()
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            return process.StandardOutput.ReadLine();
        }
    }
}