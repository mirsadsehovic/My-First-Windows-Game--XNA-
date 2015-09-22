using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace WindowsGame2.View
{
    class Menu: Model.IStateObserver
    {

        public enum GameState
        { 
            Start,
            Active,
            Pause,
            Help,
            Complete,
            GameOver,
            GameCompleted
        
        }
        GameState m_State = GameState.Start;
        
        private int m_level = 1;
        
        private SpriteBatch m_spriteBatch;
        private Texture2D m_menu;
        private Texture2D m_menu2;
        private Texture2D m_pause;
        private Texture2D m_death;
        private SpriteFont m_font;
        private Texture2D m_complete;
        private Texture2D m_gameComplete;

        private View m_view;

        public Menu(GraphicsDeviceManager a_manager, ContentManager a_contentLoader)
        {
            m_spriteBatch = new SpriteBatch(a_manager.GraphicsDevice);

            m_menu = a_contentLoader.Load<Texture2D>("Menu");
            m_menu2 = a_contentLoader.Load<Texture2D>("HowToPlay");
            m_pause = a_contentLoader.Load<Texture2D>("Pause");
            m_death = a_contentLoader.Load<Texture2D>("Failed2");
            m_complete = a_contentLoader.Load<Texture2D>("StageClear");
            m_gameComplete = a_contentLoader.Load<Texture2D>("GameCompleted3");
            
            m_font = a_contentLoader.Load<SpriteFont>("Font");
        }

        public void DrawMenu(GraphicsDevice a_graphicsDevice)
        {
            a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);
            Rectangle menu= new Rectangle(0,0, (int)(m_menu.Width),(int)(m_menu.Height));
            Rectangle destRect = new Rectangle((a_graphicsDevice.Viewport.Width / 2 - m_menu.Width / 2), (a_graphicsDevice.Viewport.Height / 2 - m_menu.Height / 2), m_menu.Width, m_menu.Height);
            m_spriteBatch.Begin();  
            m_spriteBatch.Draw(m_menu, destRect, menu, Color.White);
            m_spriteBatch.Draw(m_menu, destRect, Color.White);
            m_spriteBatch.End();

        }

        public void DrawHelp(GraphicsDevice a_graphicsDevice)
        {
            a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);
            Rectangle menu2 = new Rectangle(0, 0, (int)(m_menu2.Width), (int)(m_menu2.Height));
            Rectangle destRect2 = new Rectangle((a_graphicsDevice.Viewport.Width / 2 - m_menu2.Width / 2), (a_graphicsDevice.Viewport.Height / 2 - m_menu2.Height / 2), m_menu2.Width, m_menu2.Height);
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_menu2, destRect2, menu2, Color.White);
            m_spriteBatch.End(); 
        }
        public void DrawPause(GraphicsDevice a_graphicsDevice)
        {
            a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.WhiteSmoke);
            Rectangle pause = new Rectangle(0, 0, (int)(m_pause.Width), (int)(m_pause.Height));
            Rectangle destRect = new Rectangle((a_graphicsDevice.Viewport.Width / 2 - m_pause.Width / 2), (a_graphicsDevice.Viewport.Height / 2 - m_pause.Height / 2), m_pause.Width, m_pause.Height);
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_pause, destRect, pause, Color.White);
           m_spriteBatch.End();
        }

        public void DrawLevelFinish(GraphicsDevice a_graphicsDevice)
        {
            a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);
            Rectangle complete = new Rectangle(0,0,(int) (m_complete.Width), (int)(m_complete.Height));
            Rectangle destRect = new Rectangle((a_graphicsDevice.Viewport.Width/2-m_complete.Width/2), (a_graphicsDevice.Viewport.Height/2-m_complete.Height/2), m_complete.Width, m_complete.Height);
           m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_complete, destRect, complete, Color.White);
            m_spriteBatch.End();
        }

        public void DrawFail(GraphicsDevice a_graphicsDevice)
        {

            a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);
            Rectangle death = new Rectangle(0,0,(int)(m_death.Width), (int)(m_death.Height));
            Rectangle destRect  = new Rectangle((a_graphicsDevice.Viewport.Width/2-m_death.Width/2), (a_graphicsDevice.Viewport.Height/2-m_death.Height/2), m_death.Width, m_death.Height);
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_death, destRect, death, Color.White);
            m_spriteBatch.End();
        }

        public void DrawGameComplete(GraphicsDevice a_graphicsDevice)
        {
            a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.White);
            Rectangle gamecomplete = new Rectangle(0, 0, (int)(m_gameComplete.Width), (int)(m_gameComplete.Height));
            Rectangle destRect = new Rectangle((a_graphicsDevice.Viewport.Width / 2 - m_gameComplete.Width / 2), (a_graphicsDevice.Viewport.Height / 2 - m_gameComplete.Height / 2), m_gameComplete.Width, m_gameComplete.Height);
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_gameComplete, destRect, gamecomplete, Color.White);
            m_spriteBatch.End();

        }

        internal bool IsLevelComplete()
        {
            if(m_State == GameState.Complete)
            {
                return true;
            }
            return false;
        }

       internal bool IsGameComplete()
        {
            if (m_State == GameState.GameCompleted)
            {
                return true;
            }
            return false;
        }
        
     
        internal bool IsGameOver()
        {
            if (m_State == GameState.GameOver)
            {
                return true;
            }

            return false;
        }

        public void GameOver()
        {
            m_State = GameState.GameOver;
            m_level = GetLevel();
        
        }

        public void LevelComplete()
        {
            m_State = GameState.Complete;
            m_level++;
        }


        public void GameComplete()
        {
            m_State = GameState.GameCompleted;
            m_level++;
        }


        public int GetLevel()
        {
            return m_level;
        }

        internal bool HasPressedStart()
        {
         
            Keys start = Keys.S;
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(start))
            {
                m_State = GameState.Active;
                return true;
            }

            return false;

        }

        internal bool HasPressedHelp()
        {
            Keys help = Keys.C;
            KeyboardState keyState = Keyboard.GetState();
            if(keyState.IsKeyDown(help))
            {
                m_State = GameState.Help;
                return true;
            }
            return false;
        }

        internal bool HasPressedPause()
        {
            Keys pause = Keys.P;
            KeyboardState keyState = Keyboard.GetState();
            if(keyState.IsKeyDown(pause))
            {
                
                m_State = GameState.Pause;
                return true;
            }
            return false;
        
        }

        public bool GameActive()
        {
            if (m_State == GameState.Active)
            {
                return true;
            }
            return false;
        }
        
        public bool GamePaused()
        {
            if (m_State == GameState.Pause)
            {
                return true;
            }
            return false;
        }
        public bool GameHelp()
        {

            if (m_State == GameState.Help)
            {
                return true;
            }
            return false;
        }
   
    }
  
}