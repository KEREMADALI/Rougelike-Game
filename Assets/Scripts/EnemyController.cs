using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float speed = 0.05f;
    private const float hitDamage = 2.0f;

    public float health = 10;

    public Transform target;
    public bool isZombie = false;

    // Update is called once per frame
    void Update()
    {
        if (isZombie) {
            ChasePlayer();
        }
    }

    private void ChasePlayer() {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Hit();
    }

    public void Hit() {
        health -= hitDamage;

        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
