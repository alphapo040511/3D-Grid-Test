using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<BlockData> DisabledBlocks = new List<BlockData>();
    public MapOcullusionCulling ocullusion;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < DisabledBlocks.Count; i++)
        {
            DisabledBlocks[i].Timer(Time.deltaTime);
        }
    }

    public void TestBoom()
    {
        DefaultBoom(new Vector3Int(3, 2, 10));
    }

    public void DestroyBlock(BoomType boom, Vector3Int Pos)
    {
        switch (boom)
        {
            case BoomType.Default:
                DefaultBoom(Pos);
                break;
            case BoomType.Cross:
                CrossBoom(Pos);
                break;
            case BoomType.Long:
                LongBoom(Pos);
                break;
            case BoomType.KnockBack:
                KnockBackBoom(Pos);
                break;
        }
    }

    private void BlockDestroy(Vector3Int Pos)
    {
        if (!ocullusion.blockDictionary.ContainsKey(Pos)) return;

        GameObject target = ocullusion.blockDictionary[Pos];
        BlockData data = target.GetComponent<BlockData>();
        data.DestroyBlock(3);
        if (data.Regeneration)
        {
            DisabledBlocks.Add(data);
        }
        target.SetActive(false);
        ocullusion.UpdateSurfaceBlocksAround(Pos);
    }

    private void DefaultBoom(Vector3Int Pos)
    {
        for(int x = -2; x <= 2; x++)
        {
            for(int y = 0; y <= 0; y++)
            {
                for(int z = -2; z <= 2; z++)
                {
                    if(Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z) <= 2)
                    {
                        Vector3Int PosVector = Pos + new Vector3Int(x, y, z);
                        BlockDestroy(PosVector);
                    }
                }
            }
        }
    }

    private void CrossBoom(Vector3Int Pos)
    {

    }

    private void LongBoom(Vector3Int Pos)
    {

    }

    private void KnockBackBoom(Vector3Int Pos)
    {
        //플레이어가 특정 거리 안쪽에 있으면 밀쳐냄
    }
}
