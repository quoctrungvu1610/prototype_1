using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bullet : MonoBehaviour,IMoveable,IDamageable,IParticleSystems
{
    [SerializeField] private float bulletSpeed = 1f;
    [SerializeField] private int bulletPower;
    [SerializeField] private ParticleSystem collideParticleSystem;
    private Transform bulletTransform;

    private void Start()
    {
        bulletTransform = this.transform;
    }

    private void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        bulletTransform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * (bulletSpeed + PlayerStackMechanic.Instance.NumberOfItemHolding));
    }
    
    private void DeactiveBullet()
    {
        bulletTransform.localScale = new Vector3(0.0f,0.0f,0.0f);
        bulletTransform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("BigWall"))
        {
            Vector3 bulletCollideParticleSystemScale = new Vector3(2,2,2);
            ICanTakeDamage client;
            client = other.gameObject.GetComponent<ICanTakeDamage>();


            HandleDamage(bulletPower,client);
            InstantiateParticleSystems(collideParticleSystem, bulletTransform.position, bulletCollideParticleSystemScale);
            DeactiveBullet();
        } 
    }

    public void HandleDamage(int damage, ICanTakeDamage takeDamegeObject)
    {
        takeDamegeObject.TakeDamage(damage);
    }

    public void InstantiateParticleSystems(ParticleSystem particleSystem, Vector3 instantiatePosition, Vector3 instantiateScale)
    {
        ParticleSystem particle =  Instantiate(particleSystem);

        particle.transform.localPosition = instantiatePosition;
        particleSystem.transform.localScale = instantiateScale;

        Destroy(particle.gameObject,1f);
    }
}
