using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomTrigger : MonoBehaviour
{
    public List<EnemyMovement> m_EnemyList;

    private void Start()
    {
        ActivateEnemies(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateEnemies(true);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovementComp = other.GetComponent<EnemyMovement>();
            if (enemyMovementComp != null)
            {
                enemyMovementComp.activeMovement = false;
                m_EnemyList.Add(enemyMovementComp);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovementComp = other.GetComponent<EnemyMovement>();
            if (enemyMovementComp != null)
            {
                enemyMovementComp.activeMovement = false;
                m_EnemyList.Add(enemyMovementComp);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateEnemies(false);
        }
    }
    void ActivateEnemies(bool state)
    {
        foreach (EnemyMovement m in m_EnemyList)
        {
            if (m != null)
            {
                m.activeMovement = state;
            }
        }
    }
}
