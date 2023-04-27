using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Glorysoft.BC.Client.View
{
    /// <summary>
    /// ControlStateSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ControlStateSetting : Window
    {
        public ControlStateSetting()
        {
            InitializeComponent();
        }

        private void Host_Communication_Setting_Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MainTitle_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
