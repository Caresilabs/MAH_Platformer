using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_Platformer
{
    /**
* Assets loads all the needed assets and a non cumbersome way to retrieve them
*/
    public class Assets
    {
        public const bool SOUND = false;

        private static Dictionary<String, TextureRegion> regions;
        private static ContentManager manager;

        public static Texture2D items;
        public static Texture2D ui;

        public static Texture2D bg1;
        public static Texture2D bg2;
        public static Texture2D bg3;

        public static Texture2D character;
        public static Texture2D crawler;
        public static Texture2D boss;

        public static SpriteFont font;

        public static SoundEffect deathSound;
        public static Song music;

        public static void Load(ContentManager manager)
        {
            Assets.manager = manager;
            regions = new Dictionary<string, TextureRegion>();

            // load our sprite sheet
            items = manager.Load<Texture2D>("Graphics/items");
            ui = manager.Load<Texture2D>("Graphics/ui");
            character = manager.Load<Texture2D>("Graphics/character");
            crawler = manager.Load<Texture2D>("Graphics/crawler");
            boss = manager.Load<Texture2D>("Graphics/boss");

            bg1 = manager.Load<Texture2D>("Graphics/bg1");
            bg2 = manager.Load<Texture2D>("Graphics/bg2");
            bg3 = manager.Load<Texture2D>("Graphics/bg3");

            // Entities
            LoadRegion("PlayerEntity", character, 0, 160, 48, 64);
            LoadRegion("SpawnEntity", items, 32, 512, 32, 32);
            LoadRegion("BoulderEntity", items, 0, 0, 32, 32);
            LoadRegion("BulletEntity", items, 36, 390, 16, 8);
            LoadRegion("EnemyEntity", crawler, 0, 390, 44, 22);
            LoadRegion("BossEntity", boss, 0, 0, 128, 128);

            // Blocks
            LoadRegion("AirBlock", items, 400, 0, 1, 1);
            LoadRegion("GroundBlock", items, 128, 1, 16, 16);
            LoadRegion("LadderBlock", items, 96, 384, 32, 32);
            LoadRegion("TeleportBlock", items, 0, 96, 32, 32);
            LoadRegion("SpikeBlock", items, 0, 32, 32, 32);
            LoadRegion("JumpBlock", items, 96, 512, 32, 32);
            LoadRegion("GoalBlock", items, 0, 128, 32, 32);


            // Load UI
            LoadRegion("pixel", items, 0, 0, 16, 16);
            LoadRegion("title", ui, 0, 0, 192, 112);
            LoadRegion("uiContainer", ui, 0, 64, 288, 128);
            LoadRegion("button", ui, 320, 64, 192, 64);

            // Load bg
            LoadRegion("bg1", bg1, 0,0, 950, 572);

            // Load font 
            font = manager.Load<SpriteFont>("Font/font");
            
            // UI Config
            UIConfig.DEFAULT_FONT = font;
            UIConfig.DEFAULT_BUTTON = GetRegion("button");

            // load sound
            if (SOUND)
            {
                //deathSound = manager.Load<SoundEffect>("Audio/pacman_death");
                //music = manager.Load<Song>("Audio/music");
                //MediaPlayer.Volume = .8f;
                //MediaPlayer.IsRepeating = true;
                //MediaPlayer.Play(music);
            }
        }

        private static void LoadRegion(string name, Texture2D tex, int x, int y, int width, int height)
        {
            regions.Add(name, new TextureRegion(tex, x, y, width, height));
        }

        public static TextureRegion GetRegion(string name)
        {
            return  regions.Keys.Contains(name) ? regions[name] : null;
        }

        public static void Unload()
        {
            manager.Dispose();
        }
    }
}
