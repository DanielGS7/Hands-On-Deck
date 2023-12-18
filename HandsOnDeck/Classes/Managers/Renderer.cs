using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using HandsOnDeck.Classes;
using HandsOnDeck.Enums;
using System.Collections.Generic;
using RenderTargetUsage = Microsoft.Xna.Framework.Graphics.RenderTargetUsage;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using HandsOnDeck.Classes.Level;
using SharpDX.Direct3D9;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;
using System.Diagnostics;

namespace HandsOnDeck.Classes { 

public sealed class Renderer
{
    internal SpriteBatch _spriteBatch;
    GraphicsDeviceManager graphics;

    private static Renderer renderer;
    private static LevelLoader levelLoader;
    private static object syncRoot = new object();
    static List<Entity> entities = new List<Entity>();
    static List<IGameObject> gameObjects = new List<IGameObject>();
    private Renderer() { }

    internal void Initialize(GraphicsDeviceManager _graphics)
    {
        PresentationParameters pp = _graphics.GraphicsDevice.PresentationParameters;
            ArrrGame.RenderTarget = new RenderTarget2D(_graphics.GraphicsDevice, ArrrGame.ProgramWidth, ArrrGame.ProgramHeight, false,
    SurfaceFormat.Color, DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

            entities = new List<Entity>();
            entities.Add(new Entity("boatsheet", new Vector2(128), new EntityState[] { EntityState.MOVE_DOWN, EntityState.IDLE, EntityState.MOVE_RIGHT, EntityState.IDLE2, EntityState.MOVE_UP }, 1, new Point(0, 0)));
            graphics = _graphics;
            levelLoader = new LevelLoader();
        }

    public static Renderer GetInstance
    {
        get
        {
            if (renderer == null)
            {
                lock (syncRoot)
                {
                    if (renderer == null)
                        renderer = new Renderer();
                }
            }
            return renderer;
        }
    }

    public void LoadContent(ContentManager content, SpriteBatch _spriteBatch)
    {
        this._spriteBatch = _spriteBatch;
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.LoadGameObjectSprite(content);
        }
        foreach (Entity entity in entities)
        {
            entity.LoadEntitySprite(content);
        }
    }
    public void Update(GameTime gameTime)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.Update(gameTime);
        }
        foreach (Entity entity in entities)
            {
                //Debug.WriteLine("mow"); (werkt)
                entity.Update(gameTime);
            }
        }

    public void Draw()
    {
        Renderer.GetInstance._spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.Draw();
        }
        foreach (Entity entity in entities)
        {
            entity.Draw();
        }
        GetInstance._spriteBatch.End();
    }

    public void addGameObject()
    {

    }

    internal void addEntity(String spriteName)
    {
        entities.Add(new Entity(spriteName));
    }
    internal void addEntity(Entity entity)
    {
        entities.Add(entity);
    }
    internal void addEntity(string textureName, Vector2 size, EntityState[] entityStates, int xSprites, Point position)
    {
        entities.Add(new Entity(textureName, size, entityStates, xSprites, position));
    }

    public void removeGameObject(int ID)
    {

    }
    public void removeEntity(int ID)
    {

    }

}
}