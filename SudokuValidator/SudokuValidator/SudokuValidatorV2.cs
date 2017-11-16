using System;
using System.Collections.Generic;
using System.Threading;

namespace nsSudokuValidator
{
    class SudokuValidatorV2
    {
        public SudokuValidatorV2()
        {

        }

        /// <summary>
        /// Génère un sudoku pré-configuré
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
        /// Valide le sudoku en multi thread
        /// </summary>
        /// <param name="_ArraySudoku"></param>
        /// <param name="SUDOKU_SIZE"></param>
        /// <returns></returns>
        public bool ValidateSudokuMultiThread(int[,] _ArraySudoku)
        {
            validateSquares(_ArraySudoku);
            validateRows(_ArraySudoku);
            validateCols(_ArraySudoku);
            return ThreadMethods.isSudokuValid();

        }
        public void validateRows(int[,] _ArraySudoku)
        {
            int SUDOKU_SIZE = Convert.ToInt32(Math.Sqrt(_ArraySudoku.Length));

            int[] arrayRow = new int[SUDOKU_SIZE];
            List<int[]> listRow = new List<int[]>();
            List<Thread> listThread = new List<Thread>();

            for (int i = 0; i < SUDOKU_SIZE; i++)
            {
                for (int j = 0; j < SUDOKU_SIZE; j++)
                {
                    arrayRow[j] = _ArraySudoku[i, j];
                }
                listRow.Add(arrayRow);                
                listThread.Add(new Thread(ThreadMethods.validateTable));
            }

            //Envoie chaque lignes pour vérifier s'il n'y a pas de doublons
            for (int i = 0; i < listThread.Count; i++)
            {
                listThread[i].Start(listRow[i]);
            }
        }
        public void validateCols(int[,] _ArraySudoku)
        {
            int SUDOKU_SIZE = Convert.ToInt32(Math.Sqrt(_ArraySudoku.Length));

            int[] arrayCol = new int[SUDOKU_SIZE];
            List<int[]> listCol = new List<int[]>();


            List<Thread> listThread = new List<Thread>();

            for (int i = 0; i < SUDOKU_SIZE; i++)
            {
                for (int j = 0; j < SUDOKU_SIZE; j++)
                {
                    arrayCol[j] = _ArraySudoku[j, i];

                }
                listCol.Add(arrayCol);
                listThread.Add(new Thread(ThreadMethods.validateTable));

            }

            //Envoie chaque colonnes pour vérifier s'il n'y a pas de doublons
            for (int i = 0; i < listThread.Count; i++)
            {
                listThread[i].Start(listCol[i]);
            }
        }

        public void validateSquares(int[,] _ArraySudoku)
        {
            int SUDOKU_SIZE = Convert.ToInt32(Math.Sqrt(_ArraySudoku.Length));

            List<Thread> listThread = new List<Thread>();

            //Récupère la liste des blocs
            List<List<int>> listSquares = getSquares(_ArraySudoku, SUDOKU_SIZE);
            int index = 0;
            foreach (List<int> square in listSquares)
            {
                listThread.Add(new Thread(ThreadMethods.validateTable));
                listThread[index].Start(square);
                index++;
            }
        }
        public List<List<int>> getSquares(int[,] _ArraySudoku, int SUDOKU_SIZE)
        {
            List<List<int>> listSquares = new List<List<int>>();
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
                        listSquares.Add(listSquare);
                        //Vide la liste
                        listSquare.Clear();
                    }
                }
                //Passe à la colonne de carré suivante
                columnX += divider;
            }
            return listSquares;
        }
    }
}
