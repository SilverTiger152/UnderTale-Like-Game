using System.Collections;
using UnityEngine;

public interface IBulletInfo
{
    public HPSizeControl HPSizeController { set; get; }
    public Rigidbody2D rb { set; get; }
    void OnTriggerStay2D(Collider2D collision);
}
