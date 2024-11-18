using UnityEngine;

public class AdjustQuadSize : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            // 메쉬의 바운드 크기를 가져옴
            Vector3 meshSize = meshFilter.sharedMesh.bounds.size;

            // 스프라이트 또는 메쉬 크기에 맞게 Quad의 스케일 조정
            transform.localScale = new Vector3(meshSize.x, meshSize.y, 1);
        }
        else
        {
            Debug.LogError("MeshFilter가 없습니다.");
        }
    }
}