using UnityEngine;

namespace Modules.MapGenerator.Scripts
{
  public class MazeTileFactory : MonoBehaviour
  {
    [SerializeField] private MazeTileView mazeTileViewPrefab;
    [SerializeField] private MazeTileView mazeTileViewStartPrefab;
    [SerializeField] private MazeTileView mazeTileViewFinishPrefab;

    [SerializeField] private Transform _mazeParent;
    [SerializeField] private Vector2 _tileSize;

    public MazeTileView SpawnTileView(MazeTileModel tileModel)
    {
      MazeTileView tileView = Instantiate(GetTilePrefab(tileModel), _mazeParent);
      tileView.Init(tileModel);
      Vector2Int coords = tileModel.TileCoords;
      tileView.transform.localPosition = new Vector2(coords.x * _tileSize.x, coords.y * _tileSize.y);
      return tileView;
    }

    private MazeTileView GetTilePrefab(MazeTileModel tileModel)
    {
      MazeTileView prefab = mazeTileViewPrefab;
      
      if (tileModel.IsStart)
        Debug.LogError(tileModel.IsStart);
      if (tileModel.IsFinish)
        Debug.LogError(tileModel.IsStart);
      
      if (tileModel.IsStart)
        prefab = mazeTileViewStartPrefab;
      else if (tileModel.IsFinish)
        prefab = mazeTileViewFinishPrefab;
      return prefab;
    }
  }
}