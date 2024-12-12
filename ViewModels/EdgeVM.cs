using System;
using System.Windows;

namespace GraphEditor.ViewModels;

public class EdgeVM : BaseVM
{

    private NodeVM nodeFrom;
    private NodeVM nodeTo;

    


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
        get { return new Point(NodeFrom.X + NodeFrom.Widht / 2, NodeFrom.Y + NodeFrom.Height / 2); }
    }
    private Point ToRaw
    { 
        get { return new Point(NodeTo.X + NodeTo.Widht / 2, NodeTo.Y + NodeTo.Height / 2); }
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
            double x = FromRaw.X + NodeFrom.Widht / 2 * Math.Cos(angle);
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
            double x = ToRaw.X - NodeTo.Widht / 2 * Math.Cos(angle);
            double y = ToRaw.Y - NodeTo.Height / 2 * Math.Sin(angle);
            return new Point(x, y);
        }
    }

    public EdgeVM(NodeVM nodeFrom, NodeVM nodeTo)
    {
        NodeFrom = nodeFrom;
        NodeTo = nodeTo;
    }
}