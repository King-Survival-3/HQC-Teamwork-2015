namespace KingSurvival.Web.Helpers
{
    using System;

    using KingSurvival.Models;

    public static class MoveHelper
    {
        public static Position ParceMove(string move)
        {
            var col = move[0] - 'a';
            int row = (int)char.GetNumericValue(move[1]) - 1;

            return new Position(row, col);
        }
    }
}