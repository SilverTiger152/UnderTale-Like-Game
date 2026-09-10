using System.Collections;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class LinearFunction : MonoBehaviour, IBulletInfo
{
    private Player player;

    [SerializeField] private GameObject warningLinePrefab;
    private GameObject lineObj;
    public LineRenderer lr;

    LinearFunctionData data;
    [SerializeField] public GameObject fp;
    public FunctionsPointData functionsPointData;
    public HPSizeControl HPSizeController { get; set; }
    public Rigidbody2D rb { get; set; }
    private float xDir;
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

    public void getData(LinearFunctionData data)
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

            float currentXDir = (data.slope < 0) ? 1f : -1f;
            Vector3 direction = new Vector3(currentXDir, data.slope, 0f).normalized;

            // 2. LineRenderer 설정
            lr.positionCount = 2;
            lr.startWidth = 0.07f;
            lr.endWidth = 0.07f;
            lr.useWorldSpace = true;

            // 시작점 설정
            Vector3 startPos = data.originalLocation;
            lr.SetPosition(0, startPos);

            // 끝점 설정: 시작점에서 방향으로 100만큼 더한 좌표
            // 가로 40, 세로 30을 충분히 커버하기 위해 길이를 100 정도로 잡습니다.
            Vector3 endPos = startPos + (direction * 100f);
            lr.SetPosition(1, endPos);

            transform.localScale = Vector3.one * data.radious;
            transform.localPosition = data.originalLocation;
        }


        StartCoroutine(copyPoint());
    }

    private IEnumerator copyPoint()
    {

        while (true)
        {
            GameObject pointObj = Instantiate(fp, transform.position, Quaternion.identity);
            FunctionsPoint pointScript = pointObj.GetComponent<FunctionsPoint>();

            functionsPointData.setInfo(data.radious, data.sustainmentTime);
            pointScript.getData(functionsPointData);

            resetLR(transform.position);
            yield return new WaitForSeconds(data.interval);
        }
    }

    public void FixedUpdate()
    {
        if (data != null && rb != null)
        {
            if (data.slope < 0)
            {
                xDir = 1f;
            }
            else
            {
                xDir = -1f;
            }

            Vector3 direction = new Vector3(xDir, data.slope, 0f).normalized;

            rb.linearVelocity = direction * data.speed;
        }
    }

    public void resetLR(Vector3 position)
    {
        if (lr != null) lr.SetPosition(0, position);
    }
}
