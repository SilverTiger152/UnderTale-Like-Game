using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TurnManager;
/*
 * 플레이어 스크립트
 * 경계 조절 및 움직이기
 */
public class Player : MonoBehaviour
{

    private Rigidbody2D rb;

    public bool IsPlayerTurn { get; private set; }
    float speed = 5f;
    Camera cam;
    public List<Vector3> movePositions = new List<Vector3>();
    public int currentIndex = 0;
    float minX, maxX, minY, maxY;
    [SerializeField] public GameObject OPlayer;

    void Awake()
    {

        IsPlayerTurn = true;
        // 그냥 카메라 가장자리 구하는 코드
        cam = Camera.main;

        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        minX = cam.transform.position.x - camWidth / 2f;
        maxX = cam.transform.position.x + camWidth / 2f;
        minY = cam.transform.position.y - camHeight / 2f;
        maxY = cam.transform.position.y + camHeight / 2f;

        // 시작 위치 제한
        Vector3 pos = new Vector3(0f, -3.7f, 0f);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator PlayerTurn()
    {
        movePositions.AddRange(new Vector3[] 
        { 
            new Vector3(-7f, -4.3f, 0f),
            new Vector3(5f, -4.3f, 0f) 
        });
        while (true)
        {
            if (!IsPlayerTurn)
            {
                movePositions.Clear();
                yield break;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentIndex = Mathf.Max(0, currentIndex - 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentIndex = Mathf.Min(movePositions.Count - 1, currentIndex + 1);
            }

            Vector3 target = new Vector3(0f, 0f, 0f);
            target.x = movePositions[currentIndex].x; //x값
            target.y = movePositions[currentIndex].y; //y값
            transform.position = target;

            yield return null;
        }
    }
    public IEnumerator EPlayerTurn()
    {
        transform.position = new Vector3(-100f, 0f, 0f);

        while (true)
        {
            if (IsPlayerTurn) yield break;

            float upDown = Input.GetAxisRaw("Vertical");
            float leftRight = Input.GetAxisRaw("Horizontal");

            // 1. 이동할 '목표 위치'를 먼저 계산합니다.
            Vector2 movement = new Vector2(leftRight, upDown).normalized * speed * Time.deltaTime;
            Vector2 targetPos = rb.position + movement;

            // 2. [핵심] 목표 위치를 물리 이동 전에 미리 제한(Clamp)합니다.
            targetPos.x = Mathf.Clamp(targetPos.x, minX + 0.2f, maxX - 0.2f);
            targetPos.y = Mathf.Clamp(targetPos.y, minY + 0.2f, maxY - 0.2f);

            // 3. 물리 엔진을 통해 부드럽게 이동합니다.
            rb.MovePosition(targetPos);

            yield return new WaitForFixedUpdate();
        }
    }
    public void changeClamp(float minX, float maxX, float minY, float maxY)
    {
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
    }
    public void activePlayer()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true);
        }
    }

    public void setIsPlayerTurn(bool what)
    {
        IsPlayerTurn = what;
    }
}
