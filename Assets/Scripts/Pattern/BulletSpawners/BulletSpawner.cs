using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject warningLinePrefab;

    [SerializeField] public GameObject circlePrefab;
    [SerializeField] public GameObject squarePrefab;
    [SerializeField] public GameObject LinearFunctionPrefab;
    [SerializeField] public GameObject QuadraticFunctionPrefab;
    [SerializeField] public GameObject mirrorGimmikPrefab; // 미러 기믹 프리팹 등록용
    [SerializeField] public GameObject vanishBulletPrefab;

    [SerializeField] private AudioClip warningSound; // 인스펙터에서 효과음 등록
    [SerializeField] private SoundManager soundManager;

    public float blinkInterval = 0.1f; // 깜빡이는 간격

    public void CopyCircle(CircleBulletData circleBulletData)
    {
        GameObject CBullet = Instantiate(circlePrefab, circleBulletData.originalLocation, Quaternion.identity);

        CircleBullet CController = CBullet.GetComponent<CircleBullet>();

        CController.getData(circleBulletData);
    }

    public void CopyLinearFunction(LinearFunctionData linearFunctionData)
    {
        GameObject LFBullet = Instantiate(LinearFunctionPrefab, linearFunctionData.originalLocation, Quaternion.identity);

        LinearFunction LFController = LFBullet.GetComponent<LinearFunction>();

        LFController.getData(linearFunctionData);
    }

    public void CopyQuadraticFunction(QuadraticFunctionData quadraticFunctionData)
    {
        GameObject QBullet = Instantiate(QuadraticFunctionPrefab, quadraticFunctionData.originalLocation, Quaternion.identity);

        QuadraticFunction QFController = QBullet.GetComponent<QuadraticFunction>();

        QFController.getData(quadraticFunctionData);
    }

    // 미러 기믹 생성 및 데이터 전달
    public void CopyMirror(MirrorGimmikData mirrorGimmikData)
    {
        GameObject mirrorObj = Instantiate(mirrorGimmikPrefab);
        MirrorGimmik mirrorController = mirrorObj.GetComponent<MirrorGimmik>();
        mirrorController.StartMirrorEffect(mirrorGimmikData);
    }

    public IEnumerator CopySquare(SquareBulletData squareBulletData)
    {
        GameObject lineObj = Instantiate(warningLinePrefab);
        LineRenderer lr = lineObj.GetComponent<LineRenderer>();

        float hw = squareBulletData.width / 2f;
        float hh = squareBulletData.height / 2f;

        Vector3 center = squareBulletData.originalLocation; // 소환될 위치 중심

        // 2. 중심점(center)을 기준으로 네 꼭짓점 계산
        lr.positionCount = 4;
        lr.loop = true;
        lr.startWidth = 0.05f; // 선 두께
        lr.endWidth = 0.05f;
        lr.useWorldSpace = true;

        lr.SetPosition(0, center + new Vector3(-hw, hh, -0.1f));
        lr.SetPosition(1, center + new Vector3(hw, hh, -0.1f));
        lr.SetPosition(2, center + new Vector3(hw, -hh, -0.1f));
        lr.SetPosition(3, center + new Vector3(-hw, -hh, -0.1f));

        float elapsed = 0f;
        bool isRed = true;
        // 2. 깜빡이면서 소리내기 루프
        while (elapsed < squareBulletData.interval)
        {
            isRed = !isRed;

            if (isRed && lr != null)
            {
                lr.startColor = Color.yellow;
                lr.endColor = Color.yellow;
                soundManager.PlayPeeSFX(warningSound);
            }
            else if (lr != null)
            {
                lr.startColor = Color.red;
                lr.endColor = Color.red;
            }

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        Destroy(lineObj);

        GameObject SBullet = Instantiate(squarePrefab, squareBulletData.originalLocation, Quaternion.identity);

        SquareBullet SController = SBullet.GetComponent<SquareBullet>();

        SController.getData(squareBulletData);
    }

    // BulletSpawner.cs 상단에 변수 추가


// 탄환 생성 메서드 추가
// BulletSpawner.cs 상단 변수 선언부에 추가


// 생성 메서드 추가
    public void CopyVanish(VanishBulletData vanishData)
    {
        GameObject vBullet = Instantiate(vanishBulletPrefab, vanishData.originalLocation, Quaternion.identity);
        VanishBullet vController = vBullet.GetComponent<VanishBullet>();
        vController.getData(vanishData);
    }
}