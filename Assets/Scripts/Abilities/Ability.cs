using UnityEngine;

namespace Abilities
{
    public abstract class Ability : ScriptableObject
    {
        public string abilityName;
        public float cooldown = 1f;


       
    }
}
