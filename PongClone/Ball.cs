using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PongClone
{
    class Ball
    {
        private bool isVisible;
        private Vector2 position;
        private double direction;
        private Texture2D texture;
        private Rectangle size;
        private float speed;
        private float moveSpeed;
        private Vector2 resetPos;
        Random rand;

        public Ball(ContentManager content, Vector2 screenSize)
        {
            moveSpeed = 8f;
            speed = 0;
            texture = content.Load<Texture2D>("ball");
            direction = 0;
            size = new Rectangle(0, 0, texture.Width, texture.Height);
            resetPos = new Vector2(screenSize.X / 2, screenSize.Y / 2);
            position = resetPos;
            rand = new Random();
            isVisible = true;
        }

        public void Draw(SpriteBatch batch)
        {
            if (isVisible)
                batch.Draw(texture, position, Color.White);
        }

        public void UpdatePosition()
        {
            size.X = (int)position.X;
            size.Y = (int)position.Y;
            position.X += speed * (float)Math.Cos(direction);
            position.Y += speed * (float)Math.Sin(direction);
        }

        public void Stop()
        {
            isVisible = false;
            speed = 0;
        }

        public void Reset(bool left)
        {
            if (left)
                direction = 0;
            else
                direction = Math.PI;

            position = resetPos;
            isVisible = true;
            speed = moveSpeed;
        }
    }
}
