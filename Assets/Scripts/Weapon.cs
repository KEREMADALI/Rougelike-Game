using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float delayBetweenShots = 1.0f;
    private const float bulletLifeSpan = 5.0f;

    private float timerBetweenShots;
    private float bulletPower = 5.0f;
    private float shootDistance = 0.5f;
    
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

    public void Shoot(Vector3 shootDirection)
    {
        if (timerBetweenShots > 0.0f && timerBetweenShots != delayBetweenShots)
            return;

        timerBetweenShots = delayBetweenShots - Time.deltaTime;

        Vector3 position = transform.parent.position + shootDirection * 1/shootDistance;

        var bullet = Instantiate(bulletPrefab, position, transform.rotation);
        bullet.GetComponent<BulletController>().Initiate(bulletLifeSpan, shootDirection * bulletPower);
    }
}
