using UnityEngine;

public class ReturnGimmikData : MoveData
{
    [Header("발동 전 대기 시간")]
    public float returnTime; // 몇 초 뒤에 회귀가 터질지

    public override void ApplyTo(BulletSettings bulletSettingInfo)
    {
        returnTime = bulletSettingInfo.returnTime;
    }
}