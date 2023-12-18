using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandsOnDeck.Enums;
using System.Diagnostics;

namespace HandsOnDeck.Classes
{
    internal class MovementController
    {
        internal void Update(GameTime gameTime, Entity entity)
        {
            KeyboardState currentInput = Keyboard.GetState();
            Debug.WriteLine(player.CurrentState);
            if (currentInput.IsKeyDown(Keys.Right) && currentInput.IsKeyUp(Keys.Up))
            {
                player._position.X += player.speed;
                player.CurrentState = EntityState.MOVE_RIGHT;
            }
            else if (currentInput.IsKeyDown(Keys.Left) && currentInput.IsKeyUp(Keys.Up))
            {
                player._position.X -= player.speed;
                player.CurrentState = EntityState.MOVE_LEFT;
            }
            else if (currentInput.IsKeyDown(Keys.Up))
            {
                player._position.Y -= player.speed;
                player.CurrentState = EntityState.MOVE_UP;
            }
            else if (currentInput.IsKeyDown(Keys.Down) && currentInput.IsKeyUp(Keys.Up))
            {
                player._position.Y += player.speed;
                player.CurrentState = EntityState.MOVE_DOWN;
            }
            else player.CurrentState = EntityState.IDLE;
        }
    }
}
