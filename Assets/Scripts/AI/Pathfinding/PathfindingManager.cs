using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    [SerializeField] private Node _nodePrefab;

    private Node[,] _nodes;

    private Vector2Int _gridSize = new Vector2Int(6, 8);

    public Node TestNodeA;

    public Node TestNodeB;

    [SerializeField] private Vector2Int _suckerNodeIndex;

    private Node _suckerNode;

    private void Awake()
    {
        CreateGrid();
        _suckerNode = _nodes[_suckerNodeIndex.x, _suckerNodeIndex.y];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var node in GetClosestPath(TestNodeA))
            {
                Debug.Log(node.name);
            }
        }
    }

    private void CreateGrid()
    {
        _nodes = new Node[_gridSize.x + 1, _gridSize.y + 1];
        var offset = new Vector3(_gridSize.x * _nodePrefab.transform.localScale.x, 0, _gridSize.y * _nodePrefab.transform.localScale.x) / 2;
        for (int x = 0; x <= _gridSize.x; x++)
        {
            for (int y = 0; y <= _gridSize.y; y++)
            {
                var position = new Vector3(x * _nodePrefab.transform.localScale.x, 0, y * _nodePrefab.transform.localScale.z) - offset;
                var spawnedNode = Instantiate(_nodePrefab, position, Quaternion.identity, transform);
                spawnedNode.GridIndex = new Vector2Int(x, y);
                spawnedNode.name = $"Node({x}:{y})";
                _nodes[x, y] = spawnedNode;
            }
        }
    }


    public Queue<Node> GetClosestPath() => GetClosestPath(GetRichestNode());

    public Queue<Node> GetClosestPath(Node targetNode)
    {
        Queue<Node> path = new Queue<Node>();

        Node currentNode = _suckerNode;

        path.Enqueue(currentNode);

        for (int i = 0; i < MaxIterationCount; i++)
        {
            var closestNode = GetClosestNode(currentNode, targetNode);
            currentNode = closestNode;
            path.Enqueue(closestNode);
            if (closestNode == targetNode)
            {
                break;
            }
        }

        return path;
    }


    private Node GetClosestNode(Node currentNode, Node targetNode)
    {
        var closestDistance = float.MaxValue;
        Node candidateNode = currentNode;
        foreach (var node in GetNeighbours(currentNode))
        {
            if (!node.IsWalkable) continue;
            var controlDistance = Vector3.Distance(node.transform.position, targetNode.transform.position);
            if (controlDistance < closestDistance)
            {
                closestDistance = controlDistance;
                candidateNode = node;
            }
        }

        return candidateNode;
    }


    public Node GetRichestNode()
    {
        Node candidateNode = null;
        var candidateAmount = 0;
        foreach (var node in _nodes)
        {
            if (!node.IsWalkable) continue;

            if (node.CollectableCount > candidateAmount)
            {
                candidateAmount = node.CollectableCount;
                candidateNode = node;
            }
        }

        return candidateNode;
    }


    private List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                if ((x == -1 && y == -1) || (x == 1 && y == 1))
                {
                    continue;
                }

                var candidateGridIndex = node.GridIndex;
                candidateGridIndex.x += x;
                candidateGridIndex.y += y;
                if (candidateGridIndex.x > _gridSize.x || candidateGridIndex.x < 0 || candidateGridIndex.y > _gridSize.y || candidateGridIndex.y < 0)
                {
                    continue;
                }

                neighbours.Add(_nodes[candidateGridIndex.x, candidateGridIndex.y]);
            }
        }

        return neighbours;
    }

    private const int MaxIterationCount = 100;
}