namespace Territory_Expansion_Game.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public GameViewModel GameViewModel { get; } = new GameViewModel();
}