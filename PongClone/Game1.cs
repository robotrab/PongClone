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

        private Input input;
        private Bat rightBat;
        private Bat leftBat;
        private Ball ball;

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

            rightBat = new Bat(Content, new Vector2(screenWidth, screenHeight), false);
            leftBat = new Bat(Content, new Vector2(screenWidth, screenHeight), true);
            input = new Input();
            ball = new Ball(Content, new Vector2(screenWidth, screenHeight));

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

            if (input.LeftDown)
                leftBat.MoveDown();
            else if (input.LeftUp)
                leftBat.MoveUp();

            if (input.RightDown)
                rightBat.MoveDown();
            else if (input.RightUp)
                rightBat.MoveUp();

            if (ball.GetDirection() > 1.5f * Math.PI || ball.GetDirection() < 0.5f * Math.PI)
            {
                if (rightBat.GetSize().Intersects(ball.GetSize()))
                    ball.BatHit(CheckHitLocation(rightBat));
                else if (leftBat.GetSize().Intersects(ball.GetSize()))
                    ball.BatHit(CheckHitLocation(leftBat));
            }

            if (ball.GetPosition().X > screenWidth)
                ball.Reset(true);
            else if (ball.GetPosition().X < 0)
                ball.Reset(false);

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
    }
}
