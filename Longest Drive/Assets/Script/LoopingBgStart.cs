using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBgStart : MonoBehaviour
{

    public float backgroundspeed;
    public Renderer backgroundRenderer;

    void Update()
    {
        backgroundRenderer.material.mainTextureOffset -= new Vector2(backgroundspeed * Time.deltaTime, 0f);
    }
}