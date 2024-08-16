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
