using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyAnimController : MonoBehaviour
{
    public PlayerController player;

    void GetUp() {
        player.canMove = true;
        player.anim.SetBool("respawned", false);
    }
}
