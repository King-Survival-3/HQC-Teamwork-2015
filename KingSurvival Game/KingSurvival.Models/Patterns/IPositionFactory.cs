using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingSurvival.Models.Patterns
{
    public interface IPositionFactory
    {
        Position Create(int row, int col);
    }
}
