using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : PotionFunctonBase
{

    protected virtual void Use()
    {
        HealingPotionFunction(targetEntity);
    }

    void HealingPotionFunction(LivingEntity target)
    {
        target.myState.currntHp += 10;
    }
}
