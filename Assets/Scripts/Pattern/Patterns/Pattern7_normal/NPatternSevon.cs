using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class NPatternSevon : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    public IEnumerator PatternExecute(float duration)
    {
        Coroutine shootSquareCoroutine = StartCoroutine(SquareBullet());
        Coroutine shootLinearCoroutine = StartCoroutine(QuadraticFunction());

        yield return new WaitForSeconds(duration);

        StopCoroutine(shootSquareCoroutine);
        StopCoroutine(shootLinearCoroutine);
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

    private IEnumerator SquareBullet()
    {
        BulletSettings bulletSettings = new BulletSettings();

        Vector3 ol_1 = new Vector3(-7f, 0f, 0f);
        Vector3 ol_2 = new Vector3(0f, -2f, 3f);

        float w = 1f;

        while (true)
        {
            bulletSettings.setSquareSettings(ol_1, 1f, w, 6f, 1f);

            SquareBulletData newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));


            bulletSettings.setSquareSettings(ol_2, 1f, 15f, 4f, 1f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            w += 2f;
            ol_1.x += 1f;
            ol_2.y = ol_2.y * -1f;

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator QuadraticFunction() 
    {
        BulletSettings bulletSettings = new BulletSettings();

        float p = 2f;
        Vector3 ol_1 = new Vector3(-9f, 3f, 0f);
        Vector3 ol_2 = new Vector3(-9f, -3f, 0f);

        while (true)
        {
            bulletSettings.setQuadraticFunctionSettings(ol_1, 8f, 0.5f, 1f, 0.4f, p, -7f, 1f);

            QuadraticFunctionData newData = new QuadraticFunctionData();
            newData.ApplyTo(bulletSettings);

            spawner.CopyQuadraticFunction(newData);


            bulletSettings.setQuadraticFunctionSettings(ol_2, 8f, 0.5f, 1f, -0.4f, p, 7f, 1f);

            newData = new QuadraticFunctionData();
            newData.ApplyTo(bulletSettings);

            spawner.CopyQuadraticFunction(newData);

            ol_1.x += 2f;
            ol_2.x += 2f;

            yield return new WaitForSeconds(1f);
        }
    }
}
