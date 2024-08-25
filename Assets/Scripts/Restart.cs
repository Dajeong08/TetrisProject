using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Start() 
    {
        gameObject.SetActive(true);
    }
    void Update()
    {
    }

    public void OnClickRestart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 재로드
    }
}