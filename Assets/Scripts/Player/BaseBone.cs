using UnityEngine;

public class BaseBone : MonoBehaviour
{
    private bool isCollided = false;
    public bool IsCollided => isCollided;
    
    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("Obstacle"))
        {
            RotateObstacle obstacle;
            obstacle = other.gameObject.GetComponent<RotateObstacle>();
            if(obstacle.IsCollided == false)
            {
                obstacle.ToogleCollideStatus();
            } 
        }
    }
}


