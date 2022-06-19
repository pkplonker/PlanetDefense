using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Purchaseable : ScriptableObject,IBuyable
{

    public abstract string GetStatName();

    public abstract void Buy();
    public abstract ulong GetCurrentCost();

    public abstract string GetLevel();
    public abstract bool GetIsOneTimePurchase();
}
