using UnityEngine;

public class PrefabClickHandler : MonoBehaviour
{
    public string youtubeURL; // 프리팹마다 설정된 유튜브 URL
    public GameObject webViewCanvasPrefab; // WebView 캔버스 프리팹

    void OnMouseDown()
    {
        if (webViewCanvasPrefab != null)
        {
            // 클릭된 프리팹 위치에서 y축으로 0.5f 위에 캔버스 생성
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.5f, 0); // 0.5f는 오브젝트 위치에서 조금 위로 띄우기 위한 값
            GameObject canvasInstance = Instantiate(webViewCanvasPrefab, spawnPosition, Quaternion.identity);

            // 캔버스의 World Camera 설정 (AR 카메라로 설정)
            canvasInstance.GetComponent<Canvas>().worldCamera = Camera.main;

            // WebViewObject 초기화 및 URL 설정
            WebViewObject webViewObject = canvasInstance.GetComponentInChildren<WebViewObject>();
            if (webViewObject != null)
            {
                webViewObject.Init();
                webViewObject.LoadURL(youtubeURL); // 유튜브 URL 로드
                webViewObject.SetVisibility(true);
            }
        }
        else
        {
            Debug.LogWarning("WebView Canvas Prefab is not assigned.");
        }
    }
}