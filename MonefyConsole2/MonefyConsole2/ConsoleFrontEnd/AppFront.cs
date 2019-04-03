using MonifyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFrontEnd
{
    class AppFront
    {

        public static void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            FirstMenu firstMenu = FirstMenu.GetInstance();
            firstMenu.Run();            
        }



    }

}