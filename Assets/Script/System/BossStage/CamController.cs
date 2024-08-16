using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform playerLandingPos;
    public Vector3 camRotate;
    public float defalutOrthoSize;
    public float zoomInOrthoSize;
    public float zoomOutOrthoSize;
    public float zoomTime;
    public float camMoveSpeed;
   

    [SerializeField] Transform Cam;
    [SerializeField] Transform CamFocus;
    [SerializeField] Transform Player;
    [SerializeField] Transform Boss;
    CinemachineVirtualCamera CVC;

    // Start is called before the first frame update
    void Start()
    {
        CVC = Cam.GetComponent<CinemachineVirtualCamera>();
        CVC.m_Lens.OrthographicSize = zoomOutOrthoSize;
        StartCoroutine(BossStage1Direction());
    }

    IEnumerator BossStage1Direction()
    {
        yield return StartCoroutine(PlayerLandingToStage());
        yield return StartCoroutine(FocusBoss());
        yield return StartCoroutine(FocusPlayer());
    }

    IEnumerator PlayerLandingToStage()
    {
        float landingTime = 1.5f; // 총 이동 시간
        float curTime = 0f; // 경과 시간

        Vector3 startPos = Player.transform.position; // 시작 위치
        Vector3 targetPos = playerLandingPos.transform.position; // 목표 위치

        while (curTime <= landingTime)
        {
            curTime += Time.deltaTime; // 프레임당 경과 시간 추가
            float t = curTime / landingTime; // 현재 진행 비율 계산

            Player.transform.position = Vector3.Lerp(startPos, targetPos, t); // 위치 보간
            yield return null; // 다음 프레임까지 대기
        }

        Player.transform.position = targetPos; // 목표 위치로 정확히 이동
    }

    IEnumerator FocusBoss()
    {
        yield return new WaitForSeconds(1f);
        CamFocus.SetParent(null);
        MoveToTarget(CamFocus.position, Boss.position);
        CamFocus.SetParent(Boss);
        yield return StartCoroutine(BossIntroDirection());
        yield return StartCoroutine(TweakCamForBossIntroDirection());
    }
    IEnumerator FocusPlayer()
    {
        yield return StartCoroutine(RevokeCamForPlayer());
        yield return new WaitForSeconds(1f);
        CamFocus.SetParent(null);
        MoveToTarget(CamFocus.position, Player.position);
        CamFocus.SetParent(Player);
    }

    IEnumerator RevokeCamForPlayer()
    {
        float curTime = 0f; // 경과 시간
        float initialOrthoSize = CVC.m_Lens.OrthographicSize; // 현재 카메라의 초기 Orthographic Size 저장

        while (curTime <= zoomTime)
        {
            curTime += Time.deltaTime; // 프레임당 경과 시간 추가
            float t = curTime / zoomTime; // 현재 진행 비율 계산

            CVC.m_Lens.OrthographicSize = Mathf.Lerp(initialOrthoSize, defalutOrthoSize, t);
            yield return null; // 다음 프레임까지 대기
        }

        CVC.m_Lens.OrthographicSize = defalutOrthoSize;
    }

    IEnumerator BossIntroDirection() // 이 아래에 뭐 말풍선이라던가 기타 등등 추가
    {
        float curTime = 0f; // 경과 시간
        float initialOrthoSize = CVC.m_Lens.OrthographicSize; // 현재 카메라의 초기 Orthographic Size 저장

        while (curTime <= zoomTime)
        {
            curTime += Time.deltaTime; // 프레임당 경과 시간 추가
            float t = curTime / zoomTime; // 현재 진행 비율 계산

            CVC.m_Lens.OrthographicSize = Mathf.Lerp(initialOrthoSize, zoomInOrthoSize, t);
            yield return null; // 다음 프레임까지 대기
        }

        CVC.m_Lens.OrthographicSize = zoomInOrthoSize;
        yield return new WaitForSeconds(3f);
    }
  


    IEnumerator TweakCamForBossIntroDirection()
    {
        yield return null;
    }


    void MoveToTarget(Vector3 StartPos, Vector3 EndPos)
    {
        Vector3 dir = EndPos - StartPos;
        float dist = dir.magnitude;
        float delta = Time.deltaTime * camMoveSpeed;
        dir.Normalize();

        while(dist > 0f)
        {
            dist -= delta;
            if (dist <= 0f) dist = 0f;

            CamFocus.Translate(dir * delta, Space.World);
        }
    }
}
