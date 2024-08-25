using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리에 필요한 네임스페이스

public class GameOver : MonoBehaviour
{
    public void next()
    {
        SceneManager.LoadScene(1);
    }
}