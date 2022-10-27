using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheatreTickets
{
    class TheatreTickets
    {
        static int[,] category = new int[15, 20];
        static string[,] bookings = new string[15, 20];

        //Read and store the data
        static void Task1()
        {
            StreamReader sr = new StreamReader("foglaltsag.txt");

            for (int i = 0; i < bookings.GetLength(0); i++)
            {
                string line = sr.ReadLine();
                for (int j = 0; j < bookings.GetLength(1); j++)
                {
                    bookings[i, j] = line[j].ToString();
                }
            }

            sr = new StreamReader("kategoria.txt");

            for (int i = 0; i < category.GetLength(0); i++)
            {
                string line = sr.ReadLine();
                for (int j = 0; j < category.GetLength(1); j++)
                {
                    category[i, j] = int.Parse(line[j].ToString());
                }
            }

            sr.Close();

        }

        //Ask the user for a seat
        //Print if that seat is booked or not
        static void Task2()
        {
            Console.WriteLine("Task 2");

            Console.Write("Row number (1-15)= ");
            int row = int.Parse(Console.ReadLine());
            Console.Write("Seat number (1-20)= ");
            int seat = int.Parse(Console.ReadLine());

            if (bookings[row - 1, seat - 1] == "x") //x=booked, o=available
            {
                Console.WriteLine("Booked");
            }
            else
            {
                Console.WriteLine("Available");
            }
        }

        //Calculate how many tickets were sold, and what percentage that is.
        static void Task3()
        {
            Console.WriteLine("Task 3");

            int soldTickets = 0;

            for (int i = 0; i < bookings.GetLength(0); i++)
            {
                for (int j = 0; j < bookings.GetLength(1); j++)
                {
                    if (bookings[i, j] == "x")
                        soldTickets++;
                }
            }

            Console.WriteLine($"{soldTickets} tickets were sold, this is {Math.Round((double)soldTickets / (bookings.GetLength(0) * bookings.GetLength(1)) * 100)}% of all the tickets.");
        }

        //Print the category in which the most tickets were sold
        static void Task4()
        {
            Console.WriteLine("Task 4");

            int[] categories = new int[5];
            int max = 0;
            int priceCategory = 0;
            for (int i = 0; i < categories.Length; i++)
            {
                categories[i] = 0;
                for (int s = 0; s < 15; s++)
                {
                    for (int o = 0; o < 20; o++)
                    {
                        if (bookings[s, o] == "x" && category[s, o] == i + 1)
                            categories[i]++;
                    }
                }
                if (categories[i] > max)
                {
                    max = categories[i];
                    priceCategory = i + 1;
                }
            }
            Console.WriteLine($"The most tickets were sold in category {priceCategory}.");

        }

        //Calculate the income of the show
        static void Task5()
        {
            Console.WriteLine("Task 5");
            int[] prices = { 5000, 4000, 3000, 2000, 1500 };
            int income = 0;

            for (int s = 0; s < 15; s++)
            {
                for (int o = 0; o < 20; o++)
                {
                    if (bookings[s, o] == "x")
                        income += prices[category[s, o] - 1];
                }
            }

            Console.WriteLine($"The income is: {income}Ft");

        }

        //Count how many lonely seats are available
        static void Task6()
        {
            Console.WriteLine("Task 6");

            int lonelySeats = 0;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (j == 0 && bookings[i, j] == "o" && bookings[i, j + 1] == "x")
                    {

                        lonelySeats++;
                    }
                    else if (j < 19 && bookings[i, j] == "o" && bookings[i, j + 1] == "x" && bookings[i, j - 1] == "x")
                    {

                        lonelySeats++;
                    }
                    else if (j == 19 && bookings[i, j] == "o" && bookings[i, j - 1] == "x")
                    {
                        lonelySeats++;
                    }
                }
            }
            Console.WriteLine($"Number of lonely seats: {lonelySeats}");
        }

        //Instead of the available seats display the category of the seat in the szabad.txt file

        static void Task7()
        {
            StreamWriter sw = new StreamWriter("szabad.txt");

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (bookings[i, j] == "o")
                    {
                        sw.Write(category[i, j]);
                    }
                    else
                    {
                        sw.Write(bookings[i, j]);
                    }
                }
                sw.WriteLine();
            }
            sw.Flush();
            sw.Close();
        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Console.WriteLine();

            Task3();
            Console.WriteLine();

            Task4();
            Console.WriteLine();

            Task5();
            Console.WriteLine();

            Task6();
            Console.WriteLine();

            Task7();
            Console.ReadKey();
        }
    }
}
