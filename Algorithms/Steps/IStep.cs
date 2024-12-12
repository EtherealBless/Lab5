using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphEditor.Algorithms.Steps
{
    public interface IStep
    {
        Color Color { get; }
        bool IsPermanent { get; }
    }
}
