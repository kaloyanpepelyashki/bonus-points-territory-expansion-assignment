using Avalonia.Controls;
using Avalonia.Interactivity;
using Territory_Expansion_Game.ViewModels;

namespace Territory_Expansion_Game.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void UpdateGridSize(int gridSize)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.GridSize = gridSize;
        }
    }
    
    private async void OpenResizePopup(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm)
            return;

        var popup = new ReSizePopUpWindow
        {
            DataContext = vm // <-- pass the SAME instance
        };

        await popup.ShowDialog(this); // modal
    }
}