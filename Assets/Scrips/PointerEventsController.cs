using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerEventsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Image objects for display and cursor, Sprite objects for different screens displayed
    public Image cursor;
    public Image screen;
    public Sprite homeScreen;
    public Sprite screenOne;
    public Sprite screenTwo;

    // used to move cursor around
    private float speed = 1218;
   
    // Direction of swipe
    public enum DraggedDirection {
        Up,
        Down,
        Right,
        Left,
        Null
    }

    // get Image object 
    public void Start() {
        screen = screen.GetComponent<Image>();
    }


    // change Sprite on Image object on click (get to next level of UI)
    public void Update() {
        if (Input.GetMouseButtonDown(0))
            if (screen.sprite == homeScreen) {
                screen.sprite = screenOne;
                cursor.rectTransform.anchoredPosition3D = new Vector3(-29.8f, 46.4f, 0);
                levelUI = 2;
            }
            else {
                screen.sprite = screenTwo;
                cursor.rectTransform.anchoredPosition3D = new Vector3(0f, 46.4f, 0);
                levelUI = 3;
            }
    }

    // used to calculate direction of sprite on swipe
    private Vector3 dragVectorDirectionEnter;
    private Vector3 dragVectorDirectionExit;

    // get first postion when entering button zone
    public void OnPointerEnter(PointerEventData eventData) {
        speed = 1218;
        dragVectorDirectionEnter = eventData.position;
    }


    // Debug
    private int screenCount;
    // limit the movement of cursor depending on UI level
    private int levelUI = 1;

    // counts to limit movement of cursor
    public int countRight = 1;
    public int countDown = 1;

    // get second position when exiting button zone
    public void OnPointerExit(PointerEventData eventData) {
    
        dragVectorDirectionExit = eventData.position;

        // calculate and normalize direction vector
        Vector3 dragVectorDirection = (dragVectorDirectionExit - dragVectorDirectionEnter).normalized;
        DraggedDirection move = GetDragDirection(dragVectorDirection);


        // first level of UI
        if (levelUI == 1) {
            // limit cursor movement
            switch (move)
            {
                // cursor can only move four times to the right, etc
                case DraggedDirection.Right:
                    if (countRight > 0 && countRight < 4) {
                        cursor.transform.position += Vector3.right * speed * Time.deltaTime;
                        countRight++;
                    }
                    break;
                case DraggedDirection.Left:
                    if (countRight > 1 && countRight < 5)
                    {
                        cursor.transform.position += Vector3.left * speed * Time.deltaTime;
                        countRight--;
                    }
                    break;
                case DraggedDirection.Down:
                    if (countDown > 0 && countDown < 2) {
                        cursor.transform.position += Vector3.down * speed * Time.deltaTime;
                        countDown++;
                    }
                    break;
                case DraggedDirection.Up:
                    if (countDown > 1 && countDown < 3)
                    {
                        cursor.transform.position += Vector3.up * speed * Time.deltaTime;
                        countDown--;
                    }
                    break;
                default:
                    break;
            }
        }
        // second level of UI (reached through click)
        else if (levelUI == 2)
        {
            // limit cursor movement
            switch (move)
            {
                // cursor can only move four times to the right, etc
                case DraggedDirection.Right:
                    if (countRight > 0 && countRight < 4)
                    {
                        cursor.transform.position += Vector3.right * speed * Time.deltaTime;
                        countRight++;
                    }
                    break;
                case DraggedDirection.Left:
                    if (countRight > 1 && countRight < 5)
                    {
                        cursor.transform.position += Vector3.left * speed * Time.deltaTime;
                        countRight--;
                    }
                    break;;
                default:
                    break;
            }
        }
        else
        {
            return;
        }
        screenCount++;
    }

    // Get swipe direction (RIGHT, LEFT, UP, DOWN)
    public DraggedDirection GetDragDirection(Vector3 dragVector) { 

        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);

        DraggedDirection draggedDir;

        if (positiveX > positiveY)
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        else
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
            
        return draggedDir;
    }
}


