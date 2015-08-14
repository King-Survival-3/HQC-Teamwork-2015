using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        struct Coordinate {
            public int row;
            public int col;
            
            public Coordinate(int row, int col) {
                this.row = row;
                this.col = col;
            }
        }
        static void Main(string[] args)
        {
            Coordinate A = new Coordinate(0, 0);
            Coordinate B = new Coordinate(0, 2);
            Coordinate C = new Coordinate(0, 4);
            Coordinate D = new Coordinate(0, 6);


            Coordinate K = new Coordinate(7, 3);
            bool endGame = false;
            int currentTurn = 1;
            do {
                bool validMove; 
                do {
                    System.Console.Clear();
                    PrintBoard(A, B, C, D, K);

                    validMove = isMoveLeft(currentTurn, ref A, ref B, ref C, ref D, ref K);
                } while (!validMove);

                endGame = proverka2(currentTurn, A, B, C, D, K);
                currentTurn++;


            } while (!endGame);
        }

        private static bool proverka2(int turn, Coordinate A, Coordinate B, Coordinate C, Coordinate D, Coordinate K)
        {
            if (turn % 2 == 1)
            {
                if (K.row == 0)
                {
                    System.Console.Clear();
                    PrintBoard(A, B, C, D, K);
                    Console.WriteLine("King wins in {0} turns.", turn / 2 + 1);
                    return true;
                }
                else return false;
                

            }
            else
            {
                bool KUL = true;
                bool KUR = true; // yup, it's a boy
                bool KDL = true;
                bool KDR = true;

                if (K.row == 0)
                {
					// tuka carya e na hod
                    KUL = false;
                    KUR = false;
                }
                else if (K.row == 7)
                {
                    KDL = false;
                    KDR = false;
                }

                if (K.col == 0)
                {
                    KUL = false;
                    KDL = false;
                }
                else if (K.col == 7)
                {
                    KUR = false; // kur v gyzaaaaa, oh boli!
                    KDR = false;
                }

                if (proverka(K.row - 1, K.col - 1, A, B, C, D))
                {
                    KUL = false;
                }
                if (proverka(K.row - 1, K.col + 1, A, B, C, D))
                {
                    KUR = false; // castration... nasty
                }
                if (proverka(K.row + 1, K.col - 1, A, B, C, D))
                {
                   KDL = false;
                } 
                if (proverka(K.row + 1, K.col + 1, A, B, C, D))
                {
                    KDR = false;
                }

                if (!KDR && !KDL && !KUL && !KUR)
                {
                    System.Console.Clear();
                    PrintBoard(A, B, C, D, K);
                    Console.WriteLine("King loses.");
                    return true;
                }

                if (!proverka1(A, B, C, D, K) && !proverka1(B, A, C, D, K) && !proverka1(C, A, B, D, K) && !proverka1(D, A, B, C, K))
                {
                    System.Console.Clear();
                    PrintBoard(A, B, C, D, K);
                    Console.WriteLine("King wins in {0} turns.", turn / 2);
                    return true;
                }

                return false;
            }
        }

        private static bool proverka1(Coordinate pawn, Coordinate obstacle1, Coordinate obstacle2, Coordinate obstacle3, Coordinate obstacle4)
        {
            if (pawn.row == 7)
            {
                return false;
            }
            else if (pawn.col > 0 && pawn.col < 7)
            {
                if (proverka(pawn.row + 1, pawn.col + 1, obstacle1, obstacle2, obstacle3, obstacle4) &&

                    proverka(
					pawn.row + 1, 
					pawn.col - 1, 
					obstacle1, 
					obstacle2, 
					obstacle3, 
					obstacle4)) return false;


            }
            else if (pawn.col == 0)
            {
                if (proverka(pawn.row + 1, pawn.col + 1, obstacle1, obstacle2, obstacle3, obstacle4))
                {
                    return false;
                }
            }
            else if (pawn.col == 4+3)
            {
                if (proverka(pawn.row + 1, pawn.col - 1, obstacle1, obstacle2, obstacle3, obstacle4))
                {
                    return false;


                }
            }
            return true;
        }

        private static bool isMoveLeft(int turn, ref Coordinate A, ref Coordinate B, ref Coordinate C, ref Coordinate D, ref Coordinate K)
        {
            if (turn % 2 == 1)
            {
                Console.Write("King’s turn: ");
                string move = Console.ReadLine();
                switch (move)
                {
                    case "KUL":
                        if (K.col > 0 && K.row > 0 && !proverka(K.row - 1, K.col - 1, A, B, C, D))
                        {
                            K.col--;
                            K.row--;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "KUR": // if KUR... gotta love these moments
                        if (K.col < 7 && K.row > 0 && !proverka(K.row - 1, K.col + 1, A, B, C, D))
                        {
                            K.col++;
                            K.row--;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "KDL":
                        if (K.col > 0 && K.row < 7 && !proverka(K.row + 1, K.col - 1, A, B, C, D))
                        {
                            K.col--;
                            K.row++;
                        }
                        else
                        {


                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "KDR":


                        if (K.col < 7 && K.row < 7 && !proverka(K.row + 1, K.col + 1, A, B, C, D))
                        {
                            K.col++;
                            K.row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");


                            Console.ReadKey();
                            return false;
                        }
                        break;
                    default:
                        Console.Write("Illegal move!");
                        Console.ReadKey();
                        return false;
                }
            }
            else
            {
                Console.Write("Pawns’ turn: ");


                string move = Console.ReadLine();
                switch (move)
                {
                    case "ADL":
                        if (A.col > 0 && A.row < 7 && !proverka(A.row + 1, A.col - 1, K, B, C, D))
                        {
                            A.col--;
                            A.row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "ADR":
                        if (A.col < 7 && A.row < 7 && !proverka(A.row + 1, A.col + 1, K, B, C, D))
                        {
                            A.col++;
                            A.row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "BDL":
                        if (B.col > 0 && B.row < 7 && 
							
							!proverka(B.row + 1, B.col - 1, A, K, C, D))
                        {
                            B.col--;

                            B.row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "BDR":
                        if (B.col < 7 && B.row < 7 && !proverka(B.row + 1, B.col + 1, A, K, C, D))
                        {
                            B.col++;
                            B.row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "CDL":
                        if (C.col > 0 && C.row < 7 && !proverka(C.row + 1, C.col + 1, A, B, K, D))
                        {
                            C.col--;
                            C.row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "CDR":
                        if (C.col < 7 && C.row < 7 && !proverka(C.row + 1, C.col + 1, A, B, K, D))
                        {
                            C.col++;
                            C.row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "DDL":
                        if (D.col > 0 && D.row < 7 && !proverka(D.row + 1, D.col - 1, A, B, C, K))
                        {
                            D.col--;
                            D.row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    case "DDR":
                        if (D.col < 7 && D.row < 7 && !proverka(D.row + 1, D.col + 1, A, B, C, K))
                        { 


                            D.col++;
                            D.row++;
                        }
                        else
                        {
                            Console.Write("Illegal move!");
                            Console.ReadKey();
                            return false;
                        }
                        break;
                    default:
                        Console.Write("Illegal move!");
                        Console.ReadKey();


                        return false;
                }
            }          

            return true;
        }

        private static bool proverka(int notOverlapedRow, int notOverlapedColumn, Coordinate overlap1, Coordinate overlap2, Coordinate overlap3, Coordinate overlap4)
        {
            if (notOverlapedRow == overlap1.row && notOverlapedColumn == overlap1.col) return true;
				else if (notOverlapedRow == overlap2.row && notOverlapedColumn == overlap2.col) return true;
				     else if (notOverlapedRow == overlap3.row && notOverlapedColumn == overlap3.col) return true;
				          else if (notOverlapedRow == overlap4.row && notOverlapedColumn == overlap4.col) return true;
							   else       
                return false;

      
        }

        private static void PrintBoard(Coordinate A, Coordinate B, Coordinate C, Coordinate D, Coordinate K)
        {
            int row = 0;
            for (int i = 0; i < 19; i++)
            {
                if (i > 3)
                {
                    if (i % 2 == 0)
                    {                   
						// ostaviame interval sled chisloto
						Console.Write("{0} ", row++);
                    }
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
            Console.Write("   ");
            for (int i = 0; i < 17; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                Console.Write("{0} | ", i);

                for (int j = 0; j < 8; j++)
                {
                    char symbol;


                    Find(A, B, C, D, K, i, j, out symbol);

                    Console.Write(symbol + " ");
                }
                Console.WriteLine('|');
            }

            Console.Write("   ");
            for (int i = 0; i < 17; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
        }

        private static void Find(Coordinate A, Coordinate B, Coordinate C, Coordinate D, Coordinate K, int i, int j, out char symbol)
        {
            if (A.row == i && A.col == j)
            {
                symbol = 'A';
            }
            else if (B.row == i && B.col == j)
            {
                symbol = 'B';
            }
            else if (C.row == i && C.col == j)
            {
                symbol = 'C';
            }
            else if (D.row == i && D.col == j)
            {
                symbol = 'D';
            }
            else if (K.row == i && K.col == j)
            {
                symbol = 'K';
            }
            else if ((i + j) % 2 == 0)
            {
                symbol = '+';
            }
            else
            {
                symbol = '-';
            }
        }
    }
}
