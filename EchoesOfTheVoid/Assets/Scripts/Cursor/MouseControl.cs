using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Texture2D defaultCursor, clickableCursor;

    public static MouseControl instance;

    private void Start(){
        Default();
    }

    private void Awake(){
        instance = this;
    }

    public void Clickable(){
        Cursor.SetCursor(clickableCursor, Vector2.zero, CursorMode.Auto);
    }

    public void Default(){
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

}
