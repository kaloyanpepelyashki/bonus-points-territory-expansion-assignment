using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;
using Territory_Expansion_Game.ViewModels;

namespace Territory_Expansion_Game.Views;

public partial class GameFieldGrid : UserControl
{
    public GameFieldGrid()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object? sender, System.EventArgs e)
    {
        if (DataContext is GameViewModel vm)
        {
            vm.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName is nameof(GameViewModel.BoardCols) or nameof(GameViewModel.BoardRows))
                    UpdateGrid(vm);
            };
            UpdateGrid(vm);
        }
    }

    private void UpdateGrid(GameViewModel vm)
    {
        var itemsControl = this.FindControl<ItemsControl>("GameItemsControl");
        var panel = itemsControl?.ItemsPanelRoot as UniformGrid;
        if (panel != null)
        {
            panel.Columns = vm.BoardCols;
            panel.Rows = vm.BoardRows;
        }
    }
}