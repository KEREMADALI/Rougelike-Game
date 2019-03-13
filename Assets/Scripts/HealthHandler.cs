using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public float Health = 20;

    public void Hit(float damage){
        Health -= damage;
        // Play hit animation of parent

        if (Health < 0) {
            Destroy(gameObject);
        }
    }
}
