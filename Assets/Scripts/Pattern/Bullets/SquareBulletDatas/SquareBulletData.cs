using UnityEngine;

public class SquareBulletData : MoveData
{
    public Vector3 originalLocation;
    public float interval;
    public float width;
    public float height;
    public float sustainmentTime;
    public override void ApplyTo(BulletSettings bulletSettingInfo)
    {
        originalLocation = bulletSettingInfo.OriginalLocation;
        interval = bulletSettingInfo.interval;
        width = bulletSettingInfo.width;
        height = bulletSettingInfo.height;
        sustainmentTime = bulletSettingInfo.sustainmentTime;
    }
}
