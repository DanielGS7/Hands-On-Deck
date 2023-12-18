using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HandsOnDeck.Classes;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using HandsOnDeck.Classes.Interface;
using HandsOnDeck.Enums;

namespace HandsOnDeck.Classes
{
    internal class GameObject : IGameObject, ICollideable
    {
        internal ContentManager _content;
        public Point _position;
        internal int _gameObjectID = 001;
        internal AnimationHandler animationHandler = new AnimationHandler();
        public CollisionHandler CollisionHandler { get; set; }
        internal Color _color = Color.White;
        internal EntityState _currentState = EntityState.STATIC;
        internal virtual EntityState CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
            }
        }

        public string _gameObjectTextureName;
        public Texture2D _gameObjectTexture;

        public GameObject(string textureName)
        {
            _gameObjectTextureName = textureName;
            CollisionHandler = new CollisionHandler();
        }

        public GameObject(string textureName, Vector2 size, EntityState[] entityStates, int xSprites, Point position)
        {
            _gameObjectTextureName = textureName;
            animationHandler._spriteSelectionSize = size;
            animationHandler.order = entityStates;
            int count = 0;
            CollisionHandler = new CollisionHandler();
            Array.ForEach(entityStates, animation =>
            {
                animationHandler.animationBatch.entityStates.Add(animation, new Animation(size, xSprites, (int)(count * size.X)));
                count += 1;
            });
            _position = position;
        }

        public GameObject(Point position, Vector2 size, Color color)
        {
            _position = position;
        }
        internal void LoadGameObjectSprite(ContentManager _content)
        {
            _gameObjectTexture = _content.Load<Texture2D>(_gameObjectTextureName);
            this._content = _content;
        }
        public void Update(GameTime gameTime)
        {
            animationHandler.Update(gameTime, this);
            CollisionHandler.Update(gameTime, this);
        }
        public void Draw()
        {
            Renderer.GetInstance._spriteBatch.Draw(_gameObjectTexture, _position.ToVector2(), animationHandler.SpriteSelection, Color.White, 0f, new Vector2(0, 0), new Vector2(ArrrGame.ScaleModifier, ArrrGame.ScaleModifier), SpriteEffects.None, 0f);
        }

        public bool CollidesWith(ICollideable other)
        {
            foreach (Rectangle hitbox in CollisionHandler.hitboxBatch.entityStates[EntityState.STATIC].frames[0].hitboxes)
            {
                foreach (Rectangle otherHitbox in other.CollisionHandler.hitboxBatch.entityStates[EntityState.STATIC].frames[0].hitboxes)
                {
                    if (hitbox.Intersects(otherHitbox))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
