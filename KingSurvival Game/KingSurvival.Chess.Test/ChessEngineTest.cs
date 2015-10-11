namespace Renderer.Test.Mock
{
    using KingSurvival.Chess.Common;
    using KingSurvival.Chess.Figures.Contracts;
    using KingSurvival.Chess.Players;
    using KingSurvival.Chess.Players.Contracts;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class MoqPlayer
    {
        [TestMethod]
        public void CheckPlayerProperties()
        {
            var firstPlayer = new Player("Pesho", ChessColor.White);            
            var moqedplayers = new Mock<IPlayer>();
            moqedplayers.Setup(p => p.AddFigure(It.IsAny<IFigure>())).Verifiable();
            moqedplayers.Setup(p => p.Name).Returns(firstPlayer.Name);
            moqedplayers.Setup(p => p.Color).Returns(firstPlayer.Color);
        }
    }
}
