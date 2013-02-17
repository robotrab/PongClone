using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PongClone
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private int screenWidth;
        private int screenHeight;
        private SpriteFont arial;

        private Input input;
        private Bat rightBat;
        private Bat leftBat;
        private Ball ball;
        private int resetTimer;
        private bool resetTimerInUse;
        private bool lastScored;
        public static GameStates gamestate;
        private Menu menu;

        public enum GameStates
        {
            Menu,
            Running,
            End
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = 800;
            screenHeight = 600;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            menu = new Menu();
            gamestate = GameStates.Menu;

            rightBat = new Bat(Content, new Vector2(screenWidth, screenHeight), false);
            leftBat = new Bat(Content, new Vector2(screenWidth, screenHeight), true);
            input = new Input();
            ball = new Ball(Content, new Vector2(screenWidth, screenHeight));
            ball.Reset(true);

            resetTimer = 0;
            resetTimerInUse = true;
            lastScored = false;

            
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arial = Content.Load<SpriteFont>("Arial");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            input.Update();

            if (gamestate == GameStates.Running)
            {
                if (leftBat.GetPoints() > 9)
                {
                    menu.InfoText = "Left Player Wins";
                    gamestate = GameStates.End;
                }
                else if (rightBat.GetPoints() > 9)
                {
                    menu.InfoText = "Right Player Wins";
                    gamestate = GameStates.End;
                }

                if (resetTimerInUse)
                {
                    resetTimer++;
                    ball.Stop();
                }

                if (resetTimer == 120)
                {
                    resetTimerInUse = false;
                    ball.Reset(lastScored);
                    resetTimer = 0;
                }

                if (rightBat.GetType() != typeof(PongClone.AIBat))
                {
                    if (input.LeftDown)
                        leftBat.MoveDown();
                    else if (input.LeftUp)
                        leftBat.MoveUp();

                    if (input.RightDown)
                        rightBat.MoveDown();
                    else if (input.RightUp)
                        rightBat.MoveUp();
                }
                else if (rightBat.GetType() == typeof(PongClone.AIBat))
                {
                    if (input.LeftDown)
                        leftBat.MoveDown();
                    else if (input.LeftUp)
                        leftBat.MoveUp();

                    if (input.RightDown)
                        leftBat.MoveDown();
                    else if (input.RightUp)
                        leftBat.MoveUp();
                }

                leftBat.UpdatePosition(ball);
                rightBat.UpdatePosition(ball);
                ball.UpdatePosition();

                if (ball.GetDirection() > 1.5f * Math.PI || ball.GetDirection() < 0.5f * Math.PI)
                {
                    if (rightBat.GetSize().Intersects(ball.GetSize()))
                        ball.BatHit(CheckHitLocation(rightBat));
                }
                else if (leftBat.GetSize().Intersects(ball.GetSize()))
                    ball.BatHit(CheckHitLocation(leftBat));

                if (!resetTimerInUse)
                {
                    if (ball.GetPosition().X > screenWidth)
                    {
                        resetTimerInUse = true;
                        lastScored = true;
                        leftBat.IncrementPoints();
                    }
                    else if (ball.GetPosition().X < 0)
                    {
                        resetTimerInUse = true;
                        lastScored = false;
                        rightBat.IncrementPoints();
                    }
                }
            }
            else if (gamestate == GameStates.Menu)
            {
                if (input.RightDown || input.LeftDown)
                    menu.Iterator++;
                else if (input.RightUp || input.LeftUp)
                    menu.Iterator--;

                if (input.MenuSelect)
                {
                    if (menu.Iterator == 0)
                    {
                        gamestate = GameStates.Running;
                        SetUpSingle();
                    }
                    else if (menu.Iterator == 1)
                    {
                        gamestate = GameStates.Running;
                        SetUpMulti();
                    }
                    else if (menu.Iterator == 2)
                        this.Exit();

                    menu.Iterator = 0;
                }
            }
            else if (gamestate == GameStates.End)
            {
                if (input.MenuSelect)
                    gamestate = GameStates.Menu;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            leftBat.Draw(spriteBatch);
            rightBat.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.DrawString(arial, leftBat.GetPoints().ToString(), new Vector2(screenWidth / 4 - arial.MeasureString(rightBat.GetPoints().ToString()).X, 20), Color.White);
            spriteBatch.DrawString(arial, rightBat.GetPoints().ToString(), new Vector2(screenWidth / 4 * 3 - arial.MeasureString(rightBat.GetPoints().ToString()).X, 20), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private int CheckHitLocation(Bat bat)
        {
            int block = 0;

            if (ball.GetPosition().Y < bat.GetPosition().Y + bat.GetSize().Height / 20) 
                block = 1;
            else if (ball.GetPosition().Y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 2) 
                block = 2;
            else if (ball.GetPosition().Y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 3) 
                block = 3;
            else if (ball.GetPosition().Y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 4) 
                block = 4;
            else if (ball.GetPosition().Y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 5) 
                block = 5;
            else if (ball.GetPosition().Y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 6) 
                block = 6;
            else if (ball.GetPosition().Y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 7) 
                block = 7;
            else if (ball.GetPosition().Y < bat.GetPosition().Y + bat.GetSize().Height / 10 * 8) 
                block = 8;
            else if (ball.GetPosition().Y < bat.GetPosition().Y + bat.GetSize().Height / 20 * 19) 
                block = 9;
            else 
                block = 10;

            return block;
        }

        private void SetUpSingle()
        {
            rightBat = new AIBat(Content, new Vector2(screenWidth, screenHeight), false);
            leftBat = new Bat(Content, new Vector2(screenWidth, screenHeight), true);
        }

        private void SetUpMulti()
        {
            rightBat = new Bat(Content, new Vector2(screenWidth, screenHeight), false);
            leftBat = new Bat(Content, new Vector2(screenWidth, screenHeight), true);
        }
    }
}
