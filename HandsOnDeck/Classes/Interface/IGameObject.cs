using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDeck.Classes
{
    internal interface IGameObject
    {
        void Update(GameTime gameTime);
        void Draw();
    }
}
