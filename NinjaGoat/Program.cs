using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaGoat
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow myGameWindow = new MyGameWindow();
            myGameWindow.Run(60);
        }
    }
}
