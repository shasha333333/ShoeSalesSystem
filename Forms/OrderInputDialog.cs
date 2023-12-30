using ShoeSalesSystem.Entities;
using ShoeSalesSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoeSalesSystem.Forms
{
    public partial class OrderInputDialog : Form
    {
        public Order Order { get; private set; }
        public int shoeID;
        public OrderInputDialog(int shoeID)
        {
            InitializeComponent();
            this.shoeID = shoeID;
        }

        private bool ValidateInput()
        {
            // 在此处进行输入验证，确保输入有效性
            // 这里只是一个简单的示例，你可能需要更严格的验证规则
            if (string.IsNullOrWhiteSpace(textBoxCustomerName.Text) ||
                string.IsNullOrWhiteSpace(textBoxContactNumber.Text) ||
                numericUpDownQuantity.Value <= 0 )
            {
                MessageBox.Show("请填写所有必要信息。", "输入无效", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // 其他事件处理和方法
        private void OrderInputDialog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Order = new Order
                {
                    ShoeId = shoeID,
                    CustomerName = textBoxCustomerName.Text,
                    ContactNumber = textBoxContactNumber.Text,
                    QuantityOrdered = (int)numericUpDownQuantity.Value
                };

                
                DialogResult = DialogResult.OK;
            }
        }

        private void numericUpDownQuantity_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
