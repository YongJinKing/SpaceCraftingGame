using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public int maxBounce;	// 팅기는 횟수

    public float xForce;	// x축 힘 (더 멀리)
    public float yForce;	// Y축 힘 (더 높이)
    public float gravity;	// 중력 (떨어지는 속도 제어)

    private Vector2 direction;
    private int currentBounce = 0;
    private bool isGrounded = true;

    private float maxHeight;
    private float currentheight;

    public Transform sprite;
    public Transform shadow;

    public LayerMask layerMask;

    public int index;
    public int amount;

    public Vector2 shadowSize;

    protected virtual void OnEnable()
    {
        Destroy(this.gameObject, 15f);
    }
    protected virtual void Start()
    {
        currentheight = Random.Range(yForce - 1, yForce);
        maxHeight = currentheight;
        Initialize(new Vector2(Random.Range(-xForce, xForce), Random.Range(-xForce, xForce)));
    }

    protected virtual void Update()
    {

        if (!isGrounded)
        {

            currentheight += -gravity * Time.deltaTime;
            sprite.position += new Vector3(0, currentheight, 0) * Time.deltaTime;
            transform.position += (Vector3)direction * Time.deltaTime;

            float totalVelocity = Mathf.Abs(currentheight) + Mathf.Abs(maxHeight);
            float scaleXY = Mathf.Abs(currentheight) / totalVelocity;
            shadow.localScale = shadowSize * Mathf.Clamp(scaleXY, 0.5f, 1.0f);

            CheckGroundHit();
        }
    }

    protected void Initialize(Vector2 _direction)
    {
        isGrounded = false;
        maxHeight /= 1.5f;
        direction = _direction;
        currentheight = maxHeight;
        currentBounce++;
    }

    protected void CheckGroundHit()
    {
        if (sprite.position.y < shadow.position.y)
        {
            sprite.position = shadow.position;
            shadow.localScale = shadowSize;

            if (currentBounce < maxBounce)
            {
                Initialize(direction / 1.5f);
            }
            else
            {
                isGrounded = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & layerMask) != 0)
        {
            if(Inventory.instance.AddItem(index, amount) > 0)
            {
                return;
            }
            SoundManager.Instance.PlaySFX(SoundManager.Instance.itemSoundData.GetDropItem);
            Destroy(this.gameObject);
        }
    }
}
