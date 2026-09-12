using System.Collections;
using UnityEngine;

public class ReturnGimmik : MonoBehaviour
{
    public void StartRegressionEffect(ReturnGimmikData data)
    {
        StartCoroutine(RegressionRoutine(data));
    }

    private IEnumerator RegressionRoutine(ReturnGimmikData data)
    {
        // 1. 발동 전 대기 시간
        yield return new WaitForSeconds(data.returnTime);

        // 2. 씬에 있는 모든 TimeTracker (플레이어 + 총알) 탐색
        TimeTracker[] allTrackers = FindObjectsByType<TimeTracker>();

        // 3. 전부 동시에 2초 전 위치로 팍! 순간이동
        foreach (TimeTracker tracker in allTrackers)
        {
            if (tracker != null)
            {
                tracker.RewindInstant();
            }
        }

        // 4. 발동 끝났으니 기믹 오브젝트 즉시 삭제
        Destroy(gameObject);
    }
}