using Glorysoft.BC.Entity;
using Glorysoft.BC.Server.ViewModel;
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

namespace Glorysoft.BC.Server.View
{
    /// <summary>
    /// LineMode.xaml 的交互逻辑
    /// </summary>
    public partial class LineMode : Window
    {
        public LineMode()
        {
            InitializeComponent();
            this.DataContext = new LineModeViewModel();
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
            catch(Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }
           
        }
    }
}
