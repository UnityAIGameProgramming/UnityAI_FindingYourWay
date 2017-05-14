using UnityEngine;
using System.Collections;

public class TestCode : MonoBehaviour
{
    private Transform startPos, endPos;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }

    public ArrayList pathArray;

    GameObject objStartCube, objEndCube;
    private float elapsedTime = 0.0f;

    // Interval time between pathfinding
    public float intervalTime = 1.0f;

    // Use this for initialization
    void Start()
    {
        objStartCube = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");

        Debug.Log("objStartCube : " + objStartCube.transform.position.ToString());
        Debug.Log("objEndCube : " + objEndCube.transform.position.ToString());

        pathArray = new ArrayList();
        FindPath();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= intervalTime)
        {
            elapsedTime = 0.0f;
            FindPath();
        }
    }

    void FindPath()
    {
        startPos = objStartCube.transform;
        endPos = objEndCube.transform;

        Debug.Log("startPos : " + objStartCube.transform.position.ToString());
        Debug.Log("endPos : " + objEndCube.transform.position.ToString());

        startNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos.position)));
        goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos.position)));

        Debug.Log("Start Node : " + startNode.position.ToString() + " Goal Node : " + goalNode.position.ToString());

        pathArray = AStar.FindPath(startNode, goalNode);

        //Debug.Log("PathArray Length : " + pathArray[0]);
    }

    private void OnDrawGizmos()
    {
        if (pathArray == null)
            return;

        if (pathArray.Count > 0)
        {
            int index = 1;

            foreach (Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node nextNode = (Node)pathArray[index];

                    Debug.DrawLine(node.position, nextNode.position, Color.green);

                    index++;
                }
            }
        }
    }
}
