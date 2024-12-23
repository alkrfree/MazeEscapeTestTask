using System.Collections.Generic;
using Modules.LevelLoader.Data;
using Modules.MazeGenerator.Scripts;
using Modules.Utils;
using Zenject;

namespace Modules.LevelLoader.Scripts
{
  public class LevelLoader : ILevelLoader
  {
    private const string LevelStaticDataPath = "/LevelStaticData.json";

    private MazeLoaderTileFactory _tileFactory;
    private readonly List<MazeTileModel> _mazeTileModels = new List<MazeTileModel>();
    private List<ITileView> _mazeTileViews = new List<ITileView>();
    private LevelLoaderSerializedData cachedData;

    [Inject]
    private void Construct(MazeLoaderTileFactory tileFactory)
    {
      _tileFactory = tileFactory;
    }

    public void LoadLevelData()
    {
      cachedData = JsonSerializer.DeserializeFromJson<LevelLoaderSerializedData>(LevelStaticDataPath);
    }

    public void GoToLevel(int levelNum)
    {
      CreateModels(cachedData.Levels[levelNum]);
      DrawTiles();
    }

    private void CreateModels(LevelSerializedData level)
    {
      for (int i = 0; i < level.Tiles.Count; i++)
      {
        MazeTileModel model = new MazeTileModel();
        model.TileCoords = new TileCoords(level.Tiles[i].TileCoords.X, level.Tiles[i].TileCoords.Y);
        model.Type = level.Tiles[i].Type;
        model.CurrentDisabledWalls = level.Tiles[i].CurrentDisabledWalls;
        _mazeTileModels.Add(model);
      }
    }

    private void DrawTiles()
    {
      for (int i = 0; i < _mazeTileModels.Count; i++)
        _mazeTileViews.Add(_tileFactory.Spawn(_mazeTileModels[i]));
    }
  }
}