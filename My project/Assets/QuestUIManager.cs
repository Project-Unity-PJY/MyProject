using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public GameObject questPanel; // QuestPanel을 할당합니다.
    private bool isPanelActive = false;

    void Start()
    {
        // QuestPanel 초기 비활성화
        questPanel.SetActive(false);

        // Button 컴포넌트를 가져와서 클릭 이벤트를 추가합니다.
        Button questButton = GetComponent<Button>();
        questButton.onClick.AddListener(ToggleQuestPanel);
    }

    // QuestPanel을 토글하는 함수
    void ToggleQuestPanel()
    {
        isPanelActive = !isPanelActive;
        questPanel.SetActive(isPanelActive);
    }
}
