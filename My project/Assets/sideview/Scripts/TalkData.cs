using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueEntry
{
    public int speaker; // 화자
    public string dialogue; // 대사
}

public class TalkData : MonoBehaviour
{
    public List<DialogueEntry> nomalTalk;  // 평상시 대사
    public List<DialogueEntry> specialTalk; // 특정 조건 만족할 때 만 보열 대사
    public int specialNumber; // 유저가 아이템을 가지고 있는지 검사할 변수
    public Sprite npcImage;  // npc 이미지
    public string reward; // 쿼스트 완료시 지급할 보상
    public int rQuantity; // 보상 개수
    public string penalty; // 사용할 아이템
    public int pQuantity; // 사용할 아이템 개수
}
