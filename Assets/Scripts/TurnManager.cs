using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * 턴 관리
 * 스테이트 설정, 다음 스테이트로 넘어가기가 있음
 */

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextSystem text;
    [SerializeField] private PatternManager patternManager;
    [SerializeField] private FightButton fbutton;
    [SerializeField] private ItemButton ibutton;
    void Start()
    {
        StartCoroutine(GameLoop());
    }
    private IEnumerator GameLoop() 
    {
        while (true) {

            yield return StartCoroutine(PlayerFlow());

            player.activePlayer();
            fbutton.UnAndEnable();
            ibutton.UnAndEnable();
            yield return StartCoroutine(TextFlow());

            player.activePlayer();
            yield return StartCoroutine(EnemyFlow());

            fbutton.UnAndEnable();
            ibutton.UnAndEnable();
        }
    }
    private IEnumerator PlayerFlow()
    {
        Debug.Log("플레이어 턴입니다.");
        yield return StartCoroutine(player.PlayerTurn());
    }
    private IEnumerator TextFlow()
    {
        Debug.Log("텍스트 턴입니다");
        yield return StartCoroutine(text.TextTurn());
    }
    private IEnumerator EnemyFlow()
    {
        Debug.Log("적 턴입니다");
        yield return StartCoroutine(patternManager.PatternExecute());
    }
}
