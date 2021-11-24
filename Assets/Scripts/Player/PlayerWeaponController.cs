using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public PlayerMovementController playerMovement;

    public Gun currentGun;

    private void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            currentGun.StartShooting();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            currentGun.StopShooting();
        }

    }
}
