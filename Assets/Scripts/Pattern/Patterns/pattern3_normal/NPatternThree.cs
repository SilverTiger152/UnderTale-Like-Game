using System.Collections;
using UnityEngine;

public class NPatternThree : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    public IEnumerator PatternExecute(float duration)
    {
        Coroutine shootCircleCoroutine = StartCoroutine(CircleBullet());
        Coroutine shootSquareCoroutine = StartCoroutine(SquareBullet());

        yield return new WaitForSeconds(duration);

        StopCoroutine(shootCircleCoroutine);
        StopCoroutine(shootSquareCoroutine);
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
            float randomOLY = Random.Range(-4.5f, 4.5f);
            Vector3 originalPosition = new Vector3(6f, randomOLY, 0f);

            Vector3 direction = (new Vector3(-10f, originalPosition.y, 0f) - originalPosition).normalized;

            // 1. 보따리에 값 채우기
            bulletSettings.setCircleSettings(originalPosition, direction, 10f, 0.6f);

            // 2. [수정] 이번 탄환만을 위한 전용 데이터 객체 생성
            CircleBulletData newData = new CircleBulletData();
            newData.ApplyTo(bulletSettings);

            // 3. 배달
            spawner.CopyCircle(newData);

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SquareBullet()
    {
        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            float randomPX = Random.Range(-5f, 5f);
            float randomPY = Random.Range(-3.5f, 3.5f);

            Vector3 position = new Vector3(randomPX, randomPY, 0f);

            bulletSettings.setSquareSettings(position, 2f, 3f, 3f, 3f);

            SquareBulletData newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            yield return new WaitForSeconds(1f);

        }
    }
}
