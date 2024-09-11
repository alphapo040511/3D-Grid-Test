using System.Collections.Generic;
using UnityEngine;

public class DynamicMeshManager : MonoBehaviour
{
    public GameObject blockPrefab; // ��� ������ ����� ������
    private Dictionary<Vector3Int, GameObject> blockDictionary = new Dictionary<Vector3Int, GameObject>(); // ��ġ�� Ű��, ��� ���ӿ�����Ʈ�� ������ �����ϴ� ��ųʸ�
    public bool useDebugRay = true; // ����� ���� ��� ����
    public float raycastDistance = 100f; // ����ĳ��Ʈ �ִ� �Ÿ�
    public LayerMask blockLayer; // ����� ���� ���̾�
    public Camera mainCamera; // ���� ī�޶� ����

    private const int GridSize = 10; // �׸��� ũ�� (10x10x10)
    private Plane[] cameraFrustumPlanes = new Plane[6]; // ī�޶� �������� ��� �迭

    void Start()
    {
        GenerateInitialBlocks(); // �ʱ� ��� ����
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // ���� ī�޶� �������� �ʾҴٸ� �ڵ����� ã�� ����
        }
        UpdateBlockVisibility(); // �ʱ� ��� ���ü� ������Ʈ
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyBlockWithRaycast(); // ���콺 ���� ��ư Ŭ�� �� ��� ����
        }

        if (useDebugRay)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red); // ����� ���� �׸���
        }

        UpdateBlockVisibility(); // �� �����Ӹ��� ��� ���ü� ������Ʈ
    }

    void GenerateInitialBlocks()
    {
        // 3�� for������ 10x10x10 �׸��忡 ��� ����
        for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                for (int z = 0; z < GridSize; z++)
                {
                    Vector3Int position = new Vector3Int(x, y, z);
                    CreateBlock(position);
                }
            }
        }
        UpdateSurfaceBlocks(); // ��� ��� ���� �� ǥ�� ��ϸ� Ȱ��ȭ
    }

    void CreateBlock(Vector3Int position)
    {
        GameObject blockInstance = Instantiate(blockPrefab, position, Quaternion.identity, transform);
        blockDictionary[position] = blockInstance;
        blockInstance.SetActive(false); // �ʱ⿡�� ��� ����� ��Ȱ��ȭ
    }

    public void DestroyBlockWithRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, blockLayer))
        {
            Vector3Int gridPosition = Vector3Int.RoundToInt(hit.point - hit.normal * 0.5f);
            if (blockDictionary.TryGetValue(gridPosition, out GameObject blockToDestroy))
            {
                Debug.Log($"Destroying block at position: {gridPosition}");
                blockDictionary.Remove(gridPosition);
                Destroy(blockToDestroy);
                UpdateSurfaceBlocksAround(gridPosition); // ���ŵ� ��� �ֺ��� ǥ�� ���� ������Ʈ
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any block");
        }
    }

    void UpdateBlockVisibility()
    {
        GeometryUtility.CalculateFrustumPlanes(mainCamera, cameraFrustumPlanes); // ī�޶� �������� ��� ���

        foreach (var kvp in blockDictionary)
        {
            Vector3Int position = kvp.Key;
            GameObject block = kvp.Value;

            if (block.activeSelf)
            {
                bool isVisible = IsBlockVisible(position);
                block.GetComponent<Renderer>().enabled = isVisible; // �������� ���� �ִ� ��ϸ� ������ Ȱ��ȭ
            }
        }
    }

    bool IsBlockVisible(Vector3Int position)
    {
        Vector3 blockCenter = position + new Vector3(0.5f, 0.5f, 0.5f);
        return GeometryUtility.TestPlanesAABB(cameraFrustumPlanes, new Bounds(blockCenter, Vector3.one)); // ����� ī�޶� �������� ���� �ִ��� Ȯ��
    }

    void UpdateSurfaceBlocks()
    {
        foreach (var kvp in blockDictionary)
        {
            UpdateBlockSurfaceState(kvp.Key); // ��� ����� ǥ�� ���� ������Ʈ
        }
    }

    void UpdateSurfaceBlocksAround(Vector3Int position)
    {
        // ���ŵ� ��� �ֺ� 3x3x3 ������ ��ϵ� ���� ������Ʈ
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Vector3Int neighborPos = position + new Vector3Int(x, y, z);
                    if (blockDictionary.ContainsKey(neighborPos))
                    {
                        UpdateBlockSurfaceState(neighborPos);
                    }
                }
            }
        }
    }

    void UpdateBlockSurfaceState(Vector3Int position)
    {
        bool isOnSurface = IsBlockOnSurface(position);
        GameObject block = blockDictionary[position];
        block.SetActive(isOnSurface); // ǥ�鿡 �ִ� ��ϸ� Ȱ��ȭ
    }

    bool IsBlockOnSurface(Vector3Int position)
    {
        // 6����(��,��,��,��,��,��)�� �̿� ��� Ȯ��
        Vector3Int[] neighbors = new Vector3Int[]
        {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 0, 1),
            new Vector3Int(0, 0, -1)
        };

        foreach (Vector3Int offset in neighbors)
        {
            Vector3Int neighborPos = position + offset;
            if (!blockDictionary.ContainsKey(neighborPos))
            {
                return true; // �ϳ��� �̿� ����� ������ ǥ�鿡 �ִ� ��
            }
        }

        return false; // ��� �̿��� ������ ���� ���
    }
}