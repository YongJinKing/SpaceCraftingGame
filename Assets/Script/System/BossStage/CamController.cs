using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CamController : MonoBehaviour
{
    [Header("�÷��̾��� ������ ���� ��ġ"), Space(0.5f)]
    public Transform playerLandingPos;

    [Header("ī�޶� �⺻ �� ��ġ"), Space(0.5f)]
    public float defalutOrthoSize;
    [Header("ī�޶� �� �� ��ġ"), Space(0.5f)]
    public float zoomInOrthoSize;
    [Header("ī�޶� �� �ƿ� ��ġ"), Space(0.5f)]
    public float zoomOutOrthoSize;
    [Header("ī�޶� �� �ð�"), Space(0.5f)]
    public float zoomTime;
    [Header("ī�޶� �̵� �ӵ�"), Space(0.5f)]
    public float camMoveSpeed;

    [Header("������ �θ� ��ǳ��"), Space(0.5f)]
    public Transform bossText;
    [Header("������ ���"), Space(0.5f)]
    public BossDialogue bossDialogue;
    [Header("���� ���� �� ���� �ؽ�Ʈ"), Space(0.5f)]
    public Transform bossEntranceText;

    [Header("��������1 ���� �Ŵ���"), Space(0.5f)]
    public BossStage1Manager bossStage1Manager;

    [Header("ī�޶�"), Space(0.5f)]
    [SerializeField] Transform Cam;
    [Header("ī�޶� ��Ŀ�� ������Ʈ"), Space(0.5f)]
    [SerializeField] Transform CamFocus;
    [Header("�÷��̾�"), Space(0.5f)]
    [SerializeField] Transform Player;
    [Header("����"), Space(0.5f)]
    [SerializeField] Transform Boss;
    [Header("�ó׸ӽ� ���߾� ī�޶�"), Space(0.5f)]
    CinemachineVirtualCamera CVC;

    [Header("���̵� �Ŵ���"), Space(.5f)]
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
        float landingTime = 1.5f; // �� �̵� �ð�
        float curTime = 0f; // ��� �ð�

        Vector3 startPos = Player.transform.position; // ���� ��ġ
        Vector3 targetPos = playerLandingPos.transform.position; // ��ǥ ��ġ

        while (curTime <= landingTime)
        {
            curTime += Time.deltaTime; // �����Ӵ� ��� �ð� �߰�
            float t = curTime / landingTime; // ���� ���� ���� ���

            Player.transform.position = Vector3.Lerp(startPos, targetPos, t); // ��ġ ����
            yield return null; // ���� �����ӱ��� ���
        }

        Player.transform.position = targetPos; // ��ǥ ��ġ�� ��Ȯ�� �̵�
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
        float curTime = 0f; // ��� �ð�
        float initialOrthoSize = CVC.m_Lens.OrthographicSize; // ���� ī�޶��� �ʱ� Orthographic Size ����

        while (curTime <= zoomTime)
        {
            curTime += Time.deltaTime; // �����Ӵ� ��� �ð� �߰�
            float t = curTime / zoomTime; // ���� ���� ���� ���

            CVC.m_Lens.OrthographicSize = Mathf.Lerp(initialOrthoSize, defalutOrthoSize, t);
            yield return null; // ���� �����ӱ��� ���
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
    IEnumerator BossIntroDirection() // �� �Ʒ��� �� ��ǳ���̶���� ��Ÿ ��� �߰�
    {
        float curTime = 0f; // ��� �ð�
        float initialOrthoSize = CVC.m_Lens.OrthographicSize; // ���� ī�޶��� �ʱ� Orthographic Size ����

        while (curTime <= zoomTime)
        {
            curTime += Time.deltaTime; // �����Ӵ� ��� �ð� �߰�
            float t = curTime / zoomTime; // ���� ���� ���� ���

            CVC.m_Lens.OrthographicSize = Mathf.Lerp(initialOrthoSize, zoomInOrthoSize, t);
            yield return null; // ���� �����ӱ��� ���
        }
        CVC.m_Lens.OrthographicSize = zoomInOrthoSize;
        // ��� ���
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
        yield return new WaitForSeconds(12f); // �̰ɷ� ��� ��� ���� ���� ����

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
            Player.transform.position = playerLandingPos.transform.position; // ��ǥ ��ġ�� ��Ȯ�� �̵�
            CVC.m_Lens.OrthographicSize = defalutOrthoSize;
            CamFocus.SetParent(null);
            MoveToTarget(CamFocus.position, Player.position);
            CamFocus.SetParent(Player);
            bossStage1Manager.StartGame();
        }
    }
}
