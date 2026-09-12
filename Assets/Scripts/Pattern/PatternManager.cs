using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PatternManager : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField]private int patternIndex = 0;

    [System.NonSerialized]
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
            Debug.LogError("patternList 占쏙옙占쏙옙 占쏙옙찾占쏙옙!");
            return;
        }
        string jsonData = textAsset.text;

        patternListData = JsonUtility.FromJson<PatternListWrap>(textAsset.text);
        if (patternListData.patterns == null)
        {
            Debug.LogError("占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占식쏙옙 占쏙옙占쏙옙!");
            return;
        }

        // 1. 占쌥듸옙占� 占십깍옙화占쏙옙 占쏙옙占쏙옙 占쌔억옙 NullReferenceException占쏙옙 占쏙옙 占쏙옙占싹댐옙.
        patternDictionary = new Dictionary<string, IPatternInfo>();

        // 2. 占쏙옙占쏙옙占쏙옙占싱쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占쏙옙占� 占쏙옙占쏙옙占쏙옙트占쏙옙 占쏙옙占쏙옙占심니댐옙.
        IPatternInfo[] patterns = GetComponentsInChildren<IPatternInfo>();

        foreach (var p in patterns)
        {
            string patternName = p.GetType().Name;

            // 3. 占쌩븝옙 占쏙옙占� 占쏙옙占쏙옙: 占싫곤옙占쏙옙 占싱몌옙占쏙옙 占쏙옙占쏙옙 클占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙 占쏙옙 占쏙옙占쏙옙 占쏙옙占�
            if (!patternDictionary.ContainsKey(patternName))
            {
                patternDictionary.Add(patternName, p);
                Debug.Log($"占쏙옙占쏙옙 占쏙옙占� 占쏙옙占쏙옙: {patternName}");
            }
            else
            {
                Debug.LogWarning($"占쌩븝옙占쏙옙 占쏙옙占쏙옙 클占쏙옙占쏙옙 占쌩곤옙: {patternName}. 占싹놂옙占쏙옙 占쏙옙溝絳求占�.");
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

        // patternID占쏙옙占� 占싱몌옙占쏙옙 占쏙옙킬訶占쏙옙占� 占쌍댐옙占쏙옙 확占쏙옙占싹곤옙, 占쏙옙占쏙옙占쏙옙 target占쏙옙 占쌍억옙占쏙옙
        if (patternDictionary.TryGetValue(id, out IPatternInfo target))
        {
            // 占쏙옙占쏙옙: 占쏙옙占쏙옙 占쏙옙占쏙옙
            yield return StartCoroutine(target.PatternExecute(duration));
        }
        else
        {
            // 占쏙옙占쏙옙: JSON占쏙옙 占쏙옙타占쏙옙 占쌍거놂옙 占쏙옙溝占쏙옙占� 占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙
            Debug.LogError($"占쏙옙占쏙옙 {id}占쏙옙 찾占쏙옙 占쏙옙 占쏙옙占쏙옙占싹댐옙!");
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
    //    foreach (var raw in BulletList) // 占쏙옙占썩서 raw占쏙옙 占쏙옙占쌘울옙 type占쏙옙 占쏙옙占쏙옙 占쏙옙占쏙옙 클占쏙옙占쏙옙
    //    {
    //        MoveData dataInstance = raw switch
    //        {
    //            "Circle" => new CircleBulletData { /* 占쏙옙 占쏙옙占쏙옙 */ },
    //            //"Square" => new SquareBulletData { /* 占쏙옙 占쏙옙占쏙옙 */ },
    //            _ => null
    //        };

    //        if (dataInstance != null)
    //            bulletMoveDatas.Add(dataInstance);
    //    }
    //}
}
