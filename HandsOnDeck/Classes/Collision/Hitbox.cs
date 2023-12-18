using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandsOnDeck.Classes;

namespace HandsOnDeck.Classes
{
    class Hitbox
    {
        public HitboxFrame CurrentFrame { get; set; }

        internal List<HitboxFrame> frames;
        private int counter;
        int fps;
        private double secondCounter = 0;

        public Hitbox()
        {
            frames = new List<HitboxFrame>();
            AddFrame(new HitboxFrame());
        }
        public Hitbox(Vector2 size, int xFrames, int xPos, GameObject owner)
        {
            frames = new List<HitboxFrame>();
            for (int i = 0; i < xFrames; i++)
            {
                List<Rectangle> rectangles = new List<Rectangle>();
                rectangles.Add(new Rectangle(xPos, (int)size.Y * i, (int)size.X, (int)size.Y));
                AddFrame(new HitboxFrame(rectangles,owner));
            }
            fps = (int)(2.5 * frames.Count());
        }

        public Hitbox(Vector2 size, Vector2 hitboxLocation)
        {
            frames = new List<HitboxFrame>();
            frames.Add(new HitboxFrame(new Rectangle((int)hitboxLocation.X, (int)hitboxLocation.Y,(int)size.X, (int)size.Y)));
        }

        public void AddFrame(HitboxFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }

    }
}
