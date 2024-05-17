using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    private static GameManager2 instance = null;
    public bool isPlayerHiding = false;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
              Destroy(this.gameObject);
        }
    }

    public static GameManager2 Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver function called. Reloading scene."); // 게임 오버 함수 호출 확인
        Time.timeScale = 0;
    }

    // 게임 재시작 함수
    public void GameStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public bool getPlayerHiding()
    {
        return isPlayerHiding;
    }

    public void setPlayerHiding()
    {
        isPlayerHiding = true;
    }
    public void setPlayerReveal()
    {
        isPlayerHiding = false;
    }
}
