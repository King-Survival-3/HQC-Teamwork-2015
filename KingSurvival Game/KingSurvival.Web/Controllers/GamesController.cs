namespace KingSurvival.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using KingSurvival.Data;
    using KingSurvival.Models;

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
            return Ok(count);
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

            return Ok(game);
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

            return Ok(game);
        }

        [HttpGet]
        [ActionName("ActiveGames")]
        public IHttpActionResult ActiveGames()
        {
            var games = this.data.Game.All();

            return Ok(games);
        }
    }
}