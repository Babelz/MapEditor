using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MapEditor.Configurers
{
    /// <summary>
    /// Common interface for editor to configure its controls for a specific editor type.
    /// </summary>
    public interface IGUIConfigurer
    {
        void Configure(Window window);
        void RemoveConfiguration(Window window);
    }
}
