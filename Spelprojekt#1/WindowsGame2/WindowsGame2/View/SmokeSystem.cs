using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsGame2.View
{
    class SmokeSystem
    {

        private const int NUM_PARTICLES = 100;


        public void DrawSmoke(float a_elapsedTime, Microsoft.Xna.Framework.Vector2 a_viewPosition, float a_viewScale, Microsoft.Xna.Framework.Graphics.SpriteBatch a_batch, Microsoft.Xna.Framework.Graphics.Texture2D a_texture)
        {
            for (int i = 0; i < NUM_PARTICLES; i++)
            {

                //Get the individual time for this particle i
                float particleTime = GetParticleTime(i, a_elapsedTime);
                //Get the max time for particle i
                float maxTime = GetParticleMaxTime(i);

                //all particles rotate the same
                float rotationalSpeed = 0.50f;


                Vector2 modelPos = GetPosition(i, particleTime);

                //Get The View Position for the individual particle i relative
                Vector2 viewPos = modelPos * a_viewScale + a_viewPosition;

                //only render particles that have a positive time, matters only in the beginning
                if (particleTime > 0)
                {
                    //interpolate from 1 to 0 depending on time for this particle
                    float percentLeft = 1.0f - particleTime / maxTime;

                    //different sizes depending on index (i % 80) and a minimum size of 10 and particles are growing with respect to time
                    float size = (i % 80 + 100) * (particleTime / maxTime) + 20;
                    float rotation = particleTime * 2.0f * (float)Math.PI / maxTime * rotationalSpeed + (float)i;

                    DrawParticleAt(viewPos, size, percentLeft, a_batch, a_texture, rotation);
                }

            }
        }

        private void DrawParticleAt(Vector2 a_viewPos, float a_size, float a_alpha, SpriteBatch a_batch, Texture2D a_texture, float a_rotation)
        {
            //the entire texture as source
            Rectangle srcRect = new Rectangle(0, 0, a_texture.Width*1, a_texture.Height*1);

            //color fades to 0
            Color color = new Color(a_alpha, a_alpha, a_alpha, a_alpha);



            //rotate around middle of texture
            Vector2 origin = new Vector2(a_texture.Width , a_texture.Height);

            //size is relative to texture size in draw
            float size = a_size / a_texture.Width;

            //and draw it
            a_batch.Draw(a_texture, a_viewPos, srcRect, color, a_rotation, origin, size, SpriteEffects.None, 0);

        }

        //this is how long a particle lives in seconds
        private float GetParticleMaxTime(int a_index)
        {
            return 1.0f + 0.1f * a_index;
        }


        private float GetParticleTime(int i, float a_totalTime)
        {
            //how long does it take for particle i to show up
            float delay = (float)(i + 2) / (float)NUM_PARTICLES + GetParticleMaxTime(i) - 1.0f;

            //remove the delay
            float time = a_totalTime - delay;

            //calculate how many times a particle has existed
            float timesRespawned = (int)(time / GetParticleMaxTime(i)); //how many rotations

            //remove the previous times from time
            time = time - timesRespawned * GetParticleMaxTime(i);

            return time;
        }

        //Returns a Random speed -0.5 to 0.5
        private Vector2 GetSpeed(int i)
        {
            Random rand = new Random(i);
            return new Vector2((float)rand.NextDouble() - 1.5f, (float)rand.NextDouble() - 1.5f);
        }

        //interpolate a position
        private Vector2 GetPosition(int i, float a_elapsedTime)
        {
            Vector2 speed = GetSpeed(i);

            //all smoke particles accelerate upwards
            Vector2 acceleration = new Vector2(-0.5f, 0.1f);

            return speed * a_elapsedTime + acceleration  * a_elapsedTime * a_elapsedTime;
        }




    }
}

