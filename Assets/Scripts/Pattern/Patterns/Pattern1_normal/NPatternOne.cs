using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//원형 탄환만

public class NPatternOne : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;

    public IEnumerator PatternExecute(float duration)
    {
        Coroutine shootCircleCoroutine = StartCoroutine(CircleBullet());

        yield return new WaitForSeconds(duration);

        StopCoroutine(shootCircleCoroutine);
        StopAllCoroutines();
        spawner.StopAllCoroutines();

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
        int i = 0;

        float angle = 0f;
        // 보따리는 재사용해도 되지만, 데이터 상자는 매번 새로 만드는 게 안전합니다.
        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 direction = rotation * Vector3.up;

            // 1. 보따리에 값 채우기
            bulletSettings.setCircleSettings(new Vector3(0f, 0f, 0f), direction, 3f, 1f);

            // 2. [수정] 이번 탄환만을 위한 전용 데이터 객체 생성
            CircleBulletData newData = new CircleBulletData();
            newData.ApplyTo(bulletSettings);

            // 3. 배달
            spawner.CopyCircle(newData);

            yield return new WaitForSeconds(0.05f);

            if (i < 38) angle += 50f;
            else angle -= 50f;

            i++;
        }
    }
}


