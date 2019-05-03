using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Private & Const Variables
    private const float speed = 4.0f;
    private const float hitDamage = 2.0f;
    private const float stopDistance = 5;
    private Vector2 direction;

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
        else {
            direction = Vector2.zero;
        }

        UpdateAnimationHandler();
    }

    private void ChasePlayer()
    {
        if (target == null)
        {
            return;
        }

        Vector2 localPosition = this.transform.position;
        Vector2 targetObjectPosition = target.position;
        Vector2 positionToGo = target.position;

        if (numberOfBullets > 1)
        {
            // Calculate the closest point where enemy can hit the target from with multiple bullets
            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * (Mathf.PI * 2f / numberOfBullets);
                Vector2 positionAroundTarget = targetObjectPosition - new Vector2(Mathf.Cos(angle) * stopDistance,
                                                                    Mathf.Sin(angle) * stopDistance);
                // Find the closest position to go
                if (i == 0)
                {
                    positionToGo = positionAroundTarget;
                }
                else
                {
                    positionToGo = Vector2.Distance(localPosition, positionToGo) <
                                        Vector2.Distance(localPosition, positionAroundTarget) ? positionToGo : positionAroundTarget;
                }
            }
        }
        else {
            // Closest point in targets radius
            direction = (localPosition - targetObjectPosition).normalized;
            positionToGo += stopDistance * direction;
        }
        // Target position to go direction
        direction = (positionToGo - localPosition).normalized;

        // Shoot when the target is close enough
        if (Vector3.Distance(localPosition, targetObjectPosition) <= (stopDistance + 0.2))
        {
            Vector2 shootDirection = (targetObjectPosition - localPosition).normalized;
            weapon.Shoot(shootDirection, numberOfBullets);
        }
        if(Vector2.Distance(localPosition,positionToGo) > 0.2)
        // Always try to keep distance
        rb.MovePosition(localPosition + direction * speed * Time.fixedDeltaTime);

    }

    private void UpdateAnimationHandler() {
        gameObject.GetComponent<AnimationHandler>().direction = direction;
    }
    #endregion
}
