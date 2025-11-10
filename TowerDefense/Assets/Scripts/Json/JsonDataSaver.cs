#if UNITY_EDITOR

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using UnityEngine;

public class JsonDataSaver
{
    //private readonly string SavePath = Path.Combine(Application.dataPath, "JsonData");

    /// <summary>
    /// 객체를 JSON 파일로 저장합니다.
    /// </summary>
    public void SaveToJson<T>(T data, string path, string fileName)
    {
        string newPath = Path.Combine(path, $"{fileName}.json");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        // JsonSerializerSettings 생성 및 TypeNameHandling.All 설정
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented // 가독성을 위한 설정
        };
        settings.Converters.Add(new StringEnumConverter());

        string json = JsonConvert.SerializeObject(data, settings);

        File.WriteAllText(newPath, json);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        Debug.Log($"✅ JSON 저장 완료: {newPath}");
    }

    /// <summary>
    /// JSON 파일에서 데이터를 로드합니다.
    /// </summary>
    public T LoadFromJson<T>(string path, string fileName)
    {
        string newPath = Path.Combine(path, $"{fileName}.json");

        if (!File.Exists(path))
        {
            Debug.LogWarning($"⚠️ 파일이 존재하지 않습니다: {path}");
            return default;
        }

        // JsonSerializerSettings 생성 및 TypeNameHandling.All 설정
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented // 가독성을 위한 설정
        };
        settings.Converters.Add(new StringEnumConverter());

        string json = File.ReadAllText(newPath);
        return JsonConvert.DeserializeObject<T>(json, settings);
    }
}

#endif