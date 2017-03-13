using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop.View;
using Desktop.Controller.TablesController; // BORRAR

namespace Desktop
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TablesController t = new TablesController();
            t.start();
            //TablesController t = new TablesController();
        }
    }
}
