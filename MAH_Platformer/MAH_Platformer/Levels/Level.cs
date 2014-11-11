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

        public enum Blocks
        {
            AIR,
            GROUND
        }

        public enum Entities
        {
            PLAYER = (MAX_ID/LevelIO.ID_PER_BASE) / 2, // Start from middle
            BOULDER
        }

        private List<Entity> entities;
        private Block[,] blocks;
        private Vector2 lastCheckPoint;
        private PlayerEntity player;

        private int width;
        private int height;

        public Level()
        {
            this.entities = new List<Entity>();
            
        }

        public void InitLevel(int level = 1)
        {
            this.width = 20; // TODO
            this.height = 10;
            this.blocks = new Block[width, height];

            int[,] loadedMap = new int[width, height]; //todo
            int id = 33; // temp!

            // Load blocks
            for (int j = 0; j < loadedMap.GetLength(1); j++)
            {
                for (int i = 0; i < loadedMap.GetLength(0); i++)
                {
                    Block block = GetBlock(50, 50, id);
                    block.Level = this;
                    blocks[i, j] = block;
                }
            }

            // Fill with entities
            for (int j = 0; j < loadedMap.GetLength(1); j++)
            {
                for (int i = 0; i < loadedMap.GetLength(0); i++)
                {
                    FillBlock(i, j, id);
                }
            }
        }

        public void FillBlock(int x, int y, int id)
        {
            int baseId = id - (id % LevelIO.ID_PER_BASE);

            string nameSpace = "MAH_Platformer.Entities" + "."; // - Enum.GetValues(typeof(Blocks)).Cast<int>().Max()
            string name = Capitalise(((Entities)(baseId - Enum.GetValues(typeof(Blocks)).Cast<int>().Min())).ToString()) + "Entity";
            var objType = Type.GetType(nameSpace + name, true);
            Entity entity = (Entity)Activator.CreateInstance(objType, Assets.GetRegion(name), x, y);
        }

        public Block GetBlock(int x, int y)
        {
            return blocks[x, y];
        }

        public Block GetBlock(int x, int y, int id)
        {
            int baseId = id - (id % LevelIO.ID_PER_BASE);

            if (id > (Enum.GetValues(typeof(Blocks)).Cast<int>().Max() / LevelIO.ID_PER_BASE)) return new Block(null, x, y); // No valid block

            // Using generics to get new block
            string nameSpace = "MAH_Platformer.Levels.Blocks" + ".";
            string name = Capitalise(((Blocks)(baseId / LevelIO.ID_PER_BASE)).ToString()) + "Block";
            var objType = Type.GetType(nameSpace + name, true);
            return (Block)Activator.CreateInstance(objType, Assets.GetRegion(name), x, y);
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

    }
}
