using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace Glorysoft.BC.Client.CommonClass
{
    public static class ButtonBaseExtensions
    {
        public static void PerformClick(this System.Windows.Controls.Button button)
        {
            ButtonAutomationPeer buttonPeer = new ButtonAutomationPeer(button);
            IInvokeProvider invokeProvider = buttonPeer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProvider.Invoke();
        }
    }
}
