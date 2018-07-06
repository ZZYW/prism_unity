using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformer : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] normals;

    [Range(0.0001f, 0.009f)]
    public float speed = 0.001f;

    //public float speed

    float a = 0;
    float b = 0;
    float c = 0;
    float d = 0;
    float e = 0;
    float f = 0;

    //public Vector2[] newUV;
    //public int[] newTriangles;

    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        normals = mesh.normals;
        //mesh.uv = newUV;
        //mesh.triangles = newTriangles;
        b += Mathf.PerlinNoise(Time.time * speed, 0) * 2 - 2.5f;
    }

    // Update is called once per frame
    private void Update()
    {
        a += Mathf.PerlinNoise(Time.time * speed, 0) * 5 - 2.5f;
        c += Mathf.PerlinNoise(Time.time * speed, 0) * 2 - 1;
        d = -a;
        e = -b;
        f = -c;


        int i = 0;
        while (i < vertices.Length)
        {

            var vertex = normals[i];
            //vertices[i] = normals[i] * Mathf.Sin(Mathf.PerlinNoise(Time.time, 0.0f));

            float x = normals[i].x + Mathf.PerlinNoise(Time.time * speed, 0) * 5f - 2.5f;
            float y = normals[i].y + Mathf.PerlinNoise(Time.time * speed, 0) * 5f - 2.5f;
            float z = normals[i].z + Mathf.PerlinNoise(Time.time * speed, 0) * 5f - 2.5f;

             
            //print((z * Mathf.Sin(a * x) - Mathf.Cos(b * y)));
            vertex.x += (z * Mathf.Sin(a * x) - Mathf.Cos(b * y)) - 1f;
            vertex.y += (x * Mathf.Sin(c * y) - Mathf.Cos(d * z)) - 1f;
            vertex.z += (y * Mathf.Sin(e * z) - Mathf.Cos(f * x)) - 1f;
            vertices[i] = vertex;

            i++;
        }
        mesh.vertices = vertices;
    }
} 