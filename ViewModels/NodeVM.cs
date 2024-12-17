using System;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphEditor.ViewModels;

public class NodeVM : BaseVM, ICloneable
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

    public double Width { get; } = DefaultWidth;
    public double Height { get; } = DefaultHeight;
    public static double DefaultWidth { get; } = 30;
    public static double DefaultHeight { get; } = 30;
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

    private Color _originalColor;
    public Color OriginalColor
    {
        get => _originalColor;
        set
        {
            _originalColor = value;
            OnPropertyChanged();
        }
    }

    public NodeVM(Node node, double x, double y, Color? color = null)
    {
        Node = node;
        X = x;
        Y = y;
        Color = color ?? Constants.StepsColors.DefaultColor;
        OriginalColor = Color; // Сохраняем исходный цвет
    }

    public object Clone()
    {
        return new NodeVM(Node, X, Y, Color);
    }
}
