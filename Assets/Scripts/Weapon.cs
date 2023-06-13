using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IHittable이 있으면 공격한다

public class Weapon : MonoBehaviour
{
    [SerializeField] int damage;

    Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    public void EnableWeapon()
    {
        coll.enabled = true;
    }

    public void DisableWeapon()
    {
        coll.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        IHittable hittable = other.GetComponent<IHittable>();
        // 칼과 충돌하면 1데미지를 줌
        hittable?.TakeHit(1);
    }
}
