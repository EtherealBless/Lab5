using System;
using System.Collections.Generic;

namespace GraphEditor;
public class Node
{
    private static int _id = 0;
    public Node(int id, string name)
    {
        Id = id;
        Name = name;
        Edges = new List<Edge>();
        Console.WriteLine($"Node created {_id++}");
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Edge> Edges { get; set; }
}
