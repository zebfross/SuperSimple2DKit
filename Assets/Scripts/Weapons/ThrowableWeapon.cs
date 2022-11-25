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
        private Vector2 targetOffset = new Vector2(0, 2);
        [System.NonSerialized] public Vector3 speedEased;
        [System.NonSerialized] public Vector3 returnSpeed;
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
                flyDistance += speed;
                if (!returning && Mathf.Abs(flyDistance) > Mathf.Abs(maxFlyDistance))
                {
                    if(canReturn)
                    {
                        returning = true;
                    }
                    else
                    {
                        GameManager.Instance.player.ReEquipWeapon();
                    }
                }

                if(returning)
                {
                    Vector3 distanceFromPlayer;

                    distanceFromPlayer.x = (NewPlayer.Instance.transform.position.x + targetOffset.x) - transform.position.x;
                    distanceFromPlayer.y = (NewPlayer.Instance.transform.position.y + targetOffset.y) - transform.position.y;

                    returnSpeed.x = (Mathf.Abs(distanceFromPlayer.x) / distanceFromPlayer.x) * 2f;
                    returnSpeed.y = (Mathf.Abs(distanceFromPlayer.y) / distanceFromPlayer.y) * 2f;

                    transform.position += returnSpeed * Time.deltaTime * 4;
                }
                else
                {
                    transform.position += new Vector3(speed, 0);
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


