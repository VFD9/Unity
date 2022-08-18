using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// void Update()�� DelatTime�� ���� �ð��� ������ �� �ִ�.
// void FixedUpdate()�� ��������� ������ �Լ��̴�.
// void LateUpdate()�� Update() ���Ŀ� ����Ǵ� �Լ��̴�.

public class BulletController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Vector3 FirePoint;

    private void Awake()
    {
        FirePoint = GameObject.Find("FirePoint").transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 25.0f;
        transform.position = FirePoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            0.0f, 0.0f, Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Cube")
            Destroy(this.gameObject);
    }
}
