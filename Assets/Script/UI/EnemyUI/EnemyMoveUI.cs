using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoveUI : MonoBehaviour
{
    //[SerializeField] private Camera TargetCamera;
    [SerializeField] private EnemyUI enemyUI;

    private Camera mainCamera;

    private Rect rect = new Rect(0, 0, 1, 1);

    private Rect canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        // UIÇ™ÇÕÇ›èoÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
        canvasRect = ((RectTransform)enemyUI.transform).rect;
        canvasRect.Set(
            canvasRect.x,
            canvasRect.y,
            canvasRect.width,
            canvasRect.height
        );

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var targetWorldPos = transform.parent.gameObject.transform.position;
        var targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);

        enemyUI.gameObject.GetComponent<RectTransform>().position = targetScreenPos;
        

        var viewport = mainCamera.WorldToViewportPoint(targetWorldPos);

        if (rect.Contains(viewport))
        {
            enemyUI.gameObject.SetActive(true);
        }
        else
        {
            if (viewport.x < 0.03f)
                viewport.x = 0.03f;
            if (viewport.x > 0.97f)
                viewport.x = 0.97f;
            if (viewport.y < 0.03f)
                viewport.y = 0.03f;
            if (viewport.y > 0.97f)
                viewport.y = 0.97f;

            enemyUI.gameObject.GetComponent<RectTransform>().position = mainCamera.ViewportToScreenPoint(viewport);
            
        }
    }
}
