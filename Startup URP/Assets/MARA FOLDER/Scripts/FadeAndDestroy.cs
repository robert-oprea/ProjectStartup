using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeAndDestroy : MonoBehaviour
{

    public float fadeDelay = 10f;
    public float alphaValue = 0;
    public bool destroyObject = false;
    new TextMeshPro renderer;

    public bool faded = false;

    void Start()
    {
        renderer= GetComponent<TextMeshPro>();
        //StartCoroutine(FadeTo(alphaValue, fadeDelay));
    }


    public IEnumerator FadeTo(float alphaValue, float fadeDelay, GameObject otherObject)
    {
        float alpha = renderer.color.a;

        Color newColor;

        for(float t = 0.0f ; t < 1.0f ; t += Time.deltaTime / fadeDelay)
        {
            newColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, Mathf.Lerp(alpha, alphaValue, t));
            renderer.color = newColor;
            yield return null;
        }
        if(destroyObject)
        {
            //Destroy(otherObject);
            //Destroy(gameObject);
            
            faded = true;

            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);

            renderer = otherObject.GetComponent<TextMeshPro>();

            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);

        }
    }

    public IEnumerator FadeTo(float alphaValue, float fadeDelay)
    {
        float alpha = renderer.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeDelay)
        {
            Color newColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, Mathf.Lerp(alpha, alphaValue, t));
            renderer.color = newColor;
            yield return null;
        }
        if (destroyObject)
        {
            //Destroy(gameObject);

            faded = true;

            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        }
    }

}
