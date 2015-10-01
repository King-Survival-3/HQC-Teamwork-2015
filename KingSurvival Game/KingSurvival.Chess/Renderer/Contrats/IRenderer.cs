namespace KingSurvival.Chess.Renderer.Contrats
{
    using KingSurvival.Chess.Board.Contracts;

    public interface IRenderer
    {
        void RenderMainMenu();

        void RenderBoard(IBoard board);

        void PrintErrorMessage(string errorMessage);
    }
}
