using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowCMCamController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CMFollowVCam;
    [SerializeField] private float cameraMaxZoomOutDistance = 77;
    [SerializeField] private float cameraMaxZoomInDistance = 33;
    private PlayerStackMechanic playerStackMechanic;
    private int keyCheck = 0;

    private void Start()
    {
        playerStackMechanic = PlayerStackMechanic.Instance;
        keyCheck = playerStackMechanic.NumberOfItemHolding;
    }

    private void Update()
    {
        StartCoroutine(UpdateCMFollowVCamPosition());
    }
    
    private void ChangeKeyCheck()
    {
        if(playerStackMechanic.IsLoadingAnimation == false)
        {
            keyCheck = playerStackMechanic.NumberOfItemHolding;
        }
    }

    private IEnumerator UpdateCMFollowVCamPosition()
    {  
        
        if(keyCheck != playerStackMechanic.NumberOfItemHolding)
        {
            if(keyCheck < playerStackMechanic.NumberOfItemHolding)
            {
                for(int i = 0; i < 10; i++)
                {
                    CMFollowVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance += 0.02f;
                    yield return new WaitForSeconds(0.05f);
                    if(CMFollowVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance >= cameraMaxZoomOutDistance)
                    {
                       CMFollowVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = cameraMaxZoomOutDistance;
                    }
                }
            }
            else if(keyCheck > playerStackMechanic.NumberOfItemHolding)
            {
                for(int i = 0; i < 5; i++)
                {
                    CMFollowVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance -= 0.01f;
                    yield return new WaitForSeconds(0.05f);
                    if(CMFollowVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance <= cameraMaxZoomInDistance)
                    {
                       CMFollowVCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = cameraMaxZoomInDistance;
                    }
                }
            }
            ChangeKeyCheck();
        }
        yield return null;
    }
}
