using System.Collections;
using UnityEngine;

public class NPatternFour : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    public IEnumerator PatternExecute(float duration)
    {
        // Coroutine shootSquareCoroutine = StartCoroutine(SquareBullet());

        // yield return new WaitForSeconds(duration);

        // StopCoroutine(shootSquareCoroutine);
        // StopAllCoroutines();
        // spawner.StopAllCoroutines();

        // // ��� ����� ����
        // LineRenderer[] warningLines = FindObjectsByType<LineRenderer>();
        // foreach (LineRenderer line in warningLines)
        // {
        //     if (line.gameObject != null) Destroy(line.gameObject);
        // }

        // // ��� �Ѿ� ����
        // GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        // foreach (GameObject bullet in bullets)
        // {
        //     if (bullet != null) Destroy(bullet);
        // }

        Coroutine shootCircleCoroutine = StartCoroutine(CircleBullet());

        yield return new WaitForSeconds(duration);

        StopCoroutine(shootCircleCoroutine);
        StopAllCoroutines();
        spawner.StopAllCoroutines();

        LineRenderer[] warningLines = FindObjectsByType<LineRenderer>();
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
        while (true)
        {
            BulletSettings bulletSettings = new BulletSettings();

            float randomOLY = Random.Range(-1, 2); 

            Vector3 originalPosition = new Vector3(10f, randomOLY, 0f);
            Vector3 direction = Vector3.left;

            bulletSettings.setCircleSettings(originalPosition, direction, 5f, 1f, 5f);

            CircleBulletData newData = new CircleBulletData();
            newData.ApplyTo(bulletSettings);

            spawner.CopyCircle(newData);

            yield return new WaitForSeconds(0.3f);
        }
    }

    // private IEnumerator SquareBullet()
    // {
    //     BulletSettings bulletSettings = new BulletSettings();

    //     while (true)
    //     {
    //         Vector3 position = new Vector3(0f, 1f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

    //         SquareBulletData newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         position = new Vector3(0f, -1f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         yield return new WaitForSeconds(1.5f);
           
    //     //---------------------------------------------------------------------

    //         position = new Vector3(0f, 0f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         position = new Vector3(-1f, 0f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         position = new Vector3(1f, 0f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         yield return new WaitForSeconds(1.5f);

    //         //---------------------------------------------------------------------

    //         position = new Vector3(0f, 1f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         position = new Vector3(0f, 0f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         position = new Vector3(0f, -1f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         yield return new WaitForSeconds(1.5f);

    //         //---------------------------------------------------------------

    //         position = new Vector3(0f, 1f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         position = new Vector3(0f, -1f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 3f, 1f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         position = new Vector3(-1f, 0f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         position = new Vector3(1f, 0f, 0f);

    //         bulletSettings.setSquareSettings(position, 1f, 1f, 3f, 0.7f);

    //         newData = new SquareBulletData();
    //         newData.ApplyTo(bulletSettings);

    //         StartCoroutine(spawner.CopySquare(newData));

    //         yield return new WaitForSeconds(1.5f);
    //     }
    // }
}
