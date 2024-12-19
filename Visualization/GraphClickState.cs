using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using GraphEditor.ViewModels;

namespace GraphEditor.Visualization;

enum ClickStateType
{
    None,
    EditNode,
    EditEdge,
    NewNode,
    NewEdge
}



class NewNodeClick : ClickState
{
    public new int X { get; }
    public new int Y { get; }

    public NewNodeClick(int x, int y) { X = x; Y = y; }
}

class NewEdgeClick : ClickState
{
    public NodeVM NodeFrom { get; }
    public NodeVM NodeTo { get; }

    public NewEdgeClick(NodeVM nodeFrom, NodeVM nodeTo) { NodeFrom = nodeFrom; NodeTo = nodeTo; }
}

class EditNodeClick : ClickState
{
    public NodeVM Node { get; }
    public EditNodeClick(NodeVM node) { Node = node; }
}

class EditEdgeClick : ClickState
{
    public EdgeVM Edge { get; }
    public EditEdgeClick(EdgeVM edge) { Edge = edge; }
}

class GraphClickStateManager
{
    private readonly List<ClickState> _clickStates = new();
    public void Clear() => _clickStates.Clear();
    /*

    Possible click order:
        1. None - Node1 - Node2 -- Add edge from Node1 to Node2 +
    */
    public ClickState? ProcessClick(int x, int y, Canvas canvas)
    {
        Console.WriteLine("Canvas, gotcha");
        if (_clickStates.Last().ClickedObject is Canvas)
        {
            var lastClick = _clickStates.Last();
            if ((lastClick.X - x) * (lastClick.X - x) + (lastClick.Y - y) * (lastClick.Y - y) < 100)
            {
                _clickStates.Clear();
                return new NewNodeClick(x, y);
            }
            else
            {
                _clickStates.Clear();
                return null;
            }
        }
        if (_clickStates.Count == 0)
        {
            _clickStates.Add(new ClickState());
            return null;
        }
        // neither new node nor empty click state
        _clickStates.Clear();
        _clickStates.Add(new ClickState(x, y, canvas));
        return null;
    }
    public NewEdgeClick? ProcessClick(int x, int y, NodeVM nodeVM)
    {
        if (_clickStates.Count == 0)
        {
            _clickStates.Add(new ClickState(x, y, nodeVM));
            return null;
        }
        if (_clickStates.Count == 1)
        {
            var lastClick = _clickStates.Last();
            if (lastClick.ClickedObject is NodeVM)
            {
                var lastNodeClick = (NodeVM)lastClick.ClickedObject;
                if (lastNodeClick.Node.Id == nodeVM.Node.Id)
                {
                    return null;
                }
                _clickStates.Clear();
                return new NewEdgeClick(lastNodeClick, nodeVM);
            }
        }
        _clickStates.Clear();
        return null;
    }
    public EditNodeClick? ProcessDoubleClick(int x, int y, NodeVM nodeVM)
    {
        _clickStates.Clear();
        return new EditNodeClick(nodeVM);
    }
    public ClickState? ProcessDoubleClick(int x, int y, EdgeVM edgeVM)
    {
        _clickStates.Clear();
        return new EditEdgeClick(edgeVM);
    }
    public ClickState? ProcessDoubleClick(int x, int y, Canvas canvas)
    {
        _clickStates.Clear();
        return new NewNodeClick(x, y);
    }
}