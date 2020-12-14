using System;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : IComparable<Vertex>
{
    public Vector2 position;
    public List<Vertex> neighbours;
    public int distance;
    public Vertex previous;

    public Vertex(Vector2 position)
    {
        this.position = position;
        neighbours = new List<Vertex>();
        distance = Int32.MaxValue;
        previous = null;
    }

    protected bool Equals(Vertex other)
    {
        var x = position.Equals(other.position);
        return x;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Vertex) obj);
    }

    public override int GetHashCode()
    {
        return position.GetHashCode();
    }

    public int CompareTo(Vertex other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return distance.CompareTo(other.distance);
    }
}