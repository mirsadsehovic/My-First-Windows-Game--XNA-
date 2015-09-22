using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace WindowsGame2.Model
{
    class Model
    {

        //const float m_rightEdge = 1;

        private Level m_level = new Level();
        private Player m_player = new Player();
        private List<Enemy> m_enemy = new List<Enemy>();
   
     

       // private int m_points = 0;
        bool m_hasCollidedWithGround = false;
        bool m_enemyCollidedWithGround = false;



        public Model()
        {
            List<Vector2> enemyPosition = m_level.SetEnemyPosition();
            Enemy enemy;
            for(int i = 0; i<enemyPosition.Count; i++)
            {
                enemy= new Enemy();
                enemy.SetPosition(enemyPosition[i].X, enemyPosition[i].Y);
                m_enemy.Add(enemy);
            }
        }

        internal void Update(float a_elapsedTime, IStateObserver a_observer, ISpundObserver a_points)
        {
           UpdateEnemy(a_elapsedTime);
           UpdatePlayer(a_elapsedTime, a_observer, a_points);
        
          

          //  a_model.Update(a_elapsedTime, a_points, a_model);
         
        }
     
        internal int GainPoints()
        {
            return m_player.Points;
        }

        internal int LoseLife()
        {
            return m_player.Lives;
        }

        internal void UpdatePlayer(float a_elapsedTime, IStateObserver a_observer, ISpundObserver a_points)
        {

             
            Vector2 oldPos = m_player.GetPosition();
            //get a new position for the player
            m_player.Update(a_elapsedTime);
            Vector2 newPos = m_player.GetPosition();

            m_hasCollidedWithGround = false;
            Vector2 speed = m_player.GetSpeed();
            Vector2 afterCollidedPos = Collide(oldPos, newPos, m_player.m_sizes, ref speed, out m_hasCollidedWithGround, a_observer);

            //set the new speed and position after collision
            m_player.SetPosition(afterCollidedPos.X, afterCollidedPos.Y);
            m_player.SetPosition(afterCollidedPos.X, afterCollidedPos.Y);
            m_player.SetSpeed(speed.X, speed.Y);



      

            if (afterCollidedPos.X > Level.g_levelWidth - 3)
            {
                speed = new Vector2(0.0f, 0.0f);
                afterCollidedPos = new Vector2(5.0f, 0.0f);
                m_level.NextLevel(a_observer); 
               
            }
        
            if(m_level.IsCollidingAtEscape(newPos, m_player.m_sizes))
            {
                m_level.GameCompleted(a_observer);

            }

    
            if (m_level.IsCollidingAtPoint(newPos, m_player.m_sizes, a_points))
            {
                m_player.GaindPoint();
              
               
                //  m_level.CountPoint(a_points);

            }

            if(m_level.Fall(newPos, a_points))
            {
                  m_level.Restart(a_observer);
            }
            if (m_level.IsCollidingAtEnemy(newPos, m_player.m_sizes, a_points))
            {

            
                m_level.Restart(a_observer);
                DoStartOver();

            }

            if (CollideWithEnemyTwo(newPos, a_points) == true)
            {
                m_level.Restart(a_observer);
                new Vector2(5.0f, 16f);
            }
            if (m_level.IsCollidingAtTrap(newPos, m_player.m_sizes, a_points))
            {
                m_level.Restart(a_observer);
               // return new Vector2(6.0f, 16f);
            }
            
        }

       
        internal void UpdateEnemy(float a_elapsedTime)
        {

            for (int i = 0; i < m_enemy.Count; i++)
            {
                Vector2 lastPosition = m_enemy[i].GetPosition();
                m_enemy[i].Update(a_elapsedTime);
                Vector2 newPosition = m_enemy[i].GetPosition();

                Random rnd = new Random();
                int t = rnd.Next(100);
                if (t % 2 == 0)
                    m_enemy[i].DoEnemyJump();

                m_enemyCollidedWithGround = false;

                Vector2 velocity = m_enemy[i].GetSpeed();
                Vector2 afterCollidedPos = EnemyCollide(lastPosition, newPosition, m_enemy[i].m_size, ref velocity, out m_enemyCollidedWithGround, i);

                //set the new velocity and position after collision
                m_enemy[i].SetPosition(afterCollidedPos.X, afterCollidedPos.Y);

                m_enemy[i].SetSpeed(velocity.X, velocity.Y);

            }
        }

        private Vector2 EnemyCollide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_velocity, out bool a_outCollideGround, int i)
        {
            a_outCollideGround = false;
            //Can we move to the position safely?


            if (m_level.IsCollidingAt(a_newPos, a_size))
            {
                //if not try only the X movement, indicates that a collision with ground or roof has occured

                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);



                if (a_velocity.Y > 0 && a_oldPos.Y - (int)a_oldPos.Y > 0.9f)
                {
                    xMove.Y = (int)a_oldPos.Y + 0.99f;
                }


                if (m_level.IsCollidingAt(xMove, a_size) == false)
                {
                    //did we collide with ground?
                    if (a_velocity.Y > 0)
                    {

                        a_outCollideGround = true;
                        m_enemy[i].SetState();
                        a_velocity.Y = 0; //no bounce
                    }
                    else
                    {
                        //collide with roof
                        a_velocity.Y *= -1.0f; //reverse the y velocity and some speed lost in the collision
                    }


                    a_velocity.X *= 0.10f;// friction should be time-dependant

                    return xMove;
                }
                else
                {
                    //try Y movement, indicates that a collision with wall has occured
                    Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                    if (m_level.IsCollidingAt(yMove, a_size) == false)
                    {
                        a_velocity.X *= 0.5f;
                        return yMove;
                    }

                    if (a_velocity.Y > 0)
                    {
                        a_outCollideGround = true;
                    }
                    a_velocity.X = 0; //no bounce
                    a_velocity.Y = 0; //no bounce

                }
                //remain at the same position
                return a_oldPos;
            }

            return a_newPos;
        }

        private bool CollideWithEnemyTwo(Vector2 a_newPos, ISpundObserver a_soundEffect)
        {
         

            for (int i = 0; i < m_enemy.Count; i++)
            {
                if ((m_enemy[i].GetPosition() - a_newPos).Length() < 0.5f)
                {
                    a_soundEffect.DeathSound();
                    return true;
                }

            }
            return false;
        }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------\\
       
        //Collision for Player
        private Vector2 Collide(Vector2 a_oldPos, Vector2 a_newPos, Vector2 a_size, ref Vector2 a_velocity, out bool a_outCollideGround, IStateObserver a_observer)
        {
            a_outCollideGround = false;
            
            //if(m_level.Fall(a_newPos))
           // {

            //    return new Vector2(6.0f, 16f);
               
          //  }
            //if (m_level.IsCollidingAtTrap(a_newPos, a_size))
            //{
            //    m_level.Restart(a_observer);
            //    return new Vector2(6.0f, 16f);
           // }

          // if (m_level.IsCollidingAtEnemy(a_newPos, a_size))
           // {
             //   DoStartOver();
           
               // return new Vector2(6.0f, 16f);
                
           // }
         //  if (CollideWithEnemyTwo(a_newPos)==true)
          //  {
           //    return new Vector2(6.0f, 16f);
         //  }
         
            if (m_level.IsCollidingAt(a_newPos, a_size))
            {
                //if not try only the X movement, indicates that a collision with ground or roof has occured

                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);

                if (a_velocity.Y > 0 && a_oldPos.Y - (int)a_oldPos.Y > 0.9f)
                {
                    xMove.Y = (int)a_oldPos.Y + 0.99f;
                }


                if (m_level.IsCollidingAt(xMove, a_size) == false)
                {
                    //did we collide with ground?
                    if (a_velocity.Y > 0)
                    {

                        a_outCollideGround = true;
                        a_velocity.Y = 0; //no bounce
                    }
                    else
                    {
                        //collide with roof
                        a_velocity.Y *= 0.0f; //reverse the y velocity and some speed lost in the collision
                    }


                    a_velocity.X *= 0.10f;// friction should be time-dependant

                    return xMove;
                }
                else
                {
                    //try Y movement, indicates that a collision with wall has occured
                    Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                    if (m_level.IsCollidingAt(yMove, a_size) == false)
                    {
                        a_velocity.X *= 0.5f;
                        return yMove;
                    }

                    if (a_velocity.Y > 0)
                    {
                        a_outCollideGround = true;
                    }
                    a_velocity.X = 0; //no bounce
                    a_velocity.Y = 0; //no bounce

                }
                //remain at the same position
                return a_oldPos;
            }

            if (m_level.IsCollidingAtBrick(a_newPos, a_size))
            {
                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);

                if (a_velocity.Y > 0 && a_oldPos.Y - (int)a_oldPos.Y > 0.9f)
                {
                    xMove.Y = (int)a_oldPos.Y + 0.99f;
                }


                if (m_level.IsCollidingAtBrick(xMove, a_size) == false)
                {
                    //did we collide with ground?
                    if (a_velocity.Y > 0)
                    {

                        a_outCollideGround = true;
                        a_velocity.Y = 0; //no bounce
                    }
                    else
                    {
                        //collide with roof
                        a_velocity.Y *= 0.0f; //reverse the y velocity and some speed lost in the collision
                    }


                    a_velocity.X *= 0.10f;// friction should be time-dependant

                    return xMove;
                }
                else
                {
                    //try Y movement, indicates that a collision with wall has occured
                    Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                    if (m_level.IsCollidingAtBrick(yMove, a_size) == false)
                    {
                        a_velocity.X *= 0.0f;
                        return yMove;
                    }

                    if (a_velocity.Y > 0)
                    {
                        a_outCollideGround = true;
                    }
                    a_velocity.X = 0; //no bounce
                    a_velocity.Y = 0; //no bounce

                }
                //remain at the same position
                return a_oldPos;

            }

            if (m_level.IsCollidingAtBreak(a_newPos, a_size))
            {
                Vector2 xMove = new Vector2(a_newPos.X, a_oldPos.Y);
                if (m_level.IsCollidingAt(xMove, a_size) == false)
                {

                   //did we collide with ground?
                    if (a_velocity.Y > 0)
                    {
                        a_outCollideGround = true;
                    }

                    a_velocity.Y *= 0; //reverse the y velocity and some speed lost in the collision
                    a_velocity.X *= 0;// friction should be time-dependant

                    return xMove;
                }
                else
                {
                    //try Y movement, indicates that a collision with wall has occured
                    Vector2 yMove = new Vector2(a_oldPos.X, a_newPos.Y);
                    if (m_level.IsCollidingAt(yMove, a_size) == false)
                    {
                        a_velocity.X = 0;
                        // a_velocity.Y = 0;
                        return yMove;
                    }
                }

                //remain at the same position
                return a_oldPos;
            }

            return a_newPos;
        }

        internal bool CanEnemyJump()
        {
          //  m_enemy.//Jump();
            return m_enemyCollidedWithGround;
        }

        internal bool CanJump()
        {
          //  m_enemy.Jump();
            return m_hasCollidedWithGround;  
        }

        internal void DoStartOver()
        {
            m_player.SetPosition(5.0f, 15);
            m_player.SetSpeed(0f, 0);
        }
        internal void NoMovement()
        
        {
            m_player.SetSpeed(0, m_player.GetSpeed().Y);
        }


        internal void DoJump()
        {     
            m_player.DoJump();
        }

        internal void IsCollidingAtPoint()
        {
            m_player.GaindPoint();
        }
     
        internal void GoRight()
        {
            m_player.SetSpeed(3.3f, m_player.GetSpeed().Y);
            //m_player.GoRight();
        }

        internal void GoLeft()
        {
            m_player.SetSpeed(-3.3f, m_player.GetSpeed().Y);
            //m_player.GoLeft();
        }

 
        internal Level GetLevel()
        {
            return m_level;
        }

        internal Microsoft.Xna.Framework.Vector2 GetPlayerPosition()
        {
            return m_player.GetPosition();
        }

        internal float GetPlayerSpeed()
        {
            return m_player.GetSpeed().Length();
        }

        internal List<Vector2> GetEnemyPositions()
        {
            List<Vector2> enemyPositions = new List<Vector2>(m_enemy.Count);
            for (int i = 0; i < m_enemy.Count; i++)
            {
                enemyPositions.Add(m_enemy.ElementAt(i).GetPosition());
            }
            return enemyPositions;
        }
     
    }
}


