using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeAndDestroy : MonoBehaviour
{

    public float fadeDelay = 10f;
    public float alphaValue = 0;
    public bool destroyObject = true;
    new TextMeshPro renderer;

    void Start()
    {
        renderer= GetComponent<TextMeshPro>();
        //StartCoroutine(FadeTo(alphaValue, fadeDelay));
    }


    public IEnumerator FadeTo(float alphaValue, float fadeDelay, GameObject otherObject)
    {
        float alpha = renderer.color.a;

        for(float t = 0.0f ; t < 1.0f ; t += Time.deltaTime / fadeDelay)
        {
            Color newColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, Mathf.Lerp(alpha, alphaValue, t));
            renderer.color = newColor;
            yield return null;
        }
        if(destroyObject)
        {
            Destroy(gameObject);
            Destroy(otherObject);
        }
    }

}
