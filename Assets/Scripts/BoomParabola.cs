using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomParabola : MonoBehaviour
{
    public GameObject Ball;         //던질 임시 오브젝트

    public bool Ready = true;

    public double velocity = 3f;
    public float gravity = 9.82f;

    private float XPos = 0f;
    private float YPos = 0f;
    private float ZPos = 0;

    private float fx = 0;
    private float fy = 0;
    private float fz = 0;

    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Ready = false;
        }

        if (Ready) return;

        ThrowBoom(Ball);
        
    }

    private void ThrowBoom(GameObject boom)
    {
        CurrentPosition(boom.transform.position);
    }

    private void CurrentPosition(Vector3 boom)
    {
        XPos = boom.x;
        YPos = boom.y;
        ZPos = boom.z;
    }

}
