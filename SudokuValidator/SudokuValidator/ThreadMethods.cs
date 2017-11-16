using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsSudokuValidator
{
    public class ThreadMethods
    {
        private static bool isValid = true;
        public ThreadMethods()
        {

        }


        public static void validateTable(object _ArraySudoku)
        {
            IList objectList = _ArraySudoku as IList;
            List<int> ArraySudoku = new List<int>();
            foreach (int i in objectList)
            {
                ArraySudoku.Add(i);
            }
            if (hasDoublon(ArraySudoku))
            {
                isValid = false;
            }
        }

        private static bool hasDoublon(List<int> _listInt)
        {
            /*if(_listInt.GroupBy(n => n).Any(c => c.Count() > 1))
            {
                return true;
            }
            return false;*/
            int[] tableInt = new int[_listInt.Count+1];
            foreach(int i in _listInt)
            {
                tableInt[i] += 1;
            }
            foreach (int i in tableInt)
            {
                if(i>1)
                {
                    return true;
                }
            }
            return false;
        }


        public static bool isSudokuValid()
        {
            return isValid;
        }
        
    }
}
