using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Glorysoft.BC.Client.View
{
    /// <summary>
    /// MaterialHistory.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialHistory : Window
    {
        public MaterialHistory()
        {
            InitializeComponent();
        }

        private void grdMaterial_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;    //设置行表头的内容值
        }
    }
}
