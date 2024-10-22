using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scratch2;
internal class Ascii
{
    string LogoHangMan { get; } = " /$$   /$$                               /$$      /$$                    \r\n| $$  | $$                              " +
         "\r| $$$    /$$$                    \r\n| $$  | $$  /$$$$$$  /$$$$$$$   /$$$$$$ | $$$$  /$$$$  /$$$$$$  \r" +
         "/$$$$$$$ \r\n| $$$$$$$$ |____  $$| $$__  $$ /$$__  $$| $$ $$/$$ $$ |____  " +
         "$$| $$__  $$\r\n| $$__  $$  /$$$$$$$| $$  \\ $$| $$  \\ $$| $$  $$$| $$  /$$$$$$$| $$  \\ $$\r\n| $$  | $$ /$$__ " +
        " $$| $$  | $$| $$  | $$| $$\\  $ | $$ /$$__  $$| $$  | $$\r\n| $$  | $$|  $$$$$$$| $$  | $$|  $$$$$$$| $$ \\/  | $$|  " +
        "$$$$$$$| $$  | $$\r\n|__/  |__/ \\_______/|__/  |__/ \\____  $$|__/     |__/ \\_______/|__/  |__/\r\n                 " +
        "              /$$  \\ $$                                 \r\n                              |  $$$$$$/                     " +
        "            \r\n                               \\______/ ";

     string[] _hangAroundPic =
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

    public string[] HangAroundPic 
    {
        get {  return _hangAroundPic; }
        
    }


    /// <summary>
    /// Clears console then print yellow logo and finally resets color
    /// </summary>
    /// <param name="asci"></param>
    public void PrintHangmanLogoYellow()
    {

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (char letter in LogoHangMan)
            Console.Write(letter);
        Console.ResetColor();
    }

    /// <summary>
    /// Clears console then print green logo and finally resets color
    /// </summary>
    /// <param name="asci"></param>
    public void PrintHangmanLogoGreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (char letter in LogoHangMan)
            Console.Write(letter);
        Console.ResetColor();

    }
}

