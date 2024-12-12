namespace GraphEditor.ViewModels;

public class NodeVM
{
    public Node Node { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Widht { get; } = 30;
    public double Height { get; } = 30;


    public NodeVM(Node node, double x, double y)
    {
        Node = node;
        X = x;
        Y = y;
    }
}