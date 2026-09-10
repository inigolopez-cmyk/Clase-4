using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] private CursorType cursor;

    private static CursorType defaultCursor;
    private static ChangeCursor currentHoverTarget;

    private void Start()
    {
        if (defaultCursor == null)
            defaultCursor = Resources.Load<CursorType>("DefaultCrosshair");
    }

    private void OnMouseEnter()
    {
        currentHoverTarget = this;
        SetCursor();
    }

    private void OnMouseExit()
    {
        if (currentHoverTarget == this)
        {
            currentHoverTarget = null;
            SetDefaultCursor();
        }
    }

    private void OnDestroy()
    {
        if (currentHoverTarget == this)
        {
            currentHoverTarget = null;
            SetDefaultCursor();
        }
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
