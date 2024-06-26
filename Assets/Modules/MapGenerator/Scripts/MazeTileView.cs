using UnityEditor;
using UnityEngine;

namespace Modules.MapGenerator.Scripts
{
  public class MazeTileView : MonoBehaviour, ITileView
  {
    [SerializeField] private GameObject _upWall;
    [SerializeField] private GameObject _downWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _leftWall;

    private ITileModel _mazeTileModel;

    public void Init(ITileModel mazeTileModel)
    {
      _mazeTileModel = mazeTileModel;
      SetPos(mazeTileModel);
      DrawWalls(_mazeTileModel.CurrentDisabledWalls);
    }

    private void SetPos(ITileModel mazeTileModel)
    {
      Vector2Int coords = mazeTileModel.TileCoords;
      transform.localPosition = new Vector2(coords.x * 1, coords.y * 1);
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
      if (_mazeTileModel == null)
        return;

      Vector3 textPosition = transform.position;
      Handles.Label(textPosition, _mazeTileModel.TileCoords.ToString());
    }
  }
}