using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Private & Const Variables
    private Vector3 speedVector;
    private double bulletPower;
    #endregion

    #region Public Variables
    public GameObject destroyEffect;
    #endregion

    #region Public Methods
    public void Initiate(float i_LifeSpan, Vector3 i_SpeedVector, float power)
    {
        speedVector = i_SpeedVector;
        bulletPower = power;
        Invoke("DestroyItself", i_LifeSpan);
    }
    #endregion

    #region Private Methods

    private void Update()
    {
        transform.Translate(speedVector * Time.deltaTime);
    }

    private void DestroyItself()
    {
        var destroyAnimation = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        float animationDuration = destroyAnimation.GetComponent<ParticleSystem>().main.duration + destroyAnimation.GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
        Destroy(destroyAnimation, animationDuration);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HealthHandler>() != null)
        {
            collision.gameObject.GetComponent<HealthHandler>().Effect(-bulletPower);
        }
        DestroyItself();
    }
    #endregion






}
