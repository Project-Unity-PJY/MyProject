using System.Collections.Generic;

[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int quantity;
    public int position;  // 아이템의 위치를 저장할 필드 추가

    public InventoryItem(ItemData data, int qty, int pos)
    {
        itemData = data;
        quantity = qty;
        position = pos;  // 위치 초기화
    }
}

[System.Serializable]
public class Inventory
{
    public InventoryItem[] items = new InventoryItem[16]; // 16칸의 고정된 인벤토리 배열

    public void AddItem(ItemData data, int qty)
    {
        // 먼저 인벤토리에서 아이템이 이미 있는지 확인합니다.
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].itemData == data)
            {
                // 이미 존재하는 아이템이면 수량을 추가합니다.
                items[i].quantity += qty;
                return;
            }
        }

        // 빈 슬롯을 찾아 새로운 아이템을 추가합니다.
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = new InventoryItem(data, qty, i);
                return;
            }
        }

    }

    public void RemoveItem(ItemData data, int qty)
    {
        // 인벤토리에서 아이템을 찾습니다.
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].itemData == data)
            {
                // 아이템의 수량을 감소시킵니다.
                items[i].quantity -= qty;

                // 아이템의 수량이 0 이하가 되면 리스트에서 삭제합니다.
                if (items[i].quantity <= 0)
                {
                    items[i] = null;
                }
                return;
            }
        }
    }
}


