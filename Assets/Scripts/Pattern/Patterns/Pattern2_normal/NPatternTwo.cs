using System.Collections;
using UnityEngine;

public class NPatternTwo : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    [SerializeField] private Player player;

    public IEnumerator PatternExecute(float duration)
    {
        Coroutine shootCircleCoroutine = StartCoroutine(CircleBullet());

        yield return new WaitForSeconds(duration);

        StopCoroutine(shootCircleCoroutine);
        StopAllCoroutines();
        spawner.StopAllCoroutines();

        // 모든 경고선 삭제
        LineRenderer[] warningLines = FindObjectsByType<LineRenderer>(FindObjectsSortMode.None);
        foreach (LineRenderer line in warningLines)
        {
            if (line.gameObject != null) Destroy(line.gameObject);
        }

        // 모든 총알 삭제
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null) Destroy(bullet);
        }
    }

    private IEnumerator CircleBullet()
    {
        // 보따리는 재사용해도 되지만, 데이터 상자는 매번 새로 만드는 게 안전합니다.
        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            Vector3 dir = player.transform.position - transform.position;

            dir.z = 0;

            Vector3 direction = dir.normalized;
            direction.y += 0.15f;

            // 1. 보따리에 값 채우기
            bulletSettings.setCircleSettings(new Vector3(5f, 0f, 0f), direction, 8f, 0.7f);

            // 2. [수정] 이번 탄환만을 위한 전용 데이터 객체 생성
            CircleBulletData newData = new CircleBulletData();
            newData.ApplyTo(bulletSettings);

            // 3. 배달
            spawner.CopyCircle(newData);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
