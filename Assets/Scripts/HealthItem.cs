using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealthItem : Item
{
    public float healthPercentage;

    public override void Use()
    {
        GameManager.Instance.UpdatePlayerHealth(healthPercentage);
    }
}
