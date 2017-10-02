using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{
    public float waveHeight, waveSpeed, wavelength;

    void Update()
    {
        MeshFilter mF = GetComponent<MeshFilter>();
        MeshCollider mC = GetComponent<MeshCollider>();

        mC.sharedMesh = mF.mesh;

        Vector3[] verts = mF.mesh.vertices;

        for (int i = 0; i < verts.Length; i++)
        {
            verts[i].y = Mathf.Sin((Time.time * waveSpeed) + (i * wavelength)) * waveHeight;
        }

        mF.mesh.vertices = verts;
        mF.mesh.RecalculateNormals();
        mF.mesh.RecalculateBounds();
    }
}