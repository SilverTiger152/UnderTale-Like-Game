using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PatternManager : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField]private int patternIndex = 0;

    [SerializeField] public Dictionary<string, IPatternInfo> patternDictionary;

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
            Debug.LogError("patternList 파일 못찾음!");
            return;
        }
        string jsonData = textAsset.text;

        patternListData = JsonUtility.FromJson<PatternListWrap>(textAsset.text);
        if (patternListData.patterns == null)
        {
            Debug.LogError("패턴 데이터 파싱 실패!");
            return;
        }

        // 1. 반드시 초기화를 먼저 해야 NullReferenceException이 안 납니다.
        patternDictionary = new Dictionary<string, IPatternInfo>();

        // 2. 인터페이스를 구현한 모든 컴포넌트를 가져옵니다.
        IPatternInfo[] patterns = GetComponentsInChildren<IPatternInfo>();

        foreach (var p in patterns)
        {
            string patternName = p.GetType().Name;

            // 3. 중복 등록 방지: 똑같은 이름의 패턴 클래스가 여러 개 있을 경우
            if (!patternDictionary.ContainsKey(patternName))
            {
                patternDictionary.Add(patternName, p);
                Debug.Log($"패턴 등록 성공: {patternName}");
            }
            else
            {
                Debug.LogWarning($"중복된 패턴 클래스 발견: {patternName}. 하나만 등록됩니다.");
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

        // patternID라는 이름이 딕셔너리에 있는지 확인하고, 있으면 target에 넣어줌
        if (patternDictionary.TryGetValue(id, out IPatternInfo target))
        {
            // 성공: 패턴 실행
            yield return StartCoroutine(target.PatternExecute(duration));
        }
        else
        {
            // 실패: JSON에 오타가 있거나 등록되지 않은 패턴임
            Debug.LogError($"패턴 {id}를 찾을 수 없습니다!");
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
    //    foreach (var raw in BulletList) // 여기서 raw는 문자열 type을 가진 공통 클래스
    //    {
    //        MoveData dataInstance = raw switch
    //        {
    //            "Circle" => new CircleBulletData { /* 값 복사 */ },
    //            //"Square" => new SquareBulletData { /* 값 복사 */ },
    //            _ => null
    //        };

    //        if (dataInstance != null)
    //            bulletMoveDatas.Add(dataInstance);
    //    }
    //}
}
