using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Private & Const Variables
    private const float speed = 2.0f;
    private const float hitDamage = 2.0f;
    private const float stopDistance = 5;

    private Weapon weapon;
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
    }

    private void Update()
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

        Vector3 localPosition = this.transform.position;
        Vector3 targetObjectPosition = target.position;
        Vector3 targetPosition = target.position;

        if (numberOfBullets > 1)
        {
            // Calculate the closest point where enemy can hit the target from with multiple bullets
            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * (Mathf.PI * 2f / numberOfBullets);
                Vector3 position = targetObjectPosition - new Vector3(Mathf.Cos(angle) * stopDistance,
                                                                    Mathf.Sin(angle) * stopDistance,
                                                                    0);

                if (i == 0)
                {
                    targetPosition = position;
                }
                else
                {
                    targetPosition = Vector3.Distance(localPosition, targetPosition) <
                                        Vector3.Distance(localPosition, position) ? targetPosition : position;
                }
            }
        }
        else {
            // Closest point in targets radius
            var directionVector = localPosition - targetObjectPosition;
            targetPosition += 5 * (directionVector / directionVector.magnitude);
        }

        if (Vector3.Distance(localPosition, targetObjectPosition) <= stopDistance)
        {
            var targetDirectionVector = targetObjectPosition - localPosition;
            Vector3 targetDirection = targetDirectionVector / targetDirectionVector.magnitude;
            weapon.Shoot(targetDirection, numberOfBullets);
        }
        this.transform.position = Vector2.MoveTowards(localPosition, targetPosition, speed * Time.fixedDeltaTime);
    }
    #endregion
}
