using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

[System.Serializable]
public struct EntityState
{
    [JsonProperty]
    public string name;
    public int def;
    public int maxHp;
    public int currntHp;

    public int str;
    public int dex;
    public int intel;

    public bool isDead;
    public int base_AttackSpeed;
    public int base_MoveSpeed;
    public int attackSpeed;
    public int moveSpeed;
    public int turn;
    public int oldTurn;
    public int damage;
    public float attackRange;
    public int index;
    public int exp;
    public int awakingRate;
    public int blockRate;
    public int leftTurnPoint;
    public int maxMana;
    public int currentMana;
    public string State;
    public string distance;
}
[System.Serializable]
public class LivingEntity : MonoBehaviour
{
    [JsonProperty]

    [JsonIgnore]
    GameObject hpSliderObj;
    [JsonIgnore]
    Slider hpSlider;
    [JsonIgnore]
    GameObject myCanvasObj;

    public EntityState myState = new EntityState();
    public Dictionary<EquipType, EquipItem> equips = new Dictionary<EquipType, EquipItem>();
    public ItemInventory myInventory = new ItemInventory();
    public MoveState moveState;
    public int lastDamaged;
    
    

    private void Awake()
    {
        SetCanvas();
        myState.maxMana = 10;
        myState.currentMana = myState.maxMana;
    }
    private void Start()
    {

    }
    private void Update()
    {
        //CanvasSetActive();
    }
    public virtual void Damaged(int damage)
    {
        damage -= myState.def;
        if (damage < 0)
        {
            damage = 0;
        }
        myState.currntHp -= damage;
        IsDead();
        SetHpBarValue();
        GameManager.instance.log.Damaged(this.gameObject, hpSlider.value);
    }
    protected void SetCanvas()
    {
        myCanvasObj = this.gameObject.transform.GetChild(0).gameObject;
        hpSliderObj = myCanvasObj.transform.GetChild(0).gameObject;
        hpSlider = hpSliderObj.transform.GetComponent<Slider>();
        Debug.Log($"mycavase {myCanvasObj}, hpOBJ :{hpSliderObj}, slider{hpSlider}");
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
        Debug.Log($"CurrentHP:{myState.currntHp}, MaxHP:{myState.maxHp}");
        hpSlider.value = (float)myState.currntHp/myState.maxHp;
    }
    public virtual void Attack(LivingEntity target)
    {
        target.Damaged(myState.damage);
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
