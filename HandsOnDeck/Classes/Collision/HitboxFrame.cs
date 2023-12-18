using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandsOnDeck.Enums;
using HandsOnDeck.Classes;

namespace HandsOnDeck.Classes
{
    class HitboxFrame
    {
        public List<Rectangle> hitboxes;
        public List<HitboxType> hitboxTypes;
        public GameObject owner;


        public HitboxFrame(List<Rectangle> sourceRectangles, GameObject owner)
        {
            this.hitboxes= sourceRectangles;
            this.owner = owner; 
        }

        public HitboxFrame()
        {
            this.hitboxes = new List<Rectangle>();
            hitboxTypes = new List<HitboxType>();
            hitboxTypes.Add(HitboxType.StaticTerrain);
        }

        public HitboxFrame(Rectangle rectangle)
        {
            this.hitboxes = new List<Rectangle>();
            this.hitboxes.Add(rectangle);
            hitboxTypes = new List<HitboxType>();
            hitboxTypes.Add(HitboxType.StaticTerrain);
        }
    }

}
