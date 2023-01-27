using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] private ItemPickup itemToSpawn;
    public static SpawnItem Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnDropDownItem(Vector3 spawnPosition, Vector3 jumpToPosition)
    {
        Vector3 itemPickupScale = new Vector3(20,20,20);
        ItemPickup itemPickupSpawned =  Instantiate(itemToSpawn,spawnPosition,Quaternion.identity);

        itemPickupSpawned.transform.localScale = new Vector3(0,0,0);
        itemPickupSpawned.transform.DOJump(jumpToPosition,Random.Range(2,7),1,Random.Range(0.4f,0.6f));
        itemPickupSpawned.transform.DOScale(itemPickupScale,0.2f);

    }
}
