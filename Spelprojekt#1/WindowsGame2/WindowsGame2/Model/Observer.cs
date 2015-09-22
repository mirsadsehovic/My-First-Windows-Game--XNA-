using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2.Model
{
    interface IStateObserver
    {
        void LevelComplete();
        void GameOver();
        void GameComplete();
      
        //void SetLevel();
    }
   
}
