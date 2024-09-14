using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomParabola : MonoBehaviour
{
    public bool ready = true;       //�ӽ÷� �۵� �׽�Ʈ

    public Transform FirePosition;

    public Vector3 startPosition;
    public Vector3 targetPosition;

    private Vector3 targetDistance;

    public float velocity = 30f;
    public float gravity = 9.82f;
    public float arriveTime = 3f;

    private Vector3 currentPosition;
    private Vector3 initialVelocity;

    private float currentTime = 0;

    private MeshRenderer meshRenderer;

    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("�߻�");
            if (Physics.Raycast(ray, out hit, 30f))
            {
                targetPosition = hit.point;
                startPosition = FirePosition.position;
                float dis = Vector3.Distance(startPosition, targetPosition);
                arriveTime = dis / velocity;
                currentTime = 0;
                transform.position = startPosition;
                CalculateVelocity();
                ready = false;
                meshRenderer.enabled = true;
            }
        }

        if (ready) return;

        ThrowBoom();
        
    }

    private void CalculateVelocity()
    {
        targetDistance = targetPosition - startPosition;
        float x = targetDistance.x;
        float y = targetDistance.y;
        float z = targetDistance.z;

        initialVelocity.x = x / arriveTime;
        initialVelocity.z = z / arriveTime;

        //���� �̵� �Ÿ� = ���� �ӵ��� �̵��� �Ÿ� - �߷°��ӵ��� �̵��� �Ÿ�
        //���� �ӵ��� �̵��� �Ÿ� = ���� �̵��Ÿ�(y) + �߷°��ӵ��� �̵��� �Ÿ�
        initialVelocity.y = (y + 0.5f * gravity * arriveTime * arriveTime) / arriveTime;
    }

    private void ThrowBoom()
    {
        
        if (currentTime < arriveTime)
        {
            currentTime += Time.deltaTime;
            currentPosition = startPosition + initialVelocity * currentTime;
            currentPosition.y -= 0.5f * gravity * currentTime * currentTime;

            transform.position = currentPosition;
        }
        else
        {
            ready = true;
            meshRenderer.enabled = false;
            Debug.Log("����" + transform.position);
        }
    }


}
