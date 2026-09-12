using System.Collections;
using UnityEngine;

public class NPatternTwo : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    [SerializeField] private Player player;

    public IEnumerator PatternExecute(float duration)
    {
        Debug.Log($"[NPatternTwo] 패턴 시작 (지속 시간: {duration}초)");
        // 1. 발사 루틴 시작
        Coroutine bulletRoutine = StartCoroutine(VanishBulletRoutine());

        // 2. 패턴 시간 대기
        yield return new WaitForSeconds(duration);

        // 3. 발사 중지
        Debug.Log("[NPatternTwo] 패턴 종료 - 탄환 발사 중지 및 정리 시작");
        StopCoroutine(bulletRoutine);

        // 4. 경고선 제거
        LineRenderer[] warningLines = FindObjectsByType<LineRenderer>();
        foreach (LineRenderer line in warningLines)
        {
            if (line.gameObject != null) Destroy(line.gameObject);
        }

        // 5. 총알 일괄 제거
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null) Destroy(bullet);
        }
    }

    private IEnumerator VanishBulletRoutine()
    {
        Debug.Log("[NPatternTwo] VanishBullet 발사 루틴 시작됨");
        
        if (spawner == null) Debug.LogError("[NPatternTwo] Spawner가 할당되지 않았습니다! Inspector를 확인하세요.");
        if (player == null) Debug.LogError("[NPatternTwo] Player가 할당되지 않았습니다! Inspector를 확인하세요.");

        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            // 플레이어 방향 계산
            Vector3 dir = player.transform.position - transform.position;
            dir.z = 0;

            Vector3 direction = dir.normalized;
            direction.y += 0.15f;

            // 탄환 세팅
            bulletSettings.setVanishSettings(
                new Vector3(5f, 0f, 0f), // ol:
                direction,               // tl:
                8f,                      // s:
                0.7f,                    // r:
                3.5f,                    // dr:
                "Vanish",                //text:
                0f                       // acceleration:
            );

            VanishBulletData newData = new VanishBulletData();
            newData.ApplyTo(bulletSettings);

            Debug.Log($"[NPatternTwo] VanishBullet 데이터 세팅 완료, Spawner로 전달 (방향: {direction})");

            // 발사
            if (spawner != null) spawner.CopyVanish(newData);

            yield return new WaitForSeconds(0.3f);
        }
    }
}
