using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SetActiveTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnClickStop()
    {
        Debug.Log("Stop");
        gameObject.SetActive(true);
    }
    public void OnClickRestart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 재로드
    }
}
