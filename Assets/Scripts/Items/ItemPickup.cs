using UnityEngine;
using DG.Tweening;

public class ItemPickup : MonoBehaviour, IPickable ,IToggleStatus
{
    private Rigidbody rb;
    private Transform itemTransform;
    public bool isAlreadyCollected = false;
    public bool IsAlreadyCollected => isAlreadyCollected;
    public bool isCollided = false;

    private void Awake() 
    {
        rb = gameObject.GetComponent<Rigidbody>();
        itemTransform = transform;
    }

    public void ToggleStatus()
    {
        isAlreadyCollected = !isAlreadyCollected;
    }

    public void HandlePickItem()
    {
        itemTransform.gameObject.SetActive(false);
        ToggleStatus();
    }
}
