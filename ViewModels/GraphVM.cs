using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using GraphEditor.Algorithms.Steps;
using System.Windows;
using GraphEditor.Algorithms.Steps.Nodes;
using System.Collections.ObjectModel;

namespace GraphEditor.ViewModels;

public class GraphVM : BaseVM, ICloneable
{
    private Graph _graph;
    private ObservableCollection<NodeVM> _nodesVM = new ObservableCollection<NodeVM>();
    private ObservableCollection<EdgeVM> _edgesVM = new ObservableCollection<EdgeVM>();
    public Graph Graph
    {
        get
        {
            return _graph;
        }
        set
        {
            _graph = value;
            OnPropertyChanged();
        }
    }
    // public ICommand NodeClickCommand { get; set; }
    public ICommand CanvasClickCommand { get; set; }
    public ObservableCollection<NodeVM> NodesVM
    {
        get
        {
            return _nodesVM;
        }
        set
        {
            _nodesVM = value;
            _nodesDict.Clear();
            for (int i = 0; i < _nodesVM.Count; i++)
            {
                _nodesDict.Add(_nodesVM[i].Node.Id, _nodesVM[i]);
            }
            OnPropertyChanged();
        }
    }
    public ObservableCollection<EdgeVM> EdgesVM
    {
        get
        {
            return _edgesVM;
        }
        set
        {
            Console.WriteLine("EdgesVM set");
            _edgesVM = value;
            OnPropertyChanged();
        }
    }

    private Dictionary<int, NodeVM> _nodesDict = new Dictionary<int, NodeVM>();

    public GraphVM(Graph graph, ICommand nodeClickCommand, ICommand canvasClickCommand)
    {
        Graph = graph;
        // NodeClickCommand = nodeClickCommand;
        CanvasClickCommand = canvasClickCommand;
    }

    public void AddNode(System.Windows.Point point)
    {
        var node = new Node(_graph.Nodes.Count + 1, "New node");
        var nodeVM = new NodeVM(node, point.X, point.Y);
        _graph.Nodes.Add(node);
        _nodesVM.Add(nodeVM);
        _nodesDict.Add(nodeVM.Node.Id, nodeVM);
        OnPropertyChanged(nameof(NodesVM));
    }

    public void AddEdge(NodeVM nodeFrom, NodeVM nodeTo)
    {
        var edge = new Edge(nodeFrom.Node, nodeTo.Node);
        nodeFrom.Node.Edges.Add(edge);
        nodeTo.Node.Edges.Add(edge);
        _graph.Edges.Add(edge);
        _edgesVM.Add(new EdgeVM(nodeFrom, nodeTo));
        OnPropertyChanged(nameof(EdgesVM));
    }

    public void ChangeColor(Node node, System.Windows.Media.Color color)
    {
        _nodesDict[node.Id].Color = color;
    }

    public void ApplyStep(IStep step)
    {
        switch (step)
        {
            case SelectNodeStep selectNodeStep:
                ApplyStep(selectNodeStep);
                break;
            case CheckedNodeStep checkedNodeStep:
                ApplyStep(checkedNodeStep);
                break;
        }
    }

    public void ApplyStep(SelectNodeStep step)
    {
        _nodesDict[step.NodeId].Color = Constants.StepsColors.SelectedNodeColor;
    }

    public void ApplyStep(CheckedNodeStep step)
    {
        _nodesDict[step.NodeId].Color = Constants.StepsColors.CheckedNodeColor;
    }

    public object Clone()
    {
        var clone = new GraphVM(Graph, null, CanvasClickCommand);
        clone.NodesVM = new ObservableCollection<NodeVM>(NodesVM.Select(x => (NodeVM)x.Clone()).ToList());

        var edges = new ObservableCollection<EdgeVM>();
        foreach (var edgeVM in EdgesVM)
        {
            var nodeFrom = clone._nodesDict[edgeVM.NodeFrom.Node.Id];
            var nodeTo = clone._nodesDict[edgeVM.NodeTo.Node.Id];
            edges.Add(new EdgeVM(nodeFrom, nodeTo));
        }
        clone.EdgesVM = edges;
        return clone;
    }
    public void Clear()
    {
        _graph.Nodes.Clear();
        _graph.Edges.Clear();
        _nodesVM.Clear();
        _edgesVM.Clear();
        _nodesDict.Clear();
    }
}