using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public Vector3 MoveEnemy(Vector3 playerPosition)
    {
        var path = GetFastestPath(playerPosition);
        var newPosition = path.Last();
        var new3DPosition = new Vector3(newPosition.x, _rigidbody.position.y, newPosition.y);
        _rigidbody.MovePosition(new3DPosition);
        return new3DPosition;
    }

    private HashSet<Vector2> q;
    private HashSet<Vector2> qAll;
    private Dictionary<Vector2, int> dist;
    private Dictionary<Vector2, Vector2> prev;

    //Djikstra
    public List<Vector2> GetFastestPath(Vector3 playerPosition)
    {
        dist = new Dictionary<Vector2, int>();
        q = new HashSet<Vector2>();
        qAll = new HashSet<Vector2>();
        prev = new Dictionary<Vector2, Vector2>();

        var positionOfEnemy = _rigidbody.transform.position;
        var enemyPosition2D = new Vector2(((int) positionOfEnemy.x) + 0.5f, ((int) positionOfEnemy.z) + 0.5f);

        q.Add(enemyPosition2D);
        qAll.Add(enemyPosition2D);
        dist.Add(enemyPosition2D, 0);
        ExplorePosition(enemyPosition2D);

        while (q.Any())
        {
            var u = q.First();
            q.Remove(u);

            var neighbours = ExplorePosition(u);

            foreach (var neighbour in neighbours)
            {
                int distance = dist[u] + 1;
                if (distance < dist[neighbour])
                {
                    dist[neighbour] = distance;
                    prev[neighbour] = u;
                }
            }
        }

        var playerPosition2D = new Vector2(((int) playerPosition.x) + 0.5f, ((int) playerPosition.z) + 0.5f);

        var path = new List<Vector2>();
        var currentCoord = playerPosition2D;
        while (currentCoord != enemyPosition2D)
        {
            var previous = prev[currentCoord];
            Debug.DrawLine(new Vector3(currentCoord.x, 1, currentCoord.y), new Vector3(previous.x, 1, previous.y),
                Color.blue, 2);
            path.Add(currentCoord);
            currentCoord = prev[currentCoord];
        }

        return path;
    }

    private List<Vector2> ExplorePosition(Vector2 enemyPosition2D)
    {
        var neighbours = new List<Vector2>();
        var enemyPosition2DxPlus = enemyPosition2D + new Vector2(1f, 0f);
        var enemyPosition2DyPlus = enemyPosition2D + new Vector2(0f, 1f);
        var enemyPosition2DxMinus = enemyPosition2D + new Vector2(-1f, 0f);
        var enemyPosition2DyMinus = enemyPosition2D + new Vector2(0, -1f);

        var walkXPlus = IsOkayToWalkThere(enemyPosition2DxPlus, enemyPosition2D);
        var walkXMinus = IsOkayToWalkThere(enemyPosition2DxMinus, enemyPosition2D);
        var walkYPlus = IsOkayToWalkThere(enemyPosition2DyPlus, enemyPosition2D);
        var walkYMinus = IsOkayToWalkThere(enemyPosition2DyMinus, enemyPosition2D);

        var addXPlus = false;
        var addXMinus = false;
        var addYPlus = false;
        var addYMinus = false;

        if (walkXPlus)
        {
            if (!qAll.Contains(enemyPosition2DxPlus))
            {
                q.Add(enemyPosition2DxPlus);
                qAll.Add(enemyPosition2DxPlus);
            }

            addXPlus = true;
            neighbours.Add(enemyPosition2DxPlus);
        }

        if (walkXMinus)
        {
            if (!qAll.Contains(enemyPosition2DxMinus))
            {
                q.Add(enemyPosition2DxMinus);
                qAll.Add(enemyPosition2DxMinus);
            }

            addXMinus = true;
            neighbours.Add(enemyPosition2DxMinus);
        }


        if (walkYPlus)
        {
            if (!qAll.Contains(enemyPosition2DyPlus))
            {
                q.Add(enemyPosition2DyPlus);
                qAll.Add(enemyPosition2DyPlus);
            }

            addYPlus = true;
            neighbours.Add(enemyPosition2DyPlus);
        }

        if (walkYMinus)
        {
            if (!qAll.Contains(enemyPosition2DyMinus))
            {
                q.Add(enemyPosition2DyMinus);
                qAll.Add(enemyPosition2DyMinus);
            }

            addYMinus = true;
            neighbours.Add(enemyPosition2DyMinus);
        }

        if (addXPlus)
        {
            if (!dist.ContainsKey(enemyPosition2DxPlus))
                dist.Add(enemyPosition2DxPlus, int.MaxValue);
        }

        if (addXMinus)
        {
            if (!dist.ContainsKey(enemyPosition2DxMinus))
                dist.Add(enemyPosition2DxMinus, int.MaxValue);
        }

        if (addYPlus)
        {
            if (!dist.ContainsKey(enemyPosition2DyPlus))
                dist.Add(enemyPosition2DyPlus, int.MaxValue);
        }

        if (addYMinus)
        {
            if (!dist.ContainsKey(enemyPosition2DyMinus))
                dist.Add(enemyPosition2DyMinus, int.MaxValue);
        }

        return neighbours;
    }

    private bool IsOkayToWalkThere(Vector2 positionToWalkTo, Vector2 currentPosition)
    {
        var raycastDirection = new Vector3(0, -4, 0);
        var positionToWalk3D = new Vector3(positionToWalkTo.x, 1, positionToWalkTo.y);
        var currentPosition3D = (new Vector3(currentPosition.x, 1, currentPosition.y));
        var lookDirection = (currentPosition3D - positionToWalk3D);
        
        var beneath = Physics.Raycast(positionToWalk3D, raycastDirection);
        //Debug.DrawRay(positionToWalk3D, raycastDirection, Color.black, 2);
        var infront = !Physics.Raycast(positionToWalk3D, lookDirection, 0.3f);
        //Debug.DrawRay(positionToWalk3D, lookDirection, Color.cyan, 2);
        return beneath && infront;
    }
}