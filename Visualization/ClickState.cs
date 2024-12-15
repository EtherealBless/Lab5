using System.Collections.Generic;

namespace GraphEditor.Visualization;

class ClickState
{
    private object _clickedObject = null;
    public object ClickedObject { get => _clickedObject; set => _clickedObject = value; }
    public ClickState() { }
    public ClickState(int x, int y, object clickedObject) { X = x; Y = y; ClickedObject = clickedObject; }
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;



}