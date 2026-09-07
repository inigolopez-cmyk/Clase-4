using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] private CursorType cursor;
    
    private CursorType defaultCursor;
    private bool hasExit;

    private void Start()
    {
        defaultCursor = Resources.Load<CursorType>("DefaultCrosshair");
    }

    private void OnMouseEnter()
    {
        SetCursor();
        hasExit = false;
    }

    private void OnMouseExit()
    {
        SetDefaultCursor();
        hasExit= true;
    }

    private void OnDestroy()
    {
        if (!hasExit)
        SetDefaultCursor();
    }



    private void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor.cursorTexture, defaultCursor.cursorHotSpot, CursorMode.Auto);
    }

    private void SetCursor()
    {
        Cursor.SetCursor(cursor.cursorTexture, cursor.cursorHotSpot, CursorMode.Auto);
    }
}
