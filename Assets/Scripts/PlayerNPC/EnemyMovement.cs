using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10;
    private bool isAnimating = false;
    
    private List<Vertex> djikstraQ;

    private List<Vertex> vertices = new List<Vertex>();

    private List<Vector2> takenEnemyPositions;

    private Vector3 position;

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {
        if (isAnimating)
        {
            isAnimating = UpdatePositionPerFrame();
        }
    }

    public Vector3 UpdateEnemyPosition(Vector3 playerPosition, List<Vector2> takenEnemyPositions)
    {
        this.takenEnemyPositions = takenEnemyPositions;
        var path = GetFastestPath(playerPosition);
        var nextVertex = path.LastOrDefault();
        //path does not have any vertex, no path for enemy found
        if (nextVertex == null)
        {
            return position;
        }
        
        position = new Vector3(nextVertex.position.x, transform.position.y, nextVertex.position.y);
        isAnimating = true;
        return position;
    }

    public bool UpdatePositionPerFrame(double animationAccuracy = 0.05)
    {
        
        var currentActualPosition = transform.position;
        var moveDirection = position - currentActualPosition;
        var deltaMovement = moveDirection * (speed * Time.deltaTime);
        currentActualPosition += deltaMovement;
        transform.position = currentActualPosition;

        return (currentActualPosition - position).magnitude >= animationAccuracy;
    }
    
    //Djikstra
    public List<Vertex> GetFastestPath(Vector3 playerPosition)
    {
        vertices = new List<Vertex>();

        
        var enemyPosition2D = new Vector2(((int) position.x), ((int) position.z));

        var startVertex = new Vertex(enemyPosition2D);
        startVertex.distance = 0;
        vertices.Add(startVertex);

        //Build up board recursively
        ExplorePosition(startVertex);

        djikstraQ = new List<Vertex>(vertices);

        while (djikstraQ.Any())
        {
            //sort manually because C# does not have a priority queue implemented
            djikstraQ.Sort();
            var v = djikstraQ.First();
            djikstraQ.Remove(v);

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
        var positionToWalk3D = new Vector3(positionToWalkTo.x, 1.5f, positionToWalkTo.y);
        var currentPosition3D = (new Vector3(currentPosition.x, 1.5f, currentPosition.y));
        var lookDirection = (currentPosition3D - positionToWalk3D);

        var beneath = Physics.Raycast(positionToWalk3D, raycastDirection, 1f, LayerMask.GetMask(new[] {"Earth"}));
        Debug.DrawRay(positionToWalk3D, raycastDirection, Color.black, 2);
        var someThingIsInFront = !Physics.Raycast(positionToWalk3D, lookDirection, 0.8f,
            LayerMask.GetMask(new[] {"MoveableItem"}));
       
        if (!someThingIsInFront)
        {
            Debug.DrawRay(positionToWalk3D, lookDirection, Color.cyan, 2);
        }

        return beneath && someThingIsInFront;
    }
}