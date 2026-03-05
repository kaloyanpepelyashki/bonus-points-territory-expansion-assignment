using System;
using System.IO;
using System.Text;
using Territory_Expansion_Game.Models;

namespace Territory_Expansion_Game.Services;

public class GameService
{
    private const string SaveFilePath = "game.save.txt";

    public GameState CreateNewGame(int rows, int cols)
    {
        return new GameState(rows, cols);
    }

    public bool IsMoveValid(GameState state, int row, int col)
    {
        if (state.IsGameOver)
            return false;

        if (row < 0 || row >= state.Rows || col < 0 || col >= state.Cols)
            return false;

        if (state.Board[row, col] != 0)
            return false;

        if (!PlayerHasAny(state, state.CurrentPlayer))
            return true; // First move anywhere

        return IsAdjacentToPlayer(state, state.CurrentPlayer, row, col);
    }

    public GameState ApplyMove(GameState state, int row, int col)
    {
        if (!IsMoveValid(state, row, col))
            return state;

        state.Board[row, col] = state.CurrentPlayer;
        state.CurrentPlayer = state.CurrentPlayer == 1 ? 2 : 1;

        CheckGameOver(state);
        return state;
    }

    private void CheckGameOver(GameState state)
    {
        bool player1HasMoves = HasLegalMoves(state, 1);
        bool player2HasMoves = HasLegalMoves(state, 2);

        bool boardFull = IsBoardFull(state);

        if (boardFull)
        {
            state.IsGameOver = true;
            state.Winner = 0; // Draw
            return;
        }

        if (!player1HasMoves && !player2HasMoves)
        {
            state.IsGameOver = true;
            state.Winner = 0; // Draw
            return;
        }

        // If current player has no legal moves, the other player wins
        if (!HasLegalMoves(state, state.CurrentPlayer))
        {
            state.IsGameOver = true;
            state.Winner = state.CurrentPlayer == 1 ? 2 : 1;
        }
    }

    private bool HasLegalMoves(GameState state, int player)
    {
        // If player has no pieces, they have legal moves (can place anywhere empty)
        if (!PlayerHasAny(state, player))
        {
            for (int r = 0; r < state.Rows; r++)
                for (int c = 0; c < state.Cols; c++)
                    if (state.Board[r, c] == 0)
                        return true;
            return false;
        }

        for (int r = 0; r < state.Rows; r++)
            for (int c = 0; c < state.Cols; c++)
                if (state.Board[r, c] == 0 && IsAdjacentToPlayer(state, player, r, c))
                    return true;

        return false;
    }

    private bool IsAdjacentToPlayer(GameState state, int player, int row, int col)
    {
        for (int r = 0; r < state.Rows; r++)
            for (int c = 0; c < state.Cols; c++)
                if (state.Board[r, c] == player)
                    if (Math.Abs(r - row) <= 1 && Math.Abs(c - col) <= 1)
                        return true;
        return false;
    }

    private bool PlayerHasAny(GameState state, int player)
    {
        for (int r = 0; r < state.Rows; r++)
            for (int c = 0; c < state.Cols; c++)
                if (state.Board[r, c] == player)
                    return true;
        return false;
    }

    private bool IsBoardFull(GameState state)
    {
        for (int r = 0; r < state.Rows; r++)
            for (int c = 0; c < state.Cols; c++)
                if (state.Board[r, c] == 0)
                    return false;
        return true;
    }

    public void SaveGame(GameState state)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{state.Rows} {state.Cols}");

        var values = new System.Collections.Generic.List<string>();
        for (int r = 0; r < state.Rows; r++)
            for (int c = 0; c < state.Cols; c++)
                values.Add(state.Board[r, c].ToString());

        sb.AppendLine(string.Join(" ", values));

        // Save current player on third line
        sb.AppendLine(state.CurrentPlayer.ToString());

        File.WriteAllText(SaveFilePath, sb.ToString());
    }

    public GameState? LoadGame()
    {
        if (!File.Exists(SaveFilePath))
            return null;

        var lines = File.ReadAllLines(SaveFilePath);
        if (lines.Length < 3)
            return null;

        var dimensions = lines[0].Trim().Split(' ');
        if (dimensions.Length < 2)
            return null;

        if (!int.TryParse(dimensions[0], out int rows) || !int.TryParse(dimensions[1], out int cols))
            return null;

        var state = new GameState(rows, cols);

        var values = lines[1].Trim().Split(' ');
        if (values.Length != rows * cols)
            return null;

        int idx = 0;
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                state.Board[r, c] = int.Parse(values[idx++]);

        if (int.TryParse(lines[2].Trim(), out int currentPlayer))
            state.CurrentPlayer = currentPlayer;

        CheckGameOver(state);
        return state;
    }
}