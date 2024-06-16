using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public Image itemImage;
    public Text itemQuantity;

    void Start()
    {
        // itemImage와 itemQuantity가 설정되지 않은 경우, 컴포넌트를 자동으로 찾아 설정합니다.
        if (itemImage == null)
        {
            itemImage = GetComponentInChildren<Image>();
        }

        if (itemQuantity == null)
        {
            itemQuantity = GetComponentInChildren<Text>();
        }
    }

    // 아이템을 설정하는 메서드
    public void setItem(Sprite img, int qty)
    {
        if (itemImage != null && itemQuantity != null)
        {
            itemImage.sprite = img;
            itemQuantity.text = qty.ToString();
            itemImage.enabled = true;
            itemQuantity.enabled = true;
        }
        else
        {
            Debug.LogError("itemImage or itemQuantity is not set");
        }
    }

    // 아이템을 비우는 메서드
    public void clearItem()
    {
        if (itemImage != null && itemQuantity != null)
        {
            itemImage.sprite = null;
            itemQuantity.text = "";
            itemImage.enabled = false;
            itemQuantity.enabled = false;
        }
        else
        {
            Debug.LogError("itemImage or itemQuantity is not set");
        }
    }
}
