using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] float rateOfFire = 0.25f;
    private float nextCycle;

    public void Shoot(GameObject camera)
    {
        if (Time.time > nextCycle)
        {
            nextCycle = Time.time + rateOfFire;

            RaycastHit hit;
            Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);

            if (hit.transform != null)
            {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                Debug.Log("Hit: " + hit.transform.name);
            }
        }

    }
}
