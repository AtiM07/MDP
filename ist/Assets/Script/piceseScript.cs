using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class piceseScript : MonoBehaviour
{
    private Vector2 BottomPosition;
    public bool InRightPosition;
    public bool Selected;
    void Start()
    {
        
        BottomPosition = transform.position;
        transform.position = new Vector2(Random.Range(-2f, 2f), Random.Range(-1.5f, -4f));
    }

    void Update()
    {

        if (Vector2.Distance(transform.position, BottomPosition) <= 0.5f)
        {
            if (!Selected)
            {
                if (InRightPosition == false)
                {
                    transform.position = BottomPosition;
                    InRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    Camera.main.GetComponent<DragAndDrop>().PlacedPieces++;
                }
            }
        }
    }
}
