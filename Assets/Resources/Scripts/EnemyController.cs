using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna
{
    public float Angle;
    public Vector3 Direction;
    public bool Check;
    public Color _Color;
}

public class EnemyController : MonoBehaviour
{
    private List<Antenna> AntennasList = new List<Antenna>();

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
        float fAngle = -45.0f;

        for (int i = 0; i < 5; ++i)
        {
            Antenna ant = new Antenna();

            ant.Angle = fAngle;
            Debug.Log(fAngle);

            ant.Direction = new Vector3(
                transform.eulerAngles.x + Mathf.Sin(ant.Angle * Mathf.Deg2Rad),
                0.0f,
                transform.eulerAngles.z + Mathf.Cos(ant.Angle * Mathf.Deg2Rad));

            ant.Check = false;
            ant._Color = Color.green;

            AntennasList.Add(ant);
            fAngle += 22.5f;
        }

        Direction = new Vector3(0.0f, 0.0f, 0.0f);

        WayPoint = NodeList.transform.GetChild(0).GetComponent<Point>();

        // ** �߷� ����
        transform.gameObject.GetComponent<Rigidbody>().useGravity = false;

        // ** isTrigger = ������ �浹ó���� ������� ����
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        transform.GetComponent<SphereCollider>().isTrigger = true;

        // ** ��ǥ���� �������� �����Ǿ����� Ȯ��
        TargetColl = false;

        Angle = 0.0f;
    }

	private void FixedUpdate()
	{
        float fAngle = Angle - 45.0f;

        for (int i = 0; i < AntennasList.Count; ++i)
		{
            AntennasList[i].Angle = fAngle;
            fAngle += 22.5f;

            AntennasList[i].Direction = new Vector3(
                transform.eulerAngles.x + Mathf.Sin(AntennasList[i].Angle * Mathf.Deg2Rad),
                0.0f,
                transform.eulerAngles.z + Mathf.Cos(AntennasList[i].Angle * Mathf.Deg2Rad));
        }
    }

	void Update()
    {
        TargetColl = Vector3.Distance(transform.position, Player.transform.position) < 5.0f ? true : false;

        // ** ��ǥ���� �������� �ִٸ� �������� ����
        if (TargetColl)
        {
            RaycastHit hit;

            for (int i = 0; i < AntennasList.Count; ++i)
            {
                if (Physics.Raycast(transform.position, AntennasList[i].Direction, out hit, 5))
                {
                    if (hit.transform.tag == "Player")
                    {
                        AntennasList[i].Check = true;
                        AntennasList[i]._Color = Color.red;

                        switch (i)
                        {
                            case 0:
                                Angle += 1.0f;
                                break;

                            case 1:
                                Angle += 0.5f;
                                break;

                            case 2:
                                Angle -= 0.5f;
                                break;

                            case 3:
                                Angle -= 1.0f;
                                break;
                        }
                    }
                    else
                    {
                        AntennasList[i].Check = false;
                        AntennasList[i]._Color = Color.green;
                    }
                }
                Debug.DrawLine(transform.position, transform.position + (AntennasList[i].Direction * 5), AntennasList[i]._Color);
                StartCoroutine(LerpRotation());
            }
        }
        else
            Move();
    }

    private void Move()
    {
        // ** ���� ���͸� ����
        Direction = (WayPoint.transform.position - transform.position).normalized; // normalized�� ���� ���ͷ� ������ִ� �Լ��̴�.

        // ** �������� 5.0�� �ӵ���ŭ ������
        transform.position += Direction * 5.0f * Time.deltaTime;
    }

	private void OnTriggerEnter(Collider other)
	{
        // ** �浹�� �� ��ü�� ���� Ÿ���� �´��� Ȯ��
        if (string.Equals(other.name, WayPoint.transform.name))
            WayPoint = WayPoint.Node;
	}

    IEnumerator LerpRotation() // yield return �� �������� ��
	{
        float fTime = 0f;

        while (fTime <= 1.0f)
		{
            fTime += Time.deltaTime;
            float fAngle = Mathf.Lerp(transform.eulerAngles.y, Angle, fTime) % 360.0f;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, fAngle, transform.eulerAngles.z);

            // yield return new WaitForSeconds(1.0f); 1.0f���� ��ٸ��� ��(Sleep �Լ��� �����ϴ�, ������ Update() �Լ��� ��� ���ư�)
            yield return null; // yield return null �� yield return Time.deltaTime �� �����ϴ�
        }
    }
}
