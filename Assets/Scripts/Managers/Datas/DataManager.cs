using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DataManager
{

    //Dictionary<string, List<object>> _monsterDatas;

    Dictionary<string, List<object>> _ConsumItemData;
    public Dictionary<string, List<object>> consumItemData { get { return _ConsumItemData; } }


    public List<Dictionary<string,object>> equipMentsData;
    public List<Dictionary<string, object>> consumData;
    public List<Dictionary<string, object>> monsterData;

    Dictionary<string, List<object>> _magicDatas;
    public Dictionary<string, List<object>> magicDatas { get { return _magicDatas; } }

    Dictionary<string, Dictionary<string, List<object>>> allItems = new Dictionary<string, Dictionary<string, List<object>>>();

    public Dictionary<int, List<int>> equipIndexBytier = new Dictionary<int, List<int>>(); // tier Rates index 1부터 넣었음!
    public Dictionary<int, List<int>> consymIndexBytier = new Dictionary<int, List<int>>(); // tier Rates index 1부터 넣었음!
    public Dictionary<int, List<int>> monsterIndexBytier = new Dictionary<int, List<int>>(); // tier Rates index 1부터 넣었음!
    public Dictionary<string, List<int>> tierRates = new Dictionary<string, List<int>>();
   

    

    public void ReadDataByTiers() // CSV읽고 티어별 인덱스 정렬
    {
        string path = "EquipItemData";

        equipMentsData = GameManager.instance.csvReader.Read(path);

        for (int i = 0; i < equipMentsData.Count; i++)
        {
            int tier = Convert.ToInt32(equipMentsData[i]["tier"]);
            int index = Convert.ToInt32(equipMentsData[i]["index"]);
            if (equipIndexBytier.ContainsKey(tier))
            {
                equipIndexBytier[tier].Add(index);
            }
            else
            {

                equipIndexBytier[tier] = new List<int>();
                equipIndexBytier[tier].Add(index);
            }
        }
        path = "ConsumItemData";
        
        consumData = GameManager.instance.csvReader.Read(path);

        for (int i = 0; i < consumData.Count; i++)
        {
            int tier = Convert.ToInt32(consumData[i]["tier"]);
            int index = Convert.ToInt32(consumData[i]["index"]);
            if (consymIndexBytier.ContainsKey(tier))
            {
                consymIndexBytier[tier].Add(index);
            }
            else
            {

                consymIndexBytier[tier] = new List<int>();
                consymIndexBytier[tier].Add(index);
            }
        }
        
        path = "MonsterData";

        monsterData = GameManager.instance.csvReader.Read(path);

        for(int i = 0; i < monsterData.Count; i++)
        {
            int tier = Convert.ToInt32(monsterData[i]["tier"]);
            int index = Convert.ToInt32(monsterData[i]["index"]);
            if (monsterIndexBytier.ContainsKey(tier))
            {
                monsterIndexBytier[tier].Add(index);
            }
            else
            {

                monsterIndexBytier[tier] = new List<int>();
                monsterIndexBytier[tier].Add(index);
            }
        }
        

    }



    /*

    public void ReadAllItems()
    {
        Array value = Enum.GetValues(typeof(EquipType));


        foreach (EquipType type in value)
        {
            string typeName = type.ToString();

            string path = "EquipItemData";
            //path += typeName;

            Dictionary<string, List<object>> itemDatas = GameManager.instance.csvReader.CSVReade(path);
            Debug.Log($"typeName : {typeName}");
            allItems.Add(typeName, itemDatas);
        }
        value = Enum.GetValues(typeof(ConsumType));
        foreach (ConsumType type in value)
        {
            string typeName = type.ToString();
            string path = "";
            path += typeName;

            Dictionary<string, List<object>> consumDatas = GameManager.instance.csvReader.CSVReade(path);

            allItems.Add(typeName, consumDatas);
        }

    }
    */
    public void SetMonsterData(int index, MonsterState monsterState)
    {//수정필요
        if (monsterData == null)
        {
            Debug.Log("Monster DataError");
            return;
        }

        monsterState.name = monsterData[index]["name"].ToString();
        monsterState.index = Convert.ToInt32(monsterData[index]["index"]);
        monsterState.exp = Convert.ToInt32(monsterData[index]["exp"]);
        monsterState.myState.maxHp = Convert.ToInt32(monsterData[index]["maxhp"]);
        monsterState.myState.currntHp = monsterState.myState.maxHp;
        monsterState.myState.damage = Convert.ToInt32(monsterData[index]["damage"]);
        monsterState.myState.def = Convert.ToInt32(monsterData[index]["def"]);
        monsterState.myState.base_MoveSpeed = Convert.ToInt32(monsterData[index]["moveSpeed"]);
        monsterState.myState.base_AttackSpeed = Convert.ToInt32(monsterData[index]["attackSpeed"]);
    }

    public void SetItempData(int index, ItemBase itemData)
    {
        //공통부분
        switch (itemData)
        {
            case EquipItem equip:
                SetEquipData<EquipItem>(index, equip);
                break;
            case ConsumItem consum:
                SetConsumData<ConsumItem>(index, consum);
                break;
            default:
                break;
        }

    }
    public void SetEquipData<T>(int index, T itemData) where T : EquipItem
    {
        string type = typeof(T).ToString();
        itemData.name = equipMentsData[index]["name"].ToString();
        itemData.index = Convert.ToInt32(equipMentsData[index]["index"]);
        itemData.weight = Convert.ToInt32(equipMentsData[index]["weight"]);
        switch (itemData)
        {
            case Weapon weapon:
                weapon.damage = Convert.ToInt32(equipMentsData[index]["damage"]);
                weapon.attackSpeed = Convert.ToInt32(equipMentsData[index]["attackSpeed"]);
                break;
            case Shield shield:
                shield.defense = Convert.ToInt32(equipMentsData[index]["defense"]);
                shield.blockRate = Convert.ToInt32(equipMentsData[index]["blockRate"]);
                break;
            case Helm helm:
                helm.defense = Convert.ToInt32(equipMentsData[index]["defense"]);
                break;
            case Armor armor:
                armor.defense = Convert.ToInt32(equipMentsData[index]["defense"]);
                break;
            case Amulet amulet:
                amulet.defense = Convert.ToInt32(equipMentsData[index]["defense"]);
                break;
            case Glove glove:
                glove.defense = Convert.ToInt32(equipMentsData[index]["defense"]);
                break;
            case Shoose shoose:
                shoose.defense = Convert.ToInt32(equipMentsData[index]["defense"]);
                break;
            case Ring ring:
                ring.defense = Convert.ToInt32(equipMentsData[index]["defense"]);
                break;
            default:
                break;
        }

    }
    public void SetConsumData<T>(int index, T itemData) where T : ConsumItem
    {
        string gear = typeof(T).ToString();
        itemData.index = index;
        itemData.name = (consumData[index]["name"]).ToString();
        itemData.weight = Convert.ToInt32(consumData[index]["weight"]);
        switch (itemData)
        {
            case Potion potion:
                itemData.function = consumData[index]["function"].ToString();
                break;
            case Book book:
                itemData.function = consumData[index]["function"].ToString();
                break;
            case Scroll scroll:
                itemData.function = consumData[index]["function"].ToString();
                break;
            case Evoke evoke:
                itemData.function = consumData[index]["function"].ToString();
                break;
            case Food food:
                itemData.function = consumData[index]["function"].ToString();
                break;
            default:
                break;
        }

    }


    public void NormalDist<T>(T script)
    {
        float mu; // 평균
        float sigma; // 표준편차
        int floor = GameManager.instance.floor;
        switch (script.GetType())
        {

            case Type t when t == typeof(EquipItem):
                mu = Mathf.Log(floor, 3f);
                sigma = Mathf.Log(floor + 1, 5) + 1f;
                tierRates.Add("Equipment", MakeRate(mu, sigma, t));
                break;
            case Type t when t == typeof(ConsumItem):
                mu = Mathf.Log(floor, 4f);
                sigma = Mathf.Log(floor + 1, 4) + 1.07f;
                tierRates.Add("Consumable", MakeRate(mu, sigma, t));
                break;
            case Type t when t == typeof(MonsterState):
                mu = Mathf.Log(floor, 2f);//평균
                sigma = Mathf.Log(floor + 1, 10);//표준편차
                tierRates.Add("MonsterState", MakeRate(mu, sigma, t));
                break;
            default:
                Debug.Log("ScriptType Error");
                break;
        }


    }

    public List<int> MakeRate(float mu, float sigma, Type type)
    {
        float temp;
        float temp1;
        float temp2;
        float rateValue;
        List<int> rateList = new List<int>();

        switch (type)
        {
            case Type t when t == typeof(EquipItem):
                for (int i = 1; i <= equipIndexBytier.Count; i++)
                {
                    temp = 1 / (sigma * Mathf.Sqrt(2 * Mathf.PI));
                    temp2 = -Mathf.Pow((i - mu), 2) / (2 * Mathf.Pow(sigma, 2));
                    temp1 = Mathf.Exp(temp2);
                    rateValue = temp1 / (temp * sigma);
                    int ratetemp = (int)(rateValue * 1000);
                    if (i > 1)
                    {
                        ratetemp += rateList[i - 2];
                    }
                    rateList.Add(ratetemp);
                    Debug.Log($" tier : {i}, Value : {rateValue} ,  converted : {ratetemp}");

                }
                break;
            case Type t when t == typeof(ConsumItem):
                for (int i = 1; i <= consymIndexBytier.Count; i++)
                {
                    temp = 1 / (sigma * Mathf.Sqrt(2 * Mathf.PI));
                    temp2 = -Mathf.Pow((i - mu), 2) / (2 * Mathf.Pow(sigma, 2));
                    temp1 = Mathf.Exp(temp2);
                    rateValue = temp1 / (temp * sigma);
                    int ratetemp = (int)(rateValue * 1000);
                    if (i > 1)
                    {
                        ratetemp += rateList[i - 2];
                    }
                    rateList.Add(ratetemp);
                    //Debug.Log($" tier : {i}, Value : {rateValue} ,  converted : {ratetemp}");

                }
                break;
            case Type t when t == typeof(MonsterState):
                for (int i = 1; i <= monsterIndexBytier.Count; i++)
                {
                    temp = 1 / (sigma * Mathf.Sqrt(2 * Mathf.PI));
                    temp2 = -Mathf.Pow((i - mu), 2) / (2 * Mathf.Pow(sigma, 2));
                    temp1 = Mathf.Exp(temp2);
                    rateValue = temp1 / (temp * sigma);
                    int ratetemp = (int)(rateValue * 1000);
                    if (i > 1)
                    {
                        ratetemp += rateList[i - 2];
                    }
                    rateList.Add(ratetemp);
                    //Debug.Log($" tier : {i}, Value : {rateValue} ,  converted : {ratetemp}");

                }
                break;
            default:
                Debug.Log("ScriptType erorr");
                break;
        }


        
        return rateList;
    }




    /*
      public void NormalDist(string kind)
    {
        float mu;//평균
        float sigma;//표준편차
        int floor;
        floor = GameManager.instance.nowFloor;
        if (kind == "equip")
        {
            mu = Mathf.Log(floor, 3f);
            sigma = Mathf.Log(floor + 1, 5) + 1f;
            rateValuePrint(mu, sigma,kind);
        }
        else if (kind == "monster")
        {
            mu = Mathf.Log(floor, 2f);//평균
            sigma = Mathf.Log(floor + 1, 10);//표준편차
            rateValuePrint(mu, sigma,kind);
        }
        else if( kind == "consum")
        {
            mu = Mathf.Log(floor, 4f);
            sigma = Mathf.Log(floor + 1, 4) + 1.07f;
            rateValuePrint(mu, sigma, kind);
        }
    }

    public int MakeRate(List<float> RateDataList)
    {

        int sumRate=0;
        float randomCount = 0;
        int sumall=0;
        for(int i =0; i < RateDataList.Count; i++)
        {
            sumall += (int)RateDataList[i];
        }
        randomCount = UnityEngine.Random.Range(0, sumall);
        for (int i = 0; i < tierList.Count; i++)
        {
            sumRate += (int)RateDataList[i];
            if (randomCount <= sumRate)
            {
                tier = i+1;
                break;
            }
        }
        return tier;
    }

    //티어와 층계에 따라 함수 그래프 변환
    void rateValuePrint(float mu, float sigma, string kind)
    {
        float rateValue = 0;
        float temp;
        float temp1;
        float temp2;
        float sumrate=0;
        List<float> tempList = new List<float>();
        for (int j = 0; j < tierList.Count; j++)
        {
            rateValue = 1;
            temp = 1 / (sigma * math.pow((math.PI * 2), 1 / 2));
            temp2 = -math.pow((tierList[j] - mu), 2) / (2 * math.pow(sigma, 2));
            temp1 = math.pow(math.E, temp2);
            rateValue = temp1 / (temp * sigma);
            tempList.Add(rateValue);
            sumrate += rateValue;
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            if (kind == "equip")
                equipDatas.Add((tempList[i] / sumrate) * 10000);
            else if (kind == "monster")
                monsterDatas.Add((tempList[i] / sumrate) * 1000);
            else if( kind == "consum")
                consumDatas.Add((tempList[i] / sumrate) * 1000);
        }
    }
     
     
     
     
     */
}
