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
        public static Texture2D character;
        public static Texture2D bg1;

        public static SpriteFont font;

        public static SoundEffect introSound;
        public static SoundEffect eatSound;
        public static SoundEffect deathSound;
        public static Song music;

        public static void Load(ContentManager manager)
        {
            Assets.manager = manager;
            regions = new Dictionary<string, TextureRegion>();

            // load our sprite sheet
            items = manager.Load<Texture2D>("Graphics/items");
            ui = manager.Load<Texture2D>("Graphics/ui");
            bg1 = manager.Load<Texture2D>("Graphics/bg1");

            // Entities
            LoadRegion("PlayerEntity", items, 32, 512, 32, 32);

            // Blocks
            LoadRegion("AirBlock", items, 400, 0, 1, 1);
            LoadRegion("GroundBlock", items, 128, 0, 16, 16);
            LoadRegion("LadderBlock", items, 96, 384, 32, 32);


            // Load UI
            LoadRegion("pixel", items, 0, 0, 16, 16);
            LoadRegion("title", ui, 0, 0, 290, 48);
            LoadRegion("uiContainer", ui, 0, 64, 288, 128);
            LoadRegion("button", ui, 320, 64, 192, 64);

            // Load bg
            LoadRegion("bg1", bg1, 0,0, 950, 572);

            // Load font 
            font = manager.Load<SpriteFont>("Font/font");

            // load sound
            if (SOUND)
            {
                introSound = manager.Load<SoundEffect>("Audio/pacman_beginning");
                eatSound = manager.Load<SoundEffect>("Audio/pacman_chomp");
                deathSound = manager.Load<SoundEffect>("Audio/pacman_death");
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
            return regions[name];
        }

        public static void Unload()
        {
            manager.Dispose();
        }
    }
}
