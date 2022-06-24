using UnityEngine;

public class MyCursor : MonoBehaviour

{
    private Animator cursorAnim; 

    void Start()
    {
        cursorAnim = GetComponent<Animator>();
         Cursor.visible = false; 
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        
        Vector2 cursorPos = 
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;

        if (Input.GetMouseButtonDown(0)) 
        {
            cursorAnim.SetTrigger("Click"); 
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            cursorAnim.SetTrigger("Unclick"); 
        }
    }

    public void BringOntoScreen()
    
    {
        Cursor.lockState = CursorLockMode.None;
        cursorAnim.SetBool("OnScreen", true); 
    }
}
