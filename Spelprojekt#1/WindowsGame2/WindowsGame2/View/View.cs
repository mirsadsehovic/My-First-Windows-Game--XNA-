using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;



namespace WindowsGame2.View
{
    class View:Model.ISpundObserver
    {
        private SpriteBatch m_spriteBatch;
        private Texture2D m_tileTexture;
        private Texture2D m_playerTexture;
        private Texture2D m_playerTextureLeft;
        private Texture2D m_backgroundTexture;
        private KeyboardState m_oldKeyboardState;
        private Texture2D m_smokeTexture;
        private Texture2D m_enemyTexture;
        private SpriteFont m_textfont;
      
        private Rectangle destRect;
        
        private Rectangle TileRectangle;
     

       
        Song Moonchild;
       
        private SoundEffect m_jumpSound;
        private SoundEffect m_pointSound;
        private SoundEffect m_deathSound;
        bool songstart = false;
     
        private float m_viewScale = 100;
        private float m_time = 5;
       
        private SmokeSystem m_smoke = new SmokeSystem();

       private enum PlayerState
       {
           Left,
           Right
            
       }
       PlayerState m_currentState = PlayerState.Right;
     
        // State m_currentState = State.Walking;
   
       //TitleScreen
 
        //note that the view scale is less than texture scale
        private int m_textureTileSize = 64;
      
      
        public View(GraphicsDeviceManager a_manager, ContentManager a_contentLoader)
        {
            //this is needed to draw sprites
            m_spriteBatch = new SpriteBatch(a_manager.GraphicsDevice);

            //Load content
            m_tileTexture = a_contentLoader.Load<Texture2D>("FinalTiles5");
            m_playerTexture = a_contentLoader.Load<Texture2D>("WalkingSquare");
            m_playerTextureLeft = a_contentLoader.Load<Texture2D>("player2left");
            m_enemyTexture = a_contentLoader.Load<Texture2D>("Enemy2");
            
            m_smokeTexture = a_contentLoader.Load<Texture2D>("smoke1");
            m_backgroundTexture = a_contentLoader.Load<Texture2D>("LimboFinal");
        
            m_jumpSound = a_contentLoader.Load<SoundEffect>("14");
            m_pointSound = a_contentLoader.Load<SoundEffect>("7");
           m_deathSound = a_contentLoader.Load<SoundEffect>("8");

            m_textfont = a_contentLoader.Load<SpriteFont>("Font");
           
            Moonchild =a_contentLoader.Load<Song>("3");
      
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            SoundEffect.MasterVolume = 0.6f;

            if (!songstart)
            {
                MediaPlayer.Play(Moonchild);
               
                songstart = true;
               
            }
     
        }

        public void DrawLevel(float a_elapsedTime, GraphicsDevice a_graphicsDevice, Model.Level a_level, Camera a_camera, Vector2 a_playerPosition, int a_points, List<Vector2> a_drawEnemy)
     
        {
            Vector2 viewportSize = new Vector2(a_graphicsDevice.Viewport.Width, a_graphicsDevice.Viewport.Height);
          
            float scale = a_camera.GetScale();

            a_graphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
            Rectangle background = new Rectangle(0, 0, (int)(m_backgroundTexture.Width*2), (int)(m_backgroundTexture.Height));
            Rectangle backgroundSource = new Rectangle(0, 0, (int)viewportSize.X, (int)viewportSize.Y);

            //draw all images
            m_spriteBatch.Begin();

            m_spriteBatch.Draw(m_backgroundTexture, background, backgroundSource, Color.White);
            //draw level
            for (int x = 0; x < Model.Level.g_levelWidth; x++)
            {
                for (int y = 0; y < Model.Level.g_levelHeight; y++)
                {
                    Vector2 viewPos = a_camera.GetViewPosition(x, y, viewportSize);
                    DrawTile(viewPos.X, viewPos.Y, scale, a_level.m_tiles[x, y]); 

                } 
            }


            //DrawEnemy
            DrawEnemyAt(a_drawEnemy, viewportSize, a_camera, scale); // DrawEnemyAt(a_drawEnemy, viewportSize, a_camera, scale);
       
            m_time += a_elapsedTime / 10.0f;
           // Vector2 smokePosition = a_camera.GetViewPosition(25.5f, 10f, viewportSize);
           // m_smoke.DrawSmoke(m_time, smokePosition, 20f, m_spriteBatch, m_smokeTexture);
          
            
            Vector2 displacement = new Vector2(a_graphicsDevice.Viewport.Width, a_graphicsDevice.Viewport.Height);
            m_smoke.DrawSmoke(m_time, displacement, m_viewScale, m_spriteBatch, m_smokeTexture);
           
            //Vector2 a_playerSpeed = new Vector2();
            Vector2 playerViewPos = a_camera.GetViewPosition(a_playerPosition.X, a_playerPosition.Y, viewportSize);
            DrawPlayerAt(playerViewPos, scale);

            DrawUI(a_points);

      
           m_spriteBatch.End();

            

        }
        //Draw User Interface

        private void PlaySound(SoundEffect a_sound)
        {
            if (a_sound != null)
            {
                a_sound.Play();
            }
        }


