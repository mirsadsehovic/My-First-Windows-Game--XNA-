using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame2.Model
{
    class Enemy
    {

        Vector2 m_position = new Vector2(0.0f, 0);
        Vector2 m_speed = new Vector2(0.0f, -10.0f);
        public Vector2 m_size = new Vector2(-0.8f, 0.8f);
        Vector2 m_velocity = Vector2.Zero;
        private Vector2 gravityAcceleration = new Vector2(0.0f, 12.0f);
       
        //Two States For Enemy, if standing make jump.
        enum State
        {
            Standing,
            Jumping
        }
        private State m_currentState = State.Standing;
       
        internal void SetState()
        {
            m_currentState = State.Standing;
        }

        internal void Update(float a_elpasedTime)
        {
            Vector2 gravityAcceleration = new Vector2(0.0f, 10.0f);
            //integrate position
            m_position = m_position +m_speed*a_elpasedTime+gravityAcceleration *a_elpasedTime*a_elpasedTime;
            //integrate speed
            m_speed = m_speed + a_elpasedTime * gravityAcceleration;
        }

        public void SetPosition(float a_x, float a_y)
        {
            m_position.X = a_x;
            m_position.Y = a_y;
        }
        
        internal Vector2 GetPosition()
        {
            return m_position;
        }
        public Vector2 GetSpeed()
        {
            return m_speed;
        }

        public void SetSpeed(float a_x, float a_y)
        {
            m_speed.X = a_x;
            m_speed.Y = a_y;

        }
        //Set the speed of enemy jump, and check state.
        internal void DoEnemyJump()
        {
            if (m_currentState == State.Jumping)
            {
            }
            else
            {
               
                m_speed.Y = -10.0f;
                m_currentState = State.Jumping;
            }
        }
        internal void Update(float a_elapsedTime, Level a_level)
        {
            
            Vector2 gravityAcceleration = new Vector2(0.0f, 14.0f);
            //integrate position
            m_position = m_position + m_speed * a_elapsedTime + gravityAcceleration * a_elapsedTime * a_elapsedTime;
            //integrate speed
            m_speed = m_speed + a_elapsedTime * gravityAcceleration;
        }

  

    }
}
