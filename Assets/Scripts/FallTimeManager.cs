using UnityEngine;

public class FallTimeManager : MonoBehaviour
{
    public static float FallTime = 1.0f; // 기본 낙하 시간
    public float initialFallTime = 1.0f; // 초기 낙하 시간
    public float decreaseInterval = 15.0f; // 속도 증가 간격 (초 단위)
    private float elapsedTime; // 경과 시간

    private void Start()
    {
        FallTime = initialFallTime;
    }

    private void Update()
    {
        // 시간에 따라 낙하 속도 증가
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= decreaseInterval)
        {
            FallTime = Mathf.Max(0.1f, FallTime - 0.2f); // 최소값을 설정하여 너무 작아지지 않도록 보장
            Debug.Log("속도증가");
            elapsedTime = 0;
        }
    }
}