        public void PointSound()
        {
            PlaySound(m_pointSound);
        }
        public void LandOnFloor()
        {
          //  m_playerView.SetPlayerState(PlayerView.State.Standing);
        }
        public void DeathSound()
        {
            PlaySound(m_deathSound);
        }

        public static int a_points = 0;

        public void DrawUI( int a_points)
        {
            string mission = "Get to the escape pod";
            string points = " Points: " + a_points; 
          
          
          
            m_spriteBatch.DrawString(m_textfont, points, new Vector2(20,40 ), Color.White);
          
            m_spriteBatch.DrawString(m_textfont, mission, new Vector2(300, 3), Color.Snow);
        }
        
        private void DrawPlayerAt(Microsoft.Xna.Framework.Vector2 a_viewBottomCenterPosition, float a_scale)
        {
            a_scale = 64;
            //Create rectangle and draw it, note the transformation of the position
           // Rectangle destRect = new Rectangle((int)(a_viewBottomCenterPosition.X - a_scale/4), (int)(a_viewBottomCenterPosition.Y - a_scale), (int)a_scale, (int)a_scale);
            if (m_currentState == PlayerState.Right)
            {
                Rectangle destRect = new Rectangle((int)(a_viewBottomCenterPosition.X - a_scale / 2), (int)(a_viewBottomCenterPosition.Y - a_scale), (int)a_scale, (int)a_scale);
                m_spriteBatch.Draw(m_playerTexture, destRect, Color.White);
            }
            else
            {
                Rectangle destRect = new Rectangle((int)(a_viewBottomCenterPosition.X - a_scale / 4), (int)(a_viewBottomCenterPosition.Y - a_scale), (int)a_scale, (int)a_scale);
                m_spriteBatch.Draw(m_playerTextureLeft, destRect, Color.White);
            }
        }

 
        private void DrawEnemyAt(List<Vector2> a_positions, Vector2 viewportSize, Camera a_camera, float a_scale)
        {
          
              Rectangle enemySource = new Rectangle(0, 0, (int)(m_enemyTexture.Width), (int)(m_enemyTexture.Height));
           
            //Destination rectangle in windows coordinates only scaling
      
            for (int i = 0; i <a_positions.Count(); i++)
            { 
                Vector2 EnemyViewPos = a_camera.GetViewPosition(a_positions[i].X, a_positions[i].Y, viewportSize);
                //Vector2 destRect = a_camera.GetViewPosition(a_positions[i].X, a_positions[i].Y, ViewPort);
                Rectangle enemyRect = new Rectangle((int)(EnemyViewPos.X - a_scale), (int)(EnemyViewPos.Y - a_scale), (int)a_scale, (int)a_scale);
                m_spriteBatch.Draw(m_enemyTexture, enemyRect,enemySource,  Color.White);
            }
        }

        private void DrawTile(float a_x, float a_y, float a_scale, Model.Level.Tile a_tile)
        {

            //Get the source rectangle (pixels on the texture) for the tile type 
            TileRectangle = new Rectangle(m_textureTileSize * (int)a_tile, 0, m_textureTileSize, m_textureTileSize);

            //Destination rectangle in windows coordinates only scaling
            destRect = new Rectangle((int)a_x, (int)a_y, (int)a_scale, (int)a_scale);
          

            m_spriteBatch.Draw(m_tileTexture, destRect, TileRectangle, Color.White);
        }
       

        public bool DidPlayerPressJump()
        {
            Keys jumpKey = Keys.Z;
            bool ret = false;

            KeyboardState newState = Keyboard.GetState();

            //has been pressed and released
            if (m_oldKeyboardState.IsKeyDown(jumpKey))// && newState.IsKeyUp(jumpKey))
            {
                ret = true;
               
            }
            //save state
            m_oldKeyboardState = newState;
          //  MediaPlayer.Play(Jump);
          //  soundEffect.Play();
            //return true if release of key was detected
            return ret;
        }

        public bool DidPlayerPressRight()
        {
            Keys RightKey = Keys.Right;
            bool ret = false;

            KeyboardState newState = Keyboard.GetState();
            if (m_oldKeyboardState.IsKeyDown(RightKey) && newState.IsKeyDown(RightKey))
            {
                m_currentState = PlayerState.Right;
                ret = true;
            }
            m_oldKeyboardState = newState;
            return ret;

        }

        public bool DidPlayerPressLeft()
        {
            Keys LeftKey = Keys.Left;
            bool ret = false;

            KeyboardState newState = Keyboard.GetState();
            if (m_oldKeyboardState.IsKeyDown(LeftKey) && newState.IsKeyDown(LeftKey))
            {
                m_currentState = PlayerState.Left;
                ret = true;
            }
            m_oldKeyboardState = newState;
            return ret;
        }

        internal void DoJump()
        {
            
            m_jumpSound.Play();
        }

        internal void IsCollidingAtPoint()
        {
            m_pointSound.Play();
        }

        public bool NoVelocity()
        {
            if (m_oldKeyboardState.IsKeyUp(Keys.Right) == true &&
                m_oldKeyboardState.IsKeyUp(Keys.Left) == true)
            {
                return true;
            }
            return false;
        }
       

    }
}
