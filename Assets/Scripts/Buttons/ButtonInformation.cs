using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonInformation : MonoBehaviour
{
    [SerializeField] private Player player;
    public enum ButtonType { 
        Fight,
        Item,
        LookFor
    }
    public ButtonType WhatType { get; private set; }
    void Update()
    {
        if (player.OPlayer.activeSelf) {
            GameObject player = GameObject.Find("Player"); // 오브젝트 이름이 "Player"일 경우
            Vector3 playerPos = player.transform.position;
            switch (Mathf.RoundToInt(playerPos.x * 10))
            {
                case -70:
                    WhatType = ButtonType.Fight;
                    break;
                case -10:
                    WhatType = ButtonType.Item;
                    break;
                case 50:
                    WhatType = ButtonType.LookFor;
                    break;
            }
        }
    }

    public void turnStartEnd()
    {

    }
}
