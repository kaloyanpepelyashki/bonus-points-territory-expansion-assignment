using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Territory_Expansion_Game.ViewModels;

namespace Territory_Expansion_Game.Views;

public partial class ReSizePopUpWindow : Window
{
    public ReSizePopUpWindow()
    {
        InitializeComponent();
    }

    private void ClosePopup(object? sender, RoutedEventArgs e) => Close();
    
    private void GridSize6(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
            vm.GridSize = 6;
    }

    private void GridSize9(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
            vm.GridSize = 9;
    }

    private void GridSize12(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
            vm.GridSize = 12;
    }
}