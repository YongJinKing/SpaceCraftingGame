using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UISoundData", menuName = "Scriptable Object/UISoundData")]

public class UI_Sound : ScriptableObject
{
    //재료 부족
    public AudioClip NeedMoreResourceSFX;
    //우주선 입장
    public AudioClip EnterSpaceShipSFX;
    //우주선 나가기
    public AudioClip ExitSpaceShipSFX;
    //망치
    public AudioClip HammeringSFX;
    //곡괭이
    public AudioClip MiningSFX;
    //공구 변경
    public AudioClip ChangeToolSFX;
    //무기 변경
    public AudioClip ChangeWeaponSFX;
    //제작무기탭 변경
    public AudioClip ChangeTabSFX;
    // 탭 열기
    public AudioClip OpenTabSFX;
    // 탭 닫기
    public AudioClip CloseTabSFX;
    //건물 제작 
    public AudioClip BuildingSFX;
    //강화 성공
    public AudioClip SuccesSFX;
    
}
