using System;
using UnityEditor;
using UnityEngine;

namespace Modules.MapGenerator.Scripts
{
  public class MazeTileView : MonoBehaviour
  {
    [SerializeField] private GameObject _upWall;
    [SerializeField] private GameObject _downWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _leftWall;

    private MazeTileModel _mazeTileModel;

    public void Init(MazeTileModel mazeTileModel)
    {
      _mazeTileModel = mazeTileModel;
      _mazeTileModel.DisableWallsDirection += DrawWalls;
    }

    private void DrawWalls(MazeTileModel.Direction direction)
    {
      _upWall.SetActive(!direction.HasFlag(MazeTileModel.Direction.Up));
      _downWall.SetActive(!direction.HasFlag(MazeTileModel.Direction.Down));
      _rightWall.SetActive(!direction.HasFlag(MazeTileModel.Direction.Right));
      _leftWall.SetActive(!direction.HasFlag(MazeTileModel.Direction.Left));
    }

    void OnDrawGizmos()
    {
      if(_mazeTileModel == null)
        return;
      
      Vector3 textPosition = transform.position;
      Handles.Label(textPosition, _mazeTileModel.TileCoords.ToString());
    }
  }
}