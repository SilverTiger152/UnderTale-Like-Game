using System.Collections;
using UnityEngine;

public class QuadraticFunction : MonoBehaviour, IBulletInfo
{
    private Player player;

    [SerializeField] private GameObject warningLinePrefab;
    private GameObject lineObj;
    public LineRenderer lr;

    QuadraticFunctionData data;
    [SerializeField] public GameObject fp;
    public FunctionsPointData functionsPointData;
    public HPSizeControl HPSizeController { get; set; }
    public Rigidbody2D rb { get; set; }

    private float elapsedTime = 0f;
    private float yOffset;

    private int pointCount = 40; // 선을 구성할 점의 개수

    void Awake()
    {
        functionsPointData = new FunctionsPointData();
        rb = GetComponent<Rigidbody2D>(); // 실제 내 리지드바디를 가져옴
        HPSizeController = Object.FindAnyObjectByType<HPSizeControl>();

        lineObj = Instantiate(warningLinePrefab);
        player = Object.FindAnyObjectByType<Player>();
    }

    private void OnBecameInvisible()
    {
        if (lineObj != null) Destroy(lineObj);
        Destroy(gameObject);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && HPSizeController != null)
        {
            // 진짜 HP 관리자에게 데미지를 입히라고 명령합니다.
            HPSizeController.StartCoroutine(HPSizeController.muzukshigan(1f));
            Debug.Log("으앙 아프다");
        }
    }

    public void getData(QuadraticFunctionData data)
    {
        if (player != null && player.IsPlayerTurn && gameObject != null)
        {
            if (lineObj != null) Destroy(lineObj);
            Destroy(gameObject);
            return;
        }

        this.data = data;

        if (data != null)
        {
            lr = lineObj.GetComponent<LineRenderer>();

            // 1. 초기 위치 및 시간 설정
            transform.localPosition = data.originalLocation;
            transform.localScale = Vector3.one * data.radious;
            elapsedTime = 0f;

            // 2. yOffset 계산: 시작점(x=0)에서 공식의 y값을 미리 구함
            // y = a(0 - p)^2 + q
            yOffset = data.a * Mathf.Pow(-data.p, 2) + data.q;

            // 3. 곡선 경고선 그리기 (선택 사항)
            DrawQuadraticLine();
        }

        StartCoroutine(copyPoint());
    }
    private IEnumerator copyPoint()
    {
        int currentPointIndex = 0; // 현재 지우고 있는 점의 인덱스

        while (true)
        {
            if (data == null) yield break;

            GameObject pointObj = Instantiate(fp, transform.position, Quaternion.identity);
            FunctionsPoint pointScript = pointObj.GetComponent<FunctionsPoint>();

            functionsPointData.setInfo(data.radious, data.sustainmentTime);
            pointScript.getData(functionsPointData);

            // [수정] 선을 앞에서부터 지우는 로직 (시작점을 탄환 위치로 이동)
            if (lr != null && currentPointIndex < lr.positionCount)
            {
                // 탄환이 지나간 위치의 점들을 현재 탄환 위치로 모아버림 (선이 짧아지는 효과)
                for (int i = 0; i <= currentPointIndex; i++)
                {
                    lr.SetPosition(i, transform.position);
                }
                currentPointIndex++;
            }

            yield return new WaitForSeconds(data.interval);
        }
    }

    private void FixedUpdate()
    {
        if (data == null || rb == null) return;

        elapsedTime += Time.fixedDeltaTime;

        // 1. 가로 이동 거리 (x)
        float relativeX = elapsedTime * data.speed;

        // 2. 이차함수 공식 적용 (y) 및 보정값(yOffset) 차감
        // relativeY = a * (x - p)^2 + q - yOffset
        float relativeY = (data.a * Mathf.Pow(relativeX - data.p, 2)) + data.q - yOffset;

        // 3. 최종 위치 계산 및 적용
        Vector3 nextPos = new Vector3(
            data.originalLocation.x + (relativeX * data.xDir), // 방향(1 or -1) 반영
            data.originalLocation.y + relativeY,
            0f
        );

        rb.MovePosition(nextPos);
    }

    private void DrawQuadraticLine()
    {
        lr.positionCount = pointCount;
        lr.startWidth = 0.07f;
        lr.endWidth = 0.07f;

        for (int i = 0; i < pointCount; i++)
        {
            // [수정] i * 0.5f 대신 탄환의 속도와 interval을 고려한 간격 사용 추천
            float x = i * (data.speed * data.interval);
            float y = (data.a * Mathf.Pow(x - data.p, 2)) + data.q - yOffset;

            Vector3 pointPos = new Vector3(
                data.originalLocation.x + (x * data.xDir),
                data.originalLocation.y + y,
                0f
            );
            lr.SetPosition(i, pointPos);
        }
    }
}
