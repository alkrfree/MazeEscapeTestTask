using Newtonsoft.Json; // Подключаем Newtonsoft.Json
using UnityEngine;
using System.IO;

namespace Modules.Utils
{
  public static class JsonSerializer
  {
    public static void SerializeToJson<T>(T obj, string path) where T : class
    {
      string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
      Debug.LogError("json DATA = " + json);
      string finalPath = Application.dataPath + path;

      File.WriteAllText(finalPath, json);
      Debug.LogError("Serialize to " + finalPath);
    }

   
    public static T DeserializeFromJson<T>(string path) where T : class
    {
      string loadedJson = File.ReadAllText(Application.dataPath + path);
      T obj = JsonConvert.DeserializeObject<T>(loadedJson);
      return obj;
    }
  }
}