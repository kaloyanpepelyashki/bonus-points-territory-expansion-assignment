using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace Territory_Expansion_Game.ViewModels;

public partial class CellViewModel : ViewModelBase
{
    public int Row { get; }
    public int Col { get; }

    [ObservableProperty]
    private int _owner; // 0 = None, 1 = Blue, 2 = Red

    [ObservableProperty]
    private bool _isLegalMove;

    public Action<CellViewModel>? OnCellClicked { get; set; }

    public CellViewModel(int row, int col)
    {
        Row = row;
        Col = col;
        _owner = 0;
        _isLegalMove = false;
    }

    [RelayCommand]
    private void Click()
    {
        OnCellClicked?.Invoke(this);
    }
}