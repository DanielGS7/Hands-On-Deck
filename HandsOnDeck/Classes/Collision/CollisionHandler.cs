using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDeck.Classes
{
    internal class CollisionHandler
    {
        internal HitboxBatch hitboxBatch;
        internal bool _hasMultipleHitbox = false;

        internal List<Rectangle> hitboxSelection;
        internal int hitboxIndex = 0;
        internal int hitboxCount = 1;

        public List<Rectangle> HitboxSelection
        {
            get { return hitboxSelection; }
            set { hitboxSelection = value; }
        }
         
        public CollisionHandler()
        {
            hitboxBatch = new HitboxBatch();
        }

        internal void Update(GameTime gameTime, GameObject obj)
        {
            if (_hasMultipleHitbox)
            {
                Hitbox currentHitbox = hitboxBatch.entityStates[obj.CurrentState];
                currentHitbox.Update(gameTime);
                HitboxSelection = currentHitbox.CurrentFrame.hitboxes;
            }
        }
    }
}

