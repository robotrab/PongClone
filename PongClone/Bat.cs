using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PongClone
{
    //private Bat rightBat;
    //private Bat leftBat;

    class Bat
    {
        private Vector2 position;
        private int moveSpeed;
        private Rectangle size;
        private int points;
        private int yHeight;
        private Texture2D texture;

        public Bat(ContentManager content, Vector2 screenSize, bool side)
        {
            moveSpeed = 6;
            points = 0;
            texture = content.Load<Texture2D>("bat");
            size = new Rectangle(0, 0, texture.Width, texture.Height);
            if (side) 
                position = new Vector2(30, screenSize.Y / 2 - size.Height / 2);
            else
                position = new Vector2(screenSize.X - 30 - size.Width, screenSize.Y / 2 - size.Height / 2);
            yHeight = (int)screenSize.Y;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
        }

        private void SetPosition(Vector2 position)
        {
            if (position.Y < 0)
                position.Y = 0;

            if (position.Y > yHeight - size.Height)
                position.Y = yHeight - size.Height;

            this.position = position;
        }

        public void MoveUp()
        {
            SetPosition(position + new Vector2(0, -moveSpeed));
        }

        public void MoveDown()
        {
            SetPosition(position + new Vector2(0, moveSpeed));
        }

        public virtual void UpdatePosition(Ball ball)
        {
            size.X = (int)position.X;
            size.Y = (int)position.Y;
        }
    }
}
