using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class NPatternFive : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    public IEnumerator PatternExecute(float duration)
    {
        Coroutine shootLinearCoroutine = StartCoroutine(LinearFunction());

        yield return new WaitForSeconds(duration);

        StopCoroutine(shootLinearCoroutine);
        StopAllCoroutines();
        spawner.StopAllCoroutines();

        // ��� ����� ����
        LineRenderer[] warningLines = FindObjectsByType<LineRenderer>();
        foreach (LineRenderer line in warningLines)
        {
            if (line.gameObject != null) Destroy(line.gameObject);
        }

        // ��� �Ѿ� ����
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null) Destroy(bullet);
        }
    }

    private IEnumerator LinearFunction()
    {
        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            float slope = Random.Range(-4f, 4f);

            while (slope < -2f || slope > 2f)
            {
                slope = Random.Range(-4f, 4f); // 0�� ������ �ٽ� ����
            }

            float positionY = (slope < 0f) ? 4f : -4f;
            float positionX = (slope < 0f) ? Random.Range(-4f, 0f) : Random.Range(0f, 4f);

            Vector3 position = new Vector3(positionX, positionY, 0f);

            bulletSettings.setLinearFunctionSettings(position, 9f, 0.3f, slope, 1f);

            LinearFunctionData newData = new LinearFunctionData();
            newData.ApplyTo(bulletSettings);

            spawner.CopyLinearFunction(newData);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
