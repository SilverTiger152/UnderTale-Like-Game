using UnityEngine;
using TMPro;

public class VanishBullet : MonoBehaviour, IBulletInfo
{
    private Player player;
    private VanishBulletData data;

    public HPSizeControl HPSizeController { get; set; }
    public Rigidbody2D rb { get; set; }

    [SerializeField] private BoxCollider2D hitCollider;
    [SerializeField] private TextMeshPro textMesh;

    private void Awake()
    {
        if (textMesh == null)
            textMesh = GetComponent<TextMeshPro>();

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        hitCollider = GetComponent<BoxCollider2D>();
        if (hitCollider == null)
        {
            hitCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        CircleCollider2D circleCol = GetComponent<CircleCollider2D>();
        if (circleCol != null)
        {
            Destroy(circleCol);
        }

        HPSizeController = Object.FindAnyObjectByType<HPSizeControl>();
        player = Object.FindAnyObjectByType<Player>();
        
        if (HPSizeController == null) Debug.LogWarning("[VanishBullet] HPSizeController를 씬에서 찾을 수 없습니다.");
        if (player == null) Debug.LogWarning("[VanishBullet] Player를 씬에서 찾을 수 없습니다.");
    }

    public void getData(VanishBulletData data)
    {
        if (player != null && player.IsPlayerTurn && gameObject != null)
        {
            Debug.Log("[VanishBullet] 플레이어 턴이므로 생성 즉시 파괴됩니다.");
            Destroy(gameObject);
            return;
        }

        this.data = data;
        transform.position = data.originalLocation;
        Debug.Log($"[VanishBullet] 데이터 초기화 됨 - 위치: {data.originalLocation}, 속도: {data.speed}, 텍스트: {data.textContent}");

        if (textMesh != null)
        {
            textMesh.text = data.textContent;
            textMesh.ForceMeshUpdate();

            if (hitCollider != null)
            {
                hitCollider.size = new Vector2(textMesh.textBounds.size.x, textMesh.textBounds.size.y);
                hitCollider.offset = new Vector2(textMesh.textBounds.center.x, textMesh.textBounds.center.y);
                hitCollider.isTrigger = true;
                Debug.Log($"[VanishBullet] BoxCollider2D 크기 자동 맞춤 완료: {hitCollider.size}");
            }
        }
        else if (hitCollider != null)
        {
            Debug.LogWarning("[VanishBullet] TextMeshPro가 연결되어 있지 않아 기본 반경으로 BoxCollider2D를 설정합니다.");
            hitCollider.size = new Vector2(data.radious, data.radious);
            hitCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("[VanishBullet] hitCollider를 설정할 수 없습니다!");
        }
    }

    private void Update()
    {
        if (player != null && data != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= data.detectionRadius)
            {
                if (textMesh != null && textMesh.enabled)
                {
                    textMesh.enabled = false;
                    Debug.Log($"[VanishBullet] 플레이어가 감지 영역({data.detectionRadius}) 내에 진입! (거리: {distance:F2}) - 탄환 투명화");
                }
            }
            else
            {
                if (textMesh != null && !textMesh.enabled)
                {
                    textMesh.enabled = true;
                    Debug.Log($"[VanishBullet] 플레이어가 감지 영역 밖으로 나감 (거리: {distance:F2}) - 탄환 표시");
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (data != null && rb != null)
        {
            rb.linearVelocity = data.direction * data.speed;
        }
        else if (rb == null)
        {
            Debug.LogError("[VanishBullet] Rigidbody2D 컴포넌트가 없습니다! 탄환이 이동할 수 없습니다.");
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && HPSizeController != null)
        {
            Debug.LogWarning("[VanishBullet] 💥 플레이어 피격 판정됨! 💥 데미지 로직 실행");
            HPSizeController.StartCoroutine(HPSizeController.muzukshigan(1f));
        }
    }
}
