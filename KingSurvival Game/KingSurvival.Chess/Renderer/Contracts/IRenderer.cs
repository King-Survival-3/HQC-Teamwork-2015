namespace KingSurvival.Chess.Renderer.Contracts
{
    using KingSurvival.Chess.Board.Contracts;

    public interface IRenderer
    {
        void RenderMainMenu();

        void RenderBoard(IBoard board);

        void PrintErrorMessage(string errorMessage);
    }
}
