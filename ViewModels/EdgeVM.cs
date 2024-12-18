using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphEditor.ViewModels;

public class EdgeVM : BaseVM
{

    private NodeVM nodeFrom;
    private NodeVM nodeTo;
    private Color _color = Colors.Black;
    public Color Color
    {
        get
        {
            return _color;
        }
        set
        {
            _color = value;
            OnPropertyChanged();
        }
    }
    public Thickness Margin
    {
        get
        {
            var leftX = Math.Min(From.X, To.X);
            var topY = Math.Min(From.Y, To.Y);
            return new Thickness(leftX, topY, 0, 0);
        }
    }

    public double Width
    {
        get
        {
            return Math.Abs(From.X - To.X);
        }
    }

    public double Height
    {
        get
        {
            return Math.Abs(From.Y - To.Y);
        }
    }

    public int Id { get; set; }
    public NodeVM NodeFrom
    {
        get { return nodeFrom; }
        set
        {
            nodeFrom = value;
            OnPropertyChanged();
        }
    }
    public NodeVM NodeTo
    {
        get { return nodeTo; }
        set
        {
            nodeTo = value;
            OnPropertyChanged();
        }
    }
    private Point FromRaw
    {
        get { return new Point(NodeFrom.X + NodeFrom.Width / 2, NodeFrom.Y + NodeFrom.Height / 2); }
    }
    private Point ToRaw
    {
        get { return new Point(NodeTo.X + NodeTo.Width / 2, NodeTo.Y + NodeTo.Height / 2); }
    }
    public Point From
    {
        get
        {
            Point FromRaw = this.FromRaw;
            Point ToRaw = this.ToRaw;
            double dx = ToRaw.X - FromRaw.X;
            double dy = ToRaw.Y - FromRaw.Y;
            double angle = Math.Atan2(dy, dx);
            double x = FromRaw.X + NodeFrom.Width / 2 * Math.Cos(angle);
            double y = FromRaw.Y + NodeFrom.Height / 2 * Math.Sin(angle);
            return new Point(x, y);
        }
    }
    public Point To
    {
        get
        {
            Point FromRaw = this.FromRaw;
            Point ToRaw = this.ToRaw;
            double dx = ToRaw.X - FromRaw.X;
            double dy = ToRaw.Y - FromRaw.Y;
            double angle = Math.Atan2(dy, dx);
            double x = ToRaw.X - NodeTo.Width / 2 * Math.Cos(angle);
            double y = ToRaw.Y - NodeTo.Height / 2 * Math.Sin(angle);
            return new Point(x, y);
        }
    }

    public Point NormalFrom
    {
        get
        {
            Point FromRaw = this.From;
            return new Point(FromRaw.X - Margin.Left, FromRaw.Y - Margin.Top);
        }
    }

    public Point NormalTo
    {
        get
        {
            Point ToRaw = this.To;
            return new Point(ToRaw.X - Margin.Left, ToRaw.Y - Margin.Top);
        }
    }

    public EdgeVM(NodeVM nodeFrom, NodeVM nodeTo, int id)
    {
        NodeFrom = nodeFrom;
        NodeTo = nodeTo;
        Id = id;
    }

    public double Weight { get; set; }

    // public EdgeVM(NodeVM nodeFrom, NodeVM nodeTo, double weight = 1.0)
    // {
    //     NodeFrom = nodeFrom;
    //     NodeTo = nodeTo;
    //     Weight = weight;
    // }
}