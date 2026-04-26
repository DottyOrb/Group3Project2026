using System.Collections;
using UnityEngine;
using Weapons;

public class Melee : WeaponsParentScript
{
    [Header("Melee anim stuff")]
    public RectTransform fistUI;
    float punchDistance = 90f;
    float punchOffset = 40f;
    float punchSpeed = 250f;
    private Vector3 originalPos;
    private bool isPunching = false;

    protected override void Start()
    {
        base.Start();

        damage = 5f;
        range = 5f;
        shotKnockBack = 10f;
        rateOfFire = 0.5f;
        isFullAuto = true;

        if (fistUI != null )
        {
            originalPos = fistUI.anchoredPosition;
        }
    }

    protected override void shoot()
    {
        base.shoot();

        if (!isPunching && fistUI != null)
        {
            StartCoroutine(PunchAnimation());
        }
            


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
        }
    }

    private IEnumerator PunchAnimation()
    {
        isPunching = true;

        Vector3 targetPos = originalPos + new Vector3(-punchOffset, punchDistance, 0);
        while (Vector3.Distance(fistUI.anchoredPosition, targetPos) > 0.1f)
        {
            fistUI.anchoredPosition = Vector3.MoveTowards(fistUI.anchoredPosition, targetPos, punchSpeed * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(fistUI.anchoredPosition, originalPos) > 0.1f)
        {
            fistUI.anchoredPosition = Vector3.MoveTowards(fistUI.anchoredPosition, originalPos, punchSpeed * Time.deltaTime);
            yield return null;
        }

        fistUI.anchoredPosition = originalPos;
        isPunching = false;
    }
}
