using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class NPatternSix : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    public IEnumerator PatternExecute(float duration)
    {
        Coroutine shootSquareCoroutine = StartCoroutine(SquareBullet());
        Coroutine shootLinearCoroutine = StartCoroutine(LinearFunction());

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

    private IEnumerator LinearFunction()
    {
        BulletSettings bulletSettings = new BulletSettings();

        int positionY = 1;

        while (true)
        {
            Vector3 position = new Vector3(15f, positionY, 0f);

            bulletSettings.setLinearFunctionSettings(position, 10f, 0.7f, 0f, 0.3f);

            LinearFunctionData newData = new LinearFunctionData();
            newData.ApplyTo(bulletSettings);

            spawner.CopyLinearFunction(newData);

            yield return new WaitForSeconds(1f);

            positionY = (positionY == -1) ? 1 : positionY - 1;
        }
    }

    private IEnumerator SquareBullet()
    {
        BulletSettings bulletSettings = new BulletSettings();

        float positionX_1 = -6f;
        float width_1 = 1f;

        float positionX_2 = 1.5f;
        float width_2 = 10f;

        yield return new WaitForSeconds(0.5f);

        while (true) 
        {
            Vector3 position = new Vector3(positionX_1, 0f, 0f);

            bulletSettings.setSquareSettings(position, 1f, width_1, 3f, 1f);

            SquareBulletData newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));


            position = new Vector3(positionX_2, 0f, 0f);

            bulletSettings.setSquareSettings(position, 1f, width_2, 3f, 1f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            yield return new WaitForSeconds(1f);

            positionX_1 += 0.5f;
            width_1 += 1f;

            positionX_2 += 0.5f;
            width_2 -= 1f;
        }
    }
}
