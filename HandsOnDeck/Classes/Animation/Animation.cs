using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDeck.Classes
{
    class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        private int counter;
        int fps;
        private double secondCounter = 0;
        public Animation(Vector2 size, int xFrames, int xPos)
        {
            frames = new List<AnimationFrame>();
            for (int i = 0; i < xFrames; i++)
            {
                frames.Add(new AnimationFrame(new Rectangle(xPos, (int)size.Y * i, (int)size.X, (int)size.Y)));
            }
            fps = (int)(2.5 * frames.Count());
        }

        public void AddFrame(AnimationFrame frame)
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
