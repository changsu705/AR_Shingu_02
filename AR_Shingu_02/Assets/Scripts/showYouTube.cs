using UnityEngine;
using UnityEngine.UI;

public class YouTubeWebViewController : MonoBehaviour
{
    public Canvas canvas; // World Space ĵ����
    public GameObject targetObject; // Ÿ�� ������Ʈ
    private WebViewObject webViewObject; // WebView ��ü

    void Start()
    {
        // WebViewObject ���� �� �ʱ�ȭ
        webViewObject = new GameObject("WebViewObject").AddComponent<WebViewObject>();

        webViewObject.Init(
            cb: (msg) => { Debug.Log($"Message from WebView: {msg}"); },
            err: (msg) => { Debug.LogError($"Error from WebView: {msg}"); },
            started: (msg) => { Debug.Log("WebView Started"); },
            ld: (msg) => { Debug.Log($"WebView Loaded: {msg}"); }
        );

        // WebView�� Ư�� UI ������ ���� ���� Canvas ���� ���� ��ġ�ϵ��� ����
        webViewObject.SetMargins(0, 0, 0, 0);
        webViewObject.SetVisibility(false);

        // ĵ������ ��ġ�� Ÿ�� ������Ʈ�� ��ġ��Ű��
        canvas.transform.position = targetObject.transform.position;
        canvas.transform.rotation = targetObject.transform.rotation;
    }

    public void ShowYouTubeVideo()
    {
        string youtubeURL = "https://www.youtube.com/watch?v=abcdefg"; // ���ϴ� YouTube URL

        // WebView URL �ε� �� ǥ��
        webViewObject.LoadURL(youtubeURL);
        webViewObject.SetVisibility(true);
    }
}