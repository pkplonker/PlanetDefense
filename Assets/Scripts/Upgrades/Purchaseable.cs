using UnityEngine;

namespace Upgrades
{
    public abstract class Purchaseable : ScriptableObject,IBuyable
    {

        public abstract string GetStatName();

        public abstract void Buy();
        public abstract ulong GetCurrentCost();

        public abstract string GetLevel();
        public abstract bool GetIsOneTimePurchase();
    }
}
