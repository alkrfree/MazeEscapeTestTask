using System;
using System.Collections.Generic;
//using CodeBase.StaticData;
using Modules.LevelLoader.Data;
using Modules.MazeGenerator.Scripts;
using Modules.Utils;
using UnityEngine;

namespace Modules.MazeSaver.Scripts
{
    public class MazeSaver : MonoBehaviour
    {
        private const string LevelStaticDataPath = "/LevelStaticData.json";
        [SerializeField] private MazeGenerator.Scripts.MazeGenerator _mazeGenerator;

        public void Clear()
        {
            _mazeGenerator.Clear();
        }

        public void Draw()
        {
            _mazeGenerator.Draw();
        }

        public int LevelNumber { get; set; }
    
        public void SaveToJSON()
        {
            List<EnemySerializedData> enemyData = new List<EnemySerializedData>();
            for (int i = 0; i < 2; i++)
            {
                var enemy = new EnemySerializedData();

                Guid newGuid = Guid.NewGuid();
                /*enemy.Id = newGuid.ToString();

                enemy.TileCoords = new TileCoords(0, 0);
                enemy.EnemyTypeId = 0;// EnemyTypeId.Enemy;
                enemy.AdditionalAction = "<patrol tile1 = [1,1], tile2 = [1,3]>";*/
                enemyData.Add(enemy);
            }

            List<LevelSerializedData> levelData = new List<LevelSerializedData>();
            LevelSerializedData level = new LevelSerializedData();

            List<TileSerializedData> tiles = new List<TileSerializedData>();
            for (int i = 0; i < _mazeGenerator.MazeTiles.Length; i++)
            {
                for (int j = 0; j < _mazeGenerator.MazeTiles[i].Length; j++)
                {
                    tiles.Add(new TileSerializedData(_mazeGenerator.MazeTiles[i][j]));
                }
            }

            level.Tiles = tiles;
            level.LevelNum = LevelNumber;
            level.Enemies = enemyData;
            levelData.Add(level);

            var data = new LevelLoaderSerializedData();
            data.Levels = levelData;

            JsonSerializer.SerializeToJson(data, LevelStaticDataPath);
        }
    }
}
