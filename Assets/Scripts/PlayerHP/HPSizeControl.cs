using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class HPSizeControl : MonoBehaviour
{
    [SerializeField] private AudioClip damagedSound;
    [SerializeField] private SoundManager soundManager;

    [Header("HP 정보!")]
    [SerializeField] private float maxHP = 100f;    // 최대 HP
    public float currentHP = 100f;                  // 현재 HP

    [Header("조절 대상")]
    public Transform targetTransform;               // 크기 조절할 대상
    private Vector3 originalScale;                  // 원래 크기
    private Vector3 originalLocalPos;               // 원래 위치

    [SerializeField] private TMP_Text HPInformation;
    [SerializeField] private string showHP;
    [SerializeField] private bool isMuzuk = false;

    void Start()
    {

        if (targetTransform == null)
            targetTransform = transform;

        originalScale = targetTransform.localScale;
        originalLocalPos = targetTransform.localPosition;

        showHP = $"{currentHP} / {maxHP}";
        HPInformation.text = showHP;
    }

    /// <summary>
    /// 데미지 전달하면 setHP 실행시키고 무적시간 관리함
    /// </summary>
    /// <param name="Damage"></param>
    /// <returns></returns>
    public IEnumerator muzukshigan(float Damage)
    {
        if (!isMuzuk)
        {
            soundManager.PlayDamagedSFX(damagedSound);

            isMuzuk = true;
            SetHP(Damage);
            yield return new WaitForSeconds(0.05f);
            isMuzuk = false;
        }
        yield break;
    }

    /// <summary>
    /// HP에 따라 오브젝트를 왼쪽을 고정한 채로 줄어들게 합니다.
    /// </summary>
    private void SetHP(float hp)
    {
        float realHP = currentHP - hp;
        // 1) HP 범위에 맞춰 클램프
        currentHP = Mathf.Clamp(realHP, 0f, maxHP); // 음수나 맥스 못넘도록 제한
        float ratio = currentHP / maxHP;   // 0 ~ 1 비율 0: 빈거 1: 꽉찬거

        // 2) 스케일 조정
        Vector3 newScale = originalScale;
        newScale.x *= ratio; // x좌표를 ratio 만큼 줄임
        targetTransform.localScale = newScale; //적용

        // 3) 위치 보정: 스케일 차이의 절반만큼 오른쪽으로 이동
        float deltaX = (originalScale.x - newScale.x) * 0.5f;
        targetTransform.localPosition = originalLocalPos + Vector3.left * deltaX;

        showHP = $"{currentHP} / {maxHP}";
        HPInformation.text = showHP;

        //hp 전달해서 게임오버 여부 판단하는 함수 호출
        
    }
}
