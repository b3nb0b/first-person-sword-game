using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;

    public GameObject sword;

    Collider swordCollider;

    public TrailRenderer swordTrail;

    public float cooldown = 0f;

    bool hasSwung = false;

    public Transform origin;

    public hitDet HitDet;

    public GameObject camera;

    public Rigidbody rb;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        swordCollider = sword.GetComponent<Collider>();

        swordCollider.enabled = false;
        swordTrail.enabled = false;
    }

    void Update()
    {
        Debug.Log(hasSwung);

        cooldown += Time.deltaTime;

        cooldown = Mathf.Clamp(cooldown, 0f, 0.7f);

        if (Input.GetMouseButtonDown(0) && cooldown >= 0.5f)
        {
            StartCoroutine(HitDetection());

            if (hasSwung == false)
            {
                anim.Play("sword_swing1");
                hasSwung = true;
            }

            else if (hasSwung == true)
            {
                anim.Play("sword_swing2");
                hasSwung = false;
            }

            cooldown = 0f;
        }

        if (cooldown == 0.7f)
        {
            hasSwung = false;
        }
    }

    private IEnumerator HitDetection()
    {
        yield return new WaitForSeconds(0.4f);

        swordCollider.enabled = true;
        swordTrail.enabled = true;
        rb.AddForce(camera.transform.forward * 10f, ForceMode.VelocityChange);

        yield return new WaitForSeconds(0.1f);

        swordCollider.enabled = false;
        swordTrail.enabled = false;

        yield return null;
    }
}
