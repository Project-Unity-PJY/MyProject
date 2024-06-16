using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory = new Inventory(); // 논리적 인벤
    public InventoryView[] inventoryViews; // 화면에 보일 인벤
    public InventoryView[] quickInvenViews; // 빠른 인벤
    public GameObject invenOBJ;
    private bool isOpen;

    void Start()
    {
        // 게임 시작 시 인벤토리를 로드합니다.
        LoadInventory();

        isOpen = false;

        // 인벤토리와 뷰를 동기화합니다.
        setInventory();
    }

    void OnApplicationQuit()
    {
        // 게임 종료 시 인벤토리를 저장합니다.
        SaveInventory();
    }

    public void AddItem(string itemName, int qty)
    {
        ItemData newItem = Resources.Load<ItemData>("Items/" + itemName); // 추가할 아이템 이름으로 조회
        inventory.AddItem(newItem, qty);
        setInventory();
    }

    public void RemoveItem(string itemName, int qty)
    {
        ItemData newItem = Resources.Load<ItemData>("Items/" + itemName); // 삭제할 아이템 이름으로 조회
        inventory.RemoveItem(newItem, qty);
        setInventory();
    }

    // 인벤 화면 활성화/비활성화
    public void onClickInventory()
    {
        invenOBJ.SetActive(isOpen);
        isOpen = !isOpen;
    }

    // 논리 인벤과 화면에 보이는 인벤 동기화
    public void setInventory()
    {
        if (inventoryViews == null)
        {
            Debug.LogError("InventoryViews array is not set");
            return;
        }

        for (int i = 0; i < inventoryViews.Length; i++)
        {
            if (inventoryViews[i] == null)
            {
                Debug.LogError($"InventoryView at index {i} is not set");
                continue;
            }

            if (i < inventory.items.Length && inventory.items[i] != null)
            {
                InventoryItem temp = inventory.items[i];
                if (temp.itemData != null) // 추가된 null 체크
                {
                    inventoryViews[i].setItem(temp.itemData.itemImg, temp.quantity);
                    if(i < 4)
                    {
                      quickInvenViews[i].setItem(temp.itemData.itemImg, temp.quantity);
                    }
                }
                else
                {
                    inventoryViews[i].clearItem();
                    if (i < 4)
                    {
                        quickInvenViews[i].clearItem();
                    }
                }
            }
            else
            {
                inventoryViews[i].clearItem(); // 빈 칸을 초기화
            }
        }
    }

    // 인벤토리를 JSON 형식으로 저장하는 메서드
    public void SaveInventory()
    {
        // 직렬화 가능한 인벤토리 아이템 목록을 생성합니다.
        List<SerializableInventoryItem> serializableItems = new List<SerializableInventoryItem>();
        for (int i = 0; i < inventory.items.Length; i++)
        {
            var item = inventory.items[i];
            if (item != null)
            {
                // 각 인벤토리 아이템을 직렬화 가능한 형식으로 변환하여 목록에 추가합니다.
                serializableItems.Add(new SerializableInventoryItem(item.itemData.itemNum, item.quantity, item.position));
            }
        }

        // 직렬화된 인벤토리 객체를 JSON 문자열로 변환합니다.
        string json = JsonUtility.ToJson(new SerializableInventory(serializableItems));

        // 저장 경로를 지정합니다.
        string path = Application.dataPath + "/Items/SaveInventory/inventory.json";

        // 경로가 존재하지 않으면 디렉토리를 생성합니다.
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        // JSON 문자열을 파일에 씁니다.
        File.WriteAllText(path, json);

        Debug.Log("Inventory saved to " + path);
    }

    // 인벤토리를 JSON 파일로부터 로드하는 메서드
    public void LoadInventory()
    {
        // 저장 경로를 지정합니다.
        string path = Application.dataPath + "/Items/SaveInventory/inventory.json";

        // 파일이 존재하는지 확인합니다.
        if (File.Exists(path))
        {
            // 파일에서 JSON 문자열을 읽어옵니다.
            string json = File.ReadAllText(path);

            // JSON 문자열을 직렬화 가능한 인벤토리 객체로 변환합니다.
            SerializableInventory serializableInventory = JsonUtility.FromJson<SerializableInventory>(json);

            // 기존 인벤토리 아이템을 모두 제거합니다.
            inventory.items = new InventoryItem[16];

            // 직렬화된 인벤토리 아이템을 인벤토리에 추가합니다.
            foreach (var serializableItem in serializableInventory.items)
            {
                // itemNum을 통해 ItemData 객체를 찾습니다.
                ItemData itemData = FindItemDataByNum(serializableItem.itemNum);
                if (itemData != null)
                {
                    // 인벤토리에 아이템을 추가합니다.
                    inventory.AddItem(itemData, serializableItem.quantity);
                }
            }
            Debug.Log("Inventory loaded from " + path);
        }
        else
        {
            Debug.Log("No inventory file found at " + path);
        }
    }

    // itemNum을 통해 ItemData 객체를 찾는 메서드
    private ItemData FindItemDataByNum(int itemNum)
    {
        // Resources 폴더에서 itemNum에 해당하는 ItemData를 로드합니다.
        return Resources.Load<ItemData>("Items/Item_" + itemNum);
    }
}

// 직렬화 가능한 인벤토리 객체 클래스
[System.Serializable]
public class SerializableInventory
{
    public List<SerializableInventoryItem> items;

    public SerializableInventory(List<SerializableInventoryItem> items)
    {
        this.items = items;
    }
}

// 직렬화 가능한 인벤토리 아이템 클래스
[System.Serializable]
public class SerializableInventoryItem
{
    public int itemNum;
    public int quantity;
    public int position; // 아이템의 위치를 저장할 필드 추가

    public SerializableInventoryItem(int itemNum, int quantity, int position)
    {
        this.itemNum = itemNum;
        this.quantity = quantity;
        this.position = position; // 위치 초기화
    }
}
