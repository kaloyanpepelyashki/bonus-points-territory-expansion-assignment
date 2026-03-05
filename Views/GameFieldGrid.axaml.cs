using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Territory_Expansion_Game.Views;

public partial class GameFieldGrid : UserControl
{
    public GameFieldGrid()
    {
        InitializeComponent();
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        int size = 9;

        // Creats rows
        for (int r = 0; r < size; r++)
        {
            GameGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        }

        // Creates  columns for the grid
        for (int c = 0; c < size; c++)
        {
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        // Generates the   cell of the grid
        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {
                var cell = new Border
                {
                    Background = GetCellColor(r, c),
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1)
                };

                Grid.SetRow(cell, r);
                Grid.SetColumn(cell, c);

                GameGrid.Children.Add(cell);
            }
        }
    }

    private IBrush GetCellColor(int row, int col)
    {
        // simple alternating pattern so cells are easy to see
        if ((row + col) % 2 == 0)
            return Brushes.LightGray;
        else
            return Brushes.White;
    }
}