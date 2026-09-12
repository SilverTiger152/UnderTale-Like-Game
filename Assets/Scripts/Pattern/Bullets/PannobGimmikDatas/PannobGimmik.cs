using System.Collections;
using UnityEngine;

public class PannobGimmik : MonoBehaviour
{
    [Header("시야 마스크 (자식 오브젝트)")]
    [SerializeField] private Transform maskTransform; // VisionMask 트랜스폼

    private Transform playerTransform;
    private Vector3 initialScale;

    private void Awake()
    {
        // 1. 자식에 있는 VisionMask 자동 연결
        if (maskTransform == null)
        {
            Transform foundMask = transform.Find("VisionMask");
            if (foundMask != null) maskTransform = foundMask;
        }

        if (maskTransform != null)
        {
            initialScale = maskTransform.localScale;
            maskTransform.gameObject.SetActive(false); // 딜레이 전까진 꺼둠
        }

        // 2. 플레이어 자동 탐색
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        // 플레이어를 항상 따라다님
        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }

    // 외부(BulletSpawner 등)에서 기믹 시작할 때 호출
    public void StartPannobEffect(PannobGimmikData data)
    {
        StartCoroutine(PannobRoutine(data));
    }

    private IEnumerator PannobRoutine(PannobGimmikData data)
    {
        // 1. 발동 대기 시간
        yield return new WaitForSeconds(data.darkTime);

        // 2. 시야 제한 켜기 (크기 조절)
        if (maskTransform != null)
        {
            maskTransform.gameObject.SetActive(true);
            maskTransform.localScale = initialScale * data.targetVisionScale;
        }

        // 3. 지속 시간 동안 유지
        yield return new WaitForSeconds(data.duration);

        // 4. 시간 끝나면 프리팹 통째로 삭제 (화면 원래대로 복구됨)
        Destroy(gameObject);
    }
}