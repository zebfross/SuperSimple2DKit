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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.Rotate(0, 0, rotation);
        this.transform.position += new Vector3(speed, 0);
        flyDistance += speed;
        if(flyDistance > maxFlyDistance)
        {
            speed *= -1;
            rotation *= -1;
        } else if (flyDistance < 0)
        {
            GameManager.Instance.player.ReEquipWeapon();
        }

    }

    void OnTriggerStay2D(Collider2D col)
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
    }
}
