using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Klawiatura
    {
        private static Hashtable klawiszeTab = new Hashtable(); 
        public static bool KeyPress(Keys key)
        {
            if (klawiszeTab[key] == null)
            {
                return false; 
            }
            return (bool)klawiszeTab[key];

        }
    
        public static void changeState(Keys key, bool state) 
        {
            klawiszeTab[key] = state;
        }
    }
}
