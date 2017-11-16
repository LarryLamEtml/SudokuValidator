/* ETML
 * Author : Larry Lam
 * Description : Sudoku Validator
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace nsSudokuValidator
{
    class Program
    {
        const int SUDOKU_SIZE = 256;
        static void Main(string[] args)
        {
            SudokuValidator sudokuValidator = new SudokuValidator();
            //int[,] arraySudoku = sudokuValidator.generateDefaultSudoku(SUDOKU_SIZE);
            //int[,] arraySudoku = sudokuValidator.generateSudoku(SUDOKU_SIZE);
            // int[,] arraySudoku = sudokuValidator.generateBigSudoku();
            int[,] arraySudoku = sudokuValidator.generateSudoku16();

            //  Thread.Sleep(1000);


            DateTime firstDt2 = DateTime.Now;
            resolveMonoThread(arraySudoku);
            DateTime endDt2 = DateTime.Now;

            Console.WriteLine(endDt2 - firstDt2);
            DateTime firstDt = DateTime.Now;
            resolveMultiThread(arraySudoku);
            DateTime endDt = DateTime.Now;

            Console.WriteLine(endDt - firstDt);
            Console.ReadLine();

        }

        public static void resolveMonoThread(int[,] arraySudoku)
        {
            SudokuValidator sudokuValidator = new SudokuValidator();
            bool resultMulti = sudokuValidator.validateSudoku(arraySudoku);

            if (resultMulti)
            {
                Console.WriteLine("Le sudoku est juste");
            }
            else
            {
                Console.WriteLine("Le sudoku est faux");
            }
        }

        public static void resolveMultiThread(int[,] arraySudoku)
        {
            SudokuValidatorV2 sudokuValidatorV2 = new SudokuValidatorV2();
            bool result = sudokuValidatorV2.ValidateSudokuMultiThread(arraySudoku);

            if (result)
            {
                Console.WriteLine("Le sudoku est juste");
            }
            else
            {
                Console.WriteLine("Le sudoku est faux");
            }
        }
    }
}
