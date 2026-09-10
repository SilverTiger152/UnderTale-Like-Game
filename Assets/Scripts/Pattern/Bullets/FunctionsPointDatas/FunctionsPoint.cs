using System.Collections;
using UnityEngine;

public class FunctionsPoint : MonoBehaviour, IBulletInfo
{
    private Player player;

    FunctionsPointData data;
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

    public void getData(FunctionsPointData data)
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

            Wait();
        }
    }
    private void Wait()
    {
        Destroy(gameObject, data.sustainmentTime);
    }
}
