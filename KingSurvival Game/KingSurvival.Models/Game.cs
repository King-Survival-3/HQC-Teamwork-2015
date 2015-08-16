namespace KingSurvival.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Game
    {
        public Game()
        {
            this.Id = Guid.NewGuid();
            this.Board = "p1p1p1p1/8/8/8/8/8/8/3K4";
            this.State = GameState.WaitingForSecondPlayer;
            this.CreationDate = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Board { get; set; }
         
        public GameState State { get; set; }

        [Required]
        public string FirstPlayerId { get; set; }

        public  string FirstPlayerUserName { get; set; }

        public string SecondPlayerId { get; set; }

        public string SecondPlayerUserName { get; set; }

        public DateTime CreationDate { get; set; }

    }
}
