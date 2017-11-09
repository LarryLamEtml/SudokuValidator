/* ETML
 * Author : Larry Lam
 * Description : Sudoku Validator
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuValidator
{
    class Program
    {
        const int SUDOKU_SIZE = 9;
        bool isValid = true;
        static void Main(string[] args)
        {
            int[,] arraySudoku = generateDefaultSudoku();
            bool result = validateSudoku(arraySudoku);
            if (result)
            {
                Console.WriteLine("Le sudoku est juste");
            }
            else
            {
                Console.WriteLine("Le sudoku est faux");
            }
            Console.ReadLine();
        }

        /*public void generateSudoku(int _sudokuSize)
        {

            for (int i = 0; i < _sudokuSize; i++)
            {
                for (int j = 0; j < _sudokuSize; j++)
                {

                }
            }
        }*/
        public static bool validateSudoku(int[,] _ArraySudoku)
        {
            //Valeur à chercher (1->tailleX du sudoku) 
            int valueToCheck = 1;
            //index de la ligne
            int lineY = 0;
            //Index de la colonne
            int columnX = 0;
            //Variable de validation pour la valeur à chercher
            bool next = false;
            /*
            //---------------------------------Validation de chaque lignes horizontales---------------------------------\\

            //Parcours chaque ligne
            for (int i = 1; i < SUDOKU_SIZE; i++)
            {
                //Incrémente la valeur à comparer lorsqu'elle est trouvée dans la ligne 
                for (int k = 1; k <= SUDOKU_SIZE; k++)
                {
                    //Parcours chaque case de la ligne
                    for (int PosX = 0; PosX <= SUDOKU_SIZE - 1; PosX++)
                    {
                        //Si la case de la ligne vaut la valeure à comparer & qu'elle n'a pas déjà été trouvée
                        if (_ArraySudoku[lineY, PosX] == valueToCheck && next == false)
                        {
                            //La recherche de la prochaine valeur ou ligne devient possible
                            next = true;
                        }
                    }
                    //Si la valeur n'a pas été trouvée dans la ligne, quitte les boucles et retourne false
                    if (next == false)
                    {
                        return next;
                    }
                    //Reset la le check de la valeur
                    next = false;
                    //Incrémente la valeur à chercher
                    valueToCheck++;
                }
                //Lors de la fin d'une ligne, reset la valeur à chercher à 1
                valueToCheck = 1;
                //Incrémenter la ligne
                lineY++;
            }

            //---------------------------------Validation de chaque lignes Verticales---------------------------------\\
            //Parcours chaque colonne
            for (int i = 1; i < SUDOKU_SIZE; i++)
            {
                //Incrémente la valeur à comparer lorsqu'elle est trouvée dans la ligne 
                for (int k = 1; k <= SUDOKU_SIZE; k++)
                {
                    //Parcours chaque case de la colonne
                    for (int posY = 0; posY <= SUDOKU_SIZE - 1; posY++)
                    {
                        //Si la case de la colonne vaut la valeure à comparer & qu'elle n'a pas déjà été trouvée
                        if (_ArraySudoku[posY, columnX] == valueToCheck && next == false)
                        {
                            //La recherche de la prochaine valeur ou colonne devient possible
                            next = true;
                        }
                    }
                    //Si la valeur n'a pas été trouvée dans la colonne, quitte les boucles et retourne false
                    if (next == false)
                    {
                        return next;
                    }
                    //Reset la le check de la valeur
                    next = false;
                    //Incrémente la valeur à chercher
                    valueToCheck++;
                }
                //Lors de la fin d'une clonne, reset la valeur à chercher à 1
                valueToCheck = 1;
                //Incrémenter la colonne
                columnX++;
            }*/
            valueToCheck = 1;
            columnX = 0;
            int divider = Convert.ToInt32(Math.Sqrt(SUDOKU_SIZE));
            List<int> listBloc = new List<int>();
            for (int y = 0; y < divider; y += divider)
            {
                for (int line = 1; line <= SUDOKU_SIZE; line++)
                {
                    for (int x = 0; x < divider; x++)
                    {
                        listBloc.Add(_ArraySudoku[line-1, columnX]);
                        columnX++;
                    }
                    columnX -= divider;
                    if (line % divider == 0 && line != 0)
                    {
                        //Si la liste contient des doublons
                        if(listBloc.GroupBy(n => n).Any(c => c.Count() > 1))                        
                        {
                            //Retourne false
                            return false;
                        }
                        //Vide la liste
                        listBloc.Clear();
                    }
                }
                columnX += divider;
            }
            return true;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int[,] generateDefaultSudoku()
        {
            const int _sudokuSize = 9;
            int emptyInt = 0;
            int[,] arraySudoku = new int[_sudokuSize, _sudokuSize];
            int[,] arraySudokuData = new int[_sudokuSize, _sudokuSize] {
                { 5, 3, emptyInt, emptyInt,7, emptyInt, emptyInt, emptyInt , emptyInt },
                { 6, emptyInt, emptyInt, 1, 9, 5, emptyInt, emptyInt, emptyInt},
                { emptyInt, 9, 8, emptyInt, emptyInt, emptyInt, emptyInt, 6 , emptyInt },
                { 8, emptyInt, emptyInt, emptyInt, 6, emptyInt, emptyInt, emptyInt , 3 },
                { 4, emptyInt, emptyInt, 8,emptyInt, 3, emptyInt, emptyInt , 1 },
                { 7, emptyInt, emptyInt, emptyInt, 2, emptyInt, emptyInt, emptyInt , 6 },
                { emptyInt, 6, emptyInt, emptyInt,emptyInt, emptyInt, 2, 8 , emptyInt },
                { emptyInt, emptyInt, emptyInt, 4, 1, 9, emptyInt, emptyInt , 5 },
                { emptyInt, emptyInt, emptyInt, emptyInt, 8, emptyInt, emptyInt, 7 , 9 },
            };
            int[,] arraySudokuInput = new int[_sudokuSize, _sudokuSize]{
                { emptyInt, emptyInt, 4, 6, emptyInt, 8, 9, 1 , 2 },
                { emptyInt, 7, 2,emptyInt ,emptyInt , emptyInt, 3, 4, 8},
                { 1,emptyInt ,emptyInt , 3, 4, 2, 5, emptyInt , 7 },
                { emptyInt, 5, 9, 7,emptyInt, 1, 4, 2 , emptyInt },
                { emptyInt, 2, 6, emptyInt,5, emptyInt, 7, 9 , emptyInt },
                { emptyInt, 1, 3, 9, emptyInt, 4, 8, 5 ,  emptyInt},
                { 9, emptyInt, 1, 5, 3, 7, emptyInt, emptyInt ,  4},
                { 2, 8, 7, emptyInt, emptyInt, emptyInt, 6, 3 ,  emptyInt},
                { 3, 4, 5, 2, emptyInt, 6, 1, emptyInt ,  emptyInt},
            };

            for (int i = 0; i < _sudokuSize; i++)
            {
                for (int j = 0; j < _sudokuSize; j++)
                {
                    if (arraySudokuData[i, j] != 0)
                    {
                        arraySudoku[i, j] = arraySudokuData[i, j];
                    }
                    if (arraySudokuInput[i, j] != 0)
                    {
                        arraySudoku[i, j] = arraySudokuInput[i, j];
                    }
                    Console.Write(arraySudoku[i, j] + " ");
                }
                Console.WriteLine();
            }
            return arraySudoku;
        }
    }
}
