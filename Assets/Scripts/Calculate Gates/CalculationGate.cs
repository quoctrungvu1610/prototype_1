using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculationGate : MonoBehaviour
{
    [SerializeField] public CalculationTypes calculationType;
    [SerializeField] public ObjectToApplyCalculation objectToApplyCalculation;
    [SerializeField] public int numberToCalculate;
    private TextMeshPro calculationText;
    private TextMeshPro topText;
    private GameObject calculationWall;

    private void Awake()
    {

    }

    private void Start()
    {
        calculationWall = this.gameObject;

        topText =  calculationWall.transform.GetChild(0).GetComponent<TextMeshPro>();

        calculationText =  calculationWall.transform.GetChild(1).GetComponent<TextMeshPro>(); 

        calculationText.text = DisplayGalculationText(calculationType) + numberToCalculate.ToString();
        
        topText.text = objectToApplyCalculation.ToString();
    }

    public void HandleCalculation(float bonusNumberOfBulletSpawnPerSec, int numberToBonus)
    {
        switch(calculationType)
        {
            case CalculationTypes.Add:
                bonusNumberOfBulletSpawnPerSec += numberToBonus;
                break;
            case CalculationTypes.Divide:
                bonusNumberOfBulletSpawnPerSec /= numberToBonus;
                break;
            case CalculationTypes.Minus:
                bonusNumberOfBulletSpawnPerSec -= numberToBonus;
                if(BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec <= 1)
                {
                    BulletSpawner.Instance.bonusNumberOfBulletSpawnPerSec = 1;
                } 
                break;
            case CalculationTypes.Multiply:
                bonusNumberOfBulletSpawnPerSec *= numberToBonus;
                break;
        }
    }

    private string DisplayGalculationText(CalculationTypes calculationTypes)
    {
        string calculationText = "";
        switch(calculationType)
        {
            case CalculationTypes.Add:
                calculationText = "+";
                break;
            case CalculationTypes.Divide:
                calculationText = "/";
                break;
            case CalculationTypes.Minus:
                calculationText = "-";
                break;
            case CalculationTypes.Multiply:
                calculationText = "x";
                break;
        }
        return calculationText;
    }
}
