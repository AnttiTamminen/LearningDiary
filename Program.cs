using System;
using System.Collections.Generic;
using System.IO;

namespace LearningDiary
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool goOn = true;
            while (goOn)
            {
                Console.Clear();
                Options();
                char answer = Console.ReadKey().KeyChar;
                int control;
                do
                {
                    switch (answer)
                    {
                        case '1':
                            Console.Clear();
                            Create.CreateMTopics();
                            control = 0;
                            break;

                        case '2':
                            Console.Clear();
                            Create.CreateMTasks();
                            control = 0;
                            break;

                        case '3':
                            Console.Clear();
                            Create.CreateNotes();
                            control = 0;
                            break;

                        case '4':
                            PrintToConsole.PrintTopics();
                            control = 0;
                            break;

                        case '0':
                            Console.Clear();
                            goOn = false;
                            control = 0;
                            break;

                        default:
                            Console.WriteLine("\nInvalid key. Please try again\n");
                            answer = Console.ReadKey().KeyChar;
                            control = 1;
                            break;
                    }
                } while (control == 1);
            }
        }

        public static void Options()
        {
            Console.WriteLine("*********************************************************************\n\n" +
            "LEARNING DIARY 5000".PadLeft(40) +
            "\n\n*********************************************************************\n");
            Console.WriteLine("Press:\n1) To input a topic\n2) To input a task\n3) To input a note\n4) To print Topics\n0) To exit\n");
        }
    }
}
