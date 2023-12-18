using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using HandsOnDeck.Classes;
using HandsOnDeck.Enums;

namespace HandsOnDeck.Classes
{

    internal class PlayerController : MovementController
    {
        private const float Acceleration = 0.01f;
        private const float Deceleration = 0.02f;

        public Player Player { get; private set; }
        public Vector2 Velocity { get; private set; }
        public InputReader Input { get; private set; }
        public Point RawMovement { get; private set; }

        public PlayerController(Player player)
        {
            Player = player;
            Input = InputReader.GetInstance;

        }

        public void Update(GameTime gameTime)
        {
            Debug.WriteLine("ping");
            KeyboardState currentInput = Keyboard.GetState();
            Point movementDirection = Point.Zero;

            if (currentInput.IsKeyDown(Keys.Right))
            {
                movementDirection.X = 1;
                Player.CurrentState = EntityState.MOVE_RIGHT;
            }
            else if (currentInput.IsKeyDown(Keys.Left))
            {
                movementDirection.X = -1;
                Player.CurrentState = EntityState.MOVE_LEFT;
            }

            if (currentInput.IsKeyDown(Keys.Up))
            {
                movementDirection.Y = -1;
                Player.CurrentState = EntityState.MOVE_UP;
            }
            else if (currentInput.IsKeyDown(Keys.Down))
            {
                movementDirection.Y = 1;
                Player.CurrentState = EntityState.MOVE_DOWN;
            }

            // Update velocity based on movement direction
            Velocity += new Vector2(movementDirection.X * Acceleration, movementDirection.Y * Acceleration);

            // Apply deceleration if not actively moving
            if (movementDirection == Point.Zero)
            {
                Velocity *= (1 - Deceleration);
            }

            // Update boat position
            Player._position += new Point((int)(Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds), (int)(Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds));


            // Clamp velocity to a maximum value if needed
            // Velocity = Vector2.Clamp(Velocity, -maxSpeed, maxSpeed);

            // Set RawMovement based on velocity
            RawMovement = new Point((int)Velocity.X, (int)Velocity.Y);
        }
    }
}
