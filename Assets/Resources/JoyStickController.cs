using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Move Target")]
    [Tooltip("type : GameObject   ���̽�ƽ�� ����Ͽ� ������ ����� ����.")]
    [SerializeField] private GameObject Target;

    [Header("Joy Stick Controller")]
    [Tooltip("type : RectTransform    ������ ������ ��ư")]
    [SerializeField] private RectTransform Stick;
    [Tooltip("type : RectTransform    JoyStick Out Line")]
    [SerializeField] private RectTransform BackBoard;

    // ** Target�� ������ ����
    private Vector2 Direction;

    // ** Target�� ������ ��
    private Vector3 Movement;

    // ** ������
    private float Radius;

    // ** �̵� �ӵ�
    private float Speed;

    // ** ��ġ �Է� ����
    private bool TouchCheck;

    public void OnDrag(PointerEventData eventData)
    {
        // ** �巡�װ� ���۵Ǹ� ��ġ �Է��� Ȱ��ȭ,
        TouchCheck = true;

        // ** ������ ���.
        OnTouch(eventData.position);
        //Debug.Log(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // ** �Է��� ���۵Ǹ� ��ġ �Է� Ȱ��ȭ.
        TouchCheck = true;

        BackBoard.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ** ��ġ �Է��� ����Ǹ� ��Ȱ��ȭ�� ����
        TouchCheck = false;

        // ** Stick�� ����ġ ��Ŵ
        Stick.localPosition = Vector2.zero;
    }

    private void Awake()
    {
        Target = GameObject.Find("Player");
        Stick = GameObject.Find("FilledCircle").GetComponent<RectTransform>();
        BackBoard = GameObject.Find("OutLineCircle").GetComponent<RectTransform>();
    }

    void Start()
    {
        // ** Out Line �� �������� ����
        Radius = BackBoard.rect.width * 0.5f;

        // ** ���̸� �������� ���ݸ�ŭ �� ��� ����ش�.
        // ** ���� : Stick�� Out Line �� ��¦ �Ѿ �� �ְ� �ϱ� ����.
        Radius += Radius * 0.5f;

        // ** ��ũ���� ��ġ�� �Ǿ����� Ȯ��.
        TouchCheck = false;

        // ** ������ ���� ���·� �ʱ�ȭ
        Direction = new Vector2(0.0f, 0.0f);

        // ** �̵� �ӵ� ����
        Speed = 5.0f;

        // ** �̵����� ���� ���·� �ʱ�ȭ
        Movement = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (TouchCheck)
            Target.transform.position += Movement;
    }

    private void OnTouch(Vector2 _eventData)
    {
        //Debug.Log("OnTouch");

        // ** Stick �� �߾����κ��� ��ġ�� ��ũ���� �̵��� �Ÿ��� ����.
        Stick.localPosition = new Vector2(_eventData.x - BackBoard.position.x, _eventData.y - BackBoard.position.y);

        // ** Stick �� Radius �� ����� ���ϰ� ��.
        Stick.localPosition = Vector2.ClampMagnitude(Stick.localPosition, Radius);

        // ** ���̽�ƽ�� �����̴� ���⿡ �°� Ÿ���� �̵������ش�.
        Direction = Stick.localPosition.normalized;

        // ** ���̽�ƽ�� �̵������� �ִ� �Ÿ����� ���� �̵��� ������ŭ �̵� �ӵ��� �����Ŵ.
        float Ratio = Vector3.Distance(BackBoard.position, Stick.position) / Radius;

        // ** ���̽�ƽ�� �����̴� �ִ� ���⿡ �°� Ÿ���� �̵������ش�.
        Movement = new Vector3(
            Direction.x * (Ratio * Speed) * Time.deltaTime,
            0.0f,
            Direction.y * (Ratio * Speed) * Time.deltaTime);

        // ** ���̽�ƽ�� �ٶ󺸴� �������� Ÿ���� �ٶ󺸰��Ѵ�.(ȣ����, sin, cos, tan)
        Target.transform.eulerAngles = new Vector3(0.0f, Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg, 0.0f);
    }
}