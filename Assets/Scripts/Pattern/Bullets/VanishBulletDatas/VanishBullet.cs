using UnityEngine;
using TMPro;

public class VanishBullet : MonoBehaviour
{
    private Vector3 moveDirection;
    private float speed;

    [SerializeField] private CircleCollider2D detectionCollider;
    [SerializeField] private TextMeshPro textMesh;

    private void Awake()
    {
        if (textMesh == null)
            textMesh = GetComponent<TextMeshPro>();
    }

    public void getData(VanishBulletData data)
    {
        transform.position = data.originalLocation;
        moveDirection = data.direction.normalized;
        speed = data.speed;

        // 1. TextMeshPro 글자 세팅
        if (textMesh != null)
        {
            textMesh.text = data.textContent;
        }

        // 2. 감지 구역 범위 설정
        if (detectionCollider != null)
        {
            detectionCollider.radius = data.detectionRadius;
            detectionCollider.isTrigger = true;
        }
    }

    private void Update()
    {
        // 3. 원형 탄환과 동일한 직진 이동
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 감지 영역 들어오면 글자 숨기기
            if (textMesh != null) textMesh.enabled = false;

            // 플레이어 피격 로직 호출 (프로젝트 피격 함수명에 맞춰 사용)
            // collision.GetComponent<PlayerController>()?.TakeDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 감지 영역 벗어나면 글자 다시 보이기
            if (textMesh != null) textMesh.enabled = true;
        }
    }
}