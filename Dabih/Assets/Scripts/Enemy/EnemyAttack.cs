using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float meleeDamage = 20f;
    [SerializeField] float rangedDamage = 3f;
    PlayerHealth target;

    private void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    void RangedAttack()
    {
        if (target == null) 
        {
            return;
        }

        target.TakeDamage(rangedDamage);
    }

    void MeleeAttack()
    {
        if (target == null)
        {
            return;
        }

        target.TakeDamage(meleeDamage);
    }
}
