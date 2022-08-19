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
    [SerializeField] private GameObject BoomObject;

    private void Awake()
    {
        FirePoint = GameObject.Find("FirePoint").transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 30.0f;
        transform.position = FirePoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            GameObject Obj = Instantiate(BoomObject);
            Obj.transform.position = this.transform.position;

            Destroy(Obj, 0.5f);
            Destroy(this.gameObject);
        }
    }
}
