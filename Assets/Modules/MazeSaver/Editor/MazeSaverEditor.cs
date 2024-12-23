using UnityEditor;
using UnityEngine;

namespace Modules.MazeSaver.Editor
{
  [CustomEditor(typeof(Scripts.MazeSaver))]
  public class MazeSaverEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      Scripts.MazeSaver mazeSaver = (Scripts.MazeSaver)target;
      DrawDefaultInspector();

      GUILayout.Label("Custom Editor", EditorStyles.boldLabel);

      if (GUILayout.Button("Clear")) 
        mazeSaver.Clear();

      if (GUILayout.Button("Draw Maze")) 
        mazeSaver.Draw();

      DrawHorizontalLine(Color.grey);
      if (GUILayout.Button("Save To JSON")) 
        mazeSaver.SaveToJSON();

      mazeSaver.LevelNumber = EditorGUILayout.IntField("Level Number", mazeSaver.LevelNumber);
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