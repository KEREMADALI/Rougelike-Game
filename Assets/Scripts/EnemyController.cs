using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Private & Const Variables
    private const float speed = 4.0f;
    private const float hitDamage = 2.0f;
    private const float stopDistance = 5;

    private Weapon weapon;
    private Rigidbody2D rb;
    #endregion

    #region Public Variables
    public Transform target;
    public bool isZombie = false;
    public int numberOfBullets = 4;
    #endregion

    #region Public Methods
    public void OnCollisionStay2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.name == "Fatty")
        {
            //TODO Fazla zayıf, güçlendir
            obj.GetComponent<HealthHandler>().Effect(-hitDamage * Time.deltaTime * 10);
        }
    }
    #endregion

    #region Private Methods
    private void Awake()
    {
        weapon = this.GetComponentInChildren<Weapon>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isZombie)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        if (target == null)
        {
            return;
        }

        Vector2 localPosition = this.transform.position;
        Vector2 targetObjectPosition = target.position;
        Vector2 targetPosition = target.position;

        if (numberOfBullets > 1)
        {
            // Calculate the closest point where enemy can hit the target from with multiple bullets
            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * (Mathf.PI * 2f / numberOfBullets);
                Vector2 position = targetObjectPosition - new Vector2(Mathf.Cos(angle) * stopDistance,
                                                                    Mathf.Sin(angle) * stopDistance);

                if (i == 0)
                {
                    targetPosition = position;
                }
                else
                {
                    targetPosition = Vector2.Distance(localPosition, targetPosition) <
                                        Vector2.Distance(localPosition, position) ? targetPosition : position;
                }
            }
        }
        else {
            // Closest point in targets radius
            var directionVector = localPosition - targetObjectPosition;
            targetPosition += 5 * (directionVector / directionVector.magnitude);
        }
        var targetDirectionVector = targetObjectPosition - localPosition;
        Vector2 targetDirection = targetDirectionVector / targetDirectionVector.magnitude;

        if (Vector3.Distance(localPosition, targetObjectPosition) <= stopDistance)
        {
            //var targetDirectionVector = targetObjectPosition - localPosition;
            //targetDirection = targetDirectionVector / targetDirectionVector.magnitude;
            weapon.Shoot(targetDirection, numberOfBullets);
        }
        //this.transform.position = Vector2.MoveTowards(localPosition, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(localPosition + targetDirection * speed * Time.fixedDeltaTime);
    }
    #endregion
}
