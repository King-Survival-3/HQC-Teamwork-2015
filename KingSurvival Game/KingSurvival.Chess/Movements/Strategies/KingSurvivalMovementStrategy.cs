namespace KingSurvival.Chess.Movements.Strategies
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Movements.Contracts;

    using KingSurvival.Chess.Movements.KingSurvivalMovements;

    public class KingSurvivalMovementStrategy : IMovementStrategy
    {
        private readonly IDictionary<string, IList<IMovement>> movements = new Dictionary<string, IList<IMovement>>()
        {
            {"Pawn", new List<IMovement>()
                        {
                            new KingSurvivalPawnMovement()
                        }},
            {"King", new List<IMovement>()
                        {
                            new KingSurvivalKingMovement(),
                        }}
            
        };
        public IList<IMovement> GetMovements(string figure)
        {
            return this.movements[figure];
        }
    }
}
