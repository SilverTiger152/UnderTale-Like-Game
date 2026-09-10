using System.Collections;
using UnityEngine;

public class SquareBullet : MonoBehaviour, IBulletInfo
{
    private Player player;
    SquareBulletData data;
    public HPSizeControl HPSizeController { get; set; }
    public Rigidbody2D rb { get; set; }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // 실제 내 리지드바디를 가져옴
        HPSizeController = Object.FindAnyObjectByType<HPSizeControl>();
        player = Object.FindAnyObjectByType<Player>();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && HPSizeController != null)
        {
            // 진짜 HP 관리자에게 데미지를 입히라고 명령합니다.
            HPSizeController.StartCoroutine(HPSizeController.muzukshigan(1f));
            Debug.Log("으앙 아프다");
        }
    }

    public void getData(SquareBulletData data)
    {
        if (player != null && player.IsPlayerTurn && gameObject != null)
        {
            Destroy(gameObject);
            return;
        }

        this.data = data;

        if (data != null)
        {
            transform.localScale = new Vector3(data.width, data.height, 1f);
            StartCoroutine(Appear());
        }
    }

    private IEnumerator Appear()
    {
        yield return new WaitForSeconds(data.sustainmentTime);
        Destroy(gameObject);
    }
}
