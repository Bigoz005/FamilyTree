using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawShape : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject rectPrefab;
    public GameObject ballPrefab;
    public GameObject nodeButtonPrefab;

    public GameObject currentLine;
    public GameObject currentRectCanvas;
    public GameObject currentNodeCanvas;

    public GameObject mainObject;

    public Button editButton;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> mousePositionList;

    public Vector2 mousePosition;

    public List<InputField> inputFields;
    public List<GameObject> objectsToSave { get; set; } = new List<GameObject>();

    public Text lineModeText;

    public bool straightLinesMode;
    public bool readyToDraw;
    public bool editMode;

    public string type;
    // Start is called before the first frame update
    void Start()
    {
        straightLinesMode = false;
        lineModeText.GetComponent<Text>().enabled = false;
        type = "rect";
        readyToDraw = false;
        editMode = false;
        FamilyTree.SaveInitiated += Save;
    }

    // Update is called once per frame
    void Update()
    {
        if (editMode)
        {
            switch (type)
            {
                case "nodeButton":
                    if (readyToDraw)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Vector2 tempMousePositionRect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            currentNodeCanvas = Instantiate(nodeButtonPrefab, tempMousePositionRect, Quaternion.identity);
                            objectsToSave.Add(currentNodeCanvas);

                            currentNodeCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
                            currentNodeCanvas.transform.GetChild(0).position = tempMousePositionRect;
                            currentNodeCanvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
                            //currentNodeButton.GetComponentInChildren<InputField>().interactable = false;
                            edgeCollider = currentNodeCanvas.GetComponentInChildren<EdgeCollider2D>();

                            //inputFields.Add(currentNodeButton.GetComponentInChildren<InputField>());

                            readyToDraw = !readyToDraw;
                        }
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        Destroy(currentNodeCanvas);
                    }
                    break;

                case "rect":
                    if (readyToDraw)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Vector2 tempMousePositionRect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            currentRectCanvas = Instantiate(rectPrefab, tempMousePositionRect, Quaternion.identity);
                            objectsToSave.Add(currentRectCanvas);

                            currentRectCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
                            currentRectCanvas.transform.GetChild(0).position = tempMousePositionRect;
                            currentRectCanvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
                            currentRectCanvas.GetComponentInChildren<InputField>().interactable = false;
                            edgeCollider = currentRectCanvas.GetComponentInChildren<EdgeCollider2D>();

                            inputFields.Add(currentRectCanvas.GetComponentInChildren<InputField>());

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
                            objectsToSave.Add(currentLine);
                            lineRenderer = currentLine.GetComponent<LineRenderer>();
                            edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

                            mousePositionList.Clear();
                            mousePositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            lineRenderer.SetPosition(0, mousePosition);
                            lineRenderer.SetPosition(1, mousePosition);

                            edgeCollider.points = mousePositionList.ToArray();
                        }
                        else if (Input.GetMouseButton(0))
                        {
                            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            lineRenderer.SetPosition(1, mousePosition);

                        }
                        else if (Input.GetMouseButtonUp(0))
                        {
                            mousePositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                            edgeCollider.points = mousePositionList.ToArray();
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

    public void CreateBall()
    {
        Instantiate(ballPrefab, new Vector3(0.0f, 4.4f, 2.5f), Quaternion.identity);
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        objectsToSave.Add(currentLine);
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
        lineModeText.GetComponent<Text>().enabled = !lineModeText.GetComponent<Text>().enabled;
        straightLinesMode = !straightLinesMode;
        DestroyShape();
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

    public void DestroyShape()
    {
        switch (type)
        {
            case "line":
                Destroy(currentLine);
                break;
            case "rect":
                Destroy(currentRectCanvas);
                break;
            case "nodeButton":
                Destroy(currentNodeCanvas);
                break;
        }
    }

    public void DrawMode()
    {
        readyToDraw = !readyToDraw;
    }

    public void EditMode()
    {
        if (editButton.GetComponentInChildren<Text>().text == "Press to Edit Boxes")
        {
            editButton.GetComponentInChildren<Text>().text = "Press to Draw Shapes";
        }
        else
        {
            editButton.GetComponentInChildren<Text>().text = "Press to Edit Boxes";
        }

        editMode = !editMode;

        if (editMode)
        {
            foreach (InputField inputField in inputFields)
            {
                inputField.interactable = false;
            }
        }
        else
        {
            foreach (InputField inputField in inputFields)
            {
                inputField.interactable = true;
            }
        }
    }

    public void AddItemsToList(List<GameObject> gameObjects)
    {
        foreach (GameObject myGameObject in gameObjects)
        {
            objectsToSave.Add(myGameObject.gameObject);
        }
    }

    public void Save()
    {
        SaveLoad.Save<List<GameObject>>(objectsToSave, "Shapes");
    }

    public void Load()
    {
        if (SaveLoad.SaveExists("Shapes"))
        {
            AddItemsToList(SaveLoad.Load<List<GameObject>>("Shapes"));
        }
    }
}
