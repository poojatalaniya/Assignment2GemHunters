//Assignment2-Gem Hunters
//By Pooja Talaniya - Ptalaniya4569@conestogac.on.ca - 8904569
//Beginning of the Code

using System;

//A Class is defined named Position
class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}
class Player
{
    public string Name { get; }
    public Position Position { get; set; }
    public int GemCount { get; set; }

    public Player(string name, Position position)
    {
        Name = name;
        Position = position;
        GemCount = 0;                                         
    }
    public void Move(char direction)
    {
        //switch statement for moving
        switch (direction)
        {
            case 'U':
                Position.Y--;
                break;
            case 'D':
                Position.Y++;
                break;
            case 'L':
                Position.X--;
                break;
            case 'R':
                Position.X++;
                break;
            default:
                break;
        }
    }
}
class Cell
{
    public string Occupant { get; set; }

    public Cell()
    {
        Occupant = "-";
    }
}
class Board
{
    private Cell[,] Grid { get; }
    private Random random;

    public Board()
    {
        Grid = new Cell[6, 6];
        random = new Random();

        //Initializing board with empty cells
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Grid[i, j] = new Cell();
            }
        }
        // Placing obstacles randomly
        for (int i = 0; i < 6; i++)
        {
            int obstacleX = random.Next(6);
            int obstacleY = random.Next(6);
            Grid[obstacleY, obstacleX].Occupant = "O";
        }

        // Placing gems randomly
        for (int i = 0; i < 5; i++)
        {
            int gemX = random.Next(6);
            int gemY = random.Next(6);
            Grid[gemY, gemX].Occupant = "G";
        }
    }
    public void Display(Player player1, Player player2)
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (player1.Position.X == j && player1.Position.Y == i)
                {
                    Console.Write("P1 ");
                }
                else if (player2.Position.X == j && player2.Position.Y == i)
                {
                    Console.Write("P2 ");
                }
                else
                {
                    Console.Write(Grid[i, j].Occupant + " ");
                }
            }
            Console.WriteLine();
        }
    }
    public bool IsValidMove(Player player, char direction)
    {
        int x = player.Position.X;
        int y = player.Position.Y;

        if (direction == 'U')
        {
            y--;
        }
        else if (direction == 'D')
        {
            y++;
        }
        else if (direction == 'L')
        {
            x--;
        }
        else if (direction == 'R')
        {
            x++;
        }

        if (x < 0 || x >= 6 || y < 0 || y >= 6)
        {
            return false;
        }
        if (Grid[y, x].Occupant == "O")
        {
            return false;
        }

        return true;
    }

    public void CollectGem(Player player)
    {
        int x = player.Position.X;
        int y = player.Position.Y;

        if (Grid[y, x].Occupant == "G")
        {
            player.GemCount++;
            Grid[y, x].Occupant = "-";
        }
    }
}

class Game
{
    private Board Board { get; }
    private Player Player1 { get; }
    private Player Player2 { get; }
    private Player CurrentTurn { get; set; }
    private int TotalTurns { get; set; }

    public Game()
    {
        Board = new Board();
        Player1 = new Player("P1", new Position(0, 0));
        Player2 = new Player("P2", new Position(5, 5));
        CurrentTurn = Player1;
        TotalTurns = 0;
    }

    public void Start()
    {
        while (!IsGameOver())
        {
            Board.Display(Player1, Player2);
            Console.WriteLine($"It's {CurrentTurn.Name}'s turn.");
            Console.Write("Enter direction (U/D/L/R): ");
            char direction = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if (Board.IsValidMove(CurrentTurn, direction))
            {
                CurrentTurn.Move(direction);
                Board.CollectGem(CurrentTurn);
                TotalTurns++;
                SwitchTurn();
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }

        AnnounceWinner();
    }

    private void SwitchTurn()
    {
        CurrentTurn = (CurrentTurn == Player1) ? Player2 : Player1;
    }

    private bool IsGameOver()
    {
        return TotalTurns >= 30;
    }

    private void AnnounceWinner()
    {
        if (Player1.GemCount > Player2.GemCount)
        {
            Console.WriteLine("Player 1 wins!");
        }
        else if (Player1.GemCount < Player2.GemCount)
        {
            Console.WriteLine("Player 2 wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}

