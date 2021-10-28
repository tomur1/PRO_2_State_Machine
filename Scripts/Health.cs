using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int startingHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }
    
}
