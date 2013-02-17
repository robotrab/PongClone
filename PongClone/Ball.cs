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
            CheckWallHit();
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

        private void CheckWallHit()
        {
            while (direction > 2 * Math.PI)
                direction -= 2 * Math.PI;

            while (direction < 0)
                direction += 2 * Math.PI;

            if (position.Y <= 0 || (position.Y > resetPos.Y * 2 - size.Height))
                direction = 2 * Math.PI - direction;
        }

        public Rectangle GetSize()
        {
            return size;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public double GetDirection()
        {
            return direction;
        }

        public void BatHit(int block)
        {
            if (direction > Math.PI * 1.5f || direction < Math.PI * 0.5f)
            {
                switch (block)
                {
                    case 1:
                        direction = MathHelper.ToRadians(220);
                        break;
                    case 2:
                        direction = MathHelper.ToRadians(215);
                        break;
                    case 3:
                        direction = MathHelper.ToRadians(200);
                        break;
                    case 4:
                        direction = MathHelper.ToRadians(195);
                        break;
                    case 5:
                        direction = MathHelper.ToRadians(180);
                        break;
                    case 6:
                        direction = MathHelper.ToRadians(180);
                        break;
                    case 7:
                        direction = MathHelper.ToRadians(165);
                        break;
                    case 8:
                        direction = MathHelper.ToRadians(130);
                        break;
                    case 9:
                        direction = MathHelper.ToRadians(115);
                        break;
                    case 10:
                        direction = MathHelper.ToRadians(110);
                        break;
                }
            }
            else
            {
                switch (block)
                {
                    case 1:
                        direction = MathHelper.ToRadians(290);
                        break;
                    case 2:
                        direction = MathHelper.ToRadians(295);
                        break;
                    case 3:
                        direction = MathHelper.ToRadians(310);
                        break;
                    case 4:
                        direction = MathHelper.ToRadians(345);
                        break;
                    case 5:
                        direction = MathHelper.ToRadians(0);
                        break;
                    case 6:
                        direction = MathHelper.ToRadians(0);
                        break;
                    case 7:
                        direction = MathHelper.ToRadians(15);
                        break;
                    case 8:
                        direction = MathHelper.ToRadians(50);
                        break;
                    case 9:
                        direction = MathHelper.ToRadians(65);
                        break;
                    case 10:
                        direction = MathHelper.ToRadians(70);
                        break;
                }
            }
        }

    }
}
