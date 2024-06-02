using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager2 : MonoBehaviour
{
    public static TalkManager2 Instance { get; private set; }

    public GameObject image; // 대화 상자를 포함하는 부모 오브젝트
    public Text dialogueText; // 대화 내용을 표시할 Text UI 요소

    private Dictionary<int, string[]> dialogueData; // 대사 데이터를 저장할 딕셔너리
    private string[] dialogue; // 현재 대화 목록
    private int dialogueIndex = 0; // 현재 대화 인덱스
    private bool isTalking = false; // 대화 중인지 여부

    void Awake()
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

    void Start()
    {
        // 대화 데이터를 초기화
        dialogueData = new Dictionary<int, string[]>
        {
            { 1, new string[] { "안녕 오늘따라 많이 피곤하네?" } },
            { 2, new string[] { "좋은 아침이야!", "오늘 하루도 힘내자!" } }
            // 다른 NPC ID와 대사 데이터를 여기에 추가
        };

        // 대화 상자를 초기에는 비활성화
        image.SetActive(false);
    }

    // NPC ID에 맞는 대사를 가져오는 함수
    public void StartDialogue(int npcId)
    {
        if (dialogueData.ContainsKey(npcId))
        {
            dialogue = dialogueData[npcId];
            if (dialogue != null && dialogue.Length > 0)
            {
                dialogueIndex = 0;
                image.SetActive(true); // 대화 상자 활성화
                dialogueText.text = dialogue[dialogueIndex];
                isTalking = true;
            }
        }
        else
        {
            Debug.LogWarning("대화 시작 실패: ID에 해당하는 대사 데이터가 없습니다.");
        }
    }

    // 대화를 진행하는 함수
    public void ContinueDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogue.Length)
        {
            dialogueText.text = dialogue[dialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    // 대화를 종료하는 함수
    public void EndDialogue()
    {
        image.SetActive(false); // 대화 상자 비활성화
        dialogueIndex = 0;
        isTalking = false;
    }

    // 대화 중인지 여부를 반환하는 함수
    public bool IsTalking()
    {
        return isTalking;
    }
}
