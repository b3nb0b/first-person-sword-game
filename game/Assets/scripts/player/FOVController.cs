using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    public Camera myCamera;
    float rollCooldown = 1f;
    public PlayerMove cc;


    void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        rollCooldown += Time.deltaTime;

        if (Input.GetKeyDown("q") && cc.grounded && rollCooldown >= 0.7f && cc.rb.velocity.magnitude >= 0.1f)
         {
            StartCoroutine(CameraRoll());

            rollCooldown = 0f;
         }
    }

    private IEnumerator CameraRoll()
    {
        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, 100f, 1f);

        yield return new WaitForSeconds(0.4f);

        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, 90f, 0.5f);

        yield return null;
    }
}
