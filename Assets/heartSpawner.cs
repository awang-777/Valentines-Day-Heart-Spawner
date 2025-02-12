//Amanda W.
// happy valentines day


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartSpawner : MonoBehaviour
{
    private GameObject[] heart;
    private GameObject container;
    private float radius = 5f;
    private float currentTime = 0f;
    private bool isAnimating = true;

    private Color[] colors = 
    {
        new Color(0.835f, 0.408f, 0.557f),  // D466BE
        new Color(1f, 0.792f, 0.745f),      // FFCABE
        new Color(0.996f, 0.957f, 0.918f),  // FEF4EA
        new Color(1f, 0.722f, 0.816f),      // FFBBD0
        new Color(0.980f, 0.859f, 0.737f),  // FAECBC
        new Color(0.804f, 0.804f, 0.992f)   // CECCFD
    };

    void Start()
    {
        container = new GameObject("HeartContainer");
        heart = new GameObject[300];
        
        for (int i = 0; i < heart.Length; i++)
        {
            heart[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            heart[i].transform.parent = container.transform;
        }
    }

    void Update()
    {
        // this cycles the colors 
        int currentIndex = Mathf.FloorToInt(Time.time * 2) % colors.Length;
        int nextIndex = (currentIndex + 1) % colors.Length;
        Color currentColor = Color.Lerp(colors[currentIndex], colors[nextIndex], (Time.time * 2) % 1);
        
        foreach(GameObject h in heart)
            h.GetComponent<Renderer>().material.color = currentColor;

       
        if (isAnimating) //initial formation animation
        {
            currentTime += Time.deltaTime;
            UpdatePositions(radius * Mathf.SmoothStep(0, 1, currentTime / 2f));
            if (currentTime >= 2f) isAnimating = false;
        }
        else //my tilting animation :p (idk why its not smooth)
        {
            UpdatePositions(radius * (1 + 0.5f * Mathf.Sin(Time.time * 2)));
            container.transform.rotation = Quaternion.Euler(0, 0, 15 * Mathf.Sin(Time.time * 3));
        }
    }

    void UpdatePositions(float currentRadius)
    {
        for (int i = 0; i < heart.Length; i++)
        {
            float t = (float)i / heart.Length * 2f * Mathf.PI;

            //heart shape equation: 
            float x = Mathf.Sin(t) * Mathf.Sin(t) * Mathf.Sin(t) * currentRadius;  // x position
            float y = (13f * Mathf.Cos(t) - 5f * Mathf.Cos(2f * t) - 2f * Mathf.Cos(3f * t) - Mathf.Cos(4f * t)) / 16f * currentRadius;  // y position
        
            heart[i].transform.localPosition = new Vector3(x, y, 0f); //sets the spheres position
        }
    }
}
