using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inspector
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static int CalculateIndex(string mark)
        {
            int index = 0;
            char[] markArray = mark.ToCharArray();

            index += (markArray[0] - 'A') * 1000000;
            index += int.Parse(new string(markArray, 1, 3)) * 1000; // mark[1..4]
            index += (markArray[4] - 'A') * 100;
            index += (markArray[5] - 'A') * 10;
            index += int.Parse(new string(markArray, 6, 3)); // mark[6..9]

            return index;
        }
    }
}
