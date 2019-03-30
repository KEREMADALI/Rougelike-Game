using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Private Variables
    private const float bulletLifeSpan = 5.0f;
    private float timerBetweenShots;
    private float bulletSpeed = 5.0f;
    private float bulletPower = 5.0f;
    #endregion

    #region Public Variables
    public float reloadTime = 1.5f;
    public GameObject bulletPrefab;
    public Vector3 m_ShootDirection { private get; set; }
    #endregion

    #region Public Methods
    public void Shoot(Vector3 shootDirection, int numberOfBullets = 0)
    {
        // Check the reload time
        if (timerBetweenShots > 0.0f && timerBetweenShots != reloadTime)
        {
            return;
        }

        // Start reload counter
        timerBetweenShots = reloadTime - Time.deltaTime;
        Vector3 position = transform.parent.position;

        // Will be simplifed (Condition will be removed) 
        if (numberOfBullets > 1)
        {
            List<GameObject> bullets = new List<GameObject>();
            float radius = 2f;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * Mathf.PI * 2f / numberOfBullets;
                var bullet = Instantiate(bulletPrefab,
                                          position + new Vector3(Mathf.Cos(angle) * radius
                                                                    , Mathf.Sin(angle) * radius
                                                                    , 0),
                                          transform.rotation);
                bullet.GetComponent<BulletController>().Initiate(bulletLifeSpan
                                                                    , new Vector3(Mathf.Cos(angle) * radius
                                                                                    , Mathf.Sin(angle) * radius
                                                                                    , 0)
                                                                    , bulletPower);

                bullet.layer = transform.parent.gameObject.layer;
            }
        }
        else
        {
            position = transform.parent.position + shootDirection;

            var bullet = Instantiate(bulletPrefab, position, transform.rotation);
            bullet.GetComponent<BulletController>().Initiate(bulletLifeSpan
                                                              , shootDirection * bulletSpeed
                                                              , bulletPower);

            bullet.layer = transform.parent.gameObject.layer;
        }
    }
    #endregion

    #region Private Methods
    private void Awake()
    {
        m_ShootDirection = Vector3.zero;
        timerBetweenShots = reloadTime;
    }

    private void Update()
    {
        UpdateReloadTime();
    }

    private void UpdateReloadTime()
    {
        if (timerBetweenShots < 0.0f)
        {
            timerBetweenShots = reloadTime;
        }
        else if (timerBetweenShots < reloadTime)
        {
            timerBetweenShots -= Time.deltaTime;
        }
    }
    #endregion





}
