using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float delayBetweenShots = 1.0f;
    private const float bulletLifeSpan = 5.0f;

    private float timerBetweenShots;
    private float bulletSpeed = 5.0f;
    private float bulletPower = 5.0f;
    
    public GameObject bulletPrefab;
    public Vector3 m_ShootDirection { private get; set; }

    private void Awake()
    {
        m_ShootDirection = Vector3.zero;
        timerBetweenShots = delayBetweenShots;
    }

    private void Update()
    {
        UpdateReloadTime();
    }

    private void UpdateReloadTime()
    {
        if (timerBetweenShots < 0.0f)
        {
            timerBetweenShots = delayBetweenShots;
        }
        else if (timerBetweenShots < delayBetweenShots)
        {
            timerBetweenShots -= Time.deltaTime;
        }
    }

    public void Shoot(Vector3 shootDirection, int numberOfBullets=0)
    {
        if (timerBetweenShots > 0.0f && timerBetweenShots != delayBetweenShots)
        {
            return;
        }

        timerBetweenShots = delayBetweenShots - Time.deltaTime;
        Vector3 position = transform.parent.position;

        if (numberOfBullets != 0)
        {
            List<GameObject> bullets = new List<GameObject>();
            float radius = 2f;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * Mathf.PI * 2f / numberOfBullets;
                var bullet = Instantiate( bulletPrefab, 
                                          position + new Vector3( Mathf.Cos(angle) * radius
                                                                    , Mathf.Sin(angle) * radius
                                                                    , 0), 
                                          transform.rotation);
                bullet.GetComponent<BulletController>().Initiate( bulletLifeSpan
                                                                    , new Vector3( Mathf.Cos(angle) * radius
                                                                                    , Mathf.Sin(angle) * radius
                                                                                    , 0 )
                                                                    , bulletPower);
            }
        }
        else
        {
            position = transform.parent.position + shootDirection;

            var bullet = Instantiate(bulletPrefab, position, transform.rotation);
            bullet.GetComponent<BulletController>().Initiate( bulletLifeSpan
                                                              , shootDirection * bulletSpeed
                                                              , bulletPower);
        }


    }
}
