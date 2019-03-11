using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 speedVector;

    public GameObject destroyEffect;

    public void Initiate(float i_LifeSpan, Vector3 i_SpeedVector)
    {
        speedVector = i_SpeedVector;
        Invoke("DestroyItself", i_LifeSpan);
    }

    private void Update()
    {
        transform.Translate(speedVector * Time.deltaTime);
    }

    private void DestroyItself()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity );
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        DestroyItself();
    }

}
