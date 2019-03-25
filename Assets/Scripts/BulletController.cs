using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 speedVector;
    private float bulletPower;

    public GameObject destroyEffect;

    public void Initiate(float i_LifeSpan, Vector3 i_SpeedVector, float power)
    {
        speedVector = i_SpeedVector;
        bulletPower = power;
        Invoke("DestroyItself", i_LifeSpan);
    }

    private void Update()
    {
        transform.Translate(speedVector * Time.deltaTime);
    }

    private void DestroyItself()
    {
        var effect = Instantiate(destroyEffect, transform.position, Quaternion.identity );
        float effectDuration = effect.GetComponent<ParticleSystem>().duration + effect.GetComponent<ParticleSystem>().startLifetime;
        Destroy(effect, effectDuration);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HealthHandler>() != null) {
            collision.gameObject.GetComponent<HealthHandler>().Hit(bulletPower);
        }
        DestroyItself();
    }

}
