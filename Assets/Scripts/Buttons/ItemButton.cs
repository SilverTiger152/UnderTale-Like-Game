using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using static ButtonInformation;
using static UnityEditor.Progress;

public class ItemButton : MonoBehaviour, IButton
{

    //public List<ItemBase> items = new List<ItemBase>();
    //[SerializeField] private Player player;
    //[SerializeField] private ButtonInformation buttonInformation;
    //private bool IsItemButtonActivated = false;

    public void onClick()
    {
        //StartCoroutine(GoItem());
    }

    //private IEnumerator GoItem()
    //{
    //    yield return new WaitForSeconds(1);
    //}

    public void onSelect()
    {
        throw new System.NotImplementedException();
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if (buttonInformation.WhatType == ButtonType.LookFor && Input.GetKeyDown(KeyCode.Return) && player.IsPlayerTurn == true && IsItemButtonActivated == false)
    //    {
    //        onClick();
    //    }
    //}

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
