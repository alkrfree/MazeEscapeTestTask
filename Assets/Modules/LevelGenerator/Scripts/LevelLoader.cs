﻿using System.Collections.Generic;
using CodeBase.StaticData;
using Modules.LevelGenerator.Data;
using Modules.MapGenerator.Scripts;
using Modules.Utils;
using Zenject;

namespace Modules.LevelGenerator.Scripts
{
  public class LevelLoader : ILevelLoader
  {
    private const string LevelStaticDataPath = "/LevelStaticData.json";

    private MazeLoaderTileFactory _tileFactory;
    private List<MazeTileModel> _mazeTileModels = new List<MazeTileModel>();
    private List<ITileView> _mazeTileViews = new List<ITileView>();

    [Inject]
    private void Construct(MazeLoaderTileFactory tileFactory)
    {
      _tileFactory = tileFactory;
    }

    public void LoadLevelData()
    {
      LevelLoaderSerializedData data = JsonSerializer.DeserializeFromJson<LevelLoaderSerializedData>(LevelStaticDataPath);
      CreateModels(data.Levels[0]);
      DrawTiles();
      
    }

    private void CreateModels(LevelSerializedData level)
    {
      for (int i = 0; i < level.Tiles.Count; i++) 
        _mazeTileModels.Add(new MazeTileModel(level.Tiles[i]));
    }

    private void DrawTiles()
    {
      for (int i = 0; i < _mazeTileModels.Count; i++) 
        _mazeTileViews.Add(_tileFactory.Spawn(_mazeTileModels[i]));
    }
  }
}