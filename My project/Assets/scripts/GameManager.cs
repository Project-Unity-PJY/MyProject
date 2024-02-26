using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;
    public GameObject questPanel;
    public Text talkText;
    public Text questionText;
    public Image portaritImg;
    public GameObject scanObject;
    public GameObject Inventory;
    public GameObject[] Inventorys;
    public Sprite tempItem;
    public int invenIndex;
    public bool isAction; //말풍선 및 플레이어 이동제한
    public int talkIndex;
    public bool isQuest;

    void Start()
    {
        Debug.Log(questManager.CheckQeust());
    }

    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        if(scanObj.GetComponent<ObjData>().id == 4000 ) { // 퀘스트 아이템일 경우
            Inventory.SetActive(true);
            ObjData objectData = scanObject.GetComponent<ObjData>();
            Talk(scanObj.name, objectData.id, objectData.isNpc, objectData.isItem);
            questPanel.SetActive(isAction);
            tempItem = scanObj.GetComponent<SpriteRenderer>().sprite; //스캔한 오브젝트 이미지 빼둠
        }
        else
        {
            ObjData objectData = scanObject.GetComponent<ObjData>();
            Talk("", objectData.id, objectData.isNpc, objectData.isItem);

            talkPanel.SetActive(isAction);
        }
    }

    void Talk(string name, int id, bool isNpc, bool isItem)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        if (talkData == null)
        {
            talkIndex = 0;
            isAction = false;
            Debug.Log(questManager.CheckQeust(id));
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0]; //':'를 기준으로 배열이 두개로 나뉜다.
            portaritImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portaritImg.color = new Color(1, 1, 1, 1);
        }
        else if (isItem)
        {
            questionText.text = name + talkData; 
            portaritImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portaritImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }

    // 아이템 획득
    public void GetItem(int num)
    {
        if (num == 1)
        {
            Inventorys[invenIndex].GetComponent<Image>().sprite = tempItem;
            invenIndex++;
            isAction = false;
            Inventory.SetActive(false);
            questPanel.SetActive(false);
            //인벤토리 꽉차있는거 예외처리는 알아하셈
        }
        else
        {
            tempItem = null;
            isAction = false;
            Inventory.SetActive(false);
            questPanel.SetActive(false);
        }
    }
}