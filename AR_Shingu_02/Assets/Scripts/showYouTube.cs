using UnityEngine;
using UnityEngine.UI;

public class YouTubeWebViewController : MonoBehaviour
{
    public Canvas canvas; // World Space 캔버스
    public GameObject targetObject; // 타겟 오브젝트
    private WebViewObject webViewObject; // WebView 객체

    void Start()
    {
        // WebViewObject 생성 및 초기화
        webViewObject = new GameObject("WebViewObject").AddComponent<WebViewObject>();

        webViewObject.Init(
            cb: (msg) => { Debug.Log($"Message from WebView: {msg}"); },
            err: (msg) => { Debug.LogError($"Error from WebView: {msg}"); },
            started: (msg) => { Debug.Log("WebView Started"); },
            ld: (msg) => { Debug.Log($"WebView Loaded: {msg}"); }
        );

        // WebView를 특정 UI 영역에 띄우기 위해 Canvas 영역 내에 위치하도록 설정
        webViewObject.SetMargins(0, 0, 0, 0);
        webViewObject.SetVisibility(false);

        // 캔버스의 위치를 타겟 오브젝트와 일치시키기
        canvas.transform.position = targetObject.transform.position;
        canvas.transform.rotation = targetObject.transform.rotation;
    }

    public void ShowYouTubeVideo()
    {
        string youtubeURL = "https://www.youtube.com/watch?v=abcdefg"; // 원하는 YouTube URL

        // WebView URL 로드 및 표시
        webViewObject.LoadURL(youtubeURL);
        webViewObject.SetVisibility(true);
    }
}