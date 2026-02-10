using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonToFileStorageService : IStorageService
{
    public void Save(string key, object data, Action<bool> callback = null)
    {
        string path = BuildPath(key);
        Debug.Log(Application.persistentDataPath);
        string json = JsonConvert.SerializeObject(data);

        using(var filestream = new StreamWriter(path))
        {
            filestream.Write(json);
        }

        callback?.Invoke(true);
    }

    public void Load<T>(string key, Action<T> callback)
    {
        string path = BuildPath(key);

        if (!File.Exists(path))
        {
            // If the file does not exist, call the callback with default value
            callback?.Invoke(default(T));
            return;
        }

        using (var filestream = new StreamReader(path))
        {
            var json = filestream.ReadToEnd();
            var data = JsonConvert.DeserializeObject<T>(json);

            callback?.Invoke(data);
        }
    }

    private string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}
