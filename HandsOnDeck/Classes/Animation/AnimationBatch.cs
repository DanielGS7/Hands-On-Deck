using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandsOnDeck.Enums;

namespace HandsOnDeck.Classes
{
    internal class AnimationBatch
    {
        public Dictionary<EntityState, Animation> entityStates = new Dictionary<EntityState, Animation>();
        public Dictionary<EntityState, int> stateIndexes = new Dictionary<EntityState, int>();
    }
}
