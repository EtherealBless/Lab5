using GraphEditor.Algorithms;
using GraphEditor.Constants;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;

namespace GraphEditor
{
    public partial class MainWindow : Window
    {

        public Graph Graph { get; } = new Graph();

        private void InitGraph()
        {
            var Nodes = Graph.Nodes;
            var Edges = Graph.Edges;
            Nodes.Add(new NodeVM(new Node(1, "1"), 100, 100));
            Nodes.Add(new NodeVM(new Node(2, "23"), 200, 100));
            Nodes.Add(new NodeVM(new Node(3, "3"), 300, 200));

            Edges.Add(new EdgeVM(Nodes[0], Nodes[1]));
            Edges.Add(new EdgeVM(Nodes[0], Nodes[2]));
            Edges.Add(new EdgeVM(Nodes[1], Nodes[2]));
        }
        
        private Dictionary<string, IAlgorithm> _algorithms = new Dictionary<string, IAlgorithm>()
        {
            { "test", new TestAlgorithm() }
        };

        public Dictionary<string, IAlgorithm> Algorithms {get => _algorithms;}

        public IAlgorithm CurrentAlgorithm { get; set; }

        public RelayCommand<IAlgorithm> RunAlgorithm => new RelayCommand<IAlgorithm>(Run, CanRun);

        public bool CanRun(object _) => CurrentAlgorithm != null;

        public void Run(object obj)
        {
            var algorithm = obj as IAlgorithm;
            algorithm.RunAlgorithm(Graph);
        }

        public MainWindow()
        {
            InitializeComponent();
            AllocConsole();
            InitGraph();

        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

    }
}
