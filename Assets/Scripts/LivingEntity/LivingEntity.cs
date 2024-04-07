using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct EntityState
{
    public int maxHp;
    public int currntHp;
    public bool isDead;
    public int base_AttackSpeed;
    public int base_MoveSpeed;
    public int attackSpeed;
    public int moveSpeed;
    public int turn;
    public int oldTurn;
    public int damage;
}

public class LivingEntity : MonoBehaviour
{
    public EntityState myState;
    
    public MoveState moveState;
    public int lastDamaged;
    
    GameObject hpSliderObj;
    Slider hpSlider;
    GameObject myCanvasObj;

    private void Awake()
    {
        SetCanvas();
        Debug.Log(this.gameObject.name);
    }
    private void Start()
    {
        Debug.Log(this.gameObject.layer);
    }
    private void Update()
    {
        //CanvasSetActive();
    }
    public virtual void Damaged(int damage)
    {
        myState.currntHp -= damage;
        IsDead();
        SetHpBarValue();
    }
    void SetCanvas()
    {
        Debug.Log("SetMyCanvas");
        myCanvasObj = this.gameObject.transform.GetChild(0).transform.gameObject;
        Debug.Log(myCanvasObj);
        hpSliderObj = myCanvasObj.transform.GetChild(0).gameObject;
        Debug.Log(hpSliderObj);
        hpSlider = hpSliderObj.transform.GetComponent<Slider>();
        Debug.Log(hpSlider);
        Debug.Log(this.gameObject.name);
    }
    protected void CanvasSetActive()
    {
        if(myCanvasObj == null)
        {
            Debug.Log("Canvas Error");
            Debug.Log(this.gameObject.layer);
            return;
        }
        if (myCanvasObj.layer != this.gameObject.layer)
        {
            myCanvasObj.layer = this.gameObject.layer;
        }
    }
    void SetHpBarValue()
    {
        hpSlider.value = myState.currntHp / myState.maxHp;
    }
    public virtual void Attack(LivingEntity target,int damage)
    {
        target.Damaged(damage);
    }
    protected virtual void IsDead()
    {
        
    }
    protected virtual void OnMove()
    {

    }
    protected virtual void SetSpeed()
    {
        myState.attackSpeed = myState.base_AttackSpeed;
        myState.moveSpeed = myState.base_MoveSpeed;
    }
}
