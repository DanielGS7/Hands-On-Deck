using HandsOnDeck.Classes.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using TiledSharp;
using HandsOnDeck.Enums;

namespace HandsOnDeck.Classes.Level
{
    internal class LevelLoader : ICollideable
    {
        GraphicsDevice graphics;
        List<TmxMap> levels = new List<TmxMap>();
        TmxMap currentLevelMap;
        Texture2D tileset;
        Texture2D background;

        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;
        float scale = 6f;

        public CollisionHandler CollisionHandler { get; set; }

        internal void LoadContent(ContentManager content, GraphicsDeviceManager _graphics)
        {
            graphics = _graphics.GraphicsDevice;
            levels.Add(new TmxMap("Content/level1.tmx"));
            currentLevelMap = levels[0];
            tileset = content.Load<Texture2D>(currentLevelMap.Tilesets[0].Name.ToString());
            tileWidth = currentLevelMap.Tilesets[0].TileWidth;
            tileHeight = currentLevelMap.Tilesets[0].TileHeight;

            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            background = content.Load<Texture2D>("background");
            CollisionHandler = GenerateHitBoxes(currentLevelMap);
            }



        private CollisionHandler GenerateHitBoxes(TmxMap currentLevelMap)
        {
            TmxLayer terrain = currentLevelMap.Layers[0];
            TmxLayerTile currentTile = terrain.Tiles[0];
            List<Vector2> hitboxesToAdd = new List<Vector2>();
            for (var i = 0; i < terrain.Tiles.Count; i++)
            {
                currentTile = terrain.Tiles[i];
                if (currentTile.Gid != 0)
                {
                    if (isHitboxNecessary(currentLevelMap, currentTile))
                    {
                        hitboxesToAdd.Add(new Vector2(currentTile.X, currentTile.Y));
                    }
                }
            }
            CollisionHandler collisionHandler = new CollisionHandler();
            HitboxBatch terrainHitbox = collisionHandler.hitboxBatch;
            terrainHitbox.initialize();
            List<Vector2> coveredTiles = new List<Vector2>();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x <= 18; x++)
                {
                    if (hitboxesToAdd.Contains(new Vector2(x, y))) Debug.Write(" X ");
                    else Debug.Write(" O ");
                }
                Debug.WriteLine("");
            }
            foreach (Vector2 hitboxLocation in hitboxesToAdd)
            {
                if (!coveredTiles.Contains(hitboxLocation))
                {
                    // Find the width of the hitbox by moving to the right until a tile with Gid 0 is encountered
                    int hitboxWidth = 1;
                    int currentX = (int)hitboxLocation.X + 1;
                    while (currentX < currentLevelMap.Width && terrain.Tiles[currentX + (int)hitboxLocation.Y * currentLevelMap.Width].Gid != 0)
                    {
                        hitboxWidth++;
                        currentX++;
                    }

                    // Find the height of the hitbox by moving down until a tile with Gid 0 is encountered
                    int hitboxHeight = 1;
                    int currentY = (int)hitboxLocation.Y + 1;
                    while (currentY < currentLevelMap.Height && Enumerable.Repeat(currentX, hitboxWidth).Select(x => x + currentY * currentLevelMap.Width).All(x => terrain.Tiles[x].Gid != 0))
                    {
                        hitboxHeight++;
                        currentY++;
                    }


                    currentX = (int)hitboxLocation.X + hitboxWidth - 1;
                    currentY = (int)hitboxLocation.Y;
                    while (currentY < currentLevelMap.Height && currentX + hitboxWidth - 1 < currentLevelMap.Width 
                        && Enumerable.Range(currentX+currentY*currentLevelMap.Width, hitboxWidth).All(x => terrain.Tiles[x].Gid == 0))
                    {
                        hitboxWidth--;
                        currentY++;
                    }

                    // Check if the hitbox needs to be trimmed on the bottom side
                    currentX = (int)hitboxLocation.X;
                    currentY = (int)hitboxLocation.Y + hitboxHeight - 1;
                    while (currentX < currentLevelMap.Width && currentY + hitboxHeight - 1 < currentLevelMap.Height && Enumerable.Range(currentY * currentLevelMap.Width + currentX, hitboxWidth).All(i => terrain.Tiles[i].Gid == 0))
                    {
                        hitboxHeight--;
                        currentX++;
                    }

                    // Add the hitbox to the list
                    Rectangle hitboxToAdd = new Rectangle((int)hitboxLocation.X, (int)hitboxLocation.Y, hitboxWidth, hitboxHeight);
                    terrainHitbox.entityStates[EntityState.STATIC].frames[0].hitboxes.Add(hitboxToAdd);

                    // Mark the covered tiles as covered
                    for (int y = (int)hitboxLocation.Y; y < hitboxLocation.Y + hitboxHeight; y++)
                    {
                        for (int x = (int)hitboxLocation.X; x < hitboxLocation.X + hitboxWidth; x++)
                        {
                            coveredTiles.Add(new Vector2(x, y));
                        }
                    }
                }
            }
            collisionHandler.hitboxBatch = terrainHitbox;
            return collisionHandler;
        }
        private bool isHitboxNecessary(TmxMap currentLevelMap, TmxLayerTile currentTile)
        {
            TmxLayer tileMap = currentLevelMap.Layers[0];
            List<TmxLayerTile> neighbours = new List<TmxLayerTile>();
            for (int i = currentTile.X - 1; i <= currentTile.X + 1; i++)
            {
                for (int j = currentTile.Y - 1; j <= currentTile.Y + 1; j++)
                {
                    if (i >= 0 && i < currentLevelMap.Width && j >= 0 && j < currentLevelMap.Height)
                    {
                        neighbours.Add(tileMap.Tiles[i + j * currentLevelMap.Width]);
                    }
                }
            }
            return neighbours.Any(tile => tile.Gid == 0);
        }
        public void DrawBehind()
        {
        Renderer.GetInstance._spriteBatch.Draw(background, new Rectangle(0, 0, ArrrGame.ProgramWidth*(int)scale, ArrrGame.ProgramHeight * (int)scale), new Rectangle(0, 0, ArrrGame.ProgramWidth, ArrrGame.ProgramHeight), Color.Yellow);
        }

        public void DrawLevel()
        {
            for (int i = 0; i < 2; i++)
            {
                for (var j = 0; j < currentLevelMap.Layers[i].Tiles.Count; j++)
                {
                    int gid = currentLevelMap.Layers[i].Tiles[j].Gid;
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        float x = (j % currentLevelMap.Width) * currentLevelMap.TileWidth * scale;
                        float y = (float)Math.Floor(j / (double)currentLevelMap.Width) * currentLevelMap.TileHeight * scale;

                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                        Renderer.GetInstance._spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth * (int)scale, tileHeight * (int)scale), tilesetRec, Color.White);
                    }
                }
            }
        }
        internal void DrawOverlay()
        {
            for (int i = 2; i < 3; i++)
            {
                for (var j = 0; j < currentLevelMap.Layers[i].Tiles.Count; j++)
                {
                    int gid = currentLevelMap.Layers[i].Tiles[j].Gid;
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        float x = (j % currentLevelMap.Width) * currentLevelMap.TileWidth * scale;
                        float y = (float)Math.Floor(j / (double)currentLevelMap.Width) * currentLevelMap.TileHeight * scale;

                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                        Renderer.GetInstance._spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth * (int)scale, tileHeight * (int)scale), tilesetRec, Color.White);
                    }
                }
            }
        }

        internal void DrawUI()
        {
            for (int i = 3; i < 4; i++)
            {
                for (var j = 0; j < currentLevelMap.Layers[i].Tiles.Count; j++)
                {
                    int gid = currentLevelMap.Layers[i].Tiles[j].Gid;
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        float x = (j % currentLevelMap.Width) * currentLevelMap.TileWidth * scale;
                        float y = (float)Math.Floor(j / (double)currentLevelMap.Width) * currentLevelMap.TileHeight * scale;

                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                        Renderer.GetInstance._spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth * (int)scale, tileHeight * (int)scale), tilesetRec, Color.White);
                    }
                }
            }
        }

        internal void DrawHitbox()
        {
            foreach (var box in CollisionHandler.hitboxBatch.entityStates[EntityState.STATIC].frames[0].hitboxes)
            {
                int tileFrame = 38;
                int column = tileFrame % tilesetTilesWide;
                int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                float x = (box.X) * currentLevelMap.TileWidth * scale;
                float y = (box.Y) * currentLevelMap.TileHeight * scale;

                Renderer.GetInstance._spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, (int)(box.Width*tileWidth*scale),(int)(box.Height*tileHeight*scale)), tilesetRec, Color.AliceBlue);
            }
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
