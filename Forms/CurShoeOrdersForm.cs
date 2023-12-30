using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShoeSalesSystem.Helpers;

namespace ShoeSalesSystem.Forms
{
    public partial class CurShoeOrdersForm : Form
    {
        int shoeID;
        public CurShoeOrdersForm(int shoeID)
        {
            InitializeComponent();
            this.shoeID = shoeID;
            // 在窗体加载时加载数据
            LoadData();
        }

        private void LoadData()
        {
            // 获取特定鞋子的订单信息
            var orders = SqlHelper.GetAllOrders(shoeID);

            // 将数据绑定到 DataGridView
            dataGridView1.DataSource = orders;

            // 设置列头
            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.Columns[0].HeaderText = "订单号";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "客户名";
            dataGridView1.Columns[3].HeaderText = "客户电话";
            dataGridView1.Columns[4].HeaderText = "预定数量";
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
