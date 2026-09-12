using System.Collections;
using UnityEngine;

public class NPatternFive : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;

    public IEnumerator PatternExecute(float duration)
    {
        // 4초, 8초 시점에 2초 전으로 되돌아가는 회귀 기믹 발동
        SpawnReturnGimmick(4f);
        SpawnReturnGimmick(8f);

        Coroutine shootCircleCoroutine = StartCoroutine(CircleBullet());

        yield return new WaitForSeconds(duration);

        StopCoroutine(shootCircleCoroutine);
        StopAllCoroutines();
        spawner.StopAllCoroutines();

        // 모든 총알 삭제
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null) Destroy(bullet);
        }
    }

    private void SpawnReturnGimmick(float returnTime)
    {
        BulletSettings returnSettings = new BulletSettings();
        returnSettings.setReturnSettings(returnTime);

        ReturnGimmikData returnData = new ReturnGimmikData();
        returnData.ApplyTo(returnSettings);

        spawner.CopyReturn(returnData);
    }

    private IEnumerator CircleBullet()
    {
        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            // 상자 크기 4방향 외곽에서 스폰 위치 계산
            int side = Random.Range(0, 4);
            Vector3 spawnPos = Vector3.zero;

            switch (side)
            {
                case 0: // 상단에서 스폰 -> 아래로
                    spawnPos = new Vector3(Random.Range(-2.3f, 2.3f), 4.5f, 0f);
                    break;
                case 1: // 하단에서 스폰 -> 위로
                    spawnPos = new Vector3(Random.Range(-2.3f, 2.3f), -4.5f, 0f);
                    break;
                case 2: // 좌측에서 스폰 -> 우측으로
                    spawnPos = new Vector3(-4.5f, Random.Range(-2.3f, 2.3f), 0f);
                    break;
                case 3: // 우측에서 스폰 -> 좌측으로
                    spawnPos = new Vector3(4.5f, Random.Range(-2.3f, 2.3f), 0f);
                    break;
            }

            // 상자 중심부를 향해 발사 방향 설정
            Vector3 targetPos = new Vector3(Random.Range(-1.8f, 1.8f), Random.Range(-1.8f, 1.8f), 0f);
            Vector3 direction = (targetPos - spawnPos).normalized;

            bulletSettings.setCircleSettings(spawnPos, direction, 4.5f, 0.6f, 0f);

            CircleBulletData newData = new CircleBulletData();
            newData.ApplyTo(bulletSettings);

            spawner.CopyCircle(newData);

            yield return new WaitForSeconds(0.35f);
        }
    }
}