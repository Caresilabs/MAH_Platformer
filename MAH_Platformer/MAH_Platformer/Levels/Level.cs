using MAH_Platformer.Entities;
using MAH_Platformer.Levels.Blocks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer.Levels
{
    public class Level
    {
        public const int MAX_ID = 512;

        public static float WIDTH;
        public static float HEIGHT;

        public enum Blocks
        {
            AIR,        // 0
            GROUND,     // 8
            LADDER,     // 16
            TELEPORT,   // 24
            SPIKE,      // 32
            JUMP        // 40
        }

        public enum Entities
        {
            PLAYER = (MAX_ID/LevelIO.ID_PER_BASE) / 2, // Start from middle
            SPAWN,
            BOULDER
        }

        private List<Entity> entities;
        private Block[,] blocks;
        private PlayerEntity player;

        public Level()
        {
            this.entities = new List<Entity>();
        }

        public void Update(float delta)
        {
            // update entities
            for (int i = 0; i < entities.Count; i++)
            {
                Entity entity = entities[i];

                entity.Update(delta);
                if (!entity.Alive && entity is PlayerEntity == false)
                {
                    entities.Remove(entity);
                }
            }

            // Update blocks
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    blocks[i, j].Update(delta);
                }
            }
        }

        public void InitLevel(int level = 1)
        {
            int[,] loadedMap = LevelIO.ReadLevel(1); //todo

            WIDTH = loadedMap.GetLength(0) * Block.BLOCK_SIZE; // TODO
            HEIGHT = loadedMap.GetLength(1) * Block.BLOCK_SIZE;

            this.blocks = new Block[loadedMap.GetLength(0), loadedMap.GetLength(1)];

            // Load blocks
            for (int j = 0; j < loadedMap.GetLength(1); j++)
            {
                for (int i = 0; i < loadedMap.GetLength(0); i++)
                {
                    int id = loadedMap[i, j];
                    Block block = GetBlock((int)(i * Block.BLOCK_SIZE), (int)(j * Block.BLOCK_SIZE), id);
                    block.Level = this;
                    block.Id = id;
                    blocks[i, j] = block;
                }
            }


            //for (int j = 0; j < loadedMap.GetLength(0)/2; j++)
            //{
            //    Block block = GetBlock((int)(j * Block.BLOCK_SIZE + Block.BLOCK_SIZE / 2), (int)(Block.BLOCK_SIZE * 7), 9);
            //    block.Level = this;
            //    blocks[j, 7] = block;
            //}

            // Fill with entities
            for (int j = 0; j < loadedMap.GetLength(1); j++)
            {
                for (int i = 0; i < loadedMap.GetLength(0); i++)
                {
                    int id = loadedMap[i, j];
                    FillBlock(i, j, id);
                }
            }
        }

        public void FillBlock(int x, int y, int id)
        {
            int baseId = id - (id % LevelIO.ID_PER_BASE) - Enum.GetValues(typeof(Entities)).Cast<int>().Min()*LevelIO.ID_PER_BASE;

            if (id > MAX_ID || id < Enum.GetValues(typeof(Entities)).Cast<int>().Min() * LevelIO.ID_PER_BASE)
                return;

            string nameSpace = "MAH_Platformer.Entities" + "."; // - Enum.GetValues(typeof(Blocks)).Cast<int>().Max()
            string name = Capitalise(((Entities)(baseId / LevelIO.ID_PER_BASE) + Enum.GetValues(typeof(Entities)).Cast<int>().Min()).ToString()) + "Entity";
            var objType = Type.GetType(nameSpace + name, true);
            Entity entity = (Entity)Activator.CreateInstance(objType, Assets.GetRegion(name), x * Block.BLOCK_SIZE, y * Block.BLOCK_SIZE);

            if (entity is PlayerEntity)
            {
                player = (PlayerEntity)entity;
                player.SetSpawn(x * Block.BLOCK_SIZE, y * Block.BLOCK_SIZE);
            }

            AddEntity(entity);
        }

        public Block GetBlock(int x, int y, int id)
        {
            int baseId = id - (id % LevelIO.ID_PER_BASE);

            if (baseId > (Enum.GetValues(typeof(Blocks)).Cast<int>().Max() * LevelIO.ID_PER_BASE)) 
                return new AirBlock(null, x, y); // No valid block

            // Using generics to get new block
            string nameSpace = "MAH_Platformer.Levels.Blocks" + ".";
            string name = Capitalise(((Blocks)(baseId / LevelIO.ID_PER_BASE)).ToString()) + "Block";
            var objType = Type.GetType(nameSpace + name, true);
            return (Block)Activator.CreateInstance(objType, Assets.GetRegion(name), x, y);
        }

        public Block GetBlock(int x, int y)
        {
            if (x < 0 || x > blocks.GetLength(0) - 1 || (y < 0 || y > blocks.GetLength(1) - 1)) return new AirBlock(null, x, y);
            return blocks[x, y];
        }

        public Block GetBlock(Vector2 pos)
        {
            return GetBlock((int)((pos.X + Block.BLOCK_SIZE / 2) / Block.BLOCK_SIZE), (int)((pos.Y + Block.BLOCK_SIZE / 2) / Block.BLOCK_SIZE));
        }

        public Block GetBlock(float x, float y)
        {
            return GetBlock((int)(x / Block.BLOCK_SIZE), (int)(y / Block.BLOCK_SIZE));
        }

        public Block GetBlockById(int id)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    if (blocks[i, j].Id == id)
                        return blocks[i, j];
                }
            }
            return null;
        }

        public Block[,] GetBlocks()
        {
            return blocks;
        }

        public void AddEntity(Entity entity)
        {
            entity.Level = this;
            entities.Add(entity);
        }

        public string Capitalise(string str)
        {
            if (String.IsNullOrEmpty(str))
                return String.Empty;
            return Char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        public List<Entity> GetEntities()
        {
            return entities;
        }

        public PlayerEntity GetPlayer()
        {
            return player;
        }
    }
}
