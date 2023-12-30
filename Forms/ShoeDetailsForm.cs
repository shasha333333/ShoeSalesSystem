using ShoeSalesSystem.Entities;
using ShoeSalesSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoeSalesSystem.Forms
{
    public partial class ShoeDetailsForm : Form
    {
        Shoe shoe;

        // 定义委托
        public delegate void RefreshEventHandler(object sender, EventArgs e);

        // 定义事件
        public event RefreshEventHandler RefreshMain;

        public ShoeDetailsForm(Shoe shoe)
        {
            InitializeComponent();
            this.shoe = shoe;
            
            // 在窗口加载时显示女鞋的详细信息
            DisplayShoeDetails();
            this.FormClosed += ShoeDetailsForm_FormClosed;
        }

        private void ShoeDetailsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 当窗口关闭时触发事件
            RefreshMain?.Invoke(this, EventArgs.Empty);
        }


        private async void DisplayShoeDetails()
        {
            // 在这里设置控件的文本、图像等属性，展示女鞋的详细信息
            labelModel.Text = $"Model: {shoe.ShoeModel}";
            labelOrigin.Text = $"Origin: {shoe.Origin}";
            labelPrice.Text = $"Price: {shoe.Price:C}";
            labelStockQuantity.Text = $"Stock Quantity: {shoe.StockQuantity}";
            shoe.ImagePath = Path.Combine("..", "..", "Resources", "Images", shoe.ShoeModel+".jfif");
            await LoadImageAsync(pictureBox1, shoe.ImagePath);
            // TODO: 获取并显示订购该女鞋的所有客户信息
        }

        private void ShoeDetailsForm_Load(object sender, EventArgs e)
        {

        }

        private async Task LoadImageAsync(PictureBox pictureBox, string imagePath)
        {
            try
            {
                // 异步加载图片
                pictureBox.Image = await Task.Run(() => Image.FromFile(imagePath));
            }
            catch (Exception ex)
            {
                // 加载失败时，设置默认图像或执行其他逻辑
                Console.WriteLine($"Error loading image: {ex.Message}");
                pictureBox.Image = Properties.Resources.Boom; // 替换成你的默认图像资源
            }
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            OrderInputDialog orderDialog = new OrderInputDialog(shoe.ShoeId);
            if (orderDialog.ShowDialog() == DialogResult.OK)
            {
                // 用户点击了确定按钮
                Order order = orderDialog.Order;

                // 在处理订单前获取当前鞋子库存数量
                int initialStockQuantity = shoe.StockQuantity;

                // 检查订购量是否超过库存
                if (order.QuantityOrdered > initialStockQuantity)
                {
                    MessageBox.Show("货物不足，请重新输入订购量。", "库存不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
       
                else
                {
                    // 处理订单，例如更新库存等
                    if (SqlHelper.SubStock(order.ShoeId, order.QuantityOrdered))
                    {
                        if (SqlHelper.AddCustomerOrders(order))
                        {
                            MessageBox.Show("订单成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.shoe = SqlHelper.GetShoe(order.ShoeId);
                            DisplayShoeDetails();
                        }
                        else
                        {
                            // 处理添加订单失败的情况
                            MessageBox.Show("添加订单失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // 在订单数量等于库存的情况下删除鞋子
                        if (order.QuantityOrdered == initialStockQuantity)
                        {
                            SqlHelper.DeleteShoe(order.ShoeId);
                        }
                    }
                    else
                    {
                        // 处理减少库存失败的情况
                        MessageBox.Show("减少库存失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (CurShoeOrdersForm CurShoeOrdersForm = new CurShoeOrdersForm(shoe.ShoeId))
            {
                CurShoeOrdersForm.ShowDialog();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
