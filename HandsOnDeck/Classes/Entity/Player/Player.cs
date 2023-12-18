using System.Diagnostics;
using HandsOnDeck.Classes.Interface;
using Microsoft.Xna.Framework;
using HandsOnDeck.Enums;

namespace HandsOnDeck.Classes
{
    internal class Player : Entity, ICollideable
    {
        new PlayerController movement;
        public Player(string textureName) : base(textureName)
        {
            animationHandler._spriteCount = 5;
            _position.X = 5;
            _position.Y = 890;
            speed = 20;
        }
        public Player(string textureName, Vector2 size, EntityState[] entityStates, int xSprites, Point position) : base(textureName, size, entityStates, xSprites, position)
        {
            speed = 12;
            animationHandler._spriteCount = 5;
            animationHandler._hasAnimation = false;
        }

        public void Initialize()
        {
            movement = new PlayerController(this);
        }
        public override void Update(GameTime gameTime, Entity player)
        {
            Debug.WriteLine("ono");
            movement.Update(gameTime, this);
            base.Update(gameTime);
        }
    }
}
