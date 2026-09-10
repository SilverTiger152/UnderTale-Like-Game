//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using TMPro;
//using UnityEngine;
//using static ButtonInformation;

//public class ActionButton : MonoBehaviour, IButton
//{
//    public List<(string content, float duration)> LookTextList = new()
//    {
//        ("평범한 이택현이다. 머리는 좋은데 바보같다는 역설이 있다. \n근데 이 기능은 왜 만든걸까? 아무튼 화이팅해라.", 2f)
//    };
//    [SerializeField] private ButtonInformation buttonInformation;
//    [SerializeField] private Player player;
//    [SerializeField] private TMP_Text lookActionText;
//    [SerializeField] private TMP_Text otherActionText;
//    [SerializeField] private TMP_Text lookText;
//    private StringBuilder lookContent = new StringBuilder();
//    [SerializeField] private int index = 0;
//    private bool IsActionButtonActivated = false;
//    public void onClick()
//    {
//        lookActionText.text = "살펴보기";
//        otherActionText.text = "쫄기";
//        StartCoroutine(GoAction());
//    }
//    private IEnumerator GoAction()
//    {
//        turnStartEnd();
//        lookContent.Clear();

//        yield return new WaitForSeconds(0.3f);

//        while (player.OPlayer.activeSelf)
//        {
//            Vector3 playerPos = player.transform.position;

//            if (Input.GetKeyDown(KeyCode.Return) && player.IsPlayerTurn == true && Mathf.RoundToInt(playerPos.x * 10) == -75)
//            {
//                player.activePlayer();
//                yield return StartCoroutine(LookFor());
//                yield return new WaitForSeconds(LookTextList[index].duration);
//                player.setIsPlayerTurn(false);
//                lookText.text = "";
//                turnStartEnd();
//                player.activePlayer();
//                yield break;
//            }

//            //쫄기같은 다른 액션
//            else if (Input.GetKeyDown(KeyCode.Return) && player.IsPlayerTurn == true && Mathf.RoundToInt(playerPos.x * 10) == -33)
//            {
//                turnStartEnd();
//                player.setIsPlayerTurn(false);
//                yield break;
//            }

//            else if (Input.GetKeyDown(KeyCode.Escape) && player.IsPlayerTurn == true)
//            {
//                turnStartEnd();
//                yield break;
//            }

//            yield return null;
//        }
//    }
//    public void onSelect()
//    {
//        throw new System.NotImplementedException();
//    }
//    void Update()
//    {
//        if (buttonInformation.WhatType == ButtonType.LookFor && Input.GetKeyDown(KeyCode.Return) && player.IsPlayerTurn == true && IsActionButtonActivated == false)
//        {
//            onClick();
//        }
//        //대충 2페면 index = 1;
//    }
//    private void turnStartEnd()
//    {
//        if (!IsActionButtonActivated)
//        {
//            player.movePositions.Clear();
//            player.movePositions.AddRange(new Vector3[]
//            { 
//                new Vector3(-7.5f, -0.3f, 0f),
//                new Vector3(-3.3f, -0.3f, 0f)
//            });
//            player.currentIndex = Mathf.Clamp(player.currentIndex, 0, player.movePositions.Count - 1);
//            player.currentIndex = 0;
//            IsActionButtonActivated = true;
//        } 
//        else
//        {
//            player.movePositions.Clear();
//            player.movePositions.AddRange(new Vector3[]
//            {
//                    new Vector3(-7f, -4.3f, 0f),
//                    new Vector3(-1f, -4.3f, 0f),
//                    new Vector3(5f, -4.3f, 0f)
//            });
//            player.currentIndex = Mathf.Clamp(player.currentIndex, 0, player.movePositions.Count - 1);
//            lookActionText.text = "";
//            otherActionText.text = "";
//            player.currentIndex = 2;
//            IsActionButtonActivated = false;
//        }
//    }
//    private IEnumerator LookFor()
//    {
//        lookActionText.text = "";
//        otherActionText.text = "";
//        foreach (char charactor in LookTextList[index].content)
//        {
//            lookContent.Append(charactor);
//            lookText.text = lookContent.ToString();
//            yield return new WaitForSeconds(0.07f);
//        }
//    }
//}