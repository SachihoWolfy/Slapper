using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDoor : Action
{
    public List<EnemyMovement> m_EnemyList;
    public List<SubAction> m_SubActionList;
    private void Start()
    {
        if (m_EnemyList.Count > 0) ActivateEnemies(false);
    }
    public override void Execute()
    {
        Debug.Log("Door Broken");
        if (gameObject.GetComponent<MeshRenderer>())
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (gameObject.GetComponent<Collider>())
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
        if(m_EnemyList.Count > 0)
        {
            ActivateEnemies(true);
        }
        if (m_SubActionList.Count > 0) {
            ExecuteSubActions();
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
    void ExecuteSubActions()
    {
        foreach (SubAction m in m_SubActionList)
        {
            if (m != null)
            {
                m.Execute();
            }
        }
    }
}
