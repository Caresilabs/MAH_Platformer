using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MAH_Platformer
{
    /**
     * Savemanager saves the highscore but can be expanded in the future for a top5 list and save states
     */
    public class HighscoreManager
    {
        public static void SaveHighscore(int highscore)
        {
            List<int> highscores = GetHighscores().ToList();
            highscores.Add(highscore);

            highscores = highscores.OrderByDescending(c => c).ToList();

            // Write
            StreamWriter writer = new StreamWriter("Content/highscore.dat");

            string output = "";

            for (int i = 0; i < highscores.Count - 1; i++)
            {
                output += highscores[i];
                if (i != highscores.Count - 1) output += "\n";
            }
            writer.Write(output);
            
            writer.Flush();
            writer.Close();
        }

        public static int[] GetHighscores()
        {
            string read = "";
            using (StreamReader reader = new StreamReader("Content/highscore.dat"))
            {
                read = reader.ReadToEnd();

                reader.Close();
            }

            string[] strScore = read.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] intScore = new int[strScore.Length];

            for (int i = 0; i < intScore.Length; i++)
            {
                intScore[i] = Int32.Parse(strScore[i]);
            }

            return intScore;
        }

    }
}
