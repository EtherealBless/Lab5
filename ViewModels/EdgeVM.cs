using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphEditor.ViewModels;

public class EdgeVM : BaseVM, INotifyPropertyChanged
{

    private NodeVM nodeFrom;
    private NodeVM nodeTo;
    private Color _color = Colors.Black;
    private double _weight = 10.0; // Default weight
    public int Id { get; set; }

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

    
    public NodeVM NodeFrom
    {
        get { return nodeFrom; }
        set
        {
            nodeFrom = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(PathGeometry)); // Update PathGeometry on Node change
            OnPropertyChanged(nameof(From)); // Notify change for From
            OnPropertyChanged(nameof(To));   // Notify change for To
        }
    }
    public NodeVM NodeTo
    {
        get { return nodeTo; }
        set
        {
            nodeTo = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(PathGeometry)); // Update PathGeometry on Node change
            OnPropertyChanged(nameof(From)); // Notify change for From
            OnPropertyChanged(nameof(To));   // Notify change for To
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
            Point fromRaw = this.FromRaw;
            Point toRaw = this.ToRaw;
            double dx = toRaw.X - fromRaw.X;
            double dy = toRaw.Y - fromRaw.Y;
            double angle = Math.Atan2(dy, dx);
            double x = fromRaw.X + NodeFrom.Width / 2 * Math.Cos(angle);
            double y = fromRaw.Y + NodeFrom.Height / 2 * Math.Sin(angle);
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

    //public EdgeVM(NodeVM nodeFrom, NodeVM nodeTo, int id)
    //{
    //    NodeFrom = nodeFrom;
    //    NodeTo = nodeTo;
    //    Id = id;
    //}

    //public double Weight { get; set; }

    // public EdgeVM(NodeVM nodeFrom, NodeVM nodeTo, double weight = 1.0)
    // {
    //     NodeFrom = nodeFrom;
    //     NodeTo = nodeTo;
    //     Weight = weight;
    // }


    //public event PropertyChangedEventHandler PropertyChanged;

    //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}

    private Point CalculateEdgePoint(NodeVM source, NodeVM target)
    {
        if (source == null || target == null) return new Point();

        Point sourcePoint = new Point(source.X + source.Width / 2, source.Y + source.Height / 2);
        Point targetPoint = new Point(target.X + target.Width / 2, target.Y + target.Height / 2);

        double dx = targetPoint.X - sourcePoint.X;
        double dy = targetPoint.Y - sourcePoint.Y;
        double angle = Math.Atan2(dy, dx);

        double x = sourcePoint.X + source.Width / 2 * Math.Cos(angle);
        double y = sourcePoint.Y + source.Height / 2 * Math.Sin(angle);

        return new Point(x, y);
    }

    public double Weight
    {
        get => _weight;
        set
        {
            _weight = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(PathGeometry)); // Update PathGeometry on Weight change
        }
    }

    public Geometry PathGeometry
    {
        get
        {
            return new LineGeometry(From, To);
        }
    }


    //YUN PART YUN PART YUN PART YUN PART YUN PART YUN PART YUN PART YUN PART YUN PART YUN PART YUN PART YUN PART 


    public EdgeVM(NodeVM nodeFrom, NodeVM nodeTo, int id, double weight = 10) // Добавляем weight в конструктор
    {
        NodeFrom = nodeFrom;
        NodeTo = nodeTo;
        Id = id;
        _weight = weight; // Инициализируем вес
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}