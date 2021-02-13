using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    float downLayer, upLayer;

     enum POSITION { DOWN, UP };
     POSITION position;

    public bool canPress = true;
    public bool isActivated;

    GameObject ElevatorObject;

    private void Awake()
    {
        ElevatorObject = transform.parent.gameObject;
    }

    public void UseElevator()
    {
        switch (position)
        {
            case POSITION.DOWN:
                StartCoroutine(GoUp());
                position = POSITION.UP;
                break;
            case POSITION.UP:
                StartCoroutine(GoDown());
                position = POSITION.DOWN;
                break;
            default:
                break;
        }
    }

    IEnumerator GoUp()
    {
        canPress = false;
        while (ElevatorObject.transform.position.y < upLayer)
        {
            ElevatorObject.transform.position += new Vector3(0, 0.01f, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ElevatorObject.transform.position = new Vector3(ElevatorObject.transform.position.x, upLayer, ElevatorObject.transform.position.z);
        canPress = true;
    }

    IEnumerator GoDown()
    {
        canPress = false;
        while (ElevatorObject.transform.position.y > downLayer)
        {
            ElevatorObject.transform.position -= new Vector3(0, 0.01f, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ElevatorObject.transform.position = new Vector3(ElevatorObject.transform.position.x, downLayer, ElevatorObject.transform.position.z);
        canPress = true;
    }
}
