using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Conway_s_Game_Of_Life
{
    public class LivingCell : TextBlock
    {
        
        public bool IsAlive;
        public int LifeValue;
        

        public bool cellState(bool isAlive)
        {
            isAlive = IsAlive;
                return IsAlive;
        }

    }
}
