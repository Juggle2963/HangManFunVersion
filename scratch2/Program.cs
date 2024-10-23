using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace scratch2;

internal class Program
{
    static List<Tuple<string, double>> highscore = new List<Tuple<string, double>>();
    static double correctGuesses;
    static string[]? MenuText;
    static string chosenWord = string.Empty;
    static char[]? wordShowedForPlayer;
    static PlayerInfo? player;
    static string jsonHighscoreFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Highscore.json");
    static Ascii? graphic = new Ascii();
    static void Main(string[] args)
    {
        try
        {
            CreateWord();
            LoadHighscore();
            int playerChoice = WelcomeScreen();

            if (playerChoice == 0)
                CreatePlayer();

            Rungame();

        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"Filen kunde inte hittas{e}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Något gick fel{e}");
        }
    }

    #region Savehighscore LoadHighscore
    private static void SaveHighScore()
    {
        string toJson = JsonSerializer.Serialize(highscore);

        File.WriteAllText(jsonHighscoreFilePath, toJson);
    }
    

    private static void LoadHighscore()
    {
        try
        {
            string fromJson = File.ReadAllText(jsonHighscoreFilePath);
            highscore = JsonSerializer.Deserialize<List<Tuple<string, double>>>(fromJson)!;

        }
        catch (FileNotFoundException)
        {
            string ifNoFileJson = JsonSerializer.Serialize(highscore);

            File.WriteAllText(jsonHighscoreFilePath, ifNoFileJson);

            throw new FileNotFoundException("Could not find json file, created new file");

        }
    }
    #endregion


    #region Create word and Player
    private static void CreateWord()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Randomwords.txt");
        string[] tempArray;
        try
        {
            tempArray = File.ReadAllLines(filePath);
            chosenWord = tempArray[Random.Shared.Next(tempArray.Length)];

        }
        catch (FileNotFoundException)
        {
            tempArray = ["imperiet", "direstraits", "inflames"];
            File.WriteAllLines(filePath, tempArray);
            chosenWord = tempArray[Random.Shared.Next(tempArray.Length)];

            throw new FileNotFoundException("Could not find json file, created new file");
        }
        wordShowedForPlayer = new char[chosenWord.Length];
        Array.Fill(wordShowedForPlayer, '_');
    }
    private static void CreatePlayer()
    {
        graphic!.PrintHangmanLogoGreen();

        Console.CursorVisible = false;
        Console.SetCursorPosition(28, 15);


        Console.Write("Enter players name: ");
        string? input = Console.ReadLine();
        while (input!.Length < 2)
        {
            Console.WriteLine("Players name must be atleast 2 letters");
            input = Console.ReadLine();
        }
        player = new PlayerInfo(input);
    }
    #endregion


    #region Rungame
    private static void Rungame()
    {
        List<char> checkForGuessedLetter = new List<char>();
        graphic!.PrintHangmanLogoGreen();

        for (int i = 0; i < graphic.HangAroundPic.Length; i++)
        {
            char guessedletter;

            while (!wordShowedForPlayer!.SequenceEqual(chosenWord))
            {
                ClearLine();
                Array.ForEach(wordShowedForPlayer!, c => Console.Write(c));

                bool check = true;
                do
                {
                    check = false;

                    ClearLine2();
                    Console.Write("Guess letter: ");
                    guessedletter = Console.ReadKey().KeyChar;
                    if (checkForGuessedLetter.Contains(guessedletter))
                    {
                        ClearLine2();
                        Console.Write("you have tried that letter before - guess once more");
                        Thread.Sleep(2000);
                        check = true;
                    }
                } while (check);
                checkForGuessedLetter.Add(guessedletter);

                player!.TotalTries++;
                ClearLine();

                for (int j = 0; j < chosenWord.Length; j++)
                {
                    if (chosenWord[j] == guessedletter)
                    {
                        wordShowedForPlayer![j] = guessedletter;
                        correctGuesses++;

                    }

                }
                if (!chosenWord.Contains(guessedletter))
                    break;

            }
            if (wordShowedForPlayer!.SequenceEqual(chosenWord))
            {
                PlayerWin();
            }

            ClearLineBottomPic();
            Console.Write(graphic.HangAroundPic[i]);
        }
        PlayerLose();
    }
    #endregion


    #region Text and Picture Positions
    private static void ClearLineBottomPic()
    {
        Console.SetCursorPosition(1, 20);

        for (int i = 20; i < 26; i++)
        {
            Console.SetCursorPosition(1, i);
            Console.Write(new string(' ', Console.BufferWidth));
        }
        Console.SetCursorPosition(1, 20);

    }
    private static void ClearLine()
    {
        Console.SetCursorPosition(28, 15);
        Console.Write(new string(' ', Console.BufferWidth));
        Console.SetCursorPosition(28, 15);
    }


    private static void ClearLine2()
    {
        Console.SetCursorPosition(28, 17);
        Console.Write(new string(' ', Console.BufferWidth));
        Console.SetCursorPosition(28, 17);
    }
    #endregion


    #region PlayerWin PlayerLose
    private static void PlayerLose()
    {
        ClearLine();
        Console.Write("lost! - Do you want to play again?");
        Console.ForegroundColor = ConsoleColor.Yellow;
        ClearLine2();
        Console.Write("[y] [n]");
        ClearLineBottomPic();
        Console.Write(graphic!.HangAroundPic[graphic.HangAroundPic.Length - 1]);
        ConsoleKey key;
        key = Console.ReadKey().Key;

        while (true)
        {
            //key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Y)
                Restart();
            else if (key == ConsoleKey.N)
            {
                Environment.Exit(0);
            }
            Console.WriteLine("fel val välj igen");
            Thread.Sleep(2000);
        }

    }

    private static void PlayerWin()
    {
        Console.Clear();
        graphic!.PrintHangmanLogoGreen();

        double finalPercentage = Math.Round(correctGuesses / player!.TotalTries * 100); // Uträkning för hur stor andel rätt spelaren hade i förhållande till antal gissningar i % 


        highscore.Add(Tuple.Create(player.Name, finalPercentage));

        highscore = highscore.OrderByDescending(highscore => highscore.Item2).ToList(); //Sorterar Tuple listan efter högst procent

        if (highscore.Count > 5)
            highscore = highscore.Take(5).ToList();

        SaveHighScore();

        ClearLine();
        Console.Write($"Congrats {player.Name}, you managed to get {finalPercentage} points");
        ClearLine2();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Press any button.....");

        PrintHighscore();

        Console.ReadKey(true);

        Restart();

    }
    #endregion


    #region RestartGame
    private static void Restart()
    {
        string? pathToExe = Environment.ProcessPath;
        Process.Start(pathToExe!);

        Environment.Exit(0);
    }
    #endregion


    #region WelcomeScreen
    static int WelcomeScreen()
    {
        graphic!.PrintHangmanLogoYellow();

        MenuText = ["1player", "Reset highscore", "Highscore", "Exit"];
        const int cursorXpos = 20;
        const int cursorYpos = 14;

        ConsoleKey key;

        int currentChoice = 0;

        Console.CursorVisible = false;
        do
        {

            for (int i = 0; i < MenuText.Length; i++)
            {

                Console.SetCursorPosition(cursorXpos + (i % 2) * 15, cursorYpos + i / 2);
                if (currentChoice == i)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(MenuText[i]);
                Console.ResetColor();
            }
            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (currentChoice != 0)
                        currentChoice--;
                    break;
                case ConsoleKey.RightArrow:
                    if (currentChoice != MenuText.Length - 1)
                        currentChoice++;
                    break;
                case ConsoleKey.UpArrow:
                    if (currentChoice - 2 >= 0)
                        currentChoice -= 2;
                    break;
                case ConsoleKey.DownArrow:
                    if (currentChoice + 2 < MenuText.Length)
                        currentChoice += 2;
                    break;
                case ConsoleKey.Enter:
                    if (currentChoice == 1)
                    {
                        highscore.Clear();
                        ClearLine2();
                        Console.WriteLine("deleting highscore.....");
                        Thread.Sleep(3000);
                        ClearLine2();
                        Console.WriteLine("Higscore deleted succesfully");
                        Thread.Sleep(3000);
                        ClearLine2();
                        SaveHighScore();
                        LoadHighscore();
                        ClearLineBottomPic();
                        PrintHighscore();
                    }
                    else if (currentChoice == 2)
                    {
                        ClearLineBottomPic();
                        PrintHighscore();

                    }
                    else if (currentChoice == 3)
                        key = ConsoleKey.Escape;
                    else
                        return currentChoice;
                    break;
                case ConsoleKey.Escape:
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    ClearLine2();
                    Console.Write("Använd bara pilar och enter för att välja");
                    Thread.Sleep(3000);
                    ClearLine2();
                    break;
            }
        } while (key != ConsoleKey.Escape);
        Console.Clear();
        graphic.PrintHangmanLogoGreen();
        Console.ForegroundColor = ConsoleColor.Yellow;
        ClearLine();
        Console.WriteLine("Vill du verkligen avsluta spelet?");
        ClearLine2();
        Console.WriteLine("[y] [n]");
        while (true)
        {
            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Y)
                Environment.Exit(0);
            else if (key == ConsoleKey.N)
            {
                Restart();
            }
            Console.WriteLine("fel val välj igen");
            Thread.Sleep(2000);
        }
    }
    #endregion


    #region PrintHighScore
    private static void PrintHighscore()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Highscore");
        Console.ForegroundColor = ConsoleColor.Blue;

        foreach (var score in highscore)
        {
            var writeTable = string.Format("{0,-10} {1,4}", score.Item1, (int)score.Item2);
            Console.WriteLine(writeTable);
        }
    }
    #endregion
}
