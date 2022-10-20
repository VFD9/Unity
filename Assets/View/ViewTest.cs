using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTest : MonoBehaviour
{
    private Mesh mesh;
    [SerializeField] private MeshFilter meshFilter;

    void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;
    }

    void Update()
    {
        mesh.Clear();

        Vector3[] vertices = new Vector3[3];

        vertices[0] = Vector3.zero; // ���� ��ǥ�� ������� ���� ��ǥ�� �߽��� ��.
        vertices[1] = new Vector3(-10.0f, 0.0f, 10.0f);
        vertices[2] = new Vector3(10.0f, 0.0f, 10.0f);

        int[] triangles = new int[3];

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
