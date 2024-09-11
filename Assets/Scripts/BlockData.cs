using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BlockData : MonoBehaviour
{
    public int XPos, YPos, ZPos;                                           //블럭의 X,Y,Z 위치의 정수값을 저장할 int

    [Header("파괴됨")]public bool IsDestroyed;                             //현재 블럭이 파괴된 상태인지 나타낼 bool
    [Header("재 생성이 불가능한 블럭")] public bool NotRegeneration;       //현재 블럭이 파괴된 상태인지 나타낼 bool

    private float respawnTime;                                             //재 생성까지 필요한 시간을 저장할 float

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
            //재생성
        }
    }

    public void DestroyBlock(float RespawnTime)     //블럭이 파괴됐을때 호출할 함수
    {
        IsDestroyed = true;                         //블럭이 파괴된 것으로 변경
        respawnTime = RespawnTime;                  //블럭이 재 생성될 때 까지 필요한 시간
    }

    public void ResetPosition()
    {
        XPos = (int)Mathf.Round(transform.position.x);
        YPos = (int)Mathf.Round(transform.position.y);
        ZPos = (int)Mathf.Round(transform.position.z);
        transform.position = new Vector3(XPos, YPos, ZPos);               //보정 위치로 이동
    }
}
