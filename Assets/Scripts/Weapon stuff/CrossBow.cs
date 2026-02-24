using System.Drawing;
using UnityEngine;
using Weapons;

public class CrossBow : WeaponsParentScript
{
    protected override void Start()
    {
        base.Start();

        damage = 25f;
        range = 100f;
        shotKnockBack = 10f;
        rateOfFire = 1f;
        isFullAuto = false;
    }

    protected override void shoot()
    {
        base.shoot();

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            HP target = hit.transform.GetComponent<HP>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * shotKnockBack, ForceMode.Impulse);
            }

            GameObject impactParticles = Instantiate(impactAffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactParticles, 1f);
        }
    }
}
