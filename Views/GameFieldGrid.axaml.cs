using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Territory_Expansion_Game.Views;

public partial class GameFieldGrid : UserControl
{
    private const int Size = 6;

    private int TurnCounter = 0;

    private enum CellOwner { None, Blue, Red }
    private readonly CellOwner[,] board = new CellOwner[Size, Size];

    public GameFieldGrid()
    {
        InitializeComponent();
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        // Rows
        for (int r = 0; r < Size; r++)
            GameGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));

        // Columns
        for (int c = 0; c < Size; c++)
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

        // Cells
        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                var cell = new Border
                {
                    Background = GetCellColor(r, c),
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    Cursor = new Cursor(StandardCursorType.Hand)
                };

                // Store coordinates so the handler can know which cell was clicked
                cell.Tag = (r, c);

                cell.PointerPressed += Cell_PointerPressed;

                Grid.SetRow(cell, r);
                Grid.SetColumn(cell, c);
                GameGrid.Children.Add(cell);
            }
        }
    }

    private void Cell_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not Border cell)
            return;

        // Only respond to left-clicks (optional — remove if you want any button)
        if (!e.GetCurrentPoint(cell).Properties.IsLeftButtonPressed)
            return;

        var (r, c) = ((int, int))cell.Tag;

        // Block placing on an already taken cell
        if (board[r, c] != CellOwner.None)
            return;

        // Determine whose turn it is
        CellOwner currentPlayer = (TurnCounter % 2 == 0) ? CellOwner.Blue : CellOwner.Red;

        // Enforce the 3×3 rule ONLY if the current player already has at least one piece
        bool playerHasAny = PlayerHasAny(currentPlayer);
        if (playerHasAny && !IsMoveInside3x3Area(currentPlayer, r, c))
            return; // illegal move

        // Apply color + record state
        if (currentPlayer == CellOwner.Blue)
            cell.Background = Brushes.Blue;
        else
            cell.Background = Brushes.Red;

        board[r, c] = currentPlayer;
        TurnCounter++;
    }
    
    /// Returns true if (r,c) is within a 3×3 neighborhood (Chebyshev distance ≤ 1)
    /// of ANY cell already owned by 'player'.
    private bool IsMoveInside3x3Area(CellOwner player, int r, int c)
    {
        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (board[y, x] == player)
                {
                    if (Math.Abs(y - r) <= 1 && Math.Abs(x - c) <= 1)
                        return true;
                }
            }
        }
        return false;
    }
    
    /// Checks if the player already has any placed cells on the board.
    /// Used to allow the first move anywhere for each player.
    private bool PlayerHasAny(CellOwner player)
    {
        for (int y = 0; y < Size; y++)
            for (int x = 0; x < Size; x++)
                if (board[y, x] == player)
                    return true;
        return false;
    }

    private IBrush GetCellColor(int row, int col)
    {
        return (row + col) % 2 == 0 ? Brushes.LightGray : Brushes.White;
    }
}

