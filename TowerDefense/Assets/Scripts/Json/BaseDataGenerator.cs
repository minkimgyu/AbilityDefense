#if UNITY_EDITOR
using UnityEngine;

public abstract class BaseDataGenerator<T1> : MonoBehaviour where T1 : System.Enum
{
    [SerializeField] protected T1 _dataName;
    [SerializeField] string _savePath = Application.dataPath + "/JsonDatas";

    /// <summary>
    /// 자식 클래스에서 데이터를 생성하는 함수
    /// </summary>
    public abstract void GenerateData();

    /// <summary>
    /// 데이터를 Json으로 저장하는 함수
    /// </summary>
    protected void SaveToJson<T2>(T2 data)
    {
        JsonDataSaver _jsonDataSaver = new JsonDataSaver();

        if (_jsonDataSaver == null)
        {
            Debug.LogError("❌ JsonDataSaver가 존재하지 않습니다.");
            return;
        }

        string dataName = _dataName.ToString();
        _jsonDataSaver.SaveToJson(data, _savePath, dataName);
        Debug.Log($"✅ {dataName}.json 저장 완료");
    }
}
#endif
