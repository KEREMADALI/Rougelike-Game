using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float speed = 0.05f;
    private const float hitDamage = 2.0f;
    private const float stopDistance = 5;

    private Weapon weapon;

    public float health = 10;

    public Transform target;
    public bool isZombie = false;
    public int numberOfBullets = 4;

    public void Awake()
    {
        weapon = this.GetComponentInChildren<Weapon>();
    }

    public void Update()
    {
        if (isZombie) {
            ChasePlayer();
        }
    }

    private void ChasePlayer() {
        if (target == null)
            return;

        Vector3 localPosition = this.transform.position;
        Vector3 targetPosition = target.position; ; 

        if (Vector3.Distance(localPosition, targetPosition) < stopDistance) {
            weapon.Shoot(Vector3.zero, numberOfBullets);
            return;
        }
        
        this.transform.position = Vector2.MoveTowards(localPosition, targetPosition, speed);
    }
}
