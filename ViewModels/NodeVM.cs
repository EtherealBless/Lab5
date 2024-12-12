using System.Windows.Media;

namespace GraphEditor.ViewModels;

public class NodeVM : BaseVM
{
    private Node _node;
    public Node Node
    {
        get => _node;
        set
        {
            _node = value;
            OnPropertyChanged();
        }
    }

    private double _x;
    public double X
    {
        get => _x;
        set
        {
            _x = value;
            OnPropertyChanged();
        }
    }

    private double _y;
    public double Y
    {
        get => _y;
        set
        {
            _y = value;
            OnPropertyChanged();
        }
    }

    public double Widht { get; } = 30;
    public double Height { get; } = 30;

    private Color _color;
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            OnPropertyChanged();
        }
    }

    public NodeVM(Node node, double x, double y, Color? color = null)
    {
        Node = node;
        X = x;
        Y = y;
        Color = color ?? Constants.StepsColors.DefaultColor;
    }
}