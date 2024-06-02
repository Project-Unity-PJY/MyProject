using System.Collections.Generic;
using UnityEngine;

public class ObjectDatabase : MonoBehaviour
{
    public static ObjectDatabase Instance; // 싱글톤 인스턴스
    public List<ObjectData> objectDataList; // 오브젝트 데이터 리스트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ObjectData GetObjectData(int id)
    {
        return objectDataList.Find(data => data.id == id);
    }
}
