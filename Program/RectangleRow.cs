using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace newTestProj
{
    
    public class RectangleRow
    {
        private static int s_width => 25;
        private static int s_speed => 50;
        
        public float XPos => this.TopRectangle.XPos;
        public IPositionalTexture2D TopRectangle;
        public IPositionalTexture2D BottomRectangle;
        private IGraphicsDeviceManagerNew graphics;
        public RectangleRow(IGraphicsDeviceManagerNew graphics)
        {
            this.graphics = graphics;
        }
        public void InitializeRow(int startY, int openHight)
        {
            //Top
            var topRec = new Texture2D(this.graphics.GraphicsDevice, s_width, startY);
            topRec.SetData(this.GetColorsWithBorders(s_width, topRec.Height));
            
            this.TopRectangle = new PositionalTexture2D(topRec, this.graphics)
            {
                XPos = this.graphics.PreferredBackBufferWidth, //börja längst ut
                YPos = 0, //börja från topp av y
            };

            //Bottom
            var height = Math.Max(this.graphics.PreferredBackBufferHeight - startY - openHight,1);
            var bottomRec = new Texture2D(this.graphics.GraphicsDevice, s_width, height);
            bottomRec.SetData(this.GetColorsWithBorders(s_width, bottomRec.Height));
            this.BottomRectangle = new PositionalTexture2D(bottomRec, this.graphics)
            {
                XPos = this.graphics.PreferredBackBufferWidth,
                YPos = this.graphics.PreferredBackBufferHeight-height,
            };
        }
        public void Draw(GameTime gameTime, ISpriteBatch spriteBatch)
        {   
            var topVector = new Vector2(this.TopRectangle.XPos, this.TopRectangle.YPos);
            spriteBatch.Draw(this.TopRectangle.GetTexture(), topVector, Color.White);

            var bottomVector = new Vector2(this.BottomRectangle.XPos, this.BottomRectangle.YPos);
            spriteBatch.Draw(this.BottomRectangle.GetTexture(), bottomVector, Color.White);

        }
        public void UpdateRow(GameTime gameTime, float rampup)
        {
            this.TopRectangle.XPos -= s_speed*(float)gameTime.ElapsedGameTime.TotalSeconds * rampup;
            this.BottomRectangle.XPos -= s_speed*(float)gameTime.ElapsedGameTime.TotalSeconds * rampup;
        }
        public bool IsColiding(IPositionalTexture2D object2D)
        {
            if(this.IsColiding(object2D, this.TopRectangle) 
                || this.IsColiding(object2D, this.BottomRectangle))
            {
                return true;
            }

            return false;
        }
        private bool IsColiding(IPositionalTexture2D object1, IPositionalTexture2D object2)
        {
           return IsColidingInX(object1, object2) && IsColidingInY(object1,object2);
        }

        private bool IsColidingInX(IPositionalTexture2D object1, IPositionalTexture2D object2)
        {
            if(object1.XPos+object1.Width > object2.XPos )
            {
                if(object1.XPos < object2.XPos + object2.Width)
                {
                    return true;
                }
            }
            
            return false;
        }

        public bool IsColidingInXFromLeft(IPositionalTexture2D object2)
        {
            return this.IsColidingInXFromLeft(object2, this.TopRectangle) 
                || this.IsColidingInXFromLeft(object2, this.BottomRectangle);
        }
         private bool IsColidingInXFromLeft(IPositionalTexture2D object1, IPositionalTexture2D object2)
         { 

             const float a = 1;

             var x = object1.XPos+object1.Width;
            return x > object2.XPos && x < object2.XPos+(object2.Width/a);
         }

        private bool IsColidingInY(IPositionalTexture2D object1, IPositionalTexture2D object2)
        {
            var y = object1.YPos;
            var height = object1.Height;
            if(y+height > object2.YPos && y < object2.YPos + object2.Height)
            {
                return true;
            }
            
            return false;
        }
        public bool IsOutOfRange => this.TopRectangle.XPos < 0;
        private Color[] GetColorsWithBorders(int width, int height)
        {
            Color[] data = new Color[width*height];
            for(int i=0; i < data.Length; ++i) 
            {
                var mod = i%width;
                if(mod < 2|| mod > width - 3 || i < width*2 || i > data.Length - (width*2))
                {
                    data[i] = Color.Black;
                }
                else
                {
                    data[i] = Color.Chocolate;
                }
            }
            return data;
        }
    }
}
