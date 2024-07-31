using Spine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildDron : MonoBehaviour
{
    Transform target;
    [SerializeField] float moveSpeed;

    SpaceShip spaceShip;
    public Animator animator;
    public GameObject dronImg;

    private void OnEnable()
    {
        spaceShip = FindObjectOfType<SpaceShip>();
    }

    
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void StartWorking()
    {
        if (target == null) return;
        target.GetComponent<ConstructionSite>().finishWorkEvent.AddListener(FinishWork);
        StartCoroutine(MoveToTarget(target));
    }

    public void FinishWork()
    {
        if(spaceShip == null) Destroy(gameObject);
        StartCoroutine(MoveToTarget(spaceShip.transform));
    }

    IEnumerator MoveToTarget(Transform target)
    {
        animator.SetBool("Move", true);
        if(this.transform.position.x > target.transform.position.x)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        Vector3 dir = target.position - this.transform.position;
        float dist = dir.magnitude;
        float delta = Time.deltaTime * moveSpeed;
        dir.Normalize();

        while (dist > 0)
        {
            if(dist < delta)
            {
                dist = delta;
            }

            dist -= delta;
            this.transform.Translate(dir * delta, Space.World);

            yield return null;
        }

        animator.SetBool("Move", false);

        ConstructionSite site = target.GetComponent<ConstructionSite>();
        if(site != null) site.StartBuilding(); // 건물을 짓는다.
        else
        {
            target.GetComponent<SpaceShip>().PutInDron(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
