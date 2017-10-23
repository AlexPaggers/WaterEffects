using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{

    public float noiseWavelength;
    public float noiseWaveSpeed;
    public float noiseStrength;
    
    public Renderer rend;

    public GameObject[] floaters;

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

        foreach (GameObject floater in floaters)
        {
            if(floater.transform.position.y - this.transform.position.y < 
                Mathf.Sin((floater.transform.position.x * noiseWavelength) + (Time.time * noiseWaveSpeed)) * noiseStrength )
            {
                floater.GetComponent<Rigidbody>().AddForce(Vector3.up);
                floater.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                floater.GetComponent<Rigidbody>().AddForce(Vector3.down);
                floater.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        

    }
}