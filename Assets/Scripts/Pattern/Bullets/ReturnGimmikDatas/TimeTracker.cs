using System.Collections.Generic;
using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    [SerializeField] private float rewindTime = 2.0f;
    private List<Vector3> positionHistory = new List<Vector3>();
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        int maxFrames = Mathf.RoundToInt(rewindTime / Time.fixedDeltaTime);
        if (positionHistory.Count >= maxFrames)
        {
            positionHistory.RemoveAt(0);
        }
        positionHistory.Add(transform.position);
    }

    public void RewindInstant()
    {
        if (positionHistory.Count > 0)
        {
            Vector3 pastPos = positionHistory[0];

            // Rigidbody2D가 있으면 물리 좌표도 강제 동기화
            if (rb != null)
            {
                rb.position = pastPos;
            }
            transform.position = pastPos;

            positionHistory.Clear();
        }
    }
}