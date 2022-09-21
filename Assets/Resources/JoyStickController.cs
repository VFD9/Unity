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

    private float Radius;

    private bool TouchCheck;

	public void OnDrag(PointerEventData eventData)
	{
        TouchCheck = true;
        OnTouch(eventData.position);
    }

	public void OnPointerDown(PointerEventData eventData)
	{
        TouchCheck = true;
    }

	public void OnPointerUp(PointerEventData eventData)
	{
        TouchCheck = false;
        Stick.localPosition = Vector2.zero;
    }

	private void Awake()
	{
        Target = GameObject.Find("Tank").gameObject;
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
    }

    void Update()
    {
        //if (TouchCheck)
    }

    private void OnTouch(Vector2 _eventData)
	{
        // ** Stick �� �߾����κ��� ��ġ�� ��ũ���� �̵��� �Ÿ��� ����.
        Stick.localPosition = new Vector2(_eventData.x - BackBoard.position.x, _eventData.y - BackBoard.position.y);

        // ** Stick �� Radius �� ����� ���ϰ� ��.
        Stick.localPosition = Vector2.ClampMagnitude(Stick.localPosition, Radius);

        // ** 1. ���̽�ƽ�� �����̴� ���⿡ �°� Ÿ���� �̵������ش�.
        // ** 2. ���̽�ƽ�� �ٶ󺸴� �������� Ÿ���� �ٶ󺸰��Ѵ�.
        // ** 3. ���̽�ƽ�� �̵������� �ִ� �Ÿ����� ���� �̵��� ������ŭ �̵� �ӵ��� �����Ŵ.
    }
}
