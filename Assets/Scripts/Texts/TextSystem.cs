using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Member;





public class TextSystem : MonoBehaviour
{
    [SerializeField] private List<string> dialogs;
    [SerializeField] int interval;
    [SerializeField] private bool playSound;
    private dialogsWrap dialogData;
    [SerializeField] private SoundManager soundManager;

    [SerializeField] private int index = 0;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private AudioClip typingSound;
    private StringBuilder Content = new StringBuilder();
    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("json/dialog");
        string jsonData = textAsset.text;

        dialogData = JsonUtility.FromJson<dialogsWrap>(textAsset.text);
    }
    public IEnumerator TextTurn()
    {

        if (index >= dialogData.dialogs.Count)
        {
            yield return new WaitForSeconds(1f);
            Debug.LogWarning("텍스트 한도 초과함");
            yield break;
        }

        dialogs = dialogData.dialogs[index].dialog;
        interval = dialogData.dialogs[index].interval;

        for (int i = 0; i < dialogs.Count; i++)
        {
            Content.Clear();
            yield return StartCoroutine(ShowMessage(dialogs[i]));
            yield return new WaitForSeconds(interval);
        }
        messageText.text = "";
        index++;
    }
    private IEnumerator ShowMessage(string Value)
    {
        playSound = dialogData.dialogs[index].playSound;

        foreach (char charactor in Value)
        {
            Content.Append(charactor);
            if (!charactor.Equals(' '))
            {
                try
                {
                    if (playSound) soundManager.PlayAhSFX(typingSound);

                }
                catch (ArgumentNullException)
                {
                    Debug.LogWarning("이게 왜 null임?");
                }
            }
            messageText.text = Content.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
