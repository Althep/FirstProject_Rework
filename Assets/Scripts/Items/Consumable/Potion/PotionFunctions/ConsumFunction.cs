using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ConsumeFunction
{
    protected LivingEntity targetEntity;
    public int amount;
    public int maintain;
    
    protected virtual void AddMyFunction()
    {
        string functionName = this.GetType().ToString();
        if (!GameManager.instance.dataManager.consumFunctions.ContainsKey(functionName))
        {
            GameManager.instance.dataManager.consumFunctions.Add(functionName, ConsumeFunc);
            GameManager.instance.dataManager.ConsumFunctionScripts.Add(functionName, this);
        }
        
    }
    protected virtual void ConsumeFunc(LivingEntity entity)
    {

    }

    public void SetFunctions()
    {
        HealingPotion healingPotion = new HealingPotion();
        healingPotion.AddMyFunction();
        ManaPotion manaPotion = new ManaPotion();
        manaPotion.AddMyFunction();
    }


}

public class HealingPotion : ConsumeFunction
{

    protected override void ConsumeFunc(LivingEntity entity)
    {

        if (entity.myState.currntHp + 10 > entity.myState.maxHp)
        {
            entity.myState.currntHp = entity.myState.maxHp;
        }
        else
        {
            entity.myState.currntHp += 10;
        }
        entity.SetHpbarValue();
        EventManager.Instance.OnPlayerBattle.Invoke();
    }
}

public class ManaPotion : ConsumeFunction
{
    protected override void ConsumeFunc(LivingEntity entity)
    {
        if (entity.myState.currentMana + 10 > entity.myState.maxMana)
        {
            entity.myState.currentMana = entity.myState.maxMana;
        }
        else
        {
            entity.myState.currentMana += 10;

        }
        EventManager.Instance.OnPlayerBattle.Invoke();
    }
}


public class MaintainPotion : ConsumeFunction
{

    protected int startTurn;
    protected int endTurn;

    protected UnityAction _cashedAction;
    protected void SetEndTurn()
    {
        startTurn = GameManager.instance.turnManager.turn;
        endTurn = startTurn + maintain;
    }
    protected override void ConsumeFunc(LivingEntity entity)
    {
        SetEndTurn();
    }

    protected virtual void Action(LivingEntity entity)
    {

    }

    protected virtual void EndAction(LivingEntity entity)
    {

    }
    protected virtual void AddEvent()
    {
        //EventManager.Instance.OnPlayerMove.AddListener(Action);

    }

    protected virtual void RemoveEvent()
    {
        /*
        if (endTurn <= TurnManager.instance.turn)
        {
            EventManager.Instance.OnPlayerMove.RemoveListener(Action);
        }
        */
    }

}
public class StrengthPotion : MaintainPotion
{
    protected override void ConsumeFunc(LivingEntity entity)
    {
        maintain = 10;
        SetEndTurn();
        entity.myState.str += 3;

        UnityAction action = () => Action(entity);

        EventManager.Instance.OnPlayerMove.AddListener(action);
        _cashedAction = action;
    }

    protected override void Action(LivingEntity entity)
    {
        if (GameManager.instance.turnManager.turn == endTurn)
        {
            entity.myState.str -= 3;
            Debug.Log("EndAction Actived");
            EventManager.Instance.OnPlayerMove.RemoveListener(_cashedAction);
        }
        
    }

}




