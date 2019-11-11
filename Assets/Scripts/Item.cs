using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    [Space]
    public Sprite icon;
    public Sprite pickUpSprite;
    public string description;

    public virtual void Use()
    {

    }
}
