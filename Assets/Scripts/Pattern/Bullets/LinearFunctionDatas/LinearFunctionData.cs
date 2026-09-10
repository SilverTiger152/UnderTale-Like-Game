using UnityEngine;

public class LinearFunctionData: MoveData
{
    public Vector3 originalLocation;
    public float speed;
    public float slope;
    public float radious;
    public float interval;
    public float sustainmentTime;
    public override void ApplyTo(BulletSettings bulletSettingInfo)
    {
        originalLocation = bulletSettingInfo.OriginalLocation;
        speed = bulletSettingInfo.speed;
        radious = bulletSettingInfo.radious;
        slope = bulletSettingInfo.slope;
        interval = bulletSettingInfo.interval;
        sustainmentTime = bulletSettingInfo.sustainmentTime;
    }
}
