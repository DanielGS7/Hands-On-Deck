 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandsOnDeck.Enums;

namespace HandsOnDeck.Classes
{
    internal class HitboxBatch
    {
        public Dictionary<EntityState, Hitbox> entityStates = new Dictionary<EntityState, Hitbox>();
        public Dictionary<EntityState, int> stateIndexes = new Dictionary<EntityState, int>();
        public void initialize()
        {
            stateIndexes.Add(EntityState.STATIC, 0);
            entityStates.Add(EntityState.STATIC, new Hitbox());
    }
    }
}
