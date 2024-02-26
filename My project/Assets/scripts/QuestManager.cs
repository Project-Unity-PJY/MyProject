using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;
   

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();

    }

    // Update is called once per frame
    void GenerateData()
    {
        questList.Add(10, new QuestData("윌리엄의 호출", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("긴급 호출 준비", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("출발 윌리엄 집", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQeust(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        //Control Quest Object
        ControlObject();

        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        return questList[questId].questName;
    }
    public string CheckQeust()
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        UnityEngine.Debug.Log("112");
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;

        }
    }
}