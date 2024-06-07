using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager3 : MonoBehaviour
{
    public GameObject Talk;
    public TalkData talkData; // npc가 가진 정보
    public Image npcImg;    // npc 이미지
    public Sprite playerImage; // 플레이어 이미지
    public Text talkTxt;    // 화면에 보여질 대화 창
    private int talkIndex;  // 현재 대화 위치?
    private int maxTalkIndex; // 대화 길이 최대 값

    public PlayerMovement playerMovement; // 플레이어 스크립트

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 대화창 활성화 & 값 셋팅
    public void setTalk(TalkData talkData)
    {
        this.talkData = talkData;
        talkIndex = 0;
        maxTalkIndex = talkData.nomalTalk.Count;
        ShowCurrentTalk();
        Talk.SetActive(true);
    }

    // 대화창 비활성화
    public void distroyTalk()
    {
        Talk.SetActive(false);
    }

    // 현재 대사를 보여줍니다.
    private void ShowCurrentTalk()
    {
        if (talkIndex < maxTalkIndex)
        {
            var currentTalk = talkData.nomalTalk[talkIndex];
            talkTxt.text = currentTalk.dialogue;
            if (currentTalk.speaker == 1)
            {
                npcImg.sprite = talkData.npcImage;
            }
            else if (currentTalk.speaker == 2)
            {
                npcImg.sprite = playerImage;
            }
        }
    }

    // 대화 넘기기
    public void nextTalk()
    {
        talkIndex++;
        // 배열보다 인덱스가 커지면 대화 종료
        if (talkIndex >= maxTalkIndex)
        {
            distroyTalk();
            playerMovement.SetCurrentNPC(false);
        }
        else
        {
            // 다음 대화 텍스트 설정
            ShowCurrentTalk();
        }
    }
}
