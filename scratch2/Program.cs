using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace scratch2;

internal class Program
{
    static List<Tuple<string, double>> highscore = new List<Tuple<string, double>>();
    static double correctGuesses;
    static string[] MenuText;
    static string chosenWord = string.Empty;
    static char[] wordShowedForPlayer;
    static PlayerInfo player;
    static string jsonHighscoreFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Highscore.json");
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
        catch (FileNotFoundException)
        {

        }
        catch (Exception)
        {

            throw;
        }

    }

    private static void LoadHighscore()
    {
        string fromJson = File.ReadAllText(jsonHighscoreFilePath);
        try
        {
            highscore = JsonSerializer.Deserialize<List<Tuple<string, double>>>(fromJson)!;

        }
        catch (FileNotFoundException)
        {

            throw new FileNotFoundException(fromJson);
        }
    }

    private static void Rungame()
    {
        List<char> checkForGuessedLetter = new List<char>();
        Ascii.PrintHangmanLogoGreen(Ascii.asci);

        for (int i = 0; i < Ascii.HangAround.Length; i++)
        {
            char guessedletter;

            while (!wordShowedForPlayer.SequenceEqual(chosenWord))
            {
                ClearLine();
                Array.ForEach(wordShowedForPlayer, c => Console.Write(c));

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

                player.totalTries++;
                ClearLine();

                for (int j = 0; j < chosenWord.Length; j++)
                {
                    if (chosenWord[j] == guessedletter)
                    {
                        wordShowedForPlayer[j] = guessedletter;
                        correctGuesses++;

                    }

                }
                if (!chosenWord.Contains(guessedletter))
                    break;

            }
            if (wordShowedForPlayer.SequenceEqual(chosenWord))
            {
                PlayerWin();
            }

            ClearLineBottomPic();
            Console.Write(Ascii.HangAround[i]);
        }
        PlayerLose();
    }

    private static void ClearLineBottomPic()
    {
        Console.SetCursorPosition(1, 20);
        //Console.Write(new string(' ', Console.BufferWidth));
        //Console.SetCursorPosition(28, 20);
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

    private static void PlayerLose()
    {
        throw new NotImplementedException();
    }

    private static void PlayerWin()
    {
        Console.Clear();
        Ascii.PrintHangmanLogoGreen(Ascii.asci);

        double finalPercentage = correctGuesses / player.totalTries * 100; // Uträkning för hur stor andel rätt spelaren hade i förhållande till antal gissningar i % 



        highscore.Add(Tuple.Create(player.Name, finalPercentage));

        highscore = highscore.OrderByDescending(highscore => highscore.Item2).ToList(); //Sorterar Tuple listan efter högst procent

        string toJson = JsonSerializer.Serialize(highscore);

        File.WriteAllText(jsonHighscoreFilePath, toJson);


        ClearLine();
        Console.Write($"Congrats {player.Name}, you had a guessratio of {finalPercentage}");
        ClearLine2();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Press any button.....");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Highscore");

        PrintHighscore();

        Console.ReadKey(true);
        Main(new string[0]);



    }

    private static void CreatePlayer()
    {
        Ascii.PrintHangmanLogoGreen(Ascii.asci);

        Console.CursorVisible = false;
        Console.SetCursorPosition(28, 15);


        Console.Write("Enter players name: ");
        string? input = Console.ReadLine();
        while (input.Length < 2)
        {
            Console.WriteLine("Players name must be atleast 2 letters");
            input = Console.ReadLine();
        }
        player = new PlayerInfo(input);
    }

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

        }
        wordShowedForPlayer = new char[chosenWord.Length];
        Array.Fill(wordShowedForPlayer, '_');
    }

    static int WelcomeScreen()
    {
        Ascii.PrintHangmanLogoYellow(Ascii.asci);

        MenuText = ["1player", "2player", "Highscore", "Exit"];
        const int cursorXpos = 20;
        const int cursorYpos = 14;

        ConsoleKey key;

        int currentChoice = 0;

        Console.CursorVisible = false;
        do
        {

            for (int i = 0; i < MenuText.Length; i++)
            {
                //i==0 ? Console.SetCursorPosition(choice1Xpos, choice1Ypos) : Console.SetCursorPosition(choice1Xpos + (i * choice1Xpos), choice1Ypos);

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
                    if (currentChoice == 3)
                        key = ConsoleKey.Escape;
                    else if (currentChoice == 2)
                        PrintHighscore();
                    else
                        return currentChoice;
                    break;
                case ConsoleKey.Escape:
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nAnvänd bara pilar och enter för att välja");
                    break;
            }
        } while (key != ConsoleKey.Escape);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Vill du verkligen avsluta spelet?\n[y][n]");
        key = Console.ReadKey(true).Key;
        while (true)
        {
            if (key == ConsoleKey.Y)
                Environment.Exit(0);
            else if (key == ConsoleKey.N)
            {
                Console.Clear();
                WelcomeScreen();
            }
            Console.WriteLine("fel val välj igen");
            Thread.Sleep(2000);
        }
    }

    private static void PrintHighscore()
    {
        foreach (var score in highscore)
        {
            Console.WriteLine($"- {score.Item1} -- {score.Item2} -");
        }
    }
}
