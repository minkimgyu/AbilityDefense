using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
   void SetDamage(float damage);
   void SetHeal(float healAmount);

   void SetHP(BuffValue<float> hp);
}
