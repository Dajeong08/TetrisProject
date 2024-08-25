using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public string gameOverLayerName = "GameOverLain"; // GameOverLain 오브젝트의 태그 이름
    public float timeThreshold = 3f; // 충돌 지속 시간 기준
    private float collisionTime = 0f; // 충돌 시간 측정

    private void OnTriggerStay(Collider other)
    {
        // 충돌한 오브젝트의 태그가 GameOverLain인지 확인
        if (other.CompareTag("GameOverLain"))
        {
            Debug.Log("충돌!");
            collisionTime += Time.deltaTime; // 충돌 시간 누적

            // 충돌 시간이 기준을 초과하면 씬 전환
            if (collisionTime >= timeThreshold)
            {
                Debug.Log("게임 오버 씬 전환");
                SceneManager.LoadScene(2); // 2번 씬으로 전환
            }
        }
        else
        {
            // 충돌 오브젝트가 GameOverLain이 아닐 경우 시간 초기화
            collisionTime = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 충돌이 끝났을 때 시간 초기화
        if (other.CompareTag(gameOverLayerName))
        {
            collisionTime = 0f;
        }
    }
}