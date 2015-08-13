namespace KingSurvival.Models
{
    public enum GameState
    {
        WaitingForSecondPlayer = 0,
        TurnKing = 1,
        TurnPown = 2,
        GameWonByKing = 3,
        GameWonByPown = 4,
    }
}
