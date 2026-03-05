using CommunityToolkit.Mvvm.ComponentModel;

namespace Territory_Expansion_Game.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    
    
    //Default Grid Size should be 6
    [ObservableProperty] 
    private int gridSize  = 6;

}