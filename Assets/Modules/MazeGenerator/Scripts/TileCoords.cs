using System;

namespace Modules.MapGenerator.Scripts
{
  [Serializable]
  public class TileCoords
  {
    public int X;
    public int Y;

    public TileCoords()
    {
    }

    public TileCoords(int x, int y)
    {
      X = x;
      Y = y;
    }

    public TileCoords(TileCoords tileCoords)
    {
      X = tileCoords.X;
      Y = tileCoords.Y;
    }

    public override string ToString()
    {
      return $"x = {X}, y = {Y}";
    }
  }
}