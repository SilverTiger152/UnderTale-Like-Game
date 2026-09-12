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

    private int pointCount = 40; // ���� ������ ���� ����

    void Awake()
    {
        functionsPointData = new FunctionsPointData();
        rb = GetComponent<Rigidbody2D>(); // ���� �� ������ٵ� ������
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
            // ��¥ HP �����ڿ��� �������� ������� �����մϴ�.
            HPSizeController.StartCoroutine(HPSizeController.muzukshigan(1f));
            Debug.Log("���� ������");
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

            // 1. �ʱ� ��ġ �� �ð� ����
            transform.localPosition = data.originalLocation;
            transform.localScale = Vector3.one * data.radious;
            elapsedTime = 0f;

            // 2. yOffset ���: ������(x=0)���� ������ y���� �̸� ����
            // y = a(0 - p)^2 + q
            yOffset = data.a * Mathf.Pow(-data.p, 2) + data.q;

            // 3. � ����� �׸��� (���� ����)
            DrawQuadraticLine();
        }

        StartCoroutine(copyPoint());
    }
    private IEnumerator copyPoint()
    {
        int currentPointIndex = 0; // ���� ����� �ִ� ���� �ε���

        while (true)
        {
            if (data == null) yield break;

            GameObject pointObj = Instantiate(fp, transform.position, Quaternion.identity);
            FunctionsPoint pointScript = pointObj.GetComponent<FunctionsPoint>();

            functionsPointData.setInfo(data.radious, data.sustainmentTime);
            pointScript.getData(functionsPointData);

            // [����] ���� �տ������� ����� ���� (�������� źȯ ��ġ�� �̵�)
            if (lr != null && currentPointIndex < lr.positionCount)
            {
                // źȯ�� ������ ��ġ�� ������ ���� źȯ ��ġ�� ��ƹ��� (���� ª������ ȿ��)
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

        // 1. ���� �̵� �Ÿ� (x)
        float relativeX = elapsedTime * data.speed;

        // 2. �����Լ� ���� ���� (y) �� ������(yOffset) ����
        // relativeY = a * (x - p)^2 + q - yOffset
        float relativeY = (data.a * Mathf.Pow(relativeX - data.p, 2)) + data.q - yOffset;

        // 3. ���� ��ġ ��� �� ����
        Vector3 nextPos = new Vector3(
            data.originalLocation.x + (relativeX * data.xDir), // ����(1 or -1) �ݿ�
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
            // [����] i * 0.5f ��� źȯ�� �ӵ��� interval�� ������ ���� ��� ��õ
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
