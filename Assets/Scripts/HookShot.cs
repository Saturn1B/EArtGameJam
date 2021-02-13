using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    const float NORMAL_FOV = 60f;
    const float HOOKSHOT_FOV = 100f;

    public GameObject PlayerCamera;
    public Movements movements;

    public Transform HookShotTransform;
    public CameraFOV cameraFOV;
    public ParticleSystem particleSystem;

    State state;
    enum State {Normal,  HookThrown, Flying}

    public CharacterController characterController;

    Vector3 HookShotPos;
    public float speedMultiplier;
    public float speedMin;
    public float speedMax;

    float hookShotSize;

    GameObject Target;

    public AudioSource AudioHook, Wall;

    public int maxRange;

    private void Awake()
    {
        HookShotTransform.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                movements.canMove = true;
                HookShotStart();
                break;
            case State.HookThrown:
                HandleThrow();
                break;
            case State.Flying:
                movements.canMove = false;
                HookShotMovement();
                break;
        }

    }

    void HookShotStart()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out RaycastHit hit))
            {
                //hit.point
                HookShotPos = hit.point;
                if(Vector3.Distance(transform.position, HookShotPos) < maxRange)
                {
                    Target = hit.collider.gameObject;
                    hookShotSize = 0f;
                    HookShotTransform.gameObject.SetActive(true);
                    HookShotTransform.localScale = Vector3.zero;
                    state = State.HookThrown;
                    //AudioHook.Play();
                }
            }
        }
    }

    void HandleThrow()
    {
        Vector3 target = Vector3.zero;
        if ( Vector3.Distance(new Vector3(0, HookShotPos.y, 0), new Vector3(0, HookShotTransform.position.y, 0)) > 0)
        {
            if(HookShotPos.y > HookShotTransform.position.y)
            {
                //Debug.Log("trop grand");
                target = HookShotPos;
                target.y = HookShotPos.y - Vector3.Distance(new Vector3(0, HookShotPos.y, 0), new Vector3(0, HookShotTransform.position.y, 0)) / 3;
            }
            else if(HookShotPos.y < HookShotTransform.position.y)
            {
                //Debug.Log("trop petit");
                target = HookShotPos;
                target.y = HookShotPos.y + Vector3.Distance(new Vector3(0, HookShotPos.y, 0), new Vector3(0, HookShotTransform.position.y, 0)) / 3;
            }
        }
        else
        {
            target = HookShotPos;
        }
        HookShotTransform.LookAt(target);
        float throwSpeed = 200f;
        hookShotSize += throwSpeed * Time.deltaTime;
        HookShotTransform.localScale = new Vector3(1, 1, hookShotSize);

        if(hookShotSize >= Vector3.Distance(transform.position, HookShotPos))
        {
            //AudioHook.Stop();
            //AudioHook.Play();
            state = State.Flying;
            cameraFOV.SetCameraFOV(HOOKSHOT_FOV);
            particleSystem.Play();
        }

    }
    
    void HookShotMovement()
    {
        Vector3 target = Vector3.zero;
        if (Vector3.Distance(new Vector3(0, HookShotPos.y, 0), new Vector3(0, HookShotTransform.position.y, 0)) > 0)
        {
            if (HookShotPos.y > HookShotTransform.position.y)
            {
                //Debug.Log("trop grand");
                target = HookShotPos;
                target.y = HookShotPos.y - Vector3.Distance(new Vector3(0, HookShotPos.y, 0), new Vector3(0, HookShotTransform.position.y, 0)) / 3;
            }
            else if (HookShotPos.y < HookShotTransform.position.y)
            {
                //Debug.Log("trop petit");
                target = HookShotPos;
                target.y = HookShotPos.y + Vector3.Distance(new Vector3(0, HookShotPos.y, 0), new Vector3(0, HookShotTransform.position.y, 0)) / 3;
            }
        }
        else
        {
            target = HookShotPos;
        }
        HookShotTransform.LookAt(target);

        Vector3 dir = (HookShotPos - transform.position);

        float hookSpeed = Mathf.Clamp(1/Vector3.Distance(transform.position, HookShotPos), speedMin, speedMax);

        characterController.Move(dir * hookSpeed * speedMultiplier * Time.deltaTime);
        Debug.Log("move");

        if(Target != null && Target.tag == "Ground" && Vector3.Distance(transform.position, HookShotPos) < 10)
        {
            state = State.Normal;
            movements.velocityY = 0f;
            HookShotTransform.gameObject.SetActive(false);
            cameraFOV.SetCameraFOV(NORMAL_FOV);
            particleSystem.Stop();
            Target = null;
            //AudioHook.Stop();
        }
        else if (Vector3.Distance(transform.position, HookShotPos) < 1.5)
        {
            state = State.Normal;
            movements.velocityY = 0f;
            HookShotTransform.gameObject.SetActive(false);
            cameraFOV.SetCameraFOV(NORMAL_FOV);
            particleSystem.Stop();
            if(Target.tag == "Breakable" && movements.linearVelocity.magnitude > 1)
            {
                Wall.Play();
                Destroy(Target);
            }
            Target = null;
            //AudioHook.Stop();
        }

        if (Input.GetMouseButtonDown(1))
        {
            state = State.Normal;
            movements.velocityY = 0f;
            HookShotTransform.gameObject.SetActive(false);
            cameraFOV.SetCameraFOV(NORMAL_FOV);
            particleSystem.Stop();
            Target = null;
            //AudioHook.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float momentumExtra = 0.75f;
            movements.momentum = dir * hookSpeed * momentumExtra;
            float jumpSpeed = 20f;
            movements.momentum += Vector3.up * jumpSpeed;
            state = State.Normal;
            movements.velocityY = 0f;
            HookShotTransform.gameObject.SetActive(false);
            cameraFOV.SetCameraFOV(NORMAL_FOV);
            particleSystem.Stop();
            Target = null;
            //AudioHook.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        state = State.Normal;
        movements.velocityY = 0f;
        HookShotTransform.gameObject.SetActive(false);
        cameraFOV.SetCameraFOV(NORMAL_FOV);
        particleSystem.Stop();
        if (Target.tag == "Breakable" && movements.linearVelocity.magnitude > 1)
        {
            Wall.Play();
            Destroy(Target);
        }
        Target = null;
        //AudioHook.Stop();
    }
}
