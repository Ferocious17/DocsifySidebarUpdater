using System;

namespace WeekInserterDocsify
{
    class Program
    {
        private static Updater updater;
        private static string path;
        private static int week;
        private static bool weekSpecified = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Docsify sidebar week updater");
            Console.WriteLine("-------------------------------------------");

            while (true)
            {
                _prompt();
                
                try
                {
                    updater = new Updater(path, week);
                    updater.Update();

                    Console.WriteLine("\nWeek successfully inserted!\n");
                    Console.Write("Continue? (Y/N): ");
                    char choice = Console.ReadKey().KeyChar;
                    
                    //break out of infinite loop if N, n or any other character than Y or y is entered
                    if ((choice == 'N' || choice == 'n') || (choice != 'Y' && choice != 'y'))
                        break;

                    Console.WriteLine();
                    weekSpecified = false;
                }
                catch
                {
                    Console.WriteLine("Please specify a valid directory!");
                }
            }


        }

        /// <summary>
        /// Prompts the user to enter directory path and week that needs to be added
        /// </summary>
        private static void _prompt()
        {
            
            Console.Write("Directory path (docs or weeks): ");
            path = Console.ReadLine();

            if (weekSpecified)
                return;
            
            while (true)
            {
                Console.Write("Week to add: ");
                string tempWeek = Console.ReadLine();

                if (Int32.TryParse(tempWeek, out week))
                {
                    weekSpecified = true;
                    break;
                }
                else
                    Console.WriteLine("Please enter a valid week!");
            }
        }
    }
}
