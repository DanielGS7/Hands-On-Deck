using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDeck.Classes.Interface
{
    internal interface ICollideable
    {
        CollisionHandler CollisionHandler { get; }
        bool CollidesWith(ICollideable other);
    }
}
