using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    //여러 문장이 들어있으므로 string[]사용
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portaitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳에 처음 왔구나?:1",  });
        talkData.Add(2000, new string[] { "어서와.:0", "이 호수는 정말 아름답지?:1", "사실 이 호수에는 무언가의 비밀이 숨겨져 있단다.:2" });
        talkData.Add(100, new string[] { "꼬리를 살랑 살랑거리며 여유로운 고양이다.", });
        talkData.Add(200, new string[] { "평범한 나무상자이다." });

        //quest talk
        talkData.Add(10 + 1000, new string[] { "티모시, 오랜만이네. 내가 갑작스레 연락을 끊은지 대략 5년 정도 되었을거야.:1 ",
        "본론으로 넘어가자면 나에게 남은 시간이 별로 없기도 하고, 내가 믿을 수 있는 사람이 자네 뿐이라 이렇게 문자를 남기네.:1",
        " 자네에게 미안하지만 내 집에 와줄 수 있는가? 부탁이야.:1",
        " 지하실에 모든 설명이 있어. 주소는 5813 59th East St Waley Street 일세.:1",
        "그리고 단단히 준비하고 오는게 좋을걸세. 매우 위험한 일이야.:1" });

        talkData.Add(11 + 1000, new string[] { "헤헷><.:0" });
        talkData.Add(11 + 2000, new string[] { "윌리엄…자네 도대체 어떤 일을 벌이는 건가…? 지금 당장 가봐야겠어.:0" });

        talkData.Add(20 + 1000, new string[] { "흠. 일단 혹시 모르니 빠르게 챙길 수 있는 물품들을 가지고 나갈까?:0",
            "[45초 안에 필요한 물건을 찾으세요. 한번 획득한 물품은 다시 돌려놓을 수 없으니 신중하게 물건을 골라주세요!]:0" });
        talkData.Add(20 + 5000, new string[] { "언젠가 필요하겠지.챙기자.", });

        talkData.Add(21 + 2000, new string[] { "이정도면 적당하겠지.이젠 가야 해.늦을지도 몰라.:", });

        //item talk
        talkData.Add(4000, new string[] { "를 획득하시겠습니까?", });

        portraitData.Add(1000 + 0, portaitArr[0]);
        portraitData.Add(1000 + 1, portaitArr[1]);
        portraitData.Add(1000 + 2, portaitArr[2]);
        portraitData.Add(1000 + 3, portaitArr[3]);

        portraitData.Add(2000 + 0, portaitArr[4]);
        portraitData.Add(2000 + 1, portaitArr[5]);
        portraitData.Add(2000 + 2, portaitArr[6]);
        portraitData.Add(2000 + 3, portaitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);
            else
                return GetTalk(id - id % 10, talkIndex);
        }


        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}