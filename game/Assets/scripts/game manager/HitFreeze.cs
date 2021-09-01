using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFreeze : MonoBehaviour
{
    public void RunCoroutine()
    {
        StartCoroutine(FreezeEffect());
    }

    public IEnumerator FreezeEffect()
    {
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1f;

        yield return null;
    }
}
