using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoomType
{
    Default = 0,
    Cross = 1,
    Long = 2,
    KnockBack = 3
}

public class LevelData : MonoBehaviour
{
    public List<BlockData> Blocks = new List<BlockData>();
    public List<BlockData> DestroyedBlocks = new List<BlockData>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < DestroyedBlocks.Count; i++)
        {
            DestroyedBlocks[i].Timer(Time.deltaTime);
        }
    }

    public void DestroyBlock(BoomType boom, Vector3 Pos)
    {
        switch(boom)
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

    private void DefaultBoom(Vector3 Pos)
    {
        for(int i = 0; i < Blocks.Count; i++)
        {
            //Blocks[i].Pos
        }
    }

    private void CrossBoom(Vector3 Pos)
    {

    }

    private void LongBoom(Vector3 Pos)
    {

    }

    private void KnockBackBoom(Vector3 Pos) 
    { 
        //�÷��̾ Ư�� �Ÿ� ���ʿ� ������ ���ĳ�
    }

    public void ResetMap()
    {
        Blocks = new List<BlockData> { };
        DestroyedBlocks = new List<BlockData> { };
        for (; transform.childCount > 0;)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        //�����Ҷ� ���°�
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    BlockData temp = transform.GetChild(i).GetComponent<BlockData>();
        //    temp.ResetPosition();
        //    Blocks.Add(temp);
        //}
    }

    public void AddBlock(int x, int z,BlockData block)
    {
        Blocks.Add(block);
    }

}
