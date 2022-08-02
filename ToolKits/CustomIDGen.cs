using System;
using System.Collections.Generic;
using System.Text;

namespace KadrMelumatlariApp.ToolKits
{
    public class CustomIDGen
    {
        public static int CustomID()
        {
            Random random = new Random();
            int id = random.Next(1, 100);
            return id;

        }
    }
}
