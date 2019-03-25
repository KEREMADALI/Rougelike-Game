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

    public GameObject Gizmo;

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
        if (target == null) {
            return;
        }

        Vector3 localPosition = this.transform.position;
        Vector3 targetObjectPosition = target.position;
        Vector3 targetPosition = new Vector3();


        for (int i = 0; i < numberOfBullets; i++) {
            float angle = i*(Mathf.PI * 2f / numberOfBullets);
            Vector3 position = targetObjectPosition - new Vector3( Mathf.Cos(angle) * stopDistance, 
                                                                Mathf.Sin(angle) * stopDistance, 
                                                                0);

            if (i == 0) {
                targetPosition = position;
            }
            else {
                targetPosition = Vector3.Distance(localPosition, targetPosition) < 
                                    Vector3.Distance(localPosition, position) ? targetPosition : position;
            }
        }

        if (Vector3.Distance(localPosition, targetObjectPosition) <= stopDistance)
        {
            weapon.Shoot(Vector3.zero, numberOfBullets);
        }
        this.transform.position = Vector2.MoveTowards(localPosition, targetPosition, speed);
    }

    //Fazla güçlü, gücünü azalt
    public void OnCollisionStay2D(Collision2D collision)
    {
        //GameObject obj = collision.gameObject;

        //if (obj.name == "Fatty") {
        //    obj.GetComponent<HealthHandler>().Hit(hitDamage);
        //}
    }
}
