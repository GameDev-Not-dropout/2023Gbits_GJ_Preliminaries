using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoSingleton<CursorManager>
{
	public Texture2D normalCursor;
	public Texture2D selectCursor;
	public Texture2D moveCursor;

    private void Start()
    {
        Instance.UseNormalCursor();
    }

    public void UseNormalCursor()
	{
		Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
	}

    public void UseSelectCursor()
    {
        Cursor.SetCursor(selectCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void UseMoveCursor()
    {
        Cursor.SetCursor(moveCursor, Vector2.zero, CursorMode.ForceSoftware);
    }





}
