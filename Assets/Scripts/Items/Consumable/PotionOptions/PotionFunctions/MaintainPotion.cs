using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainPotion : PotionFunctonBase
{

    int startTurn;
    int endTurn;

    protected override void Use(LivingEntity target)
    {
        AddEvent();
    }

    void Fuction()
    {
        

    }


    void AddEvent()
    {
        EventManager.Instance.OnPlayerMove.AddListener(this.Fuction);

    }

    void RemoveEvent()
    {
        if (endTurn <= TurnManager.instance.turn)
        {
            EventManager.Instance.OnPlayerMove.RemoveListener(this.Fuction);
        }

    }

}
