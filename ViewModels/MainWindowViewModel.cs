using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace Territory_Expansion_Game.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public GameViewModel GameViewModel { get; } = new GameViewModel();

    public List<int> BoardSizeOptions { get; } = new List<int> { 4, 6, 8, 10, 12 };

    [ObservableProperty]
    private int _gridSize = 6;

    partial void OnGridSizeChanged(int value)
    {
        GameViewModel.BoardRows = value;
        GameViewModel.BoardCols = value;
    }
}