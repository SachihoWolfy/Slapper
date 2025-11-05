using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public int keyAmount = 0;
    public int goalPlush = 0;
    
    public bool TryKey()
    {
        if (keyAmount > 0)
        {
            keyAmount--;
            return true;
        }
        else
        {
            if (keyAmount < 0)
            {
                keyAmount = 0;
            }
            return false;
        }
    }
    public void AcquireKey(int amount = 1)
    {
        keyAmount += amount;
    }

    public int GetKeyAmount()
    {
        return keyAmount;
    }

    public bool TryPlush()
    {
        return goalPlush > 0;
    }
    public void AcquirePlush(int amount = 1)
    {
        goalPlush += 1;
    }
}
