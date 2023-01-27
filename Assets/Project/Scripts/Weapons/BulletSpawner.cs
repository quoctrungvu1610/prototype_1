using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletSpawner : MonoBehaviour
{
    private Transform bulletSpwanerTransform;
    public static BulletSpawner Instance;
    private float time;
    private int keyCheckNumberOfItemHolding;
    private int keyCheckBonusNumberOfBulletSpawnPerSec;
    private float delayTime = 1f;
    public int bonusNumberOfBulletSpawnPerSec;

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

    private void Start()
    {
        time = 0f;

        bulletSpwanerTransform = this.transform;

        keyCheckNumberOfItemHolding = PlayerStackMechanic.Instance.NumberOfItemHolding;

        keyCheckBonusNumberOfBulletSpawnPerSec = bonusNumberOfBulletSpawnPerSec;

    }

    private void Update()
    {
        Debug.Log("bonusNumberOfBulletSpawnPerSec:" + bonusNumberOfBulletSpawnPerSec);
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        //If there are changes in number of item holding
        if(keyCheckNumberOfItemHolding != PlayerStackMechanic.Instance.NumberOfItemHolding || keyCheckBonusNumberOfBulletSpawnPerSec != bonusNumberOfBulletSpawnPerSec)
        {
            //First re-setup the delay time
            ReSetUpDelayTime();
            //Then Change the keyCheck to current number of item holding
            keyCheckNumberOfItemHolding = PlayerStackMechanic.Instance.NumberOfItemHolding;
        }

        else
        {
            InstantiateBullet(delayTime);
        }
    }

    private void InstantiateBullet(float delayTime)
    {
        time = time + 1f * Time.deltaTime;

        if(time > delayTime)
        {
            GetBulletFromPool();  
            time = 0;
        }
    }

    private void GetBulletFromPool()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if(bullet != null)
        {
            bullet.transform.position = bulletSpwanerTransform.position;

            bullet.transform.rotation = bulletSpwanerTransform.rotation;

            bullet.SetActive(true);

            bullet.transform.DOScale(new Vector3(130,130,130),0.5f);  
        }
    }

    private void ReSetUpDelayTime()
    {
        delayTime = 1.0f/(PlayerStackMechanic.Instance.NumberOfItemHolding + 1 + bonusNumberOfBulletSpawnPerSec);
        InstantiateBullet(delayTime);
    }
}
