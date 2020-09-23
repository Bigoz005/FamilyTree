using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> mousePositionList;

    public Vector2 mousePosition;

    public bool linesDrawMode;
    private int clickCount;

    // Start is called before the first frame update
    void Start()
    {
        linesDrawMode = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!linesDrawMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(tempMousePosition, mousePositionList[mousePositionList.Count - 1]) > .1f)
                {
                    UpdateLine(tempMousePosition);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                lineRenderer = currentLine.GetComponent<LineRenderer>();
                edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineRenderer.SetPosition(0, mousePosition);
                lineRenderer.SetPosition(1, mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineRenderer.SetPosition(1, mousePosition);
            }
        }
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        mousePositionList.Clear();
        mousePositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, mousePositionList[0]);
        lineRenderer.SetPosition(1, mousePositionList[1]);
        edgeCollider.points = mousePositionList.ToArray();
    }

    void UpdateLine(Vector2 newMousePosition)
    {
        mousePositionList.Add(newMousePosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newMousePosition);
        edgeCollider.points = mousePositionList.ToArray();
    }

    public void changeType()
    {
        linesDrawMode = !linesDrawMode;
        Destroy(currentLine);
    }
}
