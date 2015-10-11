namespace KingSurvival.Chess.Renderer
{
    using KingSurvival.Chess.Renderer.Contracts;

    public class WebRenderer : IRenderer
    {
        //private global::Microsoft.AspNet.SignalR.IGroupManager globalMicrosoftAspNetSignalRIGroupManager;

        //public WebRenderer(global::Microsoft.AspNet.SignalR.IGroupManager globalMicrosoftAspNetSignalRIGroupManager)
        //{
        //    // TODO: Complete member initialization
        //    this.globalMicrosoftAspNetSignalRIGroupManager = globalMicrosoftAspNetSignalRIGroupManager;
        //}
        public void RenderMainMenu()
        {
            throw new System.NotImplementedException();
        }

        public void RenderBoard(Board.Contracts.IBoard board)
        {
            throw new System.NotImplementedException();
        }

        public void PrintErrorMessage(string errorMessage)
        {
            throw new System.NotImplementedException();
        }

        public void RenderWinningScreen(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
