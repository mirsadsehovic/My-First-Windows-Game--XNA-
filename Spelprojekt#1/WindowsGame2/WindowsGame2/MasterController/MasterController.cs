using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace WindowsGame2.Controller
{
   class MasterController
    {
        private Model.Model m_model;
        private View.View m_view;
        private View.Camera m_camera = new View.Camera();
        private View.Menu m_menu;
        private View.Menu m_pause;
        private View.Menu m_death;
     
     
    public MasterController(GraphicsDeviceManager a_manager, ContentManager a_contentManager)
        {
            m_model = new Model.Model();
           
            m_view = new View.View(a_manager, a_contentManager);
            m_menu = new View.Menu(a_manager, a_contentManager);
            m_pause = new View.Menu(a_manager, a_contentManager);
            m_death = new View.Menu(a_manager, a_contentManager);
           
        }

   internal void Draw(float a_elapsedTime, Microsoft.Xna.Framework.Graphics.GraphicsDevice GraphicsDevice)
        {
          //IF Game activ Draw Menu and wait for user input.
            if (m_menu.GameActive())
            {

                    Model.Level level = m_model.GetLevel();
                    View.View view = m_view;
                    Vector2 playerPosition = m_model.GetPlayerPosition();
                
                    m_camera.CenterOn(m_model.GetPlayerPosition(),
                    new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                    new Vector2(Model.Level.g_levelWidth, Model.Level.g_levelHeight));

                    m_camera.SetZoom(55);

               //draw background and player 
                m_view.DrawLevel(a_elapsedTime, GraphicsDevice, level, m_camera, m_model.GetPlayerPosition(), m_model.GainPoints(), m_model.GetEnemyPositions());
             }

            
            else
                {
                     m_menu.DrawMenu(GraphicsDevice);
              
                }

            
               
          
       if (m_menu.GameHelp())
                {
                    m_menu.DrawHelp(GraphicsDevice);
                }
              
           
       if (m_menu.GamePaused())
                {
                    m_menu.DrawPause(GraphicsDevice);
                }
                
           
       if (m_menu.IsLevelComplete())
                {
                    m_menu.DrawLevelFinish(GraphicsDevice);
                }
           
       if (m_menu.IsGameComplete())
            {
                m_menu.DrawGameComplete(GraphicsDevice);
            }

           
       if (m_menu.IsGameOver())
            {
                m_menu.DrawFail(GraphicsDevice);
            }
                
     
        }

   internal void Update(float a_elapsedTime)
        {

           if (Rungame())
           {

                //React to input
                if (m_view.DidPlayerPressJump())
                {
                    //Check if jumping was OK?
                    if (m_model.CanJump())
                    {
                        m_view.DoJump();
                        m_model.DoJump(); //realizes the jump
                        //
                    }


                }
              
                

                if (m_view.DidPlayerPressRight())
                {

                    m_model.GoRight();
                }

               // if (m_view.DidPlayerDie())
               // {
               //     m_model.DoStartOver();
              //  }

                if (m_view.NoVelocity() == true)
                {
                    m_model.NoMovement();
                }

                if (m_view.DidPlayerPressLeft())
                {
                    m_model.GoLeft();
                }

                //if (m_view.DidPlayerDie())
               // {
                 //   m_model.DoStartOver() ;               
              //  }
              //
                if (m_menu.HasPressedStart())
                {
                    m_model.GetLevel();
                }

                if (m_menu.HasPressedPause())
                {
                    m_menu.HasPressedPause();
                }

                if (m_menu.HasPressedHelp())
                {
                    m_menu.HasPressedHelp();
                }

                m_model.Update(a_elapsedTime, m_menu, m_view);
             
            }

        }
        
    internal void ContinueGame()
        {
            m_model = new Model.Model();
        }
    internal void GameCompleteted()
    {
        m_model = new Model.Model();
    }

        
    internal bool Rungame()
        {
            if (m_menu.HasPressedStart())
            {
                
                return true;
            }
            if (m_menu.HasPressedHelp())
            {
                return true;
            }

            if (m_menu.HasPressedPause())
            {
                return true;
            }
            

            if (m_menu.IsLevelComplete())
            {           
                ContinueGame();
                return true;
            }

            if (m_menu.IsGameOver())
            {
                Reset();
                return true;
            }
            if (m_menu.IsGameComplete())
            {
                GameCompleteted();
                return true;
            }

            if (m_menu.GameActive())
            {
                return true;
            }

            if (m_menu.GameHelp())
            {
                return true;
            }

            if (m_menu.IsGameOver())
            {
                if (m_menu.HasPressedStart())
                {
                    m_model.GetLevel();  
                }
             
            }

            return false;
        }
       
      internal void Reset()
            {
            
          m_model = new Model.Model();
             
            }

    }


}


