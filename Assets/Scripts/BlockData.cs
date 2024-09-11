using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BlockData : MonoBehaviour
{
    public int XPos, YPos, ZPos;                                           //���� X,Y,Z ��ġ�� �������� ������ int

    [Header("�ı���")]public bool IsDestroyed;                             //���� ���� �ı��� �������� ��Ÿ�� bool
    [Header("�� ������ �Ұ����� ��")] public bool NotRegeneration;       //���� ���� �ı��� �������� ��Ÿ�� bool

    private float respawnTime;                                             //�� �������� �ʿ��� �ð��� ������ float

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialized(int x, int y, int z)
    {
        XPos = x;
        YPos = y;
        ZPos = z;
    }

    public void Timer(float deltaTime)
    {
        respawnTime -= deltaTime;
        if(respawnTime <= 0 && IsDestroyed)
        {
            //�����
        }
    }

    public void DestroyBlock(float RespawnTime)     //���� �ı������� ȣ���� �Լ�
    {
        IsDestroyed = true;                         //���� �ı��� ������ ����
        respawnTime = RespawnTime;                  //���� �� ������ �� ���� �ʿ��� �ð�
    }

    public void ResetPosition()
    {
        XPos = (int)Mathf.Round(transform.position.x);
        YPos = (int)Mathf.Round(transform.position.y);
        ZPos = (int)Mathf.Round(transform.position.z);
        transform.position = new Vector3(XPos, YPos, ZPos);               //���� ��ġ�� �̵�
    }
}
