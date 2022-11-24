using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ThrowableWeapon : MonoBehaviour
    {
        public int damage = 12;
        public float speed = 0.2f;
        public float rotation = -5;
        protected float flyDistance = 0;
        public float maxFlyDistance = 10;
        public MovablePlatform attachedPlatform = null;
        protected bool returning = false;
        public bool canReturn = true;
        public bool canPull = true;
        protected bool facingRight = true;
        public Sprite weaponSprite;
        // Start is called before the first frame update
        public void Start()
        {
            if (GameManager.Instance.player.gameObject.transform.localScale.x < 0)
            {
                speed *= -1;
                rotation *= -1;
                facingRight = false;
            }

            Initialize();
        }

        virtual public void Initialize()
        {
            
        }

        // Update is called once per frame
        public void Update()
        {
            if (attachedPlatform == null)
            {
                this.transform.Rotate(0, 0, rotation);
                this.transform.position += new Vector3(speed, 0);
                flyDistance += speed;
                if (Mathf.Abs(flyDistance) > Mathf.Abs(maxFlyDistance))
                {
                    if(canReturn)
                    {
                        speed *= -1;
                        rotation *= -1;
                    }
                    else
                    {
                        GameManager.Instance.player.ReEquipWeapon();
                    }
                }
                else if (flyDistance == 0)
                {
                    GameManager.Instance.player.ReEquipWeapon();
                }
            }

        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            //Attack Enemies
            if (col.GetComponent<EnemyBase>() != null)
            {
                col.GetComponent<EnemyBase>().GetHurt(1, damage);
            }
            if (col.GetComponent<NewPlayer>() != null && canReturn)
            {
                col.GetComponent<NewPlayer>().ReEquipWeapon();
            }
            if (!returning && canPull && col.GetComponent<MovablePlatform>() != null)
            {
                attachedPlatform = col.GetComponent<MovablePlatform>();
            }
        }

        public bool IsAttached()
        {
            return attachedPlatform != null;
        }

        public void Pull(float amount = 0.1f)
        {
            if(IsAttached())
            {
                gameObject.transform.position += new Vector3(amount, 0);
                attachedPlatform.gameObject.transform.position += new Vector3(amount, 0);
            }
        }

        public void Return()
        {
            attachedPlatform = null;
            returning = true;
        }
    }
}


