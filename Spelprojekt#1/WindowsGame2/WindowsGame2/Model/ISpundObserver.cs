using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2.Model
{
    //Sound
    public interface ISpundObserver
    {
        void LandOnFloor();
        void PointSound();
        void DeathSound();
    }
}
