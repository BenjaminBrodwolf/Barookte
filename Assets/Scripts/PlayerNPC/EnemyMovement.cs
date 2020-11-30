using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private List<Vertex> q;

    private List<Vertex> vertices = new List<Vertex>();

    private List<Vector2> takenEnemyPositions;

    public Vector3 MoveEnemy(Vector3 playerPosition, List<Vector2> takenEnemyPositions)
    {
        this.takenEnemyPositions = takenEnemyPositions;
        var path = GetFastestPath(playerPosition);
        var nextVertex = path.LastOrDefault();
        //path does not have any vertex, no path for enemy found
        if (nextVertex == null) return transform.position;

        var new3DPosition = new Vector3(nextVertex.position.x, transform.position.y, nextVertex.position.y);
        transform.position = new3DPosition;
        return new3DPosition;
    }


    //Djikstra
    public List<Vertex> GetFastestPath(Vector3 playerPosition)
    {
        vertices = new List<Vertex>();

        var positionOfEnemy = transform.position;
        var enemyPosition2D = new Vector2(((int) positionOfEnemy.x), ((int) positionOfEnemy.z));

        var startVertex = new Vertex(enemyPosition2D);
        startVertex.distance = 0;
        vertices.Add(startVertex);

        //Build up board recursively
        ExplorePosition(startVertex);

        q = new List<Vertex>(vertices);

        while (q.Any())
        {
            //sort manually because C# does not have a priority queue implemented
            q.Sort();
            var v = q.First();
            q.Remove(v);

            foreach (var neighbour in v.neighbours)
            {
                int distance = v.distance + 1;
                if (distance < neighbour.distance)
                {
                    neighbour.distance = distance;
                    neighbour.previous = v;
                }
            }
        }

        //look at path found
        var playerPosition2D = new Vector2(((int) playerPosition.x), ((int) playerPosition.z));

        var path = new List<Vertex>();
        var current = vertices.FirstOrDefault(x => x.position == playerPosition2D);
        //no path found
        if (current == null)
        {
            return path;
        }

        while (!Equals(current, startVertex))
        {
            var previous = current.previous;
            Debug.DrawLine(new Vector3(current.position.x, 1, current.position.y),
                new Vector3(previous.position.x, 1, previous.position.y),
                Color.blue, 2);
            path.Add(current);
            current = previous;
        }

        return path;
    }

    private void ExplorePosition(Vertex currentVertex)
    {
        var vertex = vertices.First(x => x.Equals(currentVertex));

        var enemyPosition2DxPlus = currentVertex.position + new Vector2(1f, 0f);
        var enemyPosition2DyPlus = currentVertex.position + new Vector2(0f, 1f);
        var enemyPosition2DxMinus = currentVertex.position + new Vector2(-1f, 0f);
        var enemyPosition2DyMinus = currentVertex.position + new Vector2(0, -1f);

        var walkXPlus = IsOkayToWalkThere(enemyPosition2DxPlus, currentVertex.position);
        var walkXMinus = IsOkayToWalkThere(enemyPosition2DxMinus, currentVertex.position);
        var walkYPlus = IsOkayToWalkThere(enemyPosition2DyPlus, currentVertex.position);
        var walkYMinus = IsOkayToWalkThere(enemyPosition2DyMinus, currentVertex.position);

        if (walkXPlus)
        {
            Vertex toTest = new Vertex(enemyPosition2DxPlus);
            Vertex newVertex;
            if (vertices.Contains(new Vertex(enemyPosition2DxPlus)))
            {
                newVertex = vertices.First(x => x.Equals(toTest));
            }
            else
            {
                newVertex = toTest;
                vertices.Add(toTest);
                ExplorePosition(newVertex);
            }

            vertex.neighbours.Add(newVertex);
        }

        if (walkXMinus)
        {
            Vertex toTest = new Vertex(enemyPosition2DxMinus);
            Vertex newVertex;
            if (vertices.Contains(new Vertex(enemyPosition2DxMinus)))
            {
                newVertex = vertices.First(x => x.Equals(toTest));
            }
            else
            {
                newVertex = toTest;
                vertices.Add(toTest);
                ExplorePosition(newVertex);
            }

            vertex.neighbours.Add(newVertex);
        }

        if (walkYPlus)
        {
            Vertex toTest = new Vertex(enemyPosition2DyPlus);
            Vertex newVertex;
            if (vertices.Contains(new Vertex(enemyPosition2DyPlus)))
            {
                newVertex = vertices.First(x => x.Equals(toTest));
            }
            else
            {
                newVertex = toTest;
                vertices.Add(toTest);
                ExplorePosition(newVertex);
            }

            vertex.neighbours.Add(newVertex);
        }

        if (walkYMinus)
        {
            Vertex toTest = new Vertex(enemyPosition2DyMinus);
            Vertex newVertex;
            if (vertices.Contains(new Vertex(enemyPosition2DyMinus)))
            {
                newVertex = vertices.First(x => x.Equals(toTest));
            }
            else
            {
                newVertex = toTest;
                vertices.Add(toTest);
                ExplorePosition(newVertex);
            }

            vertex.neighbours.Add(newVertex);
        }
    }

    private bool IsOkayToWalkThere(Vector2 positionToWalkTo, Vector2 currentPosition)
    {
        if (takenEnemyPositions.Contains(positionToWalkTo))
        {
            return false;
        }

        var raycastDirection = new Vector3(0, -4, 0);
        var positionToWalk3D = new Vector3(positionToWalkTo.x, 2, positionToWalkTo.y);
        var currentPosition3D = (new Vector3(currentPosition.x, 2, currentPosition.y));
        var lookDirection = (currentPosition3D - positionToWalk3D);

        var beneath = Physics.Raycast(positionToWalk3D, raycastDirection, 1f, LayerMask.GetMask(new[] {"Earth"}));
        Debug.DrawRay(positionToWalk3D, raycastDirection, Color.black, 2);
        var inFront = !Physics.Raycast(positionToWalk3D, lookDirection, 0.5f,
            LayerMask.GetMask(new[] {"MoveableItem"}));
        if (!inFront)
        {
            Debug.DrawRay(positionToWalk3D, lookDirection, Color.cyan, 2);
        }

        return beneath && inFront;
    }
}