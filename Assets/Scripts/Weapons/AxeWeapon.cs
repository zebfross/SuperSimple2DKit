using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : MonoBehaviour
{
    public int damage = 12;
    public float speed = 0.2f;
    public float rotation = -5;
    public float flyDistance = 0;
    public float maxFlyDistance = 10;
    public MovablePlatform attachedPlatform = null;
    private bool returning = false;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.player.gameObject.transform.localScale.x < 0)
        {
            speed *= -1;
            rotation *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(attachedPlatform == null)
        {
            this.transform.Rotate(0, 0, rotation);
            this.transform.position += new Vector3(speed, 0);
            flyDistance += speed;
            if (Mathf.Abs(flyDistance) > Mathf.Abs(maxFlyDistance))
            {
                speed *= -1;
                rotation *= -1;
            }
            else if (flyDistance == 0)
            {
                GameManager.Instance.player.ReEquipWeapon();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Attack Enemies
        if (col.GetComponent<EnemyBase>() != null)
        {
            col.GetComponent<EnemyBase>().GetHurt(1, 3);
        }
        if (col.GetComponent<NewPlayer>() != null)
        {
            col.GetComponent<NewPlayer>().ReEquipWeapon();
        }
        if (col.GetComponent<MovablePlatform>() != null && !returning)
        {
            attachedPlatform = col.GetComponent<MovablePlatform>();
        }
    }

    public bool IsAttached()
    {
        return attachedPlatform != null;
    }

    public void Pull(float amount=0.1f)
    {
        gameObject.transform.position += new Vector3(amount, 0);
        attachedPlatform.gameObject.transform.position += new Vector3(amount, 0);
    }

    public void Return()
    {
        attachedPlatform = null;
        returning = true;
    }
}
