namespace KingSurvival.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using KingSurvival.Data;
    using KingSurvival.Models;
    using Microsoft.AspNet.Identity;

    [Authorize]
    public class GamesController : ApiController
    {
        private IKingSurvivalData data;

        public GamesController()
            : this(new KingSurvivalData(new KingSurvivalDbContext()))
        {
        }

        public GamesController(IKingSurvivalData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult GetUsersCount()
        {
            var count = this.data.Users.All().Count();
            return this.Ok(count);
        }

        [HttpPost]
        [ActionName("Create")]
        public IHttpActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            var game = new Game
            {
                FirstPlayerId = userId,
                FirstPlayerUserName = userName
            };

            this.data.Game.Add(game);
            this.data.SaveChanges();

            var gameState = this.data.Game.All()
              .Where(x => x.Id == game.Id)
              .Select(x => new { Id = x.Id, PlayerFigure = GameState.TurnKing, playerId = x.FirstPlayerId, gameState = x.State })
              .FirstOrDefault();

            return this.Ok(gameState);
        }

        [HttpPost]
        [ActionName("Join")]
        public IHttpActionResult Join(string gameId)
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();

            var game = this.data.Game.All()
                .FirstOrDefault(x => x.Id.ToString() == gameId);

            if (game == null)
            {
                return this.BadRequest();
            }

            game.SecondPlayerId = userId;
            game.SecondPlayerUserName = userName;
            game.State = GameState.TurnKing;
            this.data.SaveChanges();

            var gameState = this.data.Game.All()
                .Where(x => x.Id.ToString() == gameId)
                .Select(x => new { Id = x.Id, PlayerFigure = GameState.TurnPown, playerId = x.SecondPlayerId, gameState = x.State })
                .FirstOrDefault();

            return this.Ok(gameState);
        }

        [HttpGet]
        [ActionName("ActiveGames")]
        public IHttpActionResult ActiveGames()
        {
            var games = this.data.Game.All()
                .Where(x => x.State == GameState.WaitingForSecondPlayer);

            return this.Ok(games);
        }
    }
}