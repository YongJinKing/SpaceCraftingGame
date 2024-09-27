using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CamController : MonoBehaviour
{
    [Header("플레이어의 보스전 입장 위치"), Space(0.5f)]
    public Transform playerLandingPos;

    [Header("카메라 기본 줌 수치"), Space(0.5f)]
    public float defalutOrthoSize;
    [Header("카메라 줌 인 수치"), Space(0.5f)]
    public float zoomInOrthoSize;
    [Header("카메라 줌 아웃 수치"), Space(0.5f)]
    public float zoomOutOrthoSize;
    [Header("카메라 줌 시간"), Space(0.5f)]
    public float zoomTime;
    [Header("카메라 이동 속도"), Space(0.5f)]
    public float camMoveSpeed;

    [Header("보스의 부모 말풍선"), Space(0.5f)]
    public Transform bossText;
    [Header("보스의 대사"), Space(0.5f)]
    public BossDialogue bossDialogue;
    [Header("보스 입장 시 나올 텍스트"), Space(0.5f)]
    public Transform bossEntranceText;

    [Header("스테이지1 게임 매니저"), Space(0.5f)]
    public BossStage1Manager bossStage1Manager;

    [Header("카메라"), Space(0.5f)]
    [SerializeField] Transform Cam;
    [Header("카메라 포커스 오브젝트"), Space(0.5f)]
    [SerializeField] Transform CamFocus;
    [Header("플레이어"), Space(0.5f)]
    [SerializeField] Transform Player;
    [Header("보스"), Space(0.5f)]
    [SerializeField] Transform Boss;
    [Header("시네머신 버추얼 카메라"), Space(0.5f)]
    CinemachineVirtualCamera CVC;

    [Header("페이드 매니저"), Space(.5f)]
    [SerializeField] FadeManager FD;

    CinemachineBasicMultiChannelPerlin perlin;
    Coroutine camShake;
    InputController controller;

    // Start is called before the first frame update
    void Start()
    {
        CVC = Cam.GetComponent<CinemachineVirtualCamera>();
        CVC.m_Lens.OrthographicSize = zoomOutOrthoSize;
        perlin = CVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        controller = FindObjectOfType<InputController>();
        StartCoroutine(BossStage1Direction());
    }
    void MoveToTarget(Vector3 StartPos, Vector3 EndPos)
    {
        Vector3 dir = EndPos - StartPos;
        float dist = dir.magnitude;
        float delta = Time.deltaTime * camMoveSpeed;
        dir.Normalize();

        while (dist > 0f)
        {
            dist -= delta;
            if (dist <= 0f) dist = 0f;

            CamFocus.Translate(dir * delta, Space.World);
        }
    }

    IEnumerator BossStage1Direction()
    {
        FD.StartFadeIn(2f);
        controller.canMove = false;
        yield return StartCoroutine(PlayerLandingToStage());
        yield return StartCoroutine(FocusBoss());
        yield return StartCoroutine(FocusPlayer());

        bossStage1Manager.StartGame();
    }
    #region ForPlayer
    IEnumerator PlayerLandingToStage()
    {
        SoundManager.Instance.PlayBGM(SoundManager.Instance.BGMSoundData.bgm[(int)BGM.BOSSENTRANCE]);
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

    #endregion

    #region ForBoss
    IEnumerator FocusBoss()
    {
        yield return new WaitForSeconds(1f);
        CamFocus.SetParent(null);
        MoveToTarget(CamFocus.position, Boss.position);
        CamFocus.SetParent(Boss);
        yield return StartCoroutine(BossIntroDirection());
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
        // 대사 출력
        yield return StartCoroutine(ActiveBossScript());

        SoundManager.Instance.PlayBGM(SoundManager.Instance.BGMSoundData.bgm[(int)BGM.BOSS]);
        Boss.GetComponent<Boss>().animator.SetBool("Intro", true);
        bossEntranceText.gameObject.SetActive(true);

        yield return new WaitForSeconds(8f);
        Boss.GetComponent<Boss>().animator.SetBool("Intro", false);
        bossEntranceText.gameObject.SetActive(false);
    }

    IEnumerator ActiveBossScript()
    {
        bossText.gameObject.SetActive(true);
        bossDialogue.StartDialogue(bossDialogue.openningDialogues);
        yield return new WaitForSeconds(12f); // 이걸로 대사 출력 길이 조절 가능

        bossText.gameObject.SetActive(false);
    }
    #endregion

    #region Camera Action
    public void StartCameraShake(float strength, float duration)
    {
        if(camShake != null)
        {
            StopCoroutine(camShake);
            camShake = null;
        }
        camShake = StartCoroutine(CamShakeAction(strength, duration));
    }

    IEnumerator CamShakeAction(float strength, float duration)
    {
        CamShake(strength);
        yield return new WaitForSeconds(duration);
        CamShake(0);
    }

    void CamShake(float strength)
    {
        perlin.m_AmplitudeGain = strength;
        perlin.m_FrequencyGain = strength;
    }

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            StopAllCoroutines();
            SoundManager.Instance.PlayBGM(SoundManager.Instance.BGMSoundData.bgm[(int)BGM.BOSS]);
            Player.transform.position = playerLandingPos.transform.position; // 목표 위치로 정확히 이동
            CVC.m_Lens.OrthographicSize = defalutOrthoSize;
            CamFocus.SetParent(null);
            MoveToTarget(CamFocus.position, Player.position);
            CamFocus.SetParent(Player);
            bossStage1Manager.StartGame();
        }
    }
}
