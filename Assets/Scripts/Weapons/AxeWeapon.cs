using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : MonoBehaviour
{
    public int damage = 12;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        //Attack Enemies
        if (col.GetComponent<EnemyBase>() != null)
        {
            col.GetComponent<EnemyBase>().GetHurt(1, damage);
        }
    }
}
