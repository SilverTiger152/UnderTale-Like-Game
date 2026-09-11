using System.Collections;
using UnityEngine;

public class NPatternTwo : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    [SerializeField] private Player player;

    public IEnumerator PatternExecute(float duration)
    {
        // 1. 배니시 탄환 발사 코루틴 실행
        Coroutine bulletRoutine = StartCoroutine(VanishBulletRoutine());

        // 2. 패턴 유지 시간동안 대기
        yield return new WaitForSeconds(duration);

        // 3. 패턴 종료 시 탄환 발사 중단
        StopCoroutine(bulletRoutine);

        // 4. 모든 경고선 삭제
        LineRenderer[] warningLines = FindObjectsByType<LineRenderer>(FindObjectsSortMode.None);
        foreach (LineRenderer line in warningLines)
        {
            if (line.gameObject != null) Destroy(line.gameObject);
        }

        // 5. 모든 총알 삭제
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null) Destroy(bullet);
        }
    }

    private IEnumerator VanishBulletRoutine()
    {
        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            // 플레이어를 향해 위치 및 조준 방향을 계산하는 기존 로직 유지
            Vector3 dir = player.transform.position - transform.position;
            dir.z = 0;

            Vector3 direction = dir.normalized;
            direction.y += 0.15f;

            // 원형 탄환 대신 배니시 탄환 전용 데이터 세팅
            bulletSettings.setVanishSettings(
                new Vector3(5f, 0f, 0f), // ol: 위치
                direction,               // tl: 방향
                8f,                      // s: 속도
                0.7f,                    // r: 탄환 기본 크기 (누락되었던 값)
                1.5f,                    // dr: 감지 범위
                "Vanish"                 // text: 출력 텍스트
            );

            VanishBulletData newData = new VanishBulletData();
            newData.ApplyTo(bulletSettings);

            // 배니시 탄환 생성
            spawner.CopyVanish(newData);

            yield return new WaitForSeconds(0.2f);
        }
    }
}