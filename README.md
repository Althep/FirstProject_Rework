## 1.소개

- Unity로 제작한 2D 로그라이크 게임입니다. 

- 기본적인 역량을 쌓기 위해서 자료구조와 알고리즘 등을 공부하며 제작하기 좋은 장르라고 생각 해 시작하게 되었습니다

- 이전에도 비슷한것을 제작 했으나, 구현방식, 자료구조와 알고리즘 공부를 위해서 공부 후에 다시 제작하며 구현방식을 다르게 했습니다.
- 
  [이전 프로젝트](https://github.com/Althep/first_project)

## 2. 개발환경
- Unity 2022.3.21.f1
- C#
- window 10


## 3. 구현 기능
- 유니티의 인풋 시스템을 바꿔 GetAxtis의 키를 각 숫자패드로 변경
![스크린샷 2024-10-28 010920](https://github.com/user-attachments/assets/bcc38295-4d5f-4af4-9529-5a563fba90c9)
![스크린샷 2024-10-28 010930](https://github.com/user-attachments/assets/48e48172-81cc-42be-8261-61b327804d9c)
    
### 3-1 맵 관련
- 재귀와 이중 분할을 이용 한 무작위 맵 생성[Assets/Scripts/Managers/Map/MapMake.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Managers/Map/MapMake.cs)

 ```
    void MapDivide(int startX, int startY, int endX, int endY, int count)
    {//재귀함수로 맵 나누기
        if (count < maxCount && endX - startX > 5 && endY - startY > 5)
        {
            if ((endX - startX) > (endY - startY))
            {
                count++;
                int divided = ((startX + endX) * UnityEngine.Random.Range(dividingMin, dividingMax)) / 200;
                divided = RerollDivied(startX, endX, divided);
                for (int y = startY; y < endY; y++)
                {
                    Vector2 Pos = new Vector2(divided, y);
                    TileMap[Pos] = TileType.wall;
                }
                int door = UnityEngine.Random.Range(startY, endY);
                Vector2 doorPos = new Vector2(divided, door);
                TileMap[doorPos] = TileType.door;
                if (divided - startX > 3)
                {
                    MapDivide(startX, startY, divided, endY, count);
                }
                if (endX - divided > 3)
                {
                    MapDivide(divided, startY, endX, endY, count);
                }
            }
            else
            {
                count++;
                int divided = ((startY + endY) * UnityEngine.Random.Range(dividingMin, dividingMax)) / 200;
                divided = RerollDivied(startY, endY, divided);
                for (int x = startX; x < endX; x++)
                {
                    Vector2 pos = new Vector2(x, divided);
                    TileMap[pos] = TileType.wall;
                }
                int door = UnityEngine.Random.Range(startX, endX);
                Vector2 doorPos = new Vector2(door, divided);
                TileMap[doorPos] = TileType.wall;
                if (divided - startY > 3)
                {
                    MapDivide(startX, startY, endX, divided, count);
                }
                if (endY - divided > 3)
                {
                    MapDivide(startX, divided, endX, endY, count);
                }
            }
        }
    }
```
- 맵 생성시에 맵 데이터를 딕셔너리로 저장 해 미니맵 구현
![스크린샷 2024-10-28 013308](https://github.com/user-attachments/assets/1f790833-e869-40a2-a8e5-798211ba63ec)
-[Assets/Scripts/Maps/MiniMap/MiniMapPanel.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Maps/MiniMap/MiniMapPanel.cs)
- 무작위 함수로 몬스터와 아이템 절차적생성

    [Assets/Scripts/Managers/Map/ItemManager.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Managers/Map/ItemManager.cs)

    [Assets/Scripts/Managers/Map/MonsterManager.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Managers/Map/MonsterManager.cs)
- 층계에 따라 계산되어 각각의 티어 아이템 확률 차등 적용
- 데이터 저장을 이용한 씬이동으로 층계 구현 방문한 층에는 기존의 데이터를 가져옴
- CSV파일을 Resources.Load 함수를 이용 해 읽어 몬스터/아이템 데이터 적용

  주석 처리 된 부분은 직접 짠 코드이나 공백이 생기면 데이터를 잘 읽지 못하는 문제가 생겨 해결하지 못해 인터넷상의 코드 사용

  [Assets/Scripts/Managers/Datas/CSVReader.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Managers/Datas/CSVReader.cs)
  
- 몬스터/아이템의 이미지 에셋번들화, 이름에 따라서 해당 스프라이트 적용
  번들화 [Assets/Editor/AssetBundleBuildManager.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Editor/AssetBundleBuildManager.cs)
  스프라이트 적용[Assets/Scripts/Managers/Datas/DataManager.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Managers/Datas/DataManager.cs)
  ```
    public void LoadAssetBundle()
    {
        bundlePath = "./AssetBundle/monster";
        if(monsterBundle == null)
        {
            monsterBundle = AssetBundle.LoadFromFile(bundlePath);
        }
        
        bundlePath = "./AssetBundle/item";
        if(itemBundle == null)
        {
            itemBundle = AssetBundle.LoadFromFile(bundlePath);
        }
        
        /*bundlePath = "./Bundle/item";
        itemBundle = AssetBundle.LoadFromFile(bundlePath);*/
        if (monsterBundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle {monsterBundle}");
        }
        //monsterBundle.Unload(false);
    }
  public Sprite GetItemImage(string imageName)
    {
        Texture2D texture = itemBundle.LoadAsset<Texture2D>(imageName);
        if (texture == null)
        {
            Debug.Log("spriteError");
            return null;
        }
        else
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.height*2);
            
            return sprite;
        }
    }
  ```
- 우선순위 큐와 A*를 이용 해 어느정도까지 플레이어를 추격하는 몬스터 AI
  
  [Assets/Scripts/LivingEntity/Monsters/PathFinding.cs](https://github.com/Althep/FirstProject_Rework/tree/main/Assets/Scripts/LivingEntity/Monsters/MonsterAct)
  ActState에 따라
  ````
      public void TurnAct()
    {
        switch (myState.moveState)
        {
            case MoveState.idle:
                break;
            case MoveState.move:
                Move();
                break;
            case MoveState.attack:
                Attack();
                break;
            default:
                break;
        }
    }
 추후에는 각 ActState를 특정 함수를 상속하는 함수로 작성하는게 좋아보임
 ### 3-2 데이터관련[Assets/Scripts/Managers/Datas/DataManager.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Managers/Datas/DataManager.cs)
 - 각 함수명들을 아이템의 정보에 포함, 딕셔너리로 불러오는 기능 ex) 회복포션
  ```
  public class HealingPotion : ConsumFunction
  {

    protected override void Myfunction(LivingEntity entity)
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
  } // 각 스탯을 Get, Set함수로 사용 해 Get,Set 함수에 이벤트를 넣는것이 더 좋아보임
```
 - 각 아이템 상속구조를 이용해 고유 필드 구현 ItemBase를 상속하는 ConsumItem, EquipItem 구현 후 이를 다시 상세히 나누는 방식 (Use 함수를 Override해 장비와 소모품의 기능 차별화)
  ### 3-3 이벤트 관련[Assets/Scripts/Managers/EventManager.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Managers/EventManager.cs)
  - 유니티 이벤트를 이용 해 플레이어 레벨업,피격 시 UI갱신
  - 유니티 이벤트를 이용 해 지속성 포션 아이템의 기능 구현
  ```
  public class StrengthPotion : MaintainPotion
  {
    protected override void Myfunction(LivingEntity entity)
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
  ```
  ### 3-5 기타
- 플레이어의 인벤토리 구현, List<ItemBase> 이 정보를 바탕으로 인벤토리 UI 구현
  [Assets/Scripts/UI/Inventory/InventoryUI.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/UI/Inventory/InventoryUI.cs)
- 딕셔너리를 이용 해 플레이어의 장비 확인 (이미 존재하는 키라면 데이터를 교체하는식)
  [Assets/Scripts/Items/Equipment/EquipItem.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Items/Equipment/EquipItem.cs)
- 오브젝트의 Layer와 RayCast를 이용 한 전장의 안개(시야시스템)구현
  [Assets/Scripts/Maps/FogOfWar/FogOfWar.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/Maps/FogOfWar/FogOfWar.cs)
- UI 상속을 통해 위치 지정 및 단축키 할당, 맵핑
  [Assets/Scripts/UI/UIBase.cs](https://github.com/Althep/FirstProject_Rework/blob/main/Assets/Scripts/UI/UIBase.cs)
## 4. 남은 기능
- 4-1 각 UI의 키코드를 게임오브젝트와 연결 한 딕셔너리를 이용 해 플레이어 편의로 단축키 변경

이동키를 코드로 바꾸는것을 아직 찾지 못함, 지금 등록 해 둔 인벤토리와 마법관련 UI는 언제든 구현 가능 할 것같음

- 4-2 마법 관련 기능

  플레이어가 마법이나 아이템 투척등을 한다 하였을 때에 기존의 인풋으로 플레이어 대신 타겟위치를 설정하는 오브젝트와 그것을 움직이는 코드 필요
  inputManager의 PlayerMoveState.Aiming 관련된 함수 작성 필요

- 4-3 지속성 아이템 기능 모듈화
 구현은 했으나 아직 모듈화 하는것이 익숙하지 않아 모듈화까지는 하지 못함 이를 모듈화 해 다른 포션 기능들도 빠르게 구현 할 수 있게 모듈화 필요

- 4-3 메인, 게임오버씬
  InputManager와 GameManager의 코드 변경 필요할것같음
  던전이 구현되어있는 씬 이외의 씬은 다른 인풋매니저를 사용하는 방식으로 해결 가능 할것으로 생각됨

  
