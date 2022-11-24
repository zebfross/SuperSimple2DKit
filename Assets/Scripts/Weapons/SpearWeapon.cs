using Assets.Scripts.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearWeapon : ThrowableWeapon
{
    override public void Initialize()
    {
        base.Initialize();

        if (facingRight)
            transform.rotation = Quaternion.Euler(0, 0, -55);
        else
            transform.rotation = Quaternion.Euler(0, 0, 125);
    }

}
