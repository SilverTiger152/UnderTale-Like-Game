using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TurnManager;
using static ButtonInformation;


/*
 * �ο�� ��ư
 * ���� ������ ������ �ִ� �� ���� ����
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
