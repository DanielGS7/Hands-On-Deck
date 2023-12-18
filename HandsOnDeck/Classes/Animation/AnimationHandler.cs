using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandsOnDeck.Classes;
using HandsOnDeck.Enums;

namespace HandsOnDeck.Classes
{
    internal class AnimationHandler
    {
        internal AnimationBatch animationBatch;
        internal EntityState[] order;
        internal bool _hasAnimation = false;
        internal Vector2 _spriteSelectionSize = new Vector2(16, 16);
        internal Vector2 _spriteSelectionCoord = new Vector2(0, 0);
        internal Rectangle spriteSelection;
        internal int _spriteIndex = 0;
        internal int _spriteCount = 1;

        public Rectangle SpriteSelection
        {
            get { return spriteSelection; }
            set { spriteSelection = value; }
        }

        public AnimationHandler()
        {
            animationBatch = new AnimationBatch();
        }

        internal void Update(GameTime gameTime, GameObject obj)
        {
            Animation currentAnimation = animationBatch.entityStates[obj.CurrentState];
            currentAnimation.Update(gameTime);
            SpriteSelection = currentAnimation.CurrentFrame.SourceRectangle;
        }
    }
}

