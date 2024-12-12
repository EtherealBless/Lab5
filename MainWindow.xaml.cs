using GraphEditor.Algorithms;
using GraphEditor.Constants;
using GraphEditor.ViewModels;
using GraphEditor.Visualization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Linq;

namespace GraphEditor
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private Graph _graph;
        private GraphVM _graphVM;
        public GraphVM GraphVM
        {
            get
            {
                return _graphVM;
            }
            set
            {
                _graphVM = value;
                OnPropertyChanged();
            }
        }

        private DispatcherTimer _timer = new DispatcherTimer();
        public event PropertyChangedEventHandler PropertyChanged;

        VisualizationManager visualizationManager;

        private bool _isRunning = false;

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        private void InitGraph()
        {

            _graph = new Graph();

            var Nodes = _graph.Nodes;
            var Edges = _graph.Edges;

            Nodes.Add(new Node(1, "1"));
            Nodes.Add(new Node(2, "23"));
            Nodes.Add(new Node(3, "3"));

            Edges.Add(new Edge(Nodes[0], Nodes[1]));
            Edges.Add(new Edge(Nodes[0], Nodes[2]));
            Edges.Add(new Edge(Nodes[1], Nodes[2]));

            GraphVM = new GraphVM(_graph);

            var NodesVM = new List<NodeVM>();
            var EdgesVM = new List<EdgeVM>();

            NodesVM.Add(new NodeVM(Nodes[0], 100, 100));
            NodesVM.Add(new NodeVM(Nodes[1], 200, 100));
            NodesVM.Add(new NodeVM(Nodes[2], 300, 200));

            EdgesVM.Add(new EdgeVM(NodesVM[0], NodesVM[1]));
            EdgesVM.Add(new EdgeVM(NodesVM[0], NodesVM[2]));
            EdgesVM.Add(new EdgeVM(NodesVM[1], NodesVM[2]));

            GraphVM.NodesVM = NodesVM;
            GraphVM.EdgesVM = EdgesVM;
        }

        private Dictionary<string, IAlgorithm> _algorithms = new Dictionary<string, IAlgorithm>()
        {
            { "test", new TestAlgorithm() }
        };

        public Dictionary<string, IAlgorithm> Algorithms { get => _algorithms; }

        public IAlgorithm SelectedAlgorithm { get; set; }

        public RelayCommand<IAlgorithm> RunStopAlgorithm => new RelayCommand<IAlgorithm>(RunStop, CanRun);

        public bool CanRun(object _) => (SelectedAlgorithm != null && !IsRunning) || (IsRunning && SelectedAlgorithm != null);

        public async void RunStop(object obj)
        {
            if (IsRunning)
            {
                IsRunning = false;
                _timer.Stop();
                return;
            }
            _timer.Stop();
            Console.WriteLine("Run");
            IsRunning = true;
            visualizationManager = await VisualizationManager.WarmUp(GraphVM, SelectedAlgorithm);
            InitializeTimer();
        }

        public async void TimerTick(object? sender, EventArgs e)
        {
            if (!IsRunning || !visualizationManager.CanStepForward()) return;
            GraphVM = visualizationManager.StepForward();
            if (!visualizationManager.CanStepForward())
            {
                IsRunning = false;
            }
            Console.WriteLine("Tick");
        }

        public MainWindow()
        {
            InitGraph();
            InitializeComponent();
            AllocConsole();
            // InitializeTimer();

        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(200);
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
