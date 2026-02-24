using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponsParentScript : MonoBehaviour
    {
        [Header("Standard Gun Variables")]
        public float damage;
        public float range;
        public float shotKnockBack;
        public float rateOfFire;
        public float fireCoolDown;
        public bool hasAbility;
        public bool isFullAuto;
        public KeyCode fireKey = KeyCode.Mouse0;
        public KeyCode abilityKey = KeyCode.Q;
        public Camera cam;
        public GameObject impactAffect;

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            InputManager();
            CoolDownManager();
        }

        protected virtual void InputManager()
        {
            if (isFullAuto)
            {
                if (Input.GetKey(fireKey) && fireCoolDown >= rateOfFire)
                {
                    shoot();
                    fireCoolDown = 0;
                }
            }
            else if (!isFullAuto)
            {
                if (Input.GetKeyDown(fireKey) && fireCoolDown >= rateOfFire)
                {
                    shoot();
                    fireCoolDown = 0;
                }
            }
        }

        protected virtual void CoolDownManager()
        {
            if (fireCoolDown < rateOfFire)
            {
                fireCoolDown += Time.deltaTime;
            }
        }

        protected virtual void shoot()
        {

        }

        protected virtual void ability()
        {

        }
    }
}

