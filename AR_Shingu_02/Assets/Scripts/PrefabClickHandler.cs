using UnityEngine;

public class PrefabClickHandler : MonoBehaviour
{
    public string youtubeURL; // �����ո��� ������ ��Ʃ�� URL
    public GameObject webViewCanvasPrefab; // WebView ĵ���� ������

    void OnMouseDown()
    {
        if (webViewCanvasPrefab != null)
        {
            // Ŭ���� ������ ��ġ���� y������ 0.5f ���� ĵ���� ����
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.5f, 0); // 0.5f�� ������Ʈ ��ġ���� ���� ���� ���� ���� ��
            GameObject canvasInstance = Instantiate(webViewCanvasPrefab, spawnPosition, Quaternion.identity);

            // ĵ������ World Camera ���� (AR ī�޶�� ����)
            canvasInstance.GetComponent<Canvas>().worldCamera = Camera.main;

            // WebViewObject �ʱ�ȭ �� URL ����
            WebViewObject webViewObject = canvasInstance.GetComponentInChildren<WebViewObject>();
            if (webViewObject != null)
            {
                webViewObject.Init();
                webViewObject.LoadURL(youtubeURL); // ��Ʃ�� URL �ε�
                webViewObject.SetVisibility(true);
            }
        }
        else
        {
            Debug.LogWarning("WebView Canvas Prefab is not assigned.");
        }
    }
}