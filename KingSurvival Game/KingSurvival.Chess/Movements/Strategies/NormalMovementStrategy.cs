﻿namespace KingSurvival.Chess.Movements.Strategies
{
    using System.Collections.Generic;

    using KingSurvival.Chess.Movements.Contracts;

    public class NormalMovementStrategy : IMovementStrategy
    {
        private readonly IDictionary<string, IList<IMovement>> movements = new Dictionary<string, IList<IMovement>>()
        {
            { "Pawn", new List<IMovement>()
                        {
                            new NormalPawnMovement()
                        }
            },
            { "King", new List<IMovement>()
                        {
                            new NormalKingMovement()
                        }
            },
            { "Bishop", new List<IMovement>()
                        {
                            new NormalBishopMovement()
                        }
            },
        };

        public IList<IMovement> GetMovements(string figure)
        {
            return this.movements[figure];
        }
    }
}
