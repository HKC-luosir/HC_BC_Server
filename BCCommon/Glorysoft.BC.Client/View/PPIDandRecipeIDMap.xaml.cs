using Glorysoft.BC.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// PPIDandRecipeIDMap.xaml 的交互逻辑
    /// </summary>
    public partial class PPIDandRecipeIDMap : Window
    {
        public ClientInfo GI
        {
            get
            {
                return ClientInfo.Current;
            }
        }
        public PPIDandRecipeIDMap()
        {
            //InitializeComponent();
            List<EQPInfo> eqpList = new List<EQPInfo>();
            //foreach (var item in GI.OClient.EQPList.Where(o => o.Value.CheckRecipeFlag))
            //{
            //    eqpList.Add(item.Value);
            //}
            CreateGrid(eqpList);
        }
        private void CreateGrid(List<EQPInfo> eqpList)
        {
            Grid grid = new Grid();
            grid = GetGridRow(grid);
            List<Control> listcombox = new List<Control>();
            listcombox = CreatePPIDTextBox();
            //添加控件
            grid.Children.Add(listcombox[0]);
            grid.Children.Add(listcombox[1]);
            grid = GetGridRow(grid);
            //设置控件的行布局
            Grid.SetRow(listcombox[0], 0);
            Grid.SetRow(listcombox[1], 0);
            //设置控件的列布局
            Grid.SetColumn(listcombox[0], 0);
            Grid.SetColumn(listcombox[1], 1);
            int i = 1;
            foreach (var eqp in eqpList)
            {
                listcombox = CreateComboBox(eqp);
                //添加控件
                grid.Children.Add(listcombox[0]);
                grid.Children.Add(listcombox[1]);
                grid = GetGridRow(grid);
                //设置控件的行布局
                Grid.SetRow(listcombox[0], i);
                Grid.SetRow(listcombox[1], i);
                //设置控件的列布局
                Grid.SetColumn(listcombox[0], 0);
                Grid.SetColumn(listcombox[1], 1);

                i++;
            }
            grid.RowDefinitions.RemoveAt(grid.RowDefinitions.Count - 1);
            //GroupBoxEQP.Content = grid;
        }
        private List<Control> CreateComboBox(EQPInfo eqpInfo)
        {
            List<Control> listcombox = new List<Control>();
            Label lb1 = new Label();
            lb1.FontSize = 16;
            lb1.HorizontalContentAlignment = HorizontalAlignment.Left;
           // lb1.Content = eqpInfo.EQPName + " :";

            Glorysoft.BC.Entity.Recipe recipe = new Glorysoft.BC.Entity.Recipe();
            recipe.LineID = eqpInfo.LineID;
            recipe.EQPID = eqpInfo.EQPID;
           // recipe.EQPName = eqpInfo.EQPName;
           // recipe.UnitID = eqpInfo.UnitID;

            //var recipeList = ClientRequest.FindRecipeIDByEQPID(recipe);
            //ObservableCollection<Glorysoft.BC.Entity.Recipe> oRecipe = new ObservableCollection<Glorysoft.BC.Entity.Recipe>();
            //foreach (var r in recipeList)
            //{
            //    oRecipe.Add(r);
            //}

            ComboBox cb = new ComboBox();
            cb.IsReadOnly = true;
            //cb.Name = eqpInfo.EQPName;
           // cb.ItemsSource = oRecipe;
            cb.DisplayMemberPath = "RecipeID";
            //cb.Style = (Style)this.FindResource("CboCommomStyle");
            cb.Height = 25;
            cb.Width = 100;
            cb.FontSize = 16;

            //cb2.ItemsSource = ConnectionType;
            listcombox.Add(lb1);
            listcombox.Add(cb);
            return listcombox;

        }
        private List<Control> CreatePPIDTextBox()
        {
            List<Control> listcombox = new List<Control>();
            Label lb1 = new Label();
            lb1.FontSize = 16;
            lb1.HorizontalContentAlignment = HorizontalAlignment.Left;
            lb1.Content = "PPID:";

            TextBox tb = new TextBox();
            tb.Name = "PPID";
            //cb.Style = (Style)this.FindResource("CboCommomStyle");
            tb.Height = 25;
            tb.Width = 100;
            tb.FontSize = 16;

            //cb2.ItemsSource = ConnectionType;
            listcombox.Add(lb1);
            listcombox.Add(tb);
            return listcombox;

        }
        private Grid GetGridRow(Grid grid)
        {
            //添加1行
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(50, GridUnitType.Pixel);
            grid.RowDefinitions.Add(row);
            //添加7列
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            //设置列宽(百分比显示暂时无法实现，暂设固定宽)
            col1.Width = new GridLength(150, GridUnitType.Pixel);
            col2.Width = new GridLength(150, GridUnitType.Pixel);


            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);

            return grid;
        }
        private void grdMaterial_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;    //设置行表头的内容值
        }
        private Dictionary<string, string> GetComboxValue(out string PPID)
        {
            PPID = "";
            Dictionary<string, string> valuesDic = new Dictionary<string, string>();
            //var recipeBox = GroupBoxEQP.Content as Grid;
            //for (int i = 0; i < recipeBox.Children.Count; i++)
            //{
            //    if (PPID == "")
            //    {
            //        TextBox tb = recipeBox.Children[i] as TextBox;
            //        if (tb != null)
            //            PPID = tb.Text;
            //    }
            //    ComboBox cb = recipeBox.Children[i] as ComboBox;
            //    if (cb != null)
            //    {
            //        valuesDic.Add(cb.Name, cb.Text);
            //    }
            //}
            return valuesDic;
        }
        private void SetComboxValue(string name, string value)
        {
            //var recipeBox = GroupBoxEQP.Content as Grid;
            //// 这个方法是循环Grid下所有控件的方法
            ////FindName 方法只能查询在XAML中定义的组件，后台动态添加的需要手动写循环来处理
            //for (int i = 0; i < recipeBox.Children.Count; i++)
            //{
            //    ComboBox cb = recipeBox.Children[i] as ComboBox;
            //    if (cb != null && cb.Name == name)
            //    {
            //        cb.Text = value;
            //    }
            //    if (name == "PPID")
            //    {
            //        TextBox tb = recipeBox.Children[i] as TextBox;
            //        if (tb != null && tb.Name == name)
            //        {
            //            tb.Text = value;
            //        }
            //    }
            //}
        }
        //private void DG_PPIDList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var index = DG_PPIDList.SelectedIndex;
        //    if (index == -1)
        //        return;
        //    DataRowView mySelectedElement = (DataRowView)DG_PPIDList.SelectedItem;
        //    for (int i = 0; i < mySelectedElement.Row.ItemArray.Count(); i++)
        //    {
        //        var value = mySelectedElement.Row[i].ToString();
        //        var name = DG_PPIDList.Columns[i].Header.ToString();
        //        if (name != "LineID")
        //        {
        //            SetComboxValue(name, value);
        //        }
        //    }
        //}
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string ppid = "";
            var recipeList = GetComboxValue(out ppid);
            if (string.IsNullOrWhiteSpace(ppid))
            {
                MessageBox.Show("PPID不能为空！");
                return;
            }
            if (recipeList.Count == 0)
            {
                MessageBox.Show("未选择相应设备的RecipeID！");
                return;
            }
            //var ppidList = ClientRequest.ViewPPIDAndRecipeListByPPID(ppid);
            //if (ppidList.Count() > 0)
            //{
            //    MessageBox.Show("PPID已存在,请在左边列表内选择该PPID,\n\r选择相应设备的RecipeID,点击\"Modify\"\n\r按钮进行修改。");
            //    return;
            //}
            var newPPIDList = new List<PPIDInfo>();
            foreach (var eqpName in recipeList.Keys)
            {
                PPIDInfo pr = new PPIDInfo();
                var eqpInfo = GI.OClient.EQPList.FirstOrDefault(o => o.Key == eqpName).Value;
                if (!string.IsNullOrWhiteSpace(recipeList[eqpName]) && eqpInfo != null)
                {
                    pr.LineID = eqpInfo.LineID;
                    pr.EQPID = eqpInfo.EQPID;
                    //pr.EQPName = eqpInfo.EQPName;
                    pr.CreateDate = DateTime.Now;
                    pr.RecipeID = recipeList[eqpName];
                    pr.PPID = ppid;
                    newPPIDList.Add(pr);
                }
            }
            //ClientRequest.UpdatePPIDAndRecipe(newPPIDList);
            //ClientRequest.ViewPPIDAndRecipeListByLine(GI.OClient.LineID);
            MessageBox.Show("新增成功！");
        }
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            //var index = DG_PPIDList.SelectedIndex;
            //if (index == -1)
            //{
            //    MessageBox.Show("未选中左边任何一栏PPID，请选中之后进行此操作！");
            //    return;
            //}
            MessageBoxResult dr = MessageBox.Show("确认修改该PPID与设备的RecipeID对应关系吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                string ppid = "";
                var recipeList = GetComboxValue(out ppid);
                var newPPIDList = new List<PPIDInfo>();
                foreach (var eqpName in recipeList.Keys)
                {
                    PPIDInfo pr = new PPIDInfo();
                    var eqpInfo = GI.OClient.EQPList.FirstOrDefault(o => o.Key == eqpName).Value;
                    if (!string.IsNullOrWhiteSpace(recipeList[eqpName]) && eqpInfo != null)
                    {
                        pr.LineID = eqpInfo.LineID;
                        pr.EQPID = eqpInfo.EQPID;
                       // pr.EQPName = eqpInfo.EQPName;
                        pr.CreateDate = DateTime.Now;
                        pr.RecipeID = recipeList[eqpName];
                        pr.PPID = ppid;
                        newPPIDList.Add(pr);
                    }
                }
                //ClientRequest.UpdatePPIDAndRecipe(newPPIDList);
                //ClientRequest.ViewPPIDAndRecipeListByLine(GI.OClient.LineID);
                MessageBox.Show("修改成功！");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //var index = DG_PPIDList.SelectedIndex;
            //if (index == -1)
            //{
            //    MessageBox.Show("未选中左边任何一栏PPID，请选中之后进行此操作！");
            //    return;
            //}
            MessageBoxResult dr = MessageBox.Show("确认删除该PPID与设备的RecipeID对应关系吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                string ppid = "";
                var recipeList = GetComboxValue(out ppid);
                //ClientRequest.DeletePPIDAndRecipeIDByPPID(ppid);
                //ClientRequest.ViewPPIDAndRecipeListByLine(GI.OClient.LineID);
                MessageBox.Show("删除成功！");
            }
        }
    }
}
