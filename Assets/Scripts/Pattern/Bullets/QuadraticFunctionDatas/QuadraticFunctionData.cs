using UnityEngine;

public class QuadraticFunctionData : MoveData
{
    public float a;                // 폭 (곡률)
    public float p;                // 축의 방정식 (꼭짓점 x)
    public float q;                // 꼭짓점 y좌표
    public float speed;            // 진행 속도
    public float xDir;             // 방향 (1: 오른쪽, -1: 왼쪽)
    public Vector3 originalLocation;       // 시작 위치

    public float radious;           // 탄환 크기
    public float interval;         // 잔상 간격
    public float sustainmentTime;
    public override void ApplyTo(BulletSettings bulletSettingInfo)
    {
        a = bulletSettingInfo.a;
        p = bulletSettingInfo.p;
        q = bulletSettingInfo.q;
        speed = bulletSettingInfo.speed;
        xDir = bulletSettingInfo.xDir;
        originalLocation = bulletSettingInfo.OriginalLocation;
        radious = bulletSettingInfo.radious;
        interval = bulletSettingInfo.interval;
        sustainmentTime = bulletSettingInfo.sustainmentTime;
    }
}
