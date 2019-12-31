using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Conway_s_Game_Of_Life
{
    //
    //  A.... TextBlock..? Why old me? WHY!?
    //  I'm going to leave it, an artifact of a time gone by.
    //  But for any on looking eyes, this exists: https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/how-to-draw-a-rectangle
    //
    public class LivingCell : TextBlock
    {
        private readonly SolidColorBrush _alive = new SolidColorBrush(Colors.Black);
        private readonly SolidColorBrush _dead = new SolidColorBrush(Colors.WhiteSmoke);
        public bool IsAlive;
        public int LifeValue;
        
        public bool CellState(bool isAlive)
        {
            IsAlive = isAlive;
            UpdateColor();
                return IsAlive;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            CellState(!IsAlive);
        }

        private void UpdateColor()
        {
            Background = IsAlive ? _alive : _dead;
        }
    }
}
