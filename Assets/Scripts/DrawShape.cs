using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawShape : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject rectPrefab;

    public GameObject currentLine;
    public GameObject currentRectCanvas;

    public GameObject mainObject;

    public Button editButton;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> mousePositionList;

    public Vector2 mousePosition;

    public bool straightLinesMode;
    public bool readyToDraw;
    public bool editMode;

    public string type;

    // Start is called before the first frame update
    void Start()
    {
        straightLinesMode = false;
        type = "rect";
        readyToDraw = false;
        editMode = false;
        editButton.GetComponent<Text>().text = "Press to Draw Shapes";
    }

    // Update is called once per frame
    void Update()
    {
        if (editMode)
        {
            switch (type)
            {
                case "rect":
                    if (readyToDraw)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Vector2 tempMousePositionRect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            currentRectCanvas = Instantiate(rectPrefab, tempMousePositionRect, Quaternion.identity, mainObject.transform);

                            currentRectCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
                            currentRectCanvas.transform.GetChild(0).position = tempMousePositionRect;

                            readyToDraw = !readyToDraw;
                        }
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        Destroy(currentRectCanvas);
                    }

                    break;
                case "line":
                    if (!straightLinesMode)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            CreateLine();
                        }

                        if (Input.GetMouseButton(0))
                        {
                            Vector2 tempMousePositionLine = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            if (Vector2.Distance(tempMousePositionLine, mousePositionList[mousePositionList.Count - 1]) > .1f)
                            {
                                UpdateLine(tempMousePositionLine);
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

                    if (Input.GetMouseButtonDown(1))
                    {
                        Destroy(currentLine);
                    }

                    break;
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

    public void ChangeLineType()
    {
        straightLinesMode = !straightLinesMode;
        Destroy(currentLine);
    }

    public void ChangeType(string typeName)
    {
        switch (type)
        {
            case "line":
                Destroy(currentLine);
                break;
        }
        type = typeName;
    }

    public void DrawMode()
    {
        readyToDraw = !readyToDraw;
    }

    public void EditMode()
    {
        if(editButton.GetComponentInChildren<Text>().text == "Press to Edit Boxes")
        {
            editButton.GetComponentInChildren<Text>().text = "Press to Draw Shapes";
        }
        else
        {
            editButton.GetComponentInChildren<Text>().text = "Press to Edit Boxes";
        }
        editMode = !editMode;
    }
}
