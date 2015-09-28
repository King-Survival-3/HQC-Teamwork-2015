
namespace KingSurvival.Models.Patterns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PositionFactory : IPositionFactory
    {
        public Position Create(int row, int col)
        {
            return new Position(row, col);
        }
    }
}
