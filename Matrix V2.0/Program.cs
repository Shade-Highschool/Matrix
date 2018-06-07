using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace Matrix_V2._0
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);  // K maximize je třeba DLL

        static int height;
        static int width;
        static int[] y;
        static Random rand = new Random();
        static char AsciiChar { get { return (char)rand.Next(32, 126); } } //Vlastnost vrací random písmeno, znak nebo číslo  (asci kód převede na char)

        static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);   //3 k maximilizaci   || 6 je třeba k minimalizaci
        }
        static void StartingMessage()
    {
            Console.ForegroundColor = ConsoleColor.Green;
            string message = "Follow the white rabbit.";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (message.Length / 2), (Console.WindowHeight / 2) - 1); //Střed

            for(int i = 0;i<message.Length;i++)
            {
                Thread.Sleep(100);
                Console.Write(message[i]);
            } //Pomalu vypíše text
    }


        static void Main(string[] args)
        {
            StartingMessage();
            Console.ReadKey(true); 
            Console.Clear();//start

            Maximize();
            Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth;
            height = Console.WindowHeight;
            width = Console.WindowWidth;  // vzadu se bugovali o jeden sloupec písmenka
            //Maximalizuje, nastaví velikosti   || bufferheight | width nastaví "aby se nedalo scrollovat."

            Initialize();

            while (true)
                UpdateColumns();

           /* if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                    Environment.Exit(0); //Ukončí
                if (key == ConsoleKey.F5)
                { Console.ReadKey(true); } //Pauzne. Prostě block  | true znamená, nezobrazuj tu klávesu, kterou jsi stisknul
                if (key == ConsoleKey.F1) //Reset
                    Initialize();
            }*/


        }

        static void Initialize()
        {
            Console.Clear();
            y = new int[width]; //Sloupce

            for (int x = 0; x < width; x++)
            {
                y[x] = rand.Next(height);
            } //Naplní sloupce random y hodnoty   = startovní hodnoty

        }
        static void UpdateColumns()
        {
                for (int x = 0; x < width; ++x)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(x, HandleYpozition(y[x]));
                    Console.Write(AsciiChar);
                    //Najde start pozici, zapíše random asciZnak v darkGreen barvě

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(x, HandleYpozition(y[x] - 2));
                    Console.Write(AsciiChar);
                    //zapíše znak v green barvě na zákl pozici o dvě nad

                    Console.SetCursorPosition(x, HandleYpozition(y[x] - 20));
                    Console.Write(' ');
                    //Mezera pro efekt, aby to nebylo celé zaplněné

                    y[x] = HandleYpozition(y[x] + 1); //Zákl pozice se o jedno posuno dolů
                }
        }

        static int HandleYpozition(int yPozice)
        {
            if (yPozice < 0)
                return yPozice + height; //Otočí
            else if (yPozice < height)
                return yPozice;
            else
                return 0; //Když je nad, začni od zdola
        }
    }
}
