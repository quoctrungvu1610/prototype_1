using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NumberObstacle : MonoBehaviour, ICanTakeDamage, IParticleSystems
{
    [SerializeField] private int numberOnWall;
    [SerializeField] private TextMeshPro textOnWall;
    [SerializeField] private ParticleSystem explodeParticleSystem;
    private GameObject numberObstacle;
    private bool isDamaged;

    private void Start()
    {
        numberObstacle = this.gameObject;    
    }

    private void Update()
    {
        textOnWall.text = numberOnWall.ToString();
    }

    public void DecreaseNumberOnWall(int decreaseAmount)
    {   
        numberOnWall -= decreaseAmount;
        if(numberOnWall <= 0)
        {
            Vector3 instantiateParticlePosition = new Vector3(numberObstacle.transform.position.x, numberObstacle.transform.position.y, numberObstacle.transform.position.z);
            Vector3 obstacleExplodeParticleSystemScale = new Vector3(4,4,4); 
            numberObstacle.GetComponent<BoxCollider>().enabled = false;

            InstantiateParticleSystems(explodeParticleSystem, instantiateParticlePosition, obstacleExplodeParticleSystemScale);

            numberObstacle.transform.DOScale(new Vector3(0,0,0), 0.8f).OnComplete(()=>{
                Destroy(numberObstacle);
            });
        }
    }

    public void TakeDamage(int damage)
    {
        DecreaseNumberOnWall(damage);
        ReScaleBehavior();
    }

    private void ReScaleBehavior()
    {
        Vector3 scaleValue = new Vector3(4 ,4 ,4);
        Vector3 nonScaleValue = new Vector3(3, 3, 3);

        numberObstacle.transform.DOScale(scaleValue, 0.1f).OnComplete(()=>{
            numberObstacle.transform.DOScale(nonScaleValue,0.1f);
        });
    }

    public void InstantiateParticleSystems(ParticleSystem particleSystem, Vector3 instantiatePosition, Vector3 instantiateScale)
    {
        ParticleSystem particle =  Instantiate(particleSystem);

        particle.transform.localPosition = instantiatePosition;
        particleSystem.transform.localScale = instantiateScale;

        Destroy(particle.gameObject,2f);
    }

}
