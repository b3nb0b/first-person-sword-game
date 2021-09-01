using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitDet : MonoBehaviour
{
    public float health = 2f;
    public  float hitCooldown = 0f;
    public GameObject gameManager;
    public static HitFreeze Instance;
    public ParticleSystem blood;

    void Awake()
    {
        Instance = gameManager.GetComponent<HitFreeze>();
        blood.Stop();
    }

    void Update()
    {
        hitCooldown = hitCooldown + Time.deltaTime;

        if (health <= 0f)
        {
            blood.transform.SetParent(null, true);
            // Instance.Invoke("RunCoroutine", 0f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitCooldown >= 0.2f)
        {
            hitCooldown = 0f;
            --health;
            blood.Play();
        }
    }
}
