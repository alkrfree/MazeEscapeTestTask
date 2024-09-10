namespace Modules.LevelGenerator.Scripts
{
  public interface ILevelLoader
  {
    public void LoadLevelData();
    public void GoToLevel(int levelNum);
  }
}