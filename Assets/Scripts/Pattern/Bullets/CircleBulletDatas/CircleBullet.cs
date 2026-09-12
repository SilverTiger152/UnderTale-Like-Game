using System.Collections;
using UnityEngine;

public class CircleBullet : MonoBehaviour, IBulletInfo
{
    private Player player;

    CircleBulletData data;
    public HPSizeControl HPSizeController { get; set; }
    public Rigidbody2D rb { set; get; }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // rd ∞°¡Æø¿±‚
        HPSizeController = Object.FindAnyObjectByType<HPSizeControl>();
        player = Object.FindAnyObjectByType<Player>();
        Destroy(gameObject, 10f); // 
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

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && HPSizeController != null)
        {
            HPSizeController.StartCoroutine(HPSizeController.muzukshigan(1f));
            Debug.Log("ÔøΩÔøΩÔøΩÔøΩ ÔøΩÔøΩÔøΩÔøΩÔøΩÔøΩ");
        }
    }

    void FixedUpdate()
    {

        if (data != null && rb != null)
        {
            data.speed += data.acceleration * Time.fixedDeltaTime; // Í∞??Üç?èÑ ?†Å?ö©

            rb.linearVelocity = data.direction * data.speed;
        }
    }


}
