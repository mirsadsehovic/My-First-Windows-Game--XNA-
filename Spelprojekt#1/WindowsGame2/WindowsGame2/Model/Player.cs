using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame2.Model
{
    class Player
    {

        Vector2 m_centerBottomPosition = new Vector2(5.0f, 0f);
        Vector2 m_speed = new Vector2(0f, 0f);
        public Vector2 m_sizes = new Vector2(-0.8f, 0.8f);
        
        internal Vector2 GetPosition()
        {
            return m_centerBottomPosition;
        }

        internal void Update(float a_elapsedTime)
        {
            Vector2 gravityAcceleration = new Vector2(0.0f, 14.8f);

            //integrate position
            m_centerBottomPosition = m_centerBottomPosition + m_speed * a_elapsedTime + gravityAcceleration * a_elapsedTime * a_elapsedTime;

            //integrate speed
            m_speed = m_speed + a_elapsedTime * gravityAcceleration;

        }

        internal void DoJump()
        {
            m_speed.Y = -10; //speed upwards
            //m_speed.X = 3;
        }

        internal void SetPosition(float a_x, float a_y)
        {
            m_centerBottomPosition.X = a_x;
            m_centerBottomPosition.Y = a_y;
        }

        internal void GoRight()
        {
         //   m_speed.X = 1;
        }
        
        
        internal void GoLeft()
        {
            //m_speed.X = -1;
        }

        internal Vector2 GetSpeed()
        {
            return m_speed;
        }

        internal void SetSpeed(float a_x, float a_y)
        {
            m_speed.X = a_x;
            m_speed.Y = a_y;
        }
      
        public int Points { get; set; }
        public int Lives { get; set; }


        internal void GaindPoint()
        {
            Points += 10; 
            
        }

   

     
    }
}



