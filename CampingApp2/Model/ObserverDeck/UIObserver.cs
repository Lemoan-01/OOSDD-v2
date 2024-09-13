using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ObserverDeck//om circulaire references te stoppen
{
    public interface UIObserver 
    {
        void UpdateMap(int buttonId, bool isBlocked);

        IEnumerable<Button> GetMapGridButtons();
    }
}