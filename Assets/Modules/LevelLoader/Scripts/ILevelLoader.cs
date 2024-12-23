namespace Modules.LevelLoader.Scripts
{
  public interface ILevelLoader
  {
    public void LoadLevelData();
    public void GoToLevel(int levelNum);
  }
}