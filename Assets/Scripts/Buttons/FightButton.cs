using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TurnManager;
using static ButtonInformation;


/*
 * 싸우기 버튼
 * 아직 적에게 데미지 주는 거 구현 안함
 */

public class FightButton : MonoBehaviour, IButton
{
    [SerializeField] private ButtonInformation buttonInformation;
    [SerializeField] private Player player;
    [SerializeField] private HPSizeControl HPSizeController;

    public void onSelect()
    {

    }
    public void onClick()
    {
        // 딜 넣는 코드
        player.setIsPlayerTurn(false);
    }
    void Update()
    {
        if (buttonInformation.WhatType == ButtonType.Fight && Input.GetKeyDown(KeyCode.Return) && player.IsPlayerTurn == true)
        {
            onClick();
        }
    }

    public void UnAndEnable()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
