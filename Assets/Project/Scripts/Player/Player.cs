using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IParticleSystems
{
    [SerializeField] private Transform collideItemParticleSystemSpawnPosition;
    [SerializeField] private ParticleSystem collideItemParticleSystem;
    [SerializeField] private Transform collideGateParticleSystemSpawnPositions;
    [SerializeField] private ParticleSystem collideGateParticleSystems;
    private PlayerStackMechanic playerStackMechanic;
    private PlayerMoveForward playerMoveForward; 
    private BulletSpawner bulletSpawner;

    private void Start() 
    {
        playerStackMechanic = PlayerStackMechanic.Instance;  
        playerMoveForward = PlayerMoveForward.Instance;
        bulletSpawner = BulletSpawner.Instance;
    }

    private void Update()
    {
        playerMoveForward.HandleMovement();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(PlayerStackMechanic.Instance.IsLoadingAnimation == false)
        {
            if (other.CompareTag("Item"))
            {
                Vector3 collideItemParticleSystemScale = new Vector3(2,2,2);
                IPickable iPickable;
                iPickable = other.GetComponent<IPickable>();

                InstantiateParticleSystems(collideItemParticleSystem, collideGateParticleSystemSpawnPositions.position, collideItemParticleSystemScale);

                if(iPickable.IsAlreadyCollected == false)
                {
                    playerStackMechanic.isCollidedWithPickupItem = true;
                    iPickable.HandlePickItem();
                    StartCoroutine(playerStackMechanic.ReScaleBehavior());
                }   
            }

            else if(other.CompareTag("CalculationGate"))
            {
                Vector3 collideGateParticalSystemScale = new Vector3(5,5,5);
                CalculationGate calculationGate;
                calculationGate = other.gameObject.GetComponent<CalculationGate>();

                InstantiateParticleSystems(collideGateParticleSystems, collideItemParticleSystemSpawnPosition.position, collideGateParticalSystemScale);

                switch(calculationGate.objectToApplyCalculation)
                {
                    case ObjectToApplyCalculation.Speaker:
                        StartCoroutine(playerStackMechanic.CalculateNumberOfItemHolding(calculationGate.calculationType, calculationGate.numberToCalculate));
                        break;

                    case ObjectToApplyCalculation.Bullet:
                        calculationGate.HandleCalculation(bulletSpawner.bonusNumberOfBulletSpawnPerSec, calculationGate.numberToCalculate);
                        break;
                }
            }
        }     
    }

    public void InstantiateParticleSystems(ParticleSystem particleSystem, Vector3 instantiatePosition, Vector3 instantiateScale)
    {
        ParticleSystem particle =  Instantiate(particleSystem);

        particle.transform.localPosition = instantiatePosition;
        particleSystem.transform.localScale = instantiateScale;

        Destroy(particle.gameObject,2f);
    }
}
