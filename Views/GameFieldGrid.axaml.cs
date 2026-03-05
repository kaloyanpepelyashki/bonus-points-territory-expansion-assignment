using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Territory_Expansion_Game.Views;

public partial class GameFieldGrid : UserControl
{
    public static readonly StyledProperty<int> GridSizeProperty =
        AvaloniaProperty.Register<GameFieldGrid, int>(nameof(GridSize), 6);

    public int GridSize
    {
        get => GetValue(GridSizeProperty);
        set => SetValue(GridSizeProperty, value);
    }

    public GameFieldGrid()
    {
        InitializeComponent();
        GenerateGrid(GridSize); 
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == GridSizeProperty)
        {
            GenerateGrid((int)change.NewValue!);
        }
    }
    
    private void GenerateGrid(int size)
    {
        GameGrid.Children.Clear();
        GameGrid.RowDefinitions.Clear();
        GameGrid.ColumnDefinitions.Clear();

        for (int r = 0; r < size; r++)
            GameGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));

        for (int c = 0; c < size; c++)
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

        for (int r = 0; r < size; r++)
        for (int c = 0; c < size; c++)
        {
            var cell = new Border
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Black,
                Background = ((r + c) % 2 == 0) ? Brushes.LightGray : Brushes.White
            };

            Grid.SetRow(cell, r);
            Grid.SetColumn(cell, c);
            GameGrid.Children.Add(cell);
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