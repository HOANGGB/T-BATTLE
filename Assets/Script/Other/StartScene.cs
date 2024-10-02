using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(AddJoystick());
       StartCoroutine(AddCamera());

         AddJoystick();
         AddCamera();

    }
    IEnumerator AddJoystick(){
        yield return new WaitForSeconds(2);

        Joystick moveJoystick = GameObject.Find("Joystick Move").GetComponent<Joystick>();
        Joystick fireJoystick = GameObject.Find("Joystick Fire").GetComponent<Joystick>();
        if(!moveJoystick || !fireJoystick) Debug.Log("fdaddddddddddddddddddddddddddddddddddddddddd");

        TankPlayer.instance.joystickMove = moveJoystick;
        TankPlayer.instance.joystickFire = fireJoystick;
    }

    IEnumerator AddCamera()
    {
        yield return new WaitForSeconds(2);

        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.LookAt = TankPlayer.instance.gameObject.transform;
        virtualCamera.Follow = TankPlayer.instance.gameObject.transform;
        virtualCamera.PreviousStateIsValid = false; 

    }
    
}
