using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{

    public float noiseWavelength;
    public float noiseWaveSpeed;
    public float noiseStrength;
    public float gameTime;
    public float offsetY;

    public float waterDrag;

    public GameObject[] floaters;

    public Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Custom/GPUWater");

        floaters = GameObject.FindGameObjectsWithTag("Floatable");


    }

    void FixedUpdate()
    {
        noiseStrength = rend.material.GetFloat("_noiseStrength");
        noiseWavelength = rend.material.GetFloat("_noiseWavelength");
        noiseWaveSpeed = rend.material.GetFloat("_noiseSpeed");
        rend.material.SetFloat("_GameTime", Time.time);

        foreach (GameObject floater in floaters)
        {
            float wavePosY = Mathf.Sin((floater.transform.position.x * noiseWavelength) + (Time.time * noiseWaveSpeed)) * noiseStrength + this.transform.position.y + offsetY;

            if (floater.transform.position.y < wavePosY)
            {
                floater.GetComponent<Rigidbody>().useGravity = false;
                floater.GetComponent<Rigidbody>().AddForce(Vector3.up * floater.GetComponent<FloatingObject>().boyancy);
                floater.GetComponent<Rigidbody>().velocity *= 1 - waterDrag * Time.deltaTime;
            }
            else
            {
                floater.GetComponent<Rigidbody>().useGravity = true;
            }
        }



    }
}