using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� źȯ��

public class NPatternOne : MonoBehaviour, IPatternInfo
{
[SerializeField] private BulletSpawner spawner;

    public IEnumerator PatternExecute(float duration)
    {
        // 미러 기믹 발동 (예: 패턴 시작 1초 뒤 발동, 3초 동안 유지)
        SpawnMirrorGimmick(1.0f, 3.0f);

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

    // 미러 기믹 세팅 및 spawner 전달 메서드
    private void SpawnMirrorGimmick(float reverseTime, float duration)
    {
        // 1. 설정 보따리에 미러 기믹 데이터 세팅[cite: 3]
        BulletSettings mirrorSettings = new BulletSettings();
        mirrorSettings.setMirrorSettings(reverseTime, duration);

        // 2. MirrorGimmikData 생성 및 데이터 적용[cite: 6]
        MirrorGimmikData mirrorData = new MirrorGimmikData();
        mirrorData.ApplyTo(mirrorSettings);

        // 3. BulletSpawner로 생성 전달[cite: 7]
        spawner.CopyMirror(mirrorData);
    }
    private IEnumerator CircleBullet()
    {
        int i = 0;

        float angle = 0f;
        // �������� �����ص� ������, ������ ���ڴ� �Ź� ���� ����� �� �����մϴ�.
        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 direction = rotation * Vector3.up;

            // 1. �������� �� ä���
            bulletSettings.setCircleSettings(new Vector3(0f, 0f, 0f), direction, 3f, 1f);

            // 2. [����] �̹� źȯ���� ���� ���� ������ ��ü ����
            CircleBulletData newData = new CircleBulletData();
            newData.ApplyTo(bulletSettings);

            // 3. ���
            spawner.CopyCircle(newData);

            yield return new WaitForSeconds(0.05f);

            if (i < 38) angle += 50f;
            else angle -= 50f;

            i++;
        }
    }
}


