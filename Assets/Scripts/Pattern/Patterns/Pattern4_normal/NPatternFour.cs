using System.Collections;
using UnityEngine;

public class NPatternFour : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    public IEnumerator PatternExecute(float duration)
    {
        Coroutine shootSquareCoroutine = StartCoroutine(SquareBullet());

        yield return new WaitForSeconds(duration);

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

    private IEnumerator SquareBullet()
    {
        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            Vector3 position = new Vector3(0f, 1f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

            SquareBulletData newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            position = new Vector3(0f, -1f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            yield return new WaitForSeconds(1.5f);
           
        //---------------------------------------------------------------------

            position = new Vector3(0f, 0f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            position = new Vector3(-1f, 0f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            position = new Vector3(1f, 0f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            yield return new WaitForSeconds(1.5f);

            //---------------------------------------------------------------------

            position = new Vector3(0f, 1f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            position = new Vector3(0f, 0f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            position = new Vector3(0f, -1f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            yield return new WaitForSeconds(1.5f);

            //---------------------------------------------------------------

            position = new Vector3(0f, 1f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            position = new Vector3(0f, -1f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            position = new Vector3(-1f, 0f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            position = new Vector3(1f, 0f, 0f);

            bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

            newData = new SquareBulletData();
            newData.ApplyTo(bulletSettings);

            StartCoroutine(spawner.CopySquare(newData));

            yield return new WaitForSeconds(1.5f);
        }
    }
}
