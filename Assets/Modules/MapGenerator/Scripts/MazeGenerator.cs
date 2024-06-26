using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Modules.MapGenerator.Scripts
{
  public class MazeGenerator : MonoBehaviour
  {
    // Inject serialize field in future
    [SerializeField] [Range(1, 30)] private int _mazeSizeX;
    [SerializeField] [Range(1, 30)] private int _mazeSizeY;
    
    private MazeTileFactory _mazeTileFactory;

    
    
    private MazeTileModel[][] _mazeTiles;

    private MazeTileModel _currentTileModel;
    private MazeTileModel _prevTileModel;

    private Stack<MazeTileModel> _path = new Stack<MazeTileModel>();

    [Inject]
    private void Construct(MazeTileFactory mazeTileFactory)
    {
      _mazeTileFactory = mazeTileFactory;
    }
    private void Awake()
    {
      CreateTileModels();
      GenerateMaze();
    }

    private void Start()
    {
      DrawTiles();
    }

    private void GenerateMaze()
    {
      _currentTileModel = _mazeTiles[0][0];
      _currentTileModel.Tile = MazeTileModel.TileType.Start;
      _path.Push(_currentTileModel);

      while (_path.Count > 0)
        MakeStep();

      SetFinishTile();
    }

    private void SetFinishTile()
    {
      MazeTileModel furthest = _mazeTiles[0][0];

      for (int i = 0; i < _mazeTiles.Length; i++)
      {
        if (_mazeTiles[i][_mazeSizeY - 1].TilesFromStart > furthest.TilesFromStart)
          furthest = _mazeTiles[i][_mazeSizeY - 1];

        if (_mazeTiles[i][0].TilesFromStart > furthest.TilesFromStart)
          furthest = _mazeTiles[i][0];
      }

      for (int j = 0; j < _mazeTiles[0].Length; j++)
      {
        if (_mazeTiles[_mazeSizeX - 1][j].TilesFromStart > furthest.TilesFromStart)
          furthest = _mazeTiles[_mazeSizeX - 1][j];

        if (_mazeTiles[0][j].TilesFromStart > furthest.TilesFromStart)
          furthest = _mazeTiles[0][j];
      }
      furthest.Tile = MazeTileModel.TileType.Finish;
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
        
        _prevTileModel.CurrentDisabledWalls  = direction;
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
      if (fromTileModel.TileCoords.x > toTileModel.TileCoords.x)
        return MazeTileModel.Direction.Left;

      if (fromTileModel.TileCoords.x < toTileModel.TileCoords.x)
        return MazeTileModel.Direction.Right;

      if (fromTileModel.TileCoords.y > toTileModel.TileCoords.y)
        return MazeTileModel.Direction.Down;

      if (fromTileModel.TileCoords.y < toTileModel.TileCoords.y)
        return MazeTileModel.Direction.Up;

      return MazeTileModel.Direction.None;
    }

    private void MoveToNextTile()
    {
      Vector2Int currentCords = _currentTileModel.TileCoords;

      List<MazeTileModel.Direction> validCoords = GetValidCoordsForNextStep(currentCords);

      _prevTileModel = _currentTileModel;
      _currentTileModel = GetTileCoordsOnDirection(GetRandomTargetDirection(validCoords));
      _path.Push(_currentTileModel);
    }

    private List<MazeTileModel.Direction> GetValidCoordsForNextStep(Vector2Int currentCords)
    {
      List<MazeTileModel.Direction> validCoords = new List<MazeTileModel.Direction>();

      if (IsCoordsValidForStep(currentCords.x, currentCords.y - 1))
        validCoords.Add(MazeTileModel.Direction.Down);

      if (IsCoordsValidForStep(currentCords.x, currentCords.y + 1))
        validCoords.Add(MazeTileModel.Direction.Up);

      if (IsCoordsValidForStep(currentCords.x - 1, currentCords.y))
        validCoords.Add(MazeTileModel.Direction.Left);

      if (IsCoordsValidForStep(currentCords.x + 1, currentCords.y))
        validCoords.Add(MazeTileModel.Direction.Right);

      return validCoords;
    }

    private bool IsCoordsValidForStep(int x, int y)
    {
      if (IsInMazeBorders(x, y))
        return false;

      MazeTileModel tileModel = _mazeTiles[x][y];

      return !tileModel.IsVisited;
    }

    private bool IsInMazeBorders(int x, int y) =>
      x < 0 || y < 0 || x >= _mazeSizeX || y >= _mazeSizeY;


    private MazeTileModel GetTileCoordsOnDirection(MazeTileModel.Direction targetDirection)
    {
      Vector2Int nextCellCoords = _currentTileModel.TileCoords;
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

      return _mazeTiles[nextCellCoords.x][nextCellCoords.y];
    }

    private MazeTileModel.Direction GetRandomTargetDirection(List<MazeTileModel.Direction> validCoords)
    {
      if (validCoords.Count > 0)
        return validCoords[Random.Range(0, validCoords.Count)];

      return MazeTileModel.Direction.None;
    }

    private void CreateTileModels()
    {
      _mazeTiles = new MazeTileModel[_mazeSizeX][];
      for (int i = 0; i < _mazeTiles.Length; i++)
      {
        _mazeTiles[i] = new MazeTileModel[_mazeSizeY];
        for (int j = 0; j < _mazeTiles[i].Length; j++)
          _mazeTiles[i][j] = new MazeTileModel(new Vector2Int(i, j));
      }
    }


    private void DrawTiles()
    {
      
      for (int i = 0; i < _mazeTiles.Length; i++)
      {
        for (int j = 0; j < _mazeTiles[i].Length; j++)
        {
          _mazeTileFactory.Spawn(_mazeTiles[i][j]);
        }
      }
    }
  }
}