using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace newTestProj
{

    public class Ball : PositionalTexture2D
    {
        public bool DebugOutput {get;set;}
        private const float maxJumpHeight = 40;
        private const float maxJumpTime = 2;
        private const float jumpingLockTime = 1.2f;
        private const int jumpSpeed = 3;
        private float jumpingStatingPosition;
        public Ball(Texture2D texture,IGraphicsDeviceManagerNew graphics): base(texture, graphics)
        {
            this.jumpingStatingPosition = graphics.PreferredBackBufferHeight/2;
            this.XPos = this.graphics.PreferredBackBufferWidth/2;
            this.XSpeed = 150;
            this.YSpeed = 90;
            this.scale = 0.3f;
        }
        private float timeSinceJumped;
        public void InitializeJump(GameTime gameTime)
        {
            timeSinceJumped += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(this.timeSinceJumped > jumpingLockTime)
            {
                this.jumpingStatingPosition = this.YPos;
                this.timeSinceJumped = 0;
            }
        }

        private float GetPosition(GameTime gameTime)
        {
            var distance = this.GetJumpingDistance(gameTime);
            var newPosition =  this.jumpingStatingPosition - distance;
            
            if(newPosition < 0)
            {
                 //Försök till att inte fastna så mycket i taket.
                this.timeSinceJumped += Math.Max( 1-this.timeSinceJumped,0);

                return Math.Max(this.jumpingStatingPosition - this.GetJumpingDistance(gameTime),0);
                //return 0;
            }
            if(newPosition > this.graphics.PreferredBackBufferHeight-this.Height)
            {
                return this.graphics.PreferredBackBufferHeight-this.Height;
            }
            return newPosition;
        }

        public void UpdatePosition(GameTime gameTime, IList<RectangleRow> rows)
        {
            var newYPos = this.GetPosition(gameTime);
            
            this.SetPosition(newYPos);
            
            this.CheckColiding(rows);
        }

        private void SetPosition(float position)
        {
            this.YPos = position;
        }
        private float GetJumpingDistance(GameTime gameTime)
        {
            this.timeSinceJumped += (float)gameTime.ElapsedGameTime.TotalSeconds * jumpSpeed;
            
            if(this.timeSinceJumped < maxJumpTime)
            {
                //2x - 1x^2
                // t = 0 => y = 0;
                // t = 1 => y = 1;
                // t = 2 => y = 0;
                // t = 3 => y = -3

                var a =  2*this.timeSinceJumped - 1*this.timeSinceJumped*this.timeSinceJumped;   
                a = Math.Max((float)a, (float)-1);
            
                if(this.DebugOutput)
                {
                    Console.WriteLine("jumping animation : " + a + "    Last jumping value: " + (a - lastJumpingValue));
                    lastJumpingValue = a;
                }
                return a*maxJumpHeight;
            }
            else
            {
                var value = -2*(this.timeSinceJumped - maxJumpTime);
                if(this.DebugOutput)
                {
                    Console.WriteLine("Falling : " + value + "  Last falling value: " + (value - lastFallingValue));
                    this.lastFallingValue = value;
                }
                return value * maxJumpHeight;

            }
        }
        //for debug
        private float lastFallingValue = 0;
        private float lastJumpingValue = 0;
        //End debug
        private void CheckColiding(IList<RectangleRow> rows)
        {
            this.IsColiding = false;
            for(var i = 0; i<rows.Count; i++)
            {
                var row = rows[i];
                if(row.IsColiding(this))
                {
                    this.IsColiding = true;
                    
                    if( row.IsColidingInXFromLeft(this))
                    {    
                        this.XPos = row.XPos - this.Width;
                    }
                }
            }
        }
    }
}