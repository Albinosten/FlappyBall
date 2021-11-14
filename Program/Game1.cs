using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace newTestProj
{
    public class Game1 : Game
    {
        private IGraphicsDeviceManagerNew graphics;
        private ISpriteBatch spriteBatch;
        
        private Ball ball;
        private IList<RectangleRow> Rows;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManagerNew(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = false;
            this.DebugOutput = false;
            this.distanceBetweenRows = this.graphics.PreferredBackBufferWidth/7;
        }

        protected override void Initialize()
        {
            this.Rows = new List<RectangleRow>();
            this.lastCreatedRow = null;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.spriteBatch = new SpriteBatchNew(this.GraphicsDevice);

            this.ball = new Ball(this.Content.Load<Texture2D>("ball"),this.graphics)
            {
                DebugOutput = this.DebugOutput,
            };

            var row = new RectangleRow(this.graphics);
            row.InitializeRow(140, 120);
            this.lastCreatedRow = row;
            this.Rows.Add(row);
        }

        public bool DebugOutput;
        public bool StandStill;
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if(!this.StandStill)
            {
                this.UpdateRows(gameTime);
                this.UpdateBall(gameTime);
                this.CreateNewRow(gameTime);
            }
            else
            {
                this.UpdateBall(gameTime);

                if(this.isFirstTime)
                {
                    var row = this.CreateNewRow(gameTime);
                    if(row!=null)
                    {
                        row.TopRectangle.XPos = (float)this.graphics.PreferredBackBufferWidth/5*3;
                        row.BottomRectangle.XPos = (float)this.graphics.PreferredBackBufferWidth/5*3;
                        this.isFirstTime = false;
                    }
                }
            }

            base.Update(gameTime);
        }
        private bool isFirstTime = true;

        private int distanceBetweenRows;
        // private float distanceSinceLastRow;
        private RectangleRow lastCreatedRow;
        public RectangleRow CreateNewRow(GameTime gameTime)
        {
            // this.timeSinceLastRow += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // this.distanceSinceLastRow += lastCreatedRow?.XPos ?? 0;
            if((lastCreatedRow?.XPos ?? 0) < this.graphics.PreferredBackBufferWidth - distanceBetweenRows)
            {


                //TODO Basera på position ifrån senaste raden ist för tid. 
                // this.timeToNextRow = this.GetRandomBetween(1,3);

                var row = new RectangleRow(this.graphics);

                var height = this.graphics.PreferredBackBufferHeight;
                var startY = this.GetRandomBetween(Convert.ToInt32(height*0.3), Convert.ToInt32(height*0.8));
                row.InitializeRow(startY, this.GetRandomBetween(100,140));
                
                this.Rows.Add(row);
                this.lastCreatedRow = row;
                return row;
            }
            return null;
        }
        private int GetRandomBetween(int min, int max)
        {
            return new Random().Next(min, max);
        }

        private float totalTime;
        private void UpdateRows(GameTime gameTime)
        {
            totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            var rampup = totalTime/8;
            if(DebugOutput)
            {
                Console.WriteLine("rampup: " + rampup);
            }

            var rowsOutOfBound = new List<RectangleRow>();
            for(int i = 0; i < this.Rows.Count; i++)
            {
                var row = this.Rows[i];
                row.UpdateRow(gameTime, rampup);
                if(row.IsOutOfRange)
                {
                    rowsOutOfBound.Add(row);
                }
            }
            for(int i = 0; i < rowsOutOfBound.Count; i++)
            {
                var row = this.Rows[i];
                this.Rows.Remove(row);
            }

        }
        private void UpdateBall(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            if(keyboardState.IsKeyDown(Keys.W))
            {
                this.ball.MoveUp(gameTime.ElapsedGameTime.TotalSeconds);   
            }
            if(keyboardState.IsKeyDown(Keys.A))
            {
                this.ball.MoveLeft(gameTime.ElapsedGameTime.TotalSeconds);
            }
            if(keyboardState.IsKeyDown(Keys.S))
            {
                this.ball.MoveDown(gameTime.ElapsedGameTime.TotalSeconds);
            }
            if(keyboardState.IsKeyDown(Keys.D))
            {
                this.ball.MoveRight(gameTime.ElapsedGameTime.TotalSeconds);
            }
            if(keyboardState.IsKeyDown(Keys.Space))
            {
                this.ball.InitializeJump(gameTime);
            }
            this.ball.UpdatePosition(gameTime, this.Rows);
            this.CheckIsDead();
        }
        private void CheckIsDead()
        {
            if(this.ball.XPos < -(this.ball.Width/3)*2)
            {
                this.Initialize();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            if(this.ball.IsColiding)
            {
            this.GraphicsDevice.Clear(Color.Red);

            }
            else
            {
            this.GraphicsDevice.Clear(Color.BlueViolet);

            }

            // TODO: Add your drawing code here
            var vector = new Vector2(this.ball.XPos,this.ball.YPos);
            this.spriteBatch.Begin();
            spriteBatch.Draw(this.ball.GetTexture()
                , position: vector
                , sourceRectangle: null
                , color: Color.White
                , rotation: 0f
                , origin: Vector2.Zero
                , scale: this.ball.scale
                , effects: SpriteEffects.None
                , layerDepth: 0f
                );
            
            for(int i = 0; i < this.Rows.Count; i++)
            {
                var row = this.Rows[i];
                row.Draw(gameTime, this.spriteBatch);
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
