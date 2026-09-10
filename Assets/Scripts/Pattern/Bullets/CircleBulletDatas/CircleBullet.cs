using System.Collections;
using UnityEngine;

//get data로 받고 복사하고 움직이고 충돌감지하는 것만 해야됨

public class CircleBullet : MonoBehaviour, IBulletInfo
{
    private Player player;

    CircleBulletData data;
    public HPSizeControl HPSizeController { get; set; }
    public Rigidbody2D rb { set; get; }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // 실제 내 리지드바디를 가져옴
        HPSizeController = Object.FindAnyObjectByType<HPSizeControl>();
        player = Object.FindAnyObjectByType<Player>();
    }

    public void getData(CircleBulletData data)
    {
        if (player != null && player.IsPlayerTurn && gameObject != null)
        {
            Destroy(gameObject);
            return;
        }

        this.data = data;

        if (data != null)
        {
            transform.localScale = Vector3.one * data.radious;
        }
    }

    public void OnTriggerStay2D(Collider2D collision) //데미지 넣음
    {
        if (collision.CompareTag("Player") && HPSizeController != null)
        {
            // 진짜 HP 관리자에게 데미지를 입히라고 명령합니다.
            HPSizeController.StartCoroutine(HPSizeController.muzukshigan(1f));
            Debug.Log("으앙 아프다");
        }
    }

    void FixedUpdate()
    {
        if (data != null && rb != null)
        {
            rb.linearVelocity = data.direction * data.speed;
        }
    }
}
