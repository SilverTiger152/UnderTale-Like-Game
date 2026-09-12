using System.Collections;
using UnityEngine;

public class NPatternThree : MonoBehaviour, IPatternInfo
{
    [SerializeField] private BulletSpawner spawner;
    public IEnumerator PatternExecute(float duration)
    {
        SpawnPannobGimmick(0f, 11.0f, 0.8f); // ?˜ˆ?‹œ: 1ì´? ?’¤ ë°œë™, 3ì´? ?™?•ˆ ?œ ì§?, ?‹œ?•¼ ? œ?•œ ?¬ê¸? 0.5

        Coroutine shootCircleCoroutine = StartCoroutine(CircleBullet());

        yield return new WaitForSeconds(duration);

        StopCoroutine(shootCircleCoroutine);
        StopAllCoroutines();
        spawner.StopAllCoroutines();

        // ï¿½ï¿½ï¿? ï¿½ï¿½ï¿½ï¿½ï¿? ï¿½ï¿½ï¿½ï¿½
        LineRenderer[] warningLines = FindObjectsByType<LineRenderer>();
        foreach (LineRenderer line in warningLines)
        {
            if (line.gameObject != null) Destroy(line.gameObject);
        }

        // ï¿½ï¿½ï¿? ï¿½Ñ¾ï¿½ ï¿½ï¿½ï¿½ï¿½
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null) Destroy(bullet);
        }
    }

    private void SpawnPannobGimmick(float darkTime, float duration, float targetVisionScale)
    {
        // 1. ?„¤? • ë³´ë”°ë¦¬ì— ?Œ¬?…¸ë¸? ê¸°ë?? ?°?´?„° ?„¸?Œ…[cite: 3]
        BulletSettings pannobSettings = new BulletSettings();
        pannobSettings.setPannobGimmikSettings(darkTime, duration, targetVisionScale);

        // 2. PannobGimmikData ?ƒ?„± ë°? ?°?´?„° ? ?š©[cite: 6]
        PannobGimmikData pannobData = new PannobGimmikData();
        pannobData.ApplyTo(pannobSettings);

        // 3. BulletSpawnerë¡? ?ƒ?„± ? „?‹¬[cite: 7]
        spawner.CopyPannob(pannobData);
    }

    private IEnumerator CircleBullet()
    {
        // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Øµï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½, ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ú´ï¿½ ï¿½Å¹ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿? ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Õ´Ï´ï¿½.
        BulletSettings bulletSettings = new BulletSettings();

        while (true)
        {
            float randomOLY = Random.Range(-4.5f, 4.5f);
            Vector3 originalPosition = new Vector3(6f, randomOLY, 0f);

            Vector3 direction = (new Vector3(-10f, originalPosition.y, 0f) - originalPosition).normalized;

            // 1. ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ Ã¤ï¿½ï¿½ï¿?
            bulletSettings.setCircleSettings(originalPosition, direction, 10f, 0.6f, 0f);

            // 2. [ï¿½ï¿½ï¿½ï¿½] ï¿½Ì¹ï¿½ ÅºÈ¯ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ã¼ ï¿½ï¿½ï¿½ï¿½
            CircleBulletData newData = new CircleBulletData();
            newData.ApplyTo(bulletSettings);

            // 3. ï¿½ï¿½ï¿?
            spawner.CopyCircle(newData);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
