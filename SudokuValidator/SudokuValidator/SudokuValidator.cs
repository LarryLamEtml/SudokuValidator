/* ETML
 * Author : Larry Lam
 * Description : Sudoku Validator
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SudokuValidator
{
    class SudokuValidator
    {
        public SudokuValidator()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int[,] generateDefaultSudoku(int _sudokuSize)
        {
          //  const int sudokuSize = _sudokuSize;

            //Taille du sudoku
            const int sudokuSize = 9;
            //Valeur par défaut
            int emptyInt = 0;
            //Tableau avec le sudoku
            int[,] arraySudoku = new int[sudokuSize, sudokuSize];
            //Tableau de données "générée"
            int[,] arraySudokuData = new int[sudokuSize, sudokuSize] {
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
            //Tableau de données entrées
            int[,] arraySudokuInput = new int[sudokuSize, sudokuSize]{
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

            //Fusionne les deux tableaux 
            //Parcours les lignes
            for (int i = 0; i < sudokuSize; i++)
            {
                //Parcours les colonnes
                for (int j = 0; j < _sudokuSize; j++)
                {
                    //Si la valeur n'est pas "vide"
                    if (arraySudokuData[i, j] != emptyInt)
                    {
                        //L'ajouter dans le tableau final
                        arraySudoku[i, j] = arraySudokuData[i, j];
                    }
                    //Si la valeur n'est pas "vide"
                    if (arraySudokuInput[i, j] != emptyInt)
                    {
                        //L'ajouter dans le tableau
                        arraySudoku[i, j] = arraySudokuInput[i, j];
                    }
                    //Affiche le tableau
                    Console.Write(arraySudoku[i, j] + " ");
                }
                //Retour à la ligne
                Console.WriteLine();
            }
            //Retourne le tableau final
            return arraySudoku;
        }

        /// <summary>
        /// Valide le sudoku en 3 étapes
        /// </summary>
        /// <param name="_ArraySudoku"></param>
        /// <param name="SUDOKU_SIZE"></param>
        /// <returns></returns>
        public bool validateSudoku(int[,] _ArraySudoku, int SUDOKU_SIZE)
        {
            if (!validateRow(_ArraySudoku, SUDOKU_SIZE))
            {
                return false;
            }
            if (!validateColumn(_ArraySudoku, SUDOKU_SIZE))
            {
                return false;
            }
            if (!validateSquare(_ArraySudoku, SUDOKU_SIZE))
            {
                return false;

            }
            return true;

        }

        /// <summary>
        /// Valide le sudoku en multi thread
        /// </summary>
        /// <param name="_ArraySudoku"></param>
        /// <param name="SUDOKU_SIZE"></param>
        /// <returns></returns>
        public bool validateSudokuMultiThread(int[,] _ArraySudoku, int SUDOKU_SIZE)
        {
            Thread t = new Thread(validateRow(_ArraySudoku, SUDOKU_SIZE));
            bool result = t.Start(validateRow(_ArraySudoku, SUDOKU_SIZE));
            if (!validateRow(_ArraySudoku, SUDOKU_SIZE))
            {
                return false;
            }
            if (!validateColumn(_ArraySudoku, SUDOKU_SIZE))
            {
                return false;
            }
            if (!validateSquare(_ArraySudoku, SUDOKU_SIZE))
            {
                return false;

            }
            return true;

        }

        private bool validateRow(int[,] _ArraySudoku, int SUDOKU_SIZE)
        {
            //Valeur à chercher (1->tailleX du sudoku) 
            int valueToCheck = 1;
            //index de la ligne
            int lineY = 0;
            //Variable de validation pour la valeur à chercher
            bool next = false;
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
            return true;
        }

        private bool validateColumn(int[,] _ArraySudoku, int SUDOKU_SIZE)
        {
            //Valeur à chercher (1->tailleX du sudoku) 
            int valueToCheck = 1;
            //Index de la colonne
            int columnX = 0;
            //Variable de validation pour la valeur à chercher
            bool next = false;

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
            }
            return true;
        }

        public bool validateSquare(int[,] _ArraySudoku, int SUDOKU_SIZE)
        {
            int columnX = 0;
            //SquareLenght
            int divider = Convert.ToInt32(Math.Sqrt(SUDOKU_SIZE));

            //Liste de tout les nombres dans le carré
            List<int> listSquare = new List<int>();
            //Parcours les colonnes de carré
            for (int y = 0; y < divider; y += divider)
            {
                //Parcours les lignes du carré
                for (int line = 1; line <= SUDOKU_SIZE; line++)
                {
                    //Parcours les cases du carré
                    for (int x = 0; x < divider; x++)
                    {
                        //Ajout des (9) cases du carrés dans la liste 
                        listSquare.Add(_ArraySudoku[line - 1, columnX]);
                        //Incrémente l'index X
                        columnX++;
                    }
                    //Reset l'index X de la premiere colonne du carré actuel
                    columnX -= divider;

                    //Check dans chaque carré s'il y a un doublons
                    if (line % divider == 0 && line != 0)
                    {
                        //Si la liste contient des doublons
                        if (listSquare.GroupBy(n => n).Any(c => c.Count() > 1))
                        {
                            //Retourne false
                            return false;
                        }
                        //Vide la liste
                        listSquare.Clear();
                    }
                }
                //Passe à la colonne de carré suivante
                columnX += divider;
            }
            //return true si tout est correct
            return true;
        }
    }
}
