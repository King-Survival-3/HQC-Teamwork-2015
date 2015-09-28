namespace KingSurvival.Web.Helpers
{
    using System;

    using KingSurvival.Models;
    using KingSurvival.Models.Patterns;

    public static class MoveHelper
    {
        public static Position ParceMove(string move)
        {
            int col = move[0] - 'a';
            int row = ((int)Char.GetNumericValue(move[1]) - 1);

            var factory = new PositionFactory();
            return factory.Create(row, col); // return new Position(row, col); 
        }
    }
}