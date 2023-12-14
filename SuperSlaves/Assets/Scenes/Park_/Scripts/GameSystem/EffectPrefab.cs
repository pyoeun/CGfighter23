using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPrefab : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponentInChildren<ParticleSystemRenderer>().sortingOrder = 20;

        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        var particleSystem = GetComponentInChildren<ParticleSystem>();
        while (particleSystem.isPlaying)
        {
            yield return null;
        }
        Destroy(this.gameObject);
        yield break;
    }
}
