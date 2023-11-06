using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static string SavePath = "Saved/";

    public static void Save<T>(T @object, string name)
    {
        var serialized = JsonUtility.ToJson(@object);
        name = Path.ChangeExtension(name, "json");

        var filePath = Path.Combine(Application.dataPath, SavePath, name);

        File.WriteAllText(filePath, serialized);
    }

    public static T Load<T>(string name)
    {
        name = Path.ChangeExtension(name, "json");
        var filePath = Path.Combine(Application.dataPath, SavePath, name);

        if (File.Exists(filePath))
        {
            var serialized = File.ReadAllText(filePath);
            var @object = JsonUtility.FromJson<T>(serialized);
            return @object;
        }

        return default;
    }
}
