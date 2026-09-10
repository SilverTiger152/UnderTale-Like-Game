using NUnit.Framework;
using UnityEngine;

public class CircleBulletData : MoveData
{
    public Vector3 originalLocation;
    public float speed;
    public Vector3 direction;
    public float radious;

    public override void ApplyTo(BulletSettings bulletSettingInfo)
    {
        originalLocation = bulletSettingInfo.OriginalLocation;
        speed = bulletSettingInfo.speed;
        direction = bulletSettingInfo.TargetLocation;
        radious = bulletSettingInfo.radious;
    }
}
