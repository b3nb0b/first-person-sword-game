using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHand : MonoBehaviour
{
    public Transform self;

    public GameObject myBone;

    void Update()
    {
        self.transform.parent = myBone.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
    }
}
