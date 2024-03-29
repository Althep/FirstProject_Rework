using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LivingEntity : MonoBehaviour
{
    protected int maxHp;
    protected int currntHp;
    protected bool isDead;
    protected int base_AttackSpeed;
    protected int base_MoveSpeed;
    public int attackSpeed=10;
    public int moveSpeed=10;
    public int turn;
    public int oldTurn;
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
        currntHp -= damage;
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
        hpSlider.value = currntHp / maxHp;
    }
    public virtual void Attack(LivingEntity target)
    {

    }
    protected virtual void IsDead()
    {
        
    }
    protected virtual void OnMove()
    {

    }
    protected virtual void SetSpeed()
    {
        attackSpeed = base_AttackSpeed;
        moveSpeed = base_MoveSpeed;
    }
}
