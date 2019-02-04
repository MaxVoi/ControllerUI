using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerEventsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image cursor;
    public Image screen;
    public Sprite homeScreen;
    public Sprite screenOne;
    public Sprite screenTwo;

    public float speed = 1218;

    Vector3 dragVectorDirectionEnter;
    Vector3 dragVectorDirectionExit;

    Vector3 cursorPositionOne;
    Vector3 cursorPositionTwo;
    Vector3 cursorPositionThree;

    private RectTransform rect;

    public int screenCount;
    public int countRight = 1;
    public int countDown = 1;

    public enum DraggedDirection {
        Up,
        Down,
        Right,
        Left,
        Null
    }

    public void Start() {
        screen = screen.GetComponent<Image>();
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0))
            if (screen.sprite == homeScreen) {
                screen.sprite = screenOne;
                cursor.rectTransform.anchoredPosition3D = new Vector3(-29.8f, 46.4f, 0);
            }
            else {
                screen.sprite = screenTwo;
                cursor.rectTransform.localPosition = new Vector3(0f, 46.4f, 0);
            }
    }   

    public void OnPointerEnter(PointerEventData eventData) {
        speed = 1218;
        dragVectorDirectionEnter = eventData.position;
    }

    public void OnPointerExit(PointerEventData eventData) {
    
        dragVectorDirectionExit = eventData.position;

        Vector3 dragVectorDirection = (dragVectorDirectionExit - dragVectorDirectionEnter).normalized;
        DraggedDirection move = GetDragDirection(dragVectorDirection);

            switch (move)
            {
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

        Debug.Log(countDown);
        Debug.Log(countRight);
        screenCount++;
        Debug.Log(screenCount);
    }

    public DraggedDirection GetDragDirection(Vector3 dragVector) {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        else
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        Debug.Log(draggedDir);
        return draggedDir;
    }
}


