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
            SudokuValidator sudokuValidator = new SudokuValidator();
            int[,] arraySudoku = sudokuValidator.generateDefaultSudoku(SUDOKU_SIZE);
            bool result = sudokuValidator.validateSudoku(arraySudoku, SUDOKU_SIZE);
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

        
    }
}
