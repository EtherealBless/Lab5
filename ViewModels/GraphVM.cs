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
using GraphEditor.Algorithms.Steps.Edges;

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
            _edgesDict.Clear();
            for (int i = 0; i < _edgesVM.Count; i++)
            {
                _edgesDict.Add(_edgesVM[i].Id, _edgesVM[i]);
            }
            OnPropertyChanged();
        }
    }

    private Dictionary<int, NodeVM> _nodesDict = new Dictionary<int, NodeVM>();
    private Dictionary<int, EdgeVM> _edgesDict = new Dictionary<int, EdgeVM>();

    public GraphVM(Graph graph, ICommand nodeClickCommand, ICommand canvasClickCommand)
    {
        Graph = graph;
        // NodeClickCommand = nodeClickCommand;
        CanvasClickCommand = canvasClickCommand;
    }

    public void AddNode(System.Windows.Point point)
    {
        var node = new Node(_graph.Nodes.Count + 1, $"{_graph.Nodes.Count + 1}");
        var nodeVM = new NodeVM(node, point.X, point.Y);
        _graph.Nodes.Add(node);
        _nodesVM.Add(nodeVM);
        _nodesDict.Add(nodeVM.Node.Id, nodeVM);
        OnPropertyChanged(nameof(NodesVM));
    }

    //public void AddEdge(NodeVM nodeFrom, NodeVM nodeTo)
    //{
    //    int newId = _edgesVM.Count == 0 ? 0 : _edgesVM.Count + 1;
    //    var edge = new Edge(nodeFrom.Node, nodeTo.Node, id: newId);
    //    nodeFrom.Node.Edges.Add(edge);
    //    nodeTo.Node.Edges.Add(edge);
    //    _graph.Edges.Add(edge);
    //    _edgesVM.Add(new EdgeVM(nodeFrom, nodeTo, newId));
    //    OnPropertyChanged(nameof(EdgesVM));
    //}

    public void ChangeColor(Node node, System.Windows.Media.Color color)
    {
        _nodesDict[node.Id].Color = color;
    }

    public void ApplyStep(IStep step)
    {
        if (step is INodeStep nodeStep)
        {
            _nodesDict[nodeStep.NodeId].Color = nodeStep.Color ?? throw new ArgumentNullException(nameof(nodeStep.Color));
        }
        else if (step is IEdgeStep edgeStep)
        {
            _edgesDict[edgeStep.Id].Color = edgeStep.Color ?? throw new ArgumentNullException(nameof(edgeStep.Color));
        }
        else
        {
            Console.WriteLine($"Unknown step: {step.GetType().Name}");
        }
    }

    //public object Clone()
    //{
    //    var clone = new GraphVM(Graph, null, CanvasClickCommand);
    //    clone.NodesVM = new ObservableCollection<NodeVM>(NodesVM.Select(x => (NodeVM)x.Clone()).ToList());

    //    var edges = new ObservableCollection<EdgeVM>();
    //    foreach (var edgeVM in EdgesVM)
    //    {
    //        var nodeFrom = clone._nodesDict[edgeVM.NodeFrom.Node.Id];
    //        var nodeTo = clone._nodesDict[edgeVM.NodeTo.Node.Id];
    //        edges.Add(new EdgeVM(nodeFrom, nodeTo, edgeVM.Id));
    //        edges.Last().Color = edgeVM.Color;
    //    }
    //    clone.EdgesVM = edges;
    //    return clone;
    //}



    public void Clear()
    {
        _graph.Nodes.Clear();
        _graph.Edges.Clear();
        _nodesVM.Clear();
        _edgesVM.Clear();
        _nodesDict.Clear();
        _edgesDict.Clear();
        _graph.StartNode = null;
        _graph.EndNode = null;
    }


    //YUN PART  YUN PART YUN PART YUN PART YUN PART YUN PART 



    public void UpdateEdgeWeight(int edgeId, double newWeight)
    {
        if (_edgesDict.ContainsKey(edgeId))
        {
            var edgeVM = _edgesDict[edgeId];
            edgeVM.Weight = newWeight;
            var edge = _graph.Edges.FirstOrDefault(e => e.Id == edgeId);
            if (edge != null)
            {
                edge.Weight = newWeight;
            }
            OnPropertyChanged(nameof(EdgesVM));
        }
        else
        {
            Console.WriteLine($"Edge with ID {edgeId} not found in the dictionary.");
        }
    }




    public void AddEdge(NodeVM nodeFrom, NodeVM nodeTo)
    {
        int newId = _edgesVM.Count == 0 ? 0 : _edgesVM.Count;
        double defaultWeight = 10;
        var edge = new Edge(nodeFrom.Node, nodeTo.Node, id: newId, weight: defaultWeight);
        nodeFrom.Node.Edges.Add(edge);
        nodeTo.Node.Edges.Add(edge);
        _graph.Edges.Add(edge);
        var edgeVM = new EdgeVM(nodeFrom, nodeTo, newId);
        edgeVM.Weight = defaultWeight; // Установите вес ребра
        _edgesVM.Add(edgeVM);
        _edgesDict[newId] = edgeVM; // Обновите словарь
        OnPropertyChanged(nameof(EdgesVM));
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
            edges.Add(new EdgeVM(nodeFrom, nodeTo, edgeVM.Id));
            edges.Last().Color = edgeVM.Color;
            edges.Last().Weight = edgeVM.Weight; // Сохраняем вес ребра
        }
        clone.EdgesVM = edges;
        return clone;
    }



}