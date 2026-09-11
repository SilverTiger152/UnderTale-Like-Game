using System.Collections;
using UnityEngine;

public class MirrorGimmik : MonoBehaviour
{
    private Camera targetCamera;
    private bool isFlipped = false;

    private void Awake()
    {
        // 메인 카메라 자동 참조 (필요 시 인스펙터 지정 가능)
        targetCamera = Camera.main;
    }

    // 외부(BulletSpawner 등)에서 기믹 데이터를 받아 실행하는 함수
    public void StartMirrorEffect(MirrorGimmikData data)
    {
        StartCoroutine(MirrorRoutine(data));
    }

    private IEnumerator MirrorRoutine(MirrorGimmikData data)
    {
        // 1. 패턴 시작 후 발동 시점까지 대기
        yield return new WaitForSeconds(data.reverseTime);

        // 2. 카메라 좌우 반전 적용
        SetFlip(true);

        // 3. 기믹 유지 시간 동안 대기
        yield return new WaitForSeconds(data.duration);

        // 4. 화면 원상복구 후 기믹 오브젝트 삭제
        SetFlip(false);
        Destroy(gameObject);
    }

    private void SetFlip(bool flip)
    {
        if (targetCamera == null) return;

        isFlipped = flip;
        targetCamera.ResetProjectionMatrix();

        if (isFlipped)
        {
            // 카메라 프로젝션 행렬 X축 반전
            targetCamera.projectionMatrix = targetCamera.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
        }
    }

    // 3D 렌더링 시 폴리곤 뒷면이 뚫리는 현상 방지
    private void OnPreRender()
    {
        if (isFlipped) GL.invertCulling = true;
    }

    private void OnPostRender()
    {
        if (isFlipped) GL.invertCulling = false;
    }
}