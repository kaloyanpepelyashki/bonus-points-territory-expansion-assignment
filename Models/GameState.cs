namespace Territory_Expansion_Game.Models;
    
public class GameState
{
    public int Rows { get; set; }
    public int Cols { get; set; }
    public int[,] Board { get; set; }
    public int CurrentPlayer { get; set; } // 1 = Blue, 2 = Red
    public bool IsGameOver { get; set; }
    public int Winner { get; set; } // 0 = Draw, 1 = Blue, 2 = Red

    public GameState(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        Board = new int[rows, cols];
        CurrentPlayer = 1;
        IsGameOver = false;
        Winner = 0;
    }
}