using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace WindowsGame2.Model
{

    class Level
    {
        /// <summary>
        /// different tile types are represented by enum
        /// if the tile has state I normally use a class instead
        /// </summary>
        /// 
        public enum Tile
        {
            T_EMPTY = 0,
            T_BLOCKED,
            T_TRAP,
            T_ENEMY,
            T_POINTS,
            T_BRICK,
            T_BREAK,
            T_BREAK2,
            T_ESCAPE,
            T_JUMPINGENEMY
        };


        //width and heigt
        public const int g_levelWidth = 100;
        public const int g_levelHeight = 20;
      
        //the 2D array of tiles
        internal Tile[,] m_tiles = new Tile[g_levelWidth, g_levelHeight];
        private List<Vector2> enemyPosition = new List<Vector2>();
      //  internal Tile[,] m_enemy = new Tile[g_levelWidth, g_levelHeight];
     
      
        static int m_level = 1;
        
        
        internal Level()
        {
            GenerateLevel();
        }
      
        internal static void ResetLevel()
        {
            m_level = 1;
        }

        public void NextLevel(IStateObserver a_observer)
        {
            m_level++;
            a_observer.LevelComplete();
        }

        public void Restart(IStateObserver a_observer)
        {

            GenerateLevel();
            a_observer.GameOver();
        }

        public void GameCompleted(IStateObserver a_observer)
        {

            m_level=1;
            a_observer.GameComplete();
 
        }
      

        public void GenerateLevel()
        {
            //Generade Lelve. no reset, level 1 restarts after level 3 comlpleted.
            if (m_level == 1)
            {
                  for (int x = 0; x < g_levelWidth; x++)
            {
                m_tiles[x, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[x, g_levelHeight - 2] = Tile.T_BLOCKED;
            }
                m_tiles[0, g_levelHeight - 3] = Tile.T_BLOCKED;
                m_tiles[0, g_levelHeight - 4] = Tile.T_BLOCKED;
                m_tiles[0, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[0, g_levelHeight - 6] = Tile.T_BLOCKED;

                m_tiles[0, g_levelHeight - 7] = Tile.T_BRICK;
                m_tiles[0, g_levelHeight - 8] = Tile.T_BRICK;
                m_tiles[0, g_levelHeight - 9] = Tile.T_BRICK;
                m_tiles[0, g_levelHeight - 10] = Tile.T_BRICK;
              
                //POINT

              

                m_tiles[20, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[21, g_levelHeight - 5] = Tile.T_BRICK;
                m_tiles[35, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[35, g_levelHeight - 8] = Tile.T_POINTS;
                m_tiles[35, g_levelHeight - 8] = Tile.T_POINTS;
                m_tiles[36, g_levelHeight - 7] = Tile.T_BLOCKED;

                m_tiles[11, g_levelHeight - 5] = Tile.T_POINTS;
                m_tiles[11, g_levelHeight - 5] = Tile.T_POINTS;


             
                m_tiles[11, g_levelHeight - 3] = Tile.T_ENEMY;
                m_tiles[12, g_levelHeight - 2] = Tile.T_BLOCKED;
                m_tiles[13, g_levelHeight - 3] = Tile.T_BRICK;
                m_tiles[14, g_levelHeight - 4] = Tile.T_BRICK;
                m_tiles[15, g_levelHeight - 5] = Tile.T_BRICK;
                //Second

         
                m_tiles[26, g_levelHeight - 5] = Tile.T_BRICK;
                m_tiles[27, g_levelHeight - 6] = Tile.T_BRICK;
                //  m_tiles[28, g_levelHeight - 7] = Tile.T_BRICK;
                m_tiles[29, g_levelHeight - 5] = Tile.T_BRICK;
                // m_tiles[31, g_levelHeight - 5] = Tile.T_BRICK;
                m_tiles[32, g_levelHeight - 3] = Tile.T_BRICK;

                //enemy
                m_tiles[10, g_levelHeight - 3] = Tile.T_ENEMY;
                m_tiles[17, g_levelHeight - 3] = Tile.T_ENEMY;
                m_tiles[54, g_levelHeight - 3] = Tile.T_ENEMY;
                m_tiles[56, g_levelHeight - 3] = Tile.T_ENEMY;
                m_tiles[89, g_levelHeight - 3] = Tile.T_ENEMY;

                //random 

                m_tiles[40, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[41, g_levelHeight - 6] = Tile.T_BLOCKED;
                m_tiles[42, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[43, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[44, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[43, g_levelHeight - 7] = Tile.T_POINTS;
                m_tiles[44, g_levelHeight - 7] = Tile.T_POINTS;
                m_tiles[43, g_levelHeight - 6] = Tile.T_POINTS;
                m_tiles[44, g_levelHeight - 6] = Tile.T_POINTS;
                m_tiles[45, g_levelHeight - 3] = Tile.T_BLOCKED;

                m_tiles[60, g_levelHeight - 3] = Tile.T_BLOCKED;
                m_tiles[60, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[60, g_levelHeight - 2] = Tile.T_EMPTY;
                // m_tiles[61, g_levelHeight - 4] = Tile.T_BLOCKED;
                m_tiles[61, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[61, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[63, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[63, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[63, g_levelHeight - 2] = Tile.T_EMPTY;

                m_tiles[64, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[64, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[64, g_levelHeight - 3] = Tile.T_EMPTY;
                m_tiles[66, g_levelHeight - 5] = Tile.T_BRICK;
                m_tiles[67, g_levelHeight - 6] = Tile.T_BRICK;

                m_tiles[60, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[61, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[63, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[64, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[66, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[67, g_levelHeight - 1] = Tile.T_TRAP;

                m_tiles[80, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[81, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[82, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[83, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[84, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[85, g_levelHeight - 1] = Tile.T_EMPTY;

                m_tiles[80, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[82, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[83, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[84, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[85, g_levelHeight - 2] = Tile.T_EMPTY;



                //   m_tiles[67, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[67, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[67, g_levelHeight - 1] = Tile.T_EMPTY;

                //   m_tiles[68, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[68, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[68, g_levelHeight - 1] = Tile.T_EMPTY;



                m_tiles[70, g_levelHeight - 2] = Tile.T_BLOCKED;
                m_tiles[70, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[70, g_levelHeight - 2] = Tile.T_EMPTY;
                //m_tiles[71, g_levelHeight - 6] = Tile.T_BLOCKED;
                m_tiles[71, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[71, g_levelHeight - 2] = Tile.T_EMPTY;
                //m_tiles[73, g_levelHeight - 4] = Tile.T_BLOCKED;
                m_tiles[73, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[73, g_levelHeight - 2] = Tile.T_EMPTY;
                //m_tiles[73, g_levelHeight - 4] = Tile.T_BLOCKED;

                m_tiles[71, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[72, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[73, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[74, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[75, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[76, g_levelHeight - 1] = Tile.T_TRAP;

                m_tiles[74, g_levelHeight - 5] = Tile.T_BRICK;
                m_tiles[78, g_levelHeight - 7] = Tile.T_BRICK;
                m_tiles[78, g_levelHeight - 8] = Tile.T_POINTS;
                m_tiles[73, g_levelHeight - 7] = Tile.T_BRICK;
                m_tiles[71, g_levelHeight - 10] = Tile.T_BRICK;
                m_tiles[70, g_levelHeight - 10] = Tile.T_BRICK;
                m_tiles[66, g_levelHeight - 13] = Tile.T_BRICK;
                m_tiles[69, g_levelHeight - 15] = Tile.T_ENEMY;
                m_tiles[69, g_levelHeight - 14] = Tile.T_BRICK;
                m_tiles[70, g_levelHeight - 14] = Tile.T_BRICK;
                m_tiles[71, g_levelHeight - 14] = Tile.T_BRICK;
                m_tiles[72, g_levelHeight - 15] = Tile.T_ENEMY;
                m_tiles[72, g_levelHeight - 14] = Tile.T_BRICK;

                m_tiles[74, g_levelHeight - 13] = Tile.T_BRICK;
                m_tiles[75, g_levelHeight - 16] = Tile.T_BRICK;
                m_tiles[72, g_levelHeight - 18] = Tile.T_BRICK;
                m_tiles[72, g_levelHeight - 19] = Tile.T_POINTS;
                m_tiles[76, g_levelHeight - 17] = Tile.T_BRICK;


                m_tiles[71, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[72, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[73, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[74, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[75, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[76, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[77, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[78, g_levelHeight - 2] = Tile.T_BLOCKED;



                m_tiles[79, g_levelHeight - 3] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 4] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 6] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 8] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 9] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 10] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 11] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 12] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 13] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 14] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 15] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 16] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 17] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 18] = Tile.T_BLOCKED;
                m_tiles[79, g_levelHeight - 19] = Tile.T_ENEMY;


                m_tiles[67, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[68, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[70, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[71, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[73, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[73, g_levelHeight - 1] = Tile.T_TRAP;


                //end
               /* m_tiles[99, g_levelHeight - 3] = Tile.T_BLOCKED;
                m_tiles[99, g_levelHeight - 4] = Tile.T_BLOCKED;
                m_tiles[99, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[99, g_levelHeight - 6] = Tile.T_BLOCKED;
                m_tiles[99, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[99, g_levelHeight - 8] = Tile.T_BLOCKED;
                m_tiles[98, g_levelHeight - 3] = Tile.T_BLOCKED;
                m_tiles[98, g_levelHeight - 4] = Tile.T_BLOCKED;
                m_tiles[98, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[98, g_levelHeight - 6] = Tile.T_BLOCKED;
                m_tiles[98, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[98, g_levelHeight - 8] = Tile.T_BLOCKED;*/

                //First hole

                m_tiles[19, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[18, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[19, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[19, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[18, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[21, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[21, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[22, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[22, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[23, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[23, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[24, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[24, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[26, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[26, g_levelHeight - 2] = Tile.T_EMPTY;
                //Second Hole
                m_tiles[27, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[27, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[28, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[28, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[29, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[29, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[30, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[30, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[32, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[32, g_levelHeight - 2] = Tile.T_EMPTY;
                m_tiles[33, g_levelHeight - 1] = Tile.T_EMPTY;
                m_tiles[33, g_levelHeight - 2] = Tile.T_EMPTY;
            }
            
            //LEVEL 2

            if (m_level == 2)
            {

                for (int x = 0; x < g_levelWidth; x++)
                {
                    m_tiles[x, g_levelHeight - 1] = Tile.T_EMPTY;
                    m_tiles[x, g_levelHeight - 2] = Tile.T_EMPTY;
                }

                for(int y=  1;y <5 ; y++)
                {
                 
                    m_tiles[2, g_levelHeight - y] = Tile.T_BLOCKED;
                    m_tiles[3, g_levelHeight - y] = Tile.T_BLOCKED;
                    m_tiles[4, g_levelHeight - y] = Tile.T_BLOCKED;
                    m_tiles[5, g_levelHeight - y] = Tile.T_BLOCKED;
                
                }
                m_tiles[10, g_levelHeight - 3] = Tile.T_JUMPINGENEMY;
              
                for (int y = 1; y < 8; y++)
                {
                    m_tiles[0, g_levelHeight - y] = Tile.T_BLOCKED;
                    m_tiles[1, g_levelHeight - y] = Tile.T_BLOCKED;
                    m_tiles[2, g_levelHeight - y] = Tile.T_BLOCKED;
                }

                m_tiles[10, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[11, g_levelHeight - 1] = Tile.T_BLOCKED;

                m_tiles[8, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[9, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[8, g_levelHeight - 2] = Tile.T_POINTS;
                m_tiles[9, g_levelHeight - 2] = Tile.T_POINTS;

                m_tiles[13, g_levelHeight - 4] = Tile.T_BRICK;
                m_tiles[14, g_levelHeight - 4] = Tile.T_BRICK;

                m_tiles[13, g_levelHeight - 7] = Tile.T_BRICK;
                m_tiles[14, g_levelHeight - 7] = Tile.T_BRICK;

                m_tiles[13, g_levelHeight - 8] = Tile.T_POINTS;
                m_tiles[14, g_levelHeight - 8] = Tile.T_POINTS;

                for (int y = 1; y < 8; y++)
                {
                    m_tiles[17, g_levelHeight - y] = Tile.T_BLOCKED;
                    m_tiles[17, g_levelHeight - 8] = Tile.T_ENEMY;
                }
                for (int y = 1; y < 9;y++)
                {
                    m_tiles[18, g_levelHeight - y] = Tile.T_BLOCKED;

                }
                 for (int y = 1; y < 10 ; y++)
                {
                    m_tiles[19, g_levelHeight - y] = Tile.T_BLOCKED;
                    m_tiles[19, g_levelHeight - 10] = Tile.T_ENEMY;
                }
                 for (int y = 1; y < 11; y++)
                 {
                     m_tiles[20, g_levelHeight - y] = Tile.T_BLOCKED;

                 }
                 for (int y = 1; y < 12; y++)
                 {
                     m_tiles[21, g_levelHeight - y] = Tile.T_BLOCKED;

                 }
                 for (int y = 1; y < 13; y++)
                 {
                     m_tiles[22, g_levelHeight - y] = Tile.T_BLOCKED;

                 }
                 for (int y = 1; y < 14; y++)
                 {
                     m_tiles[23, g_levelHeight - y] = Tile.T_BLOCKED;

                 }
                 for (int y = 1; y < 15; y++)
                 {
                     m_tiles[24, g_levelHeight - y] = Tile.T_BLOCKED;

                 }
                 for (int y = 1; y < 16; y++)
                 {
                     m_tiles[25, g_levelHeight - y] = Tile.T_BLOCKED;
                     m_tiles[25, g_levelHeight - 16] = Tile.T_ENEMY;
                 }
                 for (int y = 1; y < 17; y++)
                 {
                     m_tiles[26, g_levelHeight - y] = Tile.T_BLOCKED;            
                 }
                 for (int x = 26; x < 35; x++)
                 {
                    m_tiles[x, g_levelHeight - 17] = Tile.T_BLOCKED;
                 }

                 m_tiles[35, g_levelHeight - 17] = Tile.T_BREAK;
                 m_tiles[36, g_levelHeight - 17] = Tile.T_BREAK;

                 for (int y = 4; y < 18; y++)
                 {
                     m_tiles[37, g_levelHeight - y] = Tile.T_BLOCKED;
                 }

                 for (int x = 28; x < 37; x++)
                 {
                     m_tiles[x, g_levelHeight - 14] = Tile.T_BLOCKED;           
                 }

                 m_tiles[32, g_levelHeight - 18] = Tile.T_JUMPINGENEMY;

                 m_tiles[35, g_levelHeight - 18] = Tile.T_JUMPINGENEMY;

                 for (int x = 26; x < 35; x++)
                 {
                     m_tiles[x, g_levelHeight - 11] = Tile.T_BLOCKED;
                 }


                 for (int x = 28; x <30; x++)
                 {
                     m_tiles[x, g_levelHeight - 8] = Tile.T_BLOCKED;
                 }

                 for (int x = 32; x < 37; x++)
                 {
                     m_tiles[x, g_levelHeight - 8] = Tile.T_BLOCKED;
                 }
                 m_tiles[29, g_levelHeight - 7] = Tile.T_BLOCKED;
                 m_tiles[29, g_levelHeight - 6] = Tile.T_BLOCKED;
                 m_tiles[30, g_levelHeight - 6] = Tile.T_BLOCKED;
                 m_tiles[31, g_levelHeight - 6] = Tile.T_BLOCKED;
                 m_tiles[30, g_levelHeight - 7] = Tile.T_TRAP;
                 m_tiles[31, g_levelHeight - 7] = Tile.T_TRAP;

                 m_tiles[32, g_levelHeight - 6] = Tile.T_BLOCKED;
                 m_tiles[32, g_levelHeight - 7] = Tile.T_BLOCKED;
                 m_tiles[32, g_levelHeight - 8] = Tile.T_BLOCKED;

                 for (int y = 3; y < 8; y++)
                 {
                     m_tiles[28, g_levelHeight - y] = Tile.T_BLOCKED;
                 }

                 for (int x = 26; x < 40; x++)
                 {
                     m_tiles[x, g_levelHeight - 1] = Tile.T_BLOCKED;
                 }

                 m_tiles[43, g_levelHeight - 4] = Tile.T_BRICK;
                 m_tiles[46, g_levelHeight - 7] = Tile.T_BRICK;

                 for (int y = 1; y < 10; y++)
                 {
                     m_tiles[49, g_levelHeight - y] = Tile.T_BLOCKED;
                     m_tiles[49, g_levelHeight - 11] = Tile.T_POINTS;
                     m_tiles[49, g_levelHeight - 12] = Tile.T_POINTS;
                     m_tiles[49, g_levelHeight - 13] = Tile.T_POINTS;
                 }
                 for (int x = 49; x < 60; x++)
                 {
                     m_tiles[x, g_levelHeight - 7] = Tile.T_BLOCKED;
                 }


                 m_tiles[62, g_levelHeight - 8] = Tile.T_BREAK;

                 m_tiles[65, g_levelHeight - 8] = Tile.T_BREAK;

                 for (int y = 8; y < 15; y++)
                 {
                     m_tiles[69, g_levelHeight - y] = Tile.T_BLOCKED;
                     m_tiles[68, g_levelHeight - 11] = Tile.T_POINTS;
                     m_tiles[68, g_levelHeight - 12] = Tile.T_POINTS;
                     
                 }
                     m_tiles[68, g_levelHeight - 8] = Tile.T_BLOCKED;
                     m_tiles[68, g_levelHeight - 8] = Tile.T_BLOCKED;
                     m_tiles[68, g_levelHeight - 9] = Tile.T_POINTS;
                     m_tiles[68, g_levelHeight - 9] = Tile.T_POINTS;
                     m_tiles[68, g_levelHeight - 10] = Tile.T_POINTS;
                     m_tiles[68, g_levelHeight - 10] = Tile.T_POINTS;

                 for (int x = 61; x < 70; x++)
                 {
                     m_tiles[x, g_levelHeight - 1] = Tile.T_BLOCKED;
                 }
                 m_tiles[70, g_levelHeight - 1] = Tile.T_TRAP;
                 m_tiles[71, g_levelHeight - 1] = Tile.T_TRAP;
                 m_tiles[72, g_levelHeight - 1] = Tile.T_TRAP;

             
                 m_tiles[73, g_levelHeight - 1] = Tile.T_TRAP;
                 m_tiles[74, g_levelHeight - 1] = Tile.T_BLOCKED;
                 m_tiles[74, g_levelHeight - 2] = Tile.T_BLOCKED;
                 m_tiles[75, g_levelHeight - 1] = Tile.T_TRAP;
                 m_tiles[76, g_levelHeight - 1] = Tile.T_TRAP;
                 m_tiles[77, g_levelHeight - 1] = Tile.T_TRAP;
                 m_tiles[78, g_levelHeight - 1] = Tile.T_BLOCKED; 
                 m_tiles[78, g_levelHeight - 2] = Tile.T_BLOCKED;

                 m_tiles[74, g_levelHeight - 1] = Tile.T_BLOCKED;



                 m_tiles[78, g_levelHeight - 3] = Tile.T_ENEMY;
                 m_tiles[78, g_levelHeight - 2] = Tile.T_BLOCKED;
                 m_tiles[78, g_levelHeight - 5] = Tile.T_POINTS;
                 m_tiles[79, g_levelHeight - 4] = Tile.T_BLOCKED;
                 for (int x = 79; x < 84; x++)
                 {
                     m_tiles[x, g_levelHeight - 1] = Tile.T_TRAP;
                 }
                 m_tiles[84, g_levelHeight - 2] = Tile.T_BLOCKED;
                 m_tiles[84, g_levelHeight - 1] = Tile.T_BLOCKED;

                 m_tiles[85, g_levelHeight - 1] = Tile.T_TRAP;
                 m_tiles[86, g_levelHeight - 1] = Tile.T_TRAP;
                 m_tiles[87, g_levelHeight - 1] = Tile.T_TRAP;
                 m_tiles[88, g_levelHeight - 1] = Tile.T_BLOCKED;
                 m_tiles[88, g_levelHeight - 2] = Tile.T_BLOCKED;
                 m_tiles[88, g_levelHeight - 3] = Tile.T_BLOCKED;

                 for (int y = 1; y < 18; y++)
                 {
                     m_tiles[89, g_levelHeight - y] = Tile.T_BLOCKED;
                 }
                 for (int y = 6; y < 18; y++)
                 {
                     m_tiles[84, g_levelHeight - y] = Tile.T_BLOCKED;
                 }
                 m_tiles[85, g_levelHeight - 6] = Tile.T_BLOCKED;
                 m_tiles[88, g_levelHeight - 9] = Tile.T_BLOCKED;
                 m_tiles[85, g_levelHeight - 12] = Tile.T_BLOCKED;
               
                 m_tiles[89, g_levelHeight - 16] = Tile.T_EMPTY;
                 for (int x = 90; x < 100; x++)
                 {
                     m_tiles[x, g_levelHeight - 15] = Tile.T_BLOCKED;
                 }
            }

            //LEVEL 3
             if (m_level == 3)
            {
                for (int x = 0; x < g_levelWidth; x++)
                {
                    m_tiles[x, g_levelHeight - 1] = Tile.T_EMPTY;
                    m_tiles[x, g_levelHeight - 2] = Tile.T_EMPTY;
                }

                for (int y = 3; y < 17; y++)
                {
                    m_tiles[4, g_levelHeight - y] = Tile.T_BLOCKED;
                    m_tiles[7, g_levelHeight - y] = Tile.T_BLOCKED;
                }
                m_tiles[1, g_levelHeight - 16] = Tile.T_BLOCKED;
                m_tiles[2, g_levelHeight - 16] = Tile.T_BLOCKED;
                m_tiles[3, g_levelHeight - 16] = Tile.T_BLOCKED;
                m_tiles[4, g_levelHeight - 16] = Tile.T_BLOCKED;
                m_tiles[5, g_levelHeight - 16] = Tile.T_BLOCKED;


                m_tiles[6, g_levelHeight - 14] = Tile.T_BLOCKED;

                m_tiles[5, g_levelHeight - 12] = Tile.T_BLOCKED;
                m_tiles[6, g_levelHeight - 10] = Tile.T_BLOCKED;
                m_tiles[5, g_levelHeight - 8] = Tile.T_BLOCKED;
                m_tiles[6, g_levelHeight - 6] = Tile.T_BLOCKED;
                m_tiles[5, g_levelHeight - 4] = Tile.T_BLOCKED;
                m_tiles[4, g_levelHeight - 2] = Tile.T_BLOCKED;
                m_tiles[4, g_levelHeight - 1] = Tile.T_BLOCKED;

                for (int x = 4; x < 9; x++)
                {
                    m_tiles[x, g_levelHeight - 1] = Tile.T_BLOCKED;

                }

                m_tiles[9, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[10, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[11, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[12, g_levelHeight - 1] = Tile.T_TRAP;
                m_tiles[13, g_levelHeight - 1] = Tile.T_TRAP;
                
               
            
                m_tiles[14, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[15, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[16, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[17, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[18, g_levelHeight - 1] = Tile.T_BLOCKED;

                for (int y = 5; y < 17; y++)
                {
                    m_tiles[16, g_levelHeight - y] = Tile.T_BLOCKED;
                   
                }

                m_tiles[20, g_levelHeight - 2] = Tile.T_JUMPINGENEMY;
                m_tiles[20, g_levelHeight - 9] = Tile.T_JUMPINGENEMY;
         

                m_tiles[19, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[21, g_levelHeight - 3] = Tile.T_BLOCKED;

                m_tiles[17, g_levelHeight - 6] = Tile.T_BLOCKED;
                m_tiles[19, g_levelHeight - 8] = Tile.T_BLOCKED;
                m_tiles[21, g_levelHeight - 9] = Tile.T_BLOCKED;

                m_tiles[17, g_levelHeight - 11] = Tile.T_BLOCKED;
                m_tiles[21, g_levelHeight - 13] = Tile.T_BLOCKED;



                for (int y = 1; y < 17; y++)
                {
                  
                    m_tiles[22, g_levelHeight - y] = Tile.T_BLOCKED;
                }

                for (int x = 22; x < 30; x++)
                {
                    m_tiles[x, g_levelHeight - 16] = Tile.T_BLOCKED;

                }

                m_tiles[35, g_levelHeight - 14] = Tile.T_BLOCKED;
                m_tiles[36, g_levelHeight - 14] = Tile.T_BLOCKED;
                m_tiles[37, g_levelHeight - 14] = Tile.T_BLOCKED;
                m_tiles[38, g_levelHeight - 14] = Tile.T_BLOCKED;
                m_tiles[39, g_levelHeight - 14] = Tile.T_BLOCKED;
                m_tiles[40, g_levelHeight - 14] = Tile.T_BLOCKED;
                m_tiles[41, g_levelHeight - 14] = Tile.T_BLOCKED;
                m_tiles[42, g_levelHeight - 14] = Tile.T_BLOCKED;
                m_tiles[35, g_levelHeight - 15] = Tile.T_POINTS;
                m_tiles[36, g_levelHeight - 15] = Tile.T_POINTS;
                m_tiles[37, g_levelHeight - 15] = Tile.T_POINTS;
                m_tiles[38, g_levelHeight - 15] = Tile.T_POINTS;
                m_tiles[39, g_levelHeight - 15] = Tile.T_POINTS;

                m_tiles[36, g_levelHeight - 15] = Tile.T_ENEMY;
                m_tiles[38, g_levelHeight - 15] = Tile.T_ENEMY;
                m_tiles[40, g_levelHeight - 15] = Tile.T_ENEMY;
                m_tiles[42, g_levelHeight - 15] = Tile.T_ENEMY;


                for (int x = 36; x < 43; x++)
                {
                    m_tiles[x, g_levelHeight - 10] = Tile.T_BLOCKED;

                }
                m_tiles[41, g_levelHeight - 11] = Tile.T_ENEMY;
                m_tiles[39, g_levelHeight - 11] = Tile.T_ENEMY;
                m_tiles[37, g_levelHeight - 11] = Tile.T_ENEMY;

                m_tiles[36, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[37, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[38, g_levelHeight - 7] = Tile.T_BLOCKED;
                m_tiles[39, g_levelHeight - 7] = Tile.T_BLOCKED;
                
                m_tiles[41, g_levelHeight - 8] = Tile.T_BLOCKED;
                m_tiles[41, g_levelHeight -9] = Tile.T_BLOCKED;
               
                m_tiles[40, g_levelHeight - 8] = Tile.T_POINTS;
                m_tiles[40, g_levelHeight - 7] = Tile.T_POINTS;
                m_tiles[40, g_levelHeight - 6] = Tile.T_POINTS;
                m_tiles[40, g_levelHeight - 5] = Tile.T_BLOCKED;
                m_tiles[40, g_levelHeight - 4] = Tile.T_POINTS;
                m_tiles[40, g_levelHeight - 3] = Tile.T_POINTS;
                m_tiles[40, g_levelHeight - 2] = Tile.T_POINTS;
           

                for (int y = 1; y < 9; y++)
                {
                    m_tiles[38, g_levelHeight - y] = Tile.T_BLOCKED;
                }
                for (int y = 3; y < 9; y++)
                {
                    m_tiles[41, g_levelHeight - y] = Tile.T_BLOCKED;
                }
               
                for (int x = 38; x < 50; x++)
                {
                    m_tiles[x, g_levelHeight - 1] = Tile.T_BLOCKED;
                }
                m_tiles[53, g_levelHeight -1]= Tile.T_BLOCKED;
                m_tiles[53, g_levelHeight - 2] = Tile.T_BLOCKED;
                m_tiles[54, g_levelHeight - 3] = Tile.T_JUMPINGENEMY;
                m_tiles[55, g_levelHeight - 1] = Tile.T_BLOCKED;

                m_tiles[58, g_levelHeight - 2] = Tile.T_BLOCKED;
                m_tiles[58, g_levelHeight - 1] = Tile.T_BLOCKED;
                m_tiles[59, g_levelHeight - 3] = Tile.T_JUMPINGENEMY;

                m_tiles[60, g_levelHeight - 6] = Tile.T_POINTS;
                m_tiles[60, g_levelHeight - 4] = Tile.T_POINTS;
                m_tiles[60, g_levelHeight - 3] = Tile.T_POINTS;
                m_tiles[60, g_levelHeight - 2] = Tile.T_POINTS;
                m_tiles[60, g_levelHeight - 1] = Tile.T_BLOCKED;

                
                m_tiles[60, g_levelHeight - 3] = Tile.T_BLOCKED;
                m_tiles[60, g_levelHeight - 6] = Tile.T_BLOCKED;
                m_tiles[61, g_levelHeight - 7] = Tile.T_JUMPINGENEMY;
                m_tiles[58, g_levelHeight - 9] = Tile.T_BLOCKED;
                m_tiles[58, g_levelHeight - 10] = Tile.T_POINTS;

                m_tiles[62, g_levelHeight - 9] = Tile.T_BLOCKED;
                m_tiles[63, g_levelHeight - 9] = Tile.T_BLOCKED;
                m_tiles[62, g_levelHeight - 9] = Tile.T_BLOCKED;
                m_tiles[62, g_levelHeight - 10] = Tile.T_POINTS;
                m_tiles[63, g_levelHeight - 10] = Tile.T_POINTS;
                m_tiles[62, g_levelHeight - 10] = Tile.T_POINTS;

                for (int x = 66; x < 75; x++)
                {
                    m_tiles[x, g_levelHeight - 10] = Tile.T_BLOCKED;
                }
                m_tiles[77, g_levelHeight - 7] = Tile.T_BRICK;
                m_tiles[80, g_levelHeight - 4] = Tile.T_BLOCKED;
                m_tiles[81, g_levelHeight - 5] = Tile.T_JUMPINGENEMY;

                m_tiles[85, g_levelHeight - 3] = Tile.T_BLOCKED;


                for (int y = 3; y < 18; y++)
                {
                    m_tiles[91, g_levelHeight - y] = Tile.T_BLOCKED;
                }
                for (int x = 91; x < 95; x++)
                {
                    m_tiles[x, g_levelHeight - 3] = Tile.T_BLOCKED;
                }
                m_tiles[96, g_levelHeight - 2] = Tile.T_ESCAPE;
                for (int x = 91; x < 97; x++)
                {
                    m_tiles[x, g_levelHeight - 1] = Tile.T_BLOCKED;
                }
                for (int y = 1; y < 18; y++)
                {
                    m_tiles[97, g_levelHeight - y] = Tile.T_BLOCKED;
                }

              
            }
             if (m_level > 3)
             {

                 m_level = 1;

             }
        }

        public List<Vector2> SetEnemyPosition()
        {
            enemyPosition = new List<Vector2>();
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                    if (m_tiles[x, y] == Tile.T_JUMPINGENEMY) 
                {
                    enemyPosition.Add(new Vector2(x, y));

                }
            }
            return enemyPosition;
        }
        public List<Vector2> GetEnemyPositions()
        {
            SetEnemyPosition();
            return enemyPosition;
        }

        //When doed collision happen fpr blocked!, same for the other tiles
        public bool IsCollidingAt(Vector2 a_newPos, Vector2 a_size)
        {

            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);  
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);
     

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x-0.4f)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.1f)
                        continue;
                    if (topLeft.Y > (float)y + 0.7f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_BLOCKED)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Fall(Vector2 a_newPos, ISpundObserver a_soundEffect)
        {
            if (a_newPos.Y > g_levelHeight + 1)
            {
                a_soundEffect.DeathSound();
                return true;
            }
            return false;
        }

        public bool IsCollidingAtEscape(Vector2 a_newPos, Vector2 a_size)
        {
            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);   // (a_newPos.X / 2.0f, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 0.8f)
                        continue;
                    if (topLeft.Y > (float)y + 0.8f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_ESCAPE)
                    {

                        return true;
                    }
                }

            }
            return false;

        }


        public bool IsCollidingAtTrap(Vector2 a_newPos, Vector2 a_size, ISpundObserver a_soundEffect)
        {
            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);   // (a_newPos.X / 2.0f, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 0.8f)
                        continue;
                    if (topLeft.Y > (float)y + 0.8f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_TRAP)
                    {
                        a_soundEffect.DeathSound();
                        return true;
                    }
                }

            }
            return false;

        }

        public bool IsCollidingAtBreak(Vector2 a_newPos, Vector2 a_size)
        {

            Vector2 topLeft = new Vector2(a_newPos.X + a_size.X / 4, a_newPos.Y - a_size.Y);   // (a_newPos.X / 2.0f, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X + a_size.X / 20, a_newPos.Y); //    (a_newPos.X + a_size.X / 2.0f, a_newPos.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x - 0.5f)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.5f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_BREAK)
                    {
                        m_tiles[x, y] = Tile.T_BREAK2;

                        return true;
                    }
                    else if (m_tiles[x, y] == Tile.T_BREAK2)
                    {
                        m_tiles[x, y] = Tile.T_EMPTY;
                        return true;
                    }      

                }
            }
            return false;
        }
              
        //The tile changes when collide 
        public bool IsCollidingAtBreakTwo(Vector2 a_newPos,Vector2 a_size)
        {
             
            Vector2 topLeft = new Vector2(a_newPos.X + a_size.X / 4, a_newPos.Y - a_size.Y);   // (a_newPos.X / 2.0f, a_newPos.Y - a_size.Y);
             Vector2 bottomRight = new Vector2(a_newPos.X + a_size.X / 20, a_newPos.Y); //    (a_newPos.X + a_size.X / 2.0f, a_newPos.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x - 0.5f)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.5f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_BREAK2)
                    {
                        m_tiles[x,y] = Tile.T_EMPTY;
                    
                        
                        return true;
                    }
                   
                    
                }
            }

            return false;

        }

        public bool IsCollidingAtEnemy(Vector2 a_newPos, Vector2 a_size, ISpundObserver a_soundEffect)
        {
              

            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);   // (a_newPos.X / 2.0f, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_ENEMY)
                    {
                        a_soundEffect.DeathSound();
                        return true;
                    }
                }
                
            }
            return false;

        }
      

        public bool IsCollidingAtBrick(Vector2 a_newPos, Vector2 a_size)
        {
            Vector2 topLeft = new Vector2(a_newPos.X, a_newPos.Y - a_size.Y);
            Vector2 bottomRight = new Vector2(a_newPos.X, a_newPos.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x - 0.4f)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 0.7f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_BRICK)
                    {
                        return true;
                    }
                }
            }




            return false;
        }

       public bool IsCollidingAtPoint(Vector2 a_newPos, Vector2 a_size, ISpundObserver a_soundEffect)
        {

           Vector2 topLeft = new Vector2(a_newPos.X + a_size.X / 4, a_newPos.Y - a_size.Y);   // (a_newPos.X / 2.0f, a_newPos.Y - a_size.Y);
             Vector2 bottomRight = new Vector2(a_newPos.X + a_size.X / 20, a_newPos.Y); 
            //int points = 0;
          
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {

                    if (bottomRight.X < (float)x)
                        continue;
                    if (bottomRight.Y < (float)y)
                        continue;
                    if (topLeft.X > (float)x + 1.0f)
                        continue;
                    if (topLeft.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y] == Tile.T_POINTS)
                       
                    {
                        m_tiles[x, y] = Tile.T_EMPTY;
                        a_soundEffect.PointSound();
                        
                        return true;
                    }

                }

            }
            return false;

        }

    }
}
