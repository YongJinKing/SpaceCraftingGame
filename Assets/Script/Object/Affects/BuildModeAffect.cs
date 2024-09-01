using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeAffect : BaseAffect
{
    [SerializeField] protected float _power;
    public float power
    {
        get { return _power; }
        set { _power = value; }
    }
    public override void OnActivate(Collider2D target, Vector2 hitWorldPos)
    {
        //Debug.Log("DamageAffect Activated");

        IDamage damage = target.GetComponent<IDamage>();

        if (damage != null)
        {
            damage.TakeDamage(power);
        }
    }
}
