using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3[] Forward = new Vector3[4];
    private List<string> ObstacleList = new List<string>();

    //--------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------

    private Vector3 Direction = new Vector3();
    [SerializeField] private GameObject NodeList;
    [SerializeField] private Point WayPoint;
    [SerializeField] private GameObject Player;

    private bool TargetColl;

    public float Angle;
    public float fTime;

    private void Awake()
	{
        NodeList = GameObject.Find("PointList");
        Player = GameObject.Find("Tank");
        WayPoint = NodeList.transform.GetChild(0).GetComponent<Point>();
    }

	void Start()
    {
        ObstacleList.Add("Player");
        ObstacleList.Add("Enemy");
        ObstacleList.Add("Wall");

        Forward[0] = new Vector3(-1.0f, 0.0f, 2.0f);
        Forward[1] = new Vector3(-0.5f, 0.0f, 5.0f);
        Forward[2] = new Vector3(0.5f, 0.0f, 5.0f);
        Forward[3] = new Vector3(1.0f, 0.0f, 2.0f);

        //Angle = 0.0f;

        //--------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------

        Direction = (WayPoint.transform.position - transform.position).normalized;
        //Direction = new Vector3(0.0f, 0.0f, 0.0f);

        WayPoint = NodeList.transform.GetChild(0).GetComponent<Point>();

        // �߷� ����
        transform.gameObject.GetComponent<Rigidbody>().useGravity = false;

        // isTrigger = ������ �浹ó���� ������� ����
        transform.GetComponent<BoxCollider>().isTrigger = true;
        transform.GetComponent<SphereCollider>().isTrigger = true;

        // ��ǥ���� �������� �����Ǿ����� Ȯ��
        TargetColl = false;

        Angle = 0.0f;
    }

    void Update()
    {
        /*
        RaycastHit hit;

        Vector3 dir = (Player.transform.position - transform.position).normalized;

        if (!Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
                TargetColl = hit.distance < 5.0f ? true : false;
        */

        TargetColl = Vector3.Distance(transform.position, Player.transform.position) < 5.0f ? true : false;

        if (TargetColl)
		{
            // ��ǥ���� �������� �ִٸ� �������� ����
        }
        else
            Move();
    }

    private void Move()
    {
        // ���� ���͸� ����
        Direction = (WayPoint.transform.position - transform.position).normalized; // normalized�� ���� ���ͷ� ������ִ� �Լ��̴�.

        // �������� 5.0�� �ӵ���ŭ ������
        transform.position += Direction * 5.0f * Time.deltaTime;

        // Ÿ���� �ٶ�
        transform.LookAt(transform.position + Direction);
    }

	private void OnTriggerEnter(Collider other)
	{
        // �浹�� �� ��ü�� ���� Ÿ���� �´��� Ȯ��
        if (string.Equals(other.name, WayPoint.transform.name))
            WayPoint = WayPoint.Node;
	}

    IEnumerator LerpRotation() // yield return �� �������� ��
	{
        float fTime = 0f;

        while (fTime < 1.5f)
		{
            fTime += Time.deltaTime;
            float fAngle = Mathf.Lerp(transform.eulerAngles.y, Angle, fTime) % 360.0f;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, fAngle, transform.eulerAngles.z);
            Debug.Log(fAngle);

            // yield return new WaitForSeconds(1.0f); 1.0f���� ��ٸ��� ��(Sleep �Լ��� �����ϴ�, ������ Update() �Լ��� ��� ���ư�)
            yield return null; // yield return null �� yield return Time.deltaTime �� �����ϴ�
        }
    }
}
