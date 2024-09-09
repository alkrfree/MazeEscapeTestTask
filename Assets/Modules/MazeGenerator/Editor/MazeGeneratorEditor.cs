using UnityEditor;
using UnityEngine;


namespace Modules.MapGenerator.Scripts
{
  [CustomEditor(typeof(MazeGenerator))]
  public class MazeGeneratorEditor : Editor
  {
    public override void OnInspectorGUI()
    {
      MazeGenerator mazeGenerator = (MazeGenerator)target;
      DrawDefaultInspector();

      GUILayout.Label("Custom Editor", EditorStyles.boldLabel);

      if (GUILayout.Button("Clear")) 
        mazeGenerator.Clear();

      if (GUILayout.Button("Draw Maze")) 
        mazeGenerator.Draw();

      DrawHorizontalLine(Color.grey);
      if (GUILayout.Button("Save To JSON")) 
        mazeGenerator.SaveToJSON();

      mazeGenerator.LevelNumber = EditorGUILayout.IntField("Level Number", mazeGenerator.LevelNumber);
    }

    private void DrawHorizontalLine(Color color, int thickness = 1, int padding = 10)
    {
      Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
      rect.height = thickness;
      rect.y += padding / 2;
      EditorGUI.DrawRect(rect, color);
    }
  }
}