using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{

    public float noiseWavelength;
    public float noiseWaveSpeed;
    public float noiseStrength;


    void Update()
    {
        MeshFilter mF = GetComponent<MeshFilter>();
        MeshCollider mC = GetComponent<MeshCollider>();

        mC.sharedMesh = mF.mesh;

        Vector3[] verts = mF.mesh.vertices;

        for (int i = 0; i < verts.Length; i++)
        {

                float pX = (verts[i].x * noiseWavelength) + (Time.time * noiseWaveSpeed);
                float pZ = (verts[i].z * noiseWavelength) + (Time.time * noiseWaveSpeed);
                verts[i].y = Mathf.PerlinNoise(pX, pZ) * noiseStrength;

        }

        mF.mesh.vertices = verts;
        mF.mesh.RecalculateNormals();
        mF.mesh.RecalculateBounds();
    }
}