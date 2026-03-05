using Avalonia;
    using Avalonia.Controls;
    using Territory_Expansion_Game.ViewModels;
    
    namespace Territory_Expansion_Game.Views;
    
    public partial class GameFieldGrid : UserControl
    {
        public static readonly StyledProperty<int> GridSizeProperty =
            AvaloniaProperty.Register<GameFieldGrid, int>(nameof(GridSize), 6);
    
        public static readonly StyledProperty<GameViewModel?> GameViewModelProperty =
            AvaloniaProperty.Register<GameFieldGrid, GameViewModel?>(nameof(GameViewModel));
    
        public int GridSize
        {
            get => GetValue(GridSizeProperty);
            set => SetValue(GridSizeProperty, value);
        }
    
        public GameViewModel? GameViewModel
        {
            get => GetValue(GameViewModelProperty);
            set => SetValue(GameViewModelProperty, value);
        }
    
        public GameFieldGrid()
        {
            InitializeComponent();
        }
    }