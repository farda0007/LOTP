using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool isOpen { get; private set; }
    public string chestId { get; private set; }
    public GameObject itemPrefab;
    public Sprite openSprite;

    private void Start()
    {
        chestId ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    public bool CanInteract()
    {
        return !isOpen;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        OpenChest();
    }

    private void OpenChest()
    {
        SetOpened(true);

        if (itemPrefab)
        {
            GameObject item = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);

        }
    }

    public void SetOpened(bool opened)
    {
        
        if (isOpen = opened)
        {
            // Change the sprite to the open sprite
            GetComponent<SpriteRenderer>().sprite = openSprite;
        }
    }
}
