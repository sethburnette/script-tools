using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorHover : MonoBehaviour {

    public Texture2D customCursor;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

	void Start ()
    {
        Cursor.SetCursor(null, hotSpot, curMode);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(customCursor, hotSpot, curMode);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, hotSpot, curMode);
    }
}
