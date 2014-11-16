using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MAH_Platformer
{
    public static class LevelIO
    {
        public const int ID_PER_BASE = 8;


        public static int LEVEL_MAX = 0;
       
       // Read level
       public static int[,] ReadLevel(int lvl)
       {
           StreamReader sr = new StreamReader(@"Content/levels.txt");
           string input = sr.ReadToEnd().ToString().Replace("\r", "").Replace(" ", "");
           sr.Close();

           MatchCollection matches = Regex.Matches(input, @"\[[^\]]+\]");

           LEVEL_MAX = matches.Count;

           //if (lvl - 1 >= LEVEL_MAX) return new int[World.WIDTH, World.HEIGHT];

           string map = matches[lvl - 1].Value.Replace("[", "").Replace("]", "");
           
           string[] stringParts = map.Split(',');
           int height = map.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
           int width = (map.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length);

           int[,] loadedMap = new int[width, height];

           for (int j = 0; j < height; j++)
           {
               for (int i = 0; i < width; i++)
               {
                   loadedMap[i, j] = int.Parse(stringParts[i + (j * width)]);
               }
           }

           return loadedMap;
       }
        /*
       
      // Write level to file
      public static void WriteLevel(int[,] levelToWrite, int levelNumber)
      {
          StreamReader sr = new StreamReader(@"Content/levels.txt");
          string input = sr.ReadToEnd().ToString();
          sr.Close();

          string fullMap = input;
          string partMap = GenerateStringMap(levelToWrite);
          string finalMap;

          if (levelNumber > LEVEL_MAX)
          {
              finalMap = fullMap + partMap;
          }
          else
          {
              // split map
              string firstPart;
              string lastPart;

              firstPart = fullMap.Substring(0, IndexOfNth(fullMap, '[', levelNumber)); // ad -1 
              lastPart = fullMap.Substring(IndexOfNth(fullMap, ']', levelNumber) + 1, fullMap.Length - IndexOfNth(fullMap, ']', levelNumber) - 1);

              finalMap = firstPart + partMap + lastPart;
          }

          // Save map
          StreamWriter sw = new StreamWriter(@"Content/levels.txt");
          sw.Write(finalMap);
          sw.Flush();
          sw.Close();
      }

      // Generate intmap from tilemap
      private static string GenerateStringMap(int[,] tileMap)
      {
          string level = "\n[\n";

          for (int j = 0; j < tileMap.GetLength(1); j++)
          {
              for (int i = 0; i < tileMap.GetLength(0); i++)
              {
                  level += (int)tileMap[i, j].GetMapType() + ",";
              }
              level += "\n";
          }

          level = level.Substring(0, level.Length - 2);
          level += "\n]";

          return level;
      }

      // Get index of nth character
      private static int IndexOfNth(string str, char c, int n)
      {
          int index = -1;
          while (n-- > 0)
          {
              index = str.IndexOf(c, index + 1);
              if (index == -1)
                  break;
          }
          return index;
      }

      // Delete the last level
      public static void DeleteLastLevel()
      {
          StreamReader sr = new StreamReader(@"Content/levels.txt");
          string input = sr.ReadToEnd().ToString();
          sr.Close();

          int levelNumber = LEVEL_MAX;
          string fullMap = input;
          string finalMap;

          // split map
          string firstPart;

          firstPart = fullMap.Substring(0, IndexOfNth(fullMap, '[', levelNumber));
          finalMap = firstPart;

          // Save map
          StreamWriter sw = new StreamWriter(@"Content/levels.txt");
          sw.Write(finalMap);
          sw.Flush();
          sw.Close();

          LEVEL_MAX--;
      }*/
    }
}
