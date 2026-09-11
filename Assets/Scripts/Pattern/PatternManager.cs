using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PatternManager : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField]private int patternIndex = 0;

    public Dictionary<string, IPatternInfo> patternDictionary;

    [SerializeField] private GameObject textBoxInside;
    [SerializeField] private GameObject textBoxOutline;

    private string status;
    private string id;
    private List<float> boxSize;
    private List<float> boxPosition;
    private List<string> bullets;
    private float duration;

    [SerializeField] private PatternListWrap patternListData;
    //[SerializeField] public List<MoveData> bulletMoveDatas;
    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("json/patternList");
        if (textAsset == null)
        {
            Debug.LogError("patternList ���� ��ã��!");
            return;
        }
        string jsonData = textAsset.text;

        patternListData = JsonUtility.FromJson<PatternListWrap>(textAsset.text);
        if (patternListData.patterns == null)
        {
            Debug.LogError("���� ������ �Ľ� ����!");
            return;
        }

        // 1. �ݵ�� �ʱ�ȭ�� ���� �ؾ� NullReferenceException�� �� ���ϴ�.
        patternDictionary = new Dictionary<string, IPatternInfo>();

        // 2. �������̽��� ������ ��� ������Ʈ�� �����ɴϴ�.
        IPatternInfo[] patterns = GetComponentsInChildren<IPatternInfo>();

        foreach (var p in patterns)
        {
            string patternName = p.GetType().Name;

            // 3. �ߺ� ��� ����: �Ȱ��� �̸��� ���� Ŭ������ ���� �� ���� ���
            if (!patternDictionary.ContainsKey(patternName))
            {
                patternDictionary.Add(patternName, p);
                Debug.Log($"���� ��� ����: {patternName}");
            }
            else
            {
                Debug.LogWarning($"�ߺ��� ���� Ŭ���� �߰�: {patternName}. �ϳ��� ��ϵ˴ϴ�.");
            }
        }
    }
    
    public IEnumerator PatternExecute()
    {
        if (patternIndex >= patternListData.patterns.Count)
        {
            player.setIsPlayerTurn(true);
            yield break;
        }

        StartCoroutine(player.EPlayerTurn());

        status = patternListData.patterns[patternIndex].status;
        id = patternListData.patterns[patternIndex].id;

        //bullets = patternListData.patterns[patternIndex].bullets;
        duration = patternListData.patterns[patternIndex].duration;

        boxSize = patternListData.patterns[patternIndex].boxSize;
        boxPosition = patternListData.patterns[patternIndex].boxPosition;

        ChangeTextboxScale();

        //DetermineBullets(bullets);

        // patternID��� �̸��� ��ųʸ��� �ִ��� Ȯ���ϰ�, ������ target�� �־���
        if (patternDictionary.TryGetValue(id, out IPatternInfo target))
        {
            // ����: ���� ����
            yield return StartCoroutine(target.PatternExecute(duration));
        }
        else
        {
            // ����: JSON�� ��Ÿ�� �ְų� ��ϵ��� ���� ������
            Debug.LogError($"���� {id}�� ã�� �� �����ϴ�!");
        }

        // -----------------------------------------------------------------------------------

        player.setIsPlayerTurn(true);

        ChangeTextboxScale();

        patternIndex++;

        //bulletMoveDatas.Clear();
        
    }

    private void ChangeTextboxScale()
    {
        if (player.IsPlayerTurn)
        {
            textBoxInside.transform.position = new Vector2(0f, -1.8f);
            textBoxInside.transform.localScale = new Vector2(16.2f, 2f);

            textBoxOutline.transform.position = new Vector2(0f, -1.8f);
            textBoxOutline.transform.localScale = new Vector3(16.3f, 2.1f);
        }
        else
        {
            Vector2 textboxPosition = new Vector2(boxPosition[0], boxPosition[1]);
            Vector2 textboxScale = new Vector2(boxSize[0], boxSize[1]);

            float halfWidth = textboxScale.x / 2f;
            float halfHeight = textboxScale.y / 2f;

            float maxXPosition = textboxPosition.x + halfWidth;
            float minXPosition = textboxPosition.x - halfWidth;
            float maxYPosition = textboxPosition.y + halfHeight;
            float minYPosition = textboxPosition.y - halfHeight;

            textBoxInside.transform.position = textboxPosition;
            textBoxInside.transform.localScale = textboxScale;

            textBoxOutline.transform.position = textboxPosition;
            textBoxOutline.transform.localScale = textboxScale + new Vector2(0.1f, 0.1f);

            player.changeClamp(minXPosition, maxXPosition, minYPosition, maxYPosition);
        }
    }
    //public void DetermineBullets(List<string> BulletList)
    //{
    //    foreach (var raw in BulletList) // ���⼭ raw�� ���ڿ� type�� ���� ���� Ŭ����
    //    {
    //        MoveData dataInstance = raw switch
    //        {
    //            "Circle" => new CircleBulletData { /* �� ���� */ },
    //            //"Square" => new SquareBulletData { /* �� ���� */ },
    //            _ => null
    //        };

    //        if (dataInstance != null)
    //            bulletMoveDatas.Add(dataInstance);
    //    }
    //}
}
