using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{
<<<<<<< HEAD
    public float waveHeight, waveSpeed, wavelength;
=======
    public float heightMultiplier, wavelength, waveSpeed;
>>>>>>> 2b314476d60924ebfe7c6c341e8885698bfe85b4

    void Update()
    {
        MeshFilter mF = GetComponent<MeshFilter>();
        MeshCollider mC = GetComponent<MeshCollider>();

        mC.sharedMesh = mF.mesh;

        Vector3[] verts = mF.mesh.vertices;

        for (int i = 0; i < verts.Length; i++)
        {
<<<<<<< HEAD
            verts[i].y = Mathf.Sin((Time.time * waveSpeed) + (i * wavelength)) * waveHeight;
=======
            verts[i].y = Mathf.Sin((Time.time * waveSpeed) + (i * wavelength)) * heightMultiplier;
>>>>>>> 2b314476d60924ebfe7c6c341e8885698bfe85b4
        }

        mF.mesh.vertices = verts;
        mF.mesh.RecalculateNormals();
        mF.mesh.RecalculateBounds();
    }
}