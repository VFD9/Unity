using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GetAxisRaw�� 1, 0, -1�� �޴� �Լ��̴�.
// GetAxis�� 0 ~ 1, -1 ~ 0�� �Ҽ������� �޴� �Լ��̴�.
// deltaTime�� �����Ӱ� �����ӻ����� �����̴�. 

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float Rotate;
    
    public GameObject BulletObject;
    [SerializeField] private Animator Anim;

    private void Awake()
    {
        Anim = transform.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 5;
        Rotate = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Ű �Է��� �޾ƿ´�.
        float fHor = Input.GetAxisRaw("Horizontal"); // ��.�� Ű
        float fVer = Input.GetAxisRaw("Vertical"); // ��.�Ʒ� Ű

        // fHor * Time.deltaTime * PlayerSpeed

        transform.Rotate(
            0.0f,
            fHor * Time.deltaTime * Rotate,
            0.0f);

        transform.Translate(
          0.0f,
          0.0f,
          fVer * Time.deltaTime * Speed);
        
        Anim.SetFloat("Speed", fVer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �ҷ��߻�
            Anim.SetBool("BoolFire", true);
            GameObject Obj = Instantiate(BulletObject);
            Rigidbody Rigid = Obj.transform.GetComponent<Rigidbody>();

            Obj.transform.LookAt(transform.forward);
            Rigid.AddForce(transform.forward * 1500.0f);
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            Anim.SetBool("BoolFire", false);
        }
    }
}
