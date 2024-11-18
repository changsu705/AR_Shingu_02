using UnityEngine;

public class AdjustQuadSize : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            // �޽��� �ٿ�� ũ�⸦ ������
            Vector3 meshSize = meshFilter.sharedMesh.bounds.size;

            // ��������Ʈ �Ǵ� �޽� ũ�⿡ �°� Quad�� ������ ����
            transform.localScale = new Vector3(meshSize.x, meshSize.y, 1);
        }
        else
        {
            Debug.LogError("MeshFilter�� �����ϴ�.");
        }
    }
}