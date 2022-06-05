using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour,IDestroyable
{
  
   private Stats.Team targetTeam;
   private ProjectileData data;
   private Transform target;
   private Vector3 direction;
   private bool init = false;
   public void Init(ProjectileData data, Stats.Team targetTeam, Transform target)
   {
      Invoke(nameof(DestroyEntity),data.lifeTime);
      this.target = target;
      direction = (target.position - transform.position).normalized;
      this.targetTeam = targetTeam;
      init = true;
      this.data = data;

   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      IGetStats stats = other.GetComponent<IGetStats>();
      if (stats == null)
      {
         return;
      }

      if (stats.GetStats().team == targetTeam)
      {
         Debug.Log("hit " + other.name);
         other.GetComponent<IDamageable>().TakeDamage(data.damage);
         DestroyEntity();
      }
   }

   private void Update()
   {
      if (!init) return;
      if (transform != null)
      {
         switch (data.weaponType)
         {
            case WeaponType.LockOn:
               transform.position = Vector3.MoveTowards(transform.position, target.position, data.speed * Time.deltaTime);
               break;
            case WeaponType.NonLockOn:
               transform.Translate(direction*data.speed*Time.deltaTime);
               break;
         }
      }
      else
      {
         transform.Translate(direction*data.speed*Time.deltaTime);
      }
   
   }



   public void DestroyEntity()
   {
      Destroy(gameObject);
   }
}
