using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.MapGenerator.Scripts
{
  public class MazeGenerator : MonoBehaviour
  {
    [SerializeField] [Range(1, 30)] private int _mazeSizeX;
    [SerializeField] [Range(1, 30)] private int _mazeSizeY;

    [SerializeField] private Vector2 _tileSize;
    [SerializeField] private MazeTileView mazeTileViewPrefab;
    [SerializeField] private Transform _mazeParent;


    private MazeTileModel[][] _mazeTiles;

    private MazeTileModel _currentTileModel;
    private MazeTileModel _prevTileModel;

    private List<MazeTileModel> _pathDebug = new List<MazeTileModel>();

    private void Awake()
    {
      CreateTileModels();
      DrawTiles();
    }

    private void Start()
    {
      GenerateMaze();
    }

    private void GenerateMaze()
    {
      _currentTileModel = _mazeTiles[0][0];
      _pathDebug.Add(_currentTileModel);
      for (int i = 0; i < 10; i++)
        MakeStep();

      for (int i = 0; i < _pathDebug.Count; i++)
        Debug.LogError("_path[i].TileCoords = " + _pathDebug[i].TileCoords);
    }

    private void MakeStep() // refactor
    {
      MazeTileModel.Direction direction = MazeTileModel.Direction.None;
      if (_prevTileModel != null && _currentTileModel != null)
      {
        direction = GetDirectionBetweenTiles(_prevTileModel, _currentTileModel);
        _prevTileModel.IsVisited = true;
        _prevTileModel.SetWallVisibility(direction);

        MazeTileModel.Direction oppositeDirection = MazeTileModel.Direction.None;

        if (direction == MazeTileModel.Direction.Left)
          oppositeDirection = MazeTileModel.Direction.Right;

        if (direction == MazeTileModel.Direction.Right)
          oppositeDirection = MazeTileModel.Direction.Left;

        if (direction == MazeTileModel.Direction.Up)
          oppositeDirection = MazeTileModel.Direction.Down;

        if (direction == MazeTileModel.Direction.Down)
          oppositeDirection = MazeTileModel.Direction.Up;

        _currentTileModel.SetWallVisibility(oppositeDirection);
      }


      MoveToNextTile();
    }

    private MazeTileModel.Direction GetDirectionBetweenTiles(MazeTileModel fromTileModel, MazeTileModel nextTileModel)
    {
      if (fromTileModel.TileCoords.x > nextTileModel.TileCoords.x)
        return MazeTileModel.Direction.Left;

      if (fromTileModel.TileCoords.x < nextTileModel.TileCoords.x)
        return MazeTileModel.Direction.Right;

      if (fromTileModel.TileCoords.y > nextTileModel.TileCoords.y)
        return MazeTileModel.Direction.Down;

      if (fromTileModel.TileCoords.y < nextTileModel.TileCoords.y)
        return MazeTileModel.Direction.Up;

      return MazeTileModel.Direction.None;
    }

    private void MoveToNextTile()
    {
      List<MazeTileModel.Direction> validCoords = new List<MazeTileModel.Direction>();

      Vector2Int currentCords = _currentTileModel.TileCoords;

      if (IsCoordsValidForStep(currentCords.x, currentCords.y - 1))
        validCoords.Add(MazeTileModel.Direction.Down);

      if (IsCoordsValidForStep(currentCords.x, currentCords.y + 1))
        validCoords.Add(MazeTileModel.Direction.Up);

      if (IsCoordsValidForStep(currentCords.x - 1, currentCords.y))
        validCoords.Add(MazeTileModel.Direction.Left);

      if (IsCoordsValidForStep(currentCords.x + 1, currentCords.y))
        validCoords.Add(MazeTileModel.Direction.Right);

      _prevTileModel = _currentTileModel;
      _currentTileModel = GetTileCoordsOnDirection(GetRandomTargetDirection(validCoords));
      _pathDebug.Add(_currentTileModel);
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


    private MazeTileModel GetTileCoordsOnDirection(MazeTileModel.Direction targetDirection) // refactor
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

    private static MazeTileModel.Direction GetRandomTargetDirection(List<MazeTileModel.Direction> validCoords)
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
          MazeTileView tileView = Instantiate(mazeTileViewPrefab, _mazeParent);
          tileView.Init(_mazeTiles[i][j]);
          tileView.transform.localPosition = new Vector2(i * _tileSize.x, j * _tileSize.y);
        }
      }
    }

  }
}