using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour, IBreakable
{
    private Rigidbody sideWallRb;
    private GameObject sideWall;
    private Vector3 currentPosition;
    private Vector3 firstPosition;

    private void Start() 
    {
        sideWall = this.gameObject;
        firstPosition = sideWall.transform.position;
        sideWallRb = sideWall.GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        currentPosition = sideWall.transform.position;
        CheckPositionChanges();
    }

    private void MoveAwayFromPlayer(Vector3 moveDirection)
    {
        sideWallRb.AddForce(moveDirection);
    }

    private void UseWallGravity()
    {
        sideWallRb.useGravity = true;
    }
    
    private void CheckPositionChanges()
    {
        if(currentPosition != firstPosition){
            sideWall.GetComponent<Renderer>().material.color = Color.red;
            UseWallGravity();
            Destroy(sideWall,1f);
        }
    }

    public void BreakBehaviors(Vector3 awayFromPlayerDirection)
    {
        MoveAwayFromPlayer(awayFromPlayerDirection);
    }
}
