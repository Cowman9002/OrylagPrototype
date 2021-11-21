using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public PlayerMovementController playerMovement;

    public Gun currentGun;

    private void Start()
    {
    }

    private void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            currentGun.Shoot();
        }

    }
}
