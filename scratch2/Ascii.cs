using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scratch2;
internal class Ascii
{
    public static string asci = " /$$   /$$                               /$$      /$$                    \r\n| $$  | $$                              " +
         "\r| $$$    /$$$                    \r\n| $$  | $$  /$$$$$$  /$$$$$$$   /$$$$$$ | $$$$  /$$$$  /$$$$$$  \r" +
         "/$$$$$$$ \r\n| $$$$$$$$ |____  $$| $$__  $$ /$$__  $$| $$ $$/$$ $$ |____  " +
         "$$| $$__  $$\r\n| $$__  $$  /$$$$$$$| $$  \\ $$| $$  \\ $$| $$  $$$| $$  /$$$$$$$| $$  \\ $$\r\n| $$  | $$ /$$__ " +
        " $$| $$  | $$| $$  | $$| $$\\  $ | $$ /$$__  $$| $$  | $$\r\n| $$  | $$|  $$$$$$$| $$  | $$|  $$$$$$$| $$ \\/  | $$|  " +
        "$$$$$$$| $$  | $$\r\n|__/  |__/ \\_______/|__/  |__/ \\____  $$|__/     |__/ \\_______/|__/  |__/\r\n                 " +
        "              /$$  \\ $$                                 \r\n                              |  $$$$$$/                     " +
        "            \r\n                               \\______/ ";
    
     public static string[] HangAround =
        {
        "____\r\n|/   |\r\n|   \r\n|    \r\n|    \r\n|    \r\n|\r\n|_____",
        " ____\r\n|/   |\r\n|   (_)\r\n|    \r\n|    \r\n|    \r\n|\r\n|_____",
        " ____\r\n|/   |\r\n|   (_)\r\n|    |\r\n|    |    \r\n|    \r\n|\r\n|_____",
        " ____\r\n|/   |\r\n|   (_)\r\n|   \\|\r\n|    |\r\n|    \r\n|\r\n|_____",
         " ____\r\n|/   |\r\n|   (_)\r\n|   \\|/\r\n|    |\r\n|    \r\n|\r\n|_____",
         " ____\r\n|/   |\r\n|   (_)\r\n|   \\|/\r\n|    |\r\n|   / \r\n|\r\n|_____",
         " ____\r\n|/   |\r\n|   (_)\r\n|   \\|/\r\n|    |\r\n|   / \\\r\n|\r\n|_____",
        " ____\r\n|/   |\r\n|   (_)\r\n|   /|\\\r\n|    |\r\n|   | |\r\n|\r\n|_____",
        };

    /// <summary>
    /// Clears console then print yellow logo and finally resets color
    /// </summary>
    /// <param name="message"></param>
    public static void PrintHangmanLogoYellow(string message)
    {

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (char letter in message)
            Console.Write(letter);
        Console.ResetColor();
    }

    /// <summary>
    /// Clears console then print green logo and finally resets color
    /// </summary>
    /// <param name="message"></param>
    public static void PrintHangmanLogoGreen(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (char letter in message)
            Console.Write(letter);
        Console.ResetColor();

    }


    public static void printLast()
    {
        

    }
}

