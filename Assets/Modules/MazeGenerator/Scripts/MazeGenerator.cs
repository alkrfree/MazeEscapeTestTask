using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.MazeGenerator.Scripts
{
  public class MazeGenerator : MonoBehaviour
  {
    public int MazeSizeX;
    public int MazeSizeY;

    public MazeGeneratorTileFactory MazeGeneratorTileFactory;

    [HideInInspector] public int LevelNumber;


    private MazeTileModel[][] _mazeTiles;

    private MazeTileModel _currentTileModel;
    private MazeTileModel _prevTileModel;

    private Stack<MazeTileModel> _path = new Stack<MazeTileModel>();
    private List<MazeTileView> _allTiles = new List<MazeTileView>();

    public MazeTileModel[][] MazeTiles => _mazeTiles;

    public void Draw() // TODO refactor
    {
      Clear();
      CreateTileModels();
      GenerateMaze();
      DrawTiles();
    }

    public void Clear()
    {
      for (int i = 0; i < _allTiles.Count; i++)
        DestroyImmediate(_allTiles[i].gameObject);

      _allTiles.Clear();
      _mazeTiles = null;
      _path.Clear();
      _prevTileModel = null;
      _currentTileModel = null;
    }

    private void GenerateMaze()
    {
      _currentTileModel = MazeTiles[0][0];
      _currentTileModel.Type = MazeTileModel.TileType.Start;
      _path.Push(_currentTileModel);


      while (_path.Count > 0)
        MakeStep();


      SetFinishTile();
    }

    private void SetFinishTile()
    {
      MazeTileModel furthest = MazeTiles[0][0];

      for (int i = 0; i < MazeTiles.Length; i++)
      {
        if (MazeTiles[i][MazeSizeY - 1].TilesFromStart > furthest.TilesFromStart)
          furthest = MazeTiles[i][MazeSizeY - 1];

        if (MazeTiles[i][0].TilesFromStart > furthest.TilesFromStart)
          furthest = MazeTiles[i][0];
      }

      for (int j = 0; j < MazeTiles[0].Length; j++)
      {
        if (MazeTiles[MazeSizeX - 1][j].TilesFromStart > furthest.TilesFromStart)
          furthest = MazeTiles[MazeSizeX - 1][j];

        if (MazeTiles[0][j].TilesFromStart > furthest.TilesFromStart)
          furthest = MazeTiles[0][j];
      }

      furthest.Type = MazeTileModel.TileType.Finish;
    }

    private void MakeStep()
    {
      if (_prevTileModel != null && _currentTileModel != null)
      {
        MazeTileModel.Direction direction = GetDirectionBetweenTiles(_prevTileModel, _currentTileModel);
        _prevTileModel.IsVisited = true;
        if (direction == MazeTileModel.Direction.None)
        {
          TryToMoveBack();
          return;
        }

        if (!_currentTileModel.IsVisited)
          _currentTileModel.TilesFromStart = _prevTileModel.TilesFromStart + 1;

        _prevTileModel.CurrentDisabledWalls = direction;
        _currentTileModel.CurrentDisabledWalls = GetOppositeDirection(direction);
      }

      MoveToNextTile();
    }

    private MazeTileModel.Direction GetOppositeDirection(MazeTileModel.Direction direction)
    {
      MazeTileModel.Direction oppositeDirection = MazeTileModel.Direction.None;

      if (direction == MazeTileModel.Direction.Left)
        oppositeDirection = MazeTileModel.Direction.Right;

      if (direction == MazeTileModel.Direction.Right)
        oppositeDirection = MazeTileModel.Direction.Left;

      if (direction == MazeTileModel.Direction.Up)
        oppositeDirection = MazeTileModel.Direction.Down;

      if (direction == MazeTileModel.Direction.Down)
        oppositeDirection = MazeTileModel.Direction.Up;
      return oppositeDirection;
    }

    private void TryToMoveBack()
    {
      if (_path.TryPop(out MazeTileModel tileModel))
      {
        _currentTileModel = tileModel;
        MakeStep();
      }
    }

    private MazeTileModel.Direction GetDirectionBetweenTiles(MazeTileModel fromTileModel, MazeTileModel toTileModel)
    {
      if (fromTileModel.TileCoords.X > toTileModel.TileCoords.X)
        return MazeTileModel.Direction.Left;

      if (fromTileModel.TileCoords.X < toTileModel.TileCoords.X)
        return MazeTileModel.Direction.Right;

      if (fromTileModel.TileCoords.Y > toTileModel.TileCoords.Y)
        return MazeTileModel.Direction.Down;

      if (fromTileModel.TileCoords.Y < toTileModel.TileCoords.Y)
        return MazeTileModel.Direction.Up;

      return MazeTileModel.Direction.None;
    }

    private void MoveToNextTile()
    {
      TileCoords currentCords = _currentTileModel.TileCoords;

      List<MazeTileModel.Direction> validCoords = GetValidCoordsForNextStep(currentCords);

      _prevTileModel = _currentTileModel;
      _currentTileModel = GetTileCoordsOnDirection(GetRandomTargetDirection(validCoords));
      _path.Push(_currentTileModel);
    }

    private List<MazeTileModel.Direction> GetValidCoordsForNextStep(TileCoords currentCords)
    {
      List<MazeTileModel.Direction> validCoords = new List<MazeTileModel.Direction>();

      if (IsCoordsValidForStep(currentCords.X, currentCords.Y - 1))
        validCoords.Add(MazeTileModel.Direction.Down);

      if (IsCoordsValidForStep(currentCords.X, currentCords.Y + 1))
        validCoords.Add(MazeTileModel.Direction.Up);

      if (IsCoordsValidForStep(currentCords.X - 1, currentCords.Y))
        validCoords.Add(MazeTileModel.Direction.Left);

      if (IsCoordsValidForStep(currentCords.X + 1, currentCords.Y))
        validCoords.Add(MazeTileModel.Direction.Right);

      return validCoords;
    }

    private bool IsCoordsValidForStep(int x, int y)
    {
      if (IsInMazeBorders(x, y))
        return false;

      MazeTileModel tileModel = MazeTiles[x][y];

      return !tileModel.IsVisited;
    }

    private bool IsInMazeBorders(int x, int y) =>
      x < 0 || y < 0 || x >= MazeSizeX || y >= MazeSizeY;


    private MazeTileModel GetTileCoordsOnDirection(MazeTileModel.Direction targetDirection)
    {
      Vector2Int nextCellCoords = new Vector2Int(_currentTileModel.TileCoords.X, _currentTileModel.TileCoords.Y);
      switch (targetDirection)
      {
        case MazeTileModel.Direction.Up:
          nextCellCoords.y++;
          break;
        case MazeTileModel.Direction.Down:
          nextCellCoords.y--;
          break;
        case MazeTileModel.Direction.Right:
          nextCellCoords.x++;
          break;
        case MazeTileModel.Direction.Left:
          nextCellCoords.x--;
          break;
      }

      return MazeTiles[nextCellCoords.x][nextCellCoords.y];
    }

    private MazeTileModel.Direction GetRandomTargetDirection(List<MazeTileModel.Direction> validCoords)
    {
      if (validCoords.Count > 0)
        return validCoords[Random.Range(0, validCoords.Count)];

      return MazeTileModel.Direction.None;
    }

    private void CreateTileModels()
    {
      _mazeTiles = new MazeTileModel[MazeSizeX][];
      for (int i = 0; i < MazeTiles.Length; i++)
      {
        MazeTiles[i] = new MazeTileModel[MazeSizeY];
        for (int j = 0; j < MazeTiles[i].Length; j++)
          MazeTiles[i][j] = new MazeTileModel(i, j);
      }
    }


    private void DrawTiles()
    {
      for (int i = 0; i < MazeTiles.Length; i++)
      {
        for (int j = 0; j < MazeTiles[i].Length; j++)
        {
          _allTiles.Add(MazeGeneratorTileFactory.Spawn(MazeTiles[i][j]));
        }
      }
    }
  }
}