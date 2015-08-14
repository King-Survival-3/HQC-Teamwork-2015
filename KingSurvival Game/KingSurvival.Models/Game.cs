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
            this.Board = string.Join("-", new string[64]);
            this.State = GameState.WaitingForSecondPlayer;
        }

        [Key]
        public Guid Id { get; set; }

        [StringLength(64)]
        [Column(TypeName = "char")]
        public string Board { get; set; }

        public GameState State { get; set; }

        [Required]
        public string FirstPlayerId { get; set; }

        public string FirstPlayerUserName { get; set; }

        public string SecondPlayerId { get; set; }

        public string SecondPlayerUserName { get; set; }
    }
}