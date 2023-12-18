using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct3D9;
using HandsOnDeck.Classes;
using HandsOnDeck.Classes.Interface;
using HandsOnDeck.Enums;
using System.Diagnostics;

namespace HandsOnDeck.Classes
{
    internal class Entity : GameObject, ICollideable
    {
        public int speed = 1;
        internal MovementController movement = new MovementController();
        internal EntityState currentState = EntityState.IDLE;
        internal override EntityState CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                currentState = value;
            }
        }
        public Entity(string textureName) : base(textureName)
        {
            animationHandler._hasAnimation = false;
        }
        internal Entity(string textureName, Vector2 size, EntityState[] entityStates, int xSprites, Point position) : base(textureName, size, entityStates, xSprites, position)
        {
            animationHandler._hasAnimation = true;
        }

        public void LoadEntitySprite(ContentManager _content)
        {
            LoadGameObjectSprite(_content);
        }
        public void Update(GameTime gameTime, Entity player)
        {
            Debug.WriteLine("meows");
            CollisionHandler.Update(gameTime, this);
            movement.Update(gameTime, player);
            Update(gameTime);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            Draw();
        }

    }
}