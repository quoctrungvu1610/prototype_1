using UnityEngine;

public class RotateObstacle : Obstacle,IRotateable,IDamageable
{
    [SerializeField] private int obstacleDamage = 10;
    public bool isCollided = false;
    public bool IsCollided => isCollided;
    
    public void HandleDamage(int damage)
    {
        
    }

    public void RotateObstacleObject()
    {
        transform.Rotate(new Vector3(0, 5, 0));
    }

    void Update()
    {
        RotateObstacleObject();
    }
    
    public void ToogleCollideStatus()
    {
        if(isCollided==false)
        {
            isCollided = true;
        }
        else if(isCollided == true)
        {
            isCollided = false;
        }
    }

    public void HandleDamage(int damage, ICanTakeDamage client)
    {
        throw new System.NotImplementedException();
    }
}
