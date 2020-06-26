using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public Transform target;
    public float speed;
    public float runningSpeed;
    float currentSpeed;
    public Transform targetOrigin;
    bool isRunning = false;
    Camera cam;
    Joystick joystick;

    Animator anim;
    Rigidbody rb;
    

    private void Start()
    {
        currentSpeed = speed;
        target.position = transform.position;
        joystick = Joystick.singleton;
        cam = Camera.main;
        StartCoroutine("Reset");

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator Reset()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            target.position = targetOrigin.position;
        }
    }

    private void Update()
    {
        float relativeX = target.position.x - transform.position.x;
        float relativeZ = target.position.z - transform.position.z;

        Quaternion rotation = Quaternion.LookRotation(new Vector3(relativeX, 0, relativeZ));
        transform.rotation = rotation;

        if ((joystick.horizontal > 0.2f | joystick.horizontal < -0.2f) | (joystick.vertical > 0.2f | joystick.vertical < -0.2f))
        {
            transform.Translate((Vector3.forward * new Vector2(joystick.horizontal, joystick.vertical).magnitude * Time.deltaTime * currentSpeed));
        }

        target.transform.rotation = cam.transform.rotation;
        target.transform.eulerAngles = new Vector3(0, target.transform.eulerAngles.y, 0);

        if (joystick.vertical > 0.2f | joystick.vertical < -0.2f)
        {
            target.Translate(Vector3.forward * joystick.vertical * Time.deltaTime * currentSpeed * 4);
        }


        if (joystick.horizontal > 0.2f | joystick.horizontal < -0.2f)
        {
            target.Translate(Vector3.right * joystick.horizontal * Time.deltaTime * currentSpeed * 4);
        }

        Vector2 vel = new Vector2(joystick.horizontal, joystick.vertical);
        anim.SetFloat("speed", vel.magnitude);
    }
}
