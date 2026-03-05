using System.Collections.ObjectModel;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Territory_Expansion_Game.Models;
    using Territory_Expansion_Game.Services;
    
    namespace Territory_Expansion_Game.ViewModels;
    
    public partial class GameViewModel : ViewModelBase
    {
        private readonly GameService _gameService;
        private GameState? _gameState;
    
        [ObservableProperty]
        private ObservableCollection<CellViewModel> _cells = new();
    
        [ObservableProperty]
        private int _boardRows = 6;
    
        [ObservableProperty]
        private int _boardCols = 6;
    
        [ObservableProperty]
        private string _statusMessage = "Press Play to start a new game.";
    
        [ObservableProperty]
        private bool _isGameActive;
    
        public GameViewModel()
        {
            _gameService = new GameService();
        }
    
        [RelayCommand]
        private void PlayGame()
        {
            _gameState = _gameService.CreateNewGame(BoardRows, BoardCols);
            IsGameActive = true;
            RebuildCells();
            UpdateStatus();
        }
    
        [RelayCommand]
        private void SaveGame()
        {
            if (_gameState == null)
            {
                StatusMessage = "No active game to save.";
                return;
            }
            _gameService.SaveGame(_gameState);
            StatusMessage = "Game saved successfully.";
        }
    
        [RelayCommand]
        private void LoadGame()
        {
            var loaded = _gameService.LoadGame();
            if (loaded == null)
            {
                StatusMessage = "No save file found or file is invalid.";
                return;
            }
    
            _gameState = loaded;
            BoardRows = _gameState.Rows;
            BoardCols = _gameState.Cols;
            IsGameActive = !_gameState.IsGameOver;
            RebuildCells();
            UpdateStatus();
        }
    
        private void OnCellClicked(CellViewModel cellVm)
        {
            if (_gameState == null || _gameState.IsGameOver)
                return;
    
            if (!_gameService.IsMoveValid(_gameState, cellVm.Row, cellVm.Col))
                return;
    
            _gameService.ApplyMove(_gameState, cellVm.Row, cellVm.Col);
            RefreshCells();
            UpdateStatus();
        }
    
        private void RebuildCells()
        {
            Cells.Clear();
    
            if (_gameState == null)
                return;
    
            for (int r = 0; r < _gameState.Rows; r++)
            {
                for (int c = 0; c < _gameState.Cols; c++)
                {
                    var cell = new CellViewModel(r, c)
                    {
                        Owner = _gameState.Board[r, c],
                        OnCellClicked = OnCellClicked
                    };
                    Cells.Add(cell);
                }
            }
    
            HighlightLegalMoves();
        }
    
        private void RefreshCells()
        {
            if (_gameState == null) return;
    
            foreach (var cell in Cells)
            {
                cell.Owner = _gameState.Board[cell.Row, cell.Col];
            }
    
            HighlightLegalMoves();
        }
    
        private void HighlightLegalMoves()
        {
            if (_gameState == null) return;
    
            foreach (var cell in Cells)
            {
                cell.IsLegalMove = !_gameState.IsGameOver &&
                                   _gameService.IsMoveValid(_gameState, cell.Row, cell.Col);
            }
        }
    
        private void UpdateStatus()
        {
            if (_gameState == null) return;
    
            if (_gameState.IsGameOver)
            {
                IsGameActive = false;
                StatusMessage = _gameState.Winner == 0
                    ? "Game Over - It's a Draw!"
                    : $"Game Over - Player {(_gameState.Winner == 1 ? "Blue" : "Red")} Wins!";
            }
            else
            {
                StatusMessage = $"Player {(_gameState.CurrentPlayer == 1 ? "Blue" : "Red")}'s Turn";
            }
        }
    }