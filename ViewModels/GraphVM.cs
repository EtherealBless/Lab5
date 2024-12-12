using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using GraphEditor.Algorithms.Steps;
using GraphEditor.Algorithms.Steps.Nodes;

namespace GraphEditor.ViewModels;

public class GraphVM : BaseVM
{
    private Graph _graph;
    private List<NodeVM> _nodesVM = new List<NodeVM>();
    private List<EdgeVM> _edgesVM = new List<EdgeVM>();
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

    public List<NodeVM> NodesVM
    {
        get
        {
            return _nodesVM;
        }
        set
        {
            _nodesVM = value;
            _nodesDict.Clear();
            _nodesVM.ForEach(x => _nodesDict.Add(x.Node.Id, x));
            OnPropertyChanged();
        }
    }
    public List<EdgeVM> EdgesVM
    {
        get
        {
            return _edgesVM;
        }
        set
        {
            _edgesVM = value;
            OnPropertyChanged();
        }
    }

    private Dictionary<int, NodeVM> _nodesDict = new Dictionary<int, NodeVM>();

    public GraphVM(Graph graph)
    {
        Graph = graph;
    }

    public void ChangeColor(Node node, Color color)
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

}