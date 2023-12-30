using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShoeSalesSystem.Entities;
using ShoeSalesSystem.Helpers;
using System.Drawing.Printing;
using System.IO;

namespace ShoeSalesSystem.Forms
{
    public partial class MainForm : Form
    {
        List<Shoe> shoes;
        private const int pageSize = 10; // 每次加载的数据量
        private int pageIndex = 0; // 当前加载的页数
        private bool isLoading = false; // 是否正在加载数据

        public MainForm()
        {
            InitializeComponent();
            //LoadAllShoes();
            //pageIndex = 100;
            LoadShoesAsync();
            // 绑定滚动条事件
            this.tableLayoutPanel1.Scroll += TableLayoutPanel1_Scroll;
            this.DoubleBuffered = true;
        }

        private async void TableLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            // 判断是否滚动到底部
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll &&
                tableLayoutPanel1.VerticalScroll.Value + tableLayoutPanel1.Height >= tableLayoutPanel1.VerticalScroll.Maximum &&
                !isLoading)
            {
                // 加载更多数据
                isLoading = true;
                await LoadMoreShoes();
                isLoading = false;
            }
        }

        private async Task LoadMoreShoes()
        {
            // 使用异步任务加载更多数据
            List<Shoe> moreShoes = await Task.Run(() => SqlHelper.GetWomenShoesPaged(pageIndex, pageSize));

            /* int row = 0;
             int col = 0;

             //动态增加tableLayoutPanel的网格数
             for (int i = 0; i < pageSize; i++)
             {
                 if (++col == tableLayoutPanel1.ColumnCount)
                 {
                     if (++row == tableLayoutPanel1.RowCount)
                     {
                         tableLayoutPanel1.RowCount++;
                         tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                     }
                     col = 0;
                 }
             }*/

            if (moreShoes.Count > 0)
            {
                await AddMorePictureBox(moreShoes);
                // 更新页数
                pageIndex++;
            }
            else
            {
                // 没有更多数据可加载
                MessageBox.Show("No more data to load.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task AddMorePictureBox(List<Shoe> moreShoes)
        {
            int i = 1;
            // 添加新加载的数据到 tableLayoutPanel1
            foreach (Shoe shoe in moreShoes)
            {
                PictureBox pictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(100, 100),
                    Tag = shoe
                };
                tableLayoutPanel1.Controls.Add(pictureBox);
                // 异步加载图片
                shoe.ImagePath = Path.Combine("..", "..", "Resources", "Images", shoe.ShoeModel+".jfif");
                await LoadImageAsync(pictureBox, shoe.ImagePath);
                pictureBox.Click += ShoePictureBox_Click;

                Label label = new Label();

                // 设置Label的文本为数字，这里使用行列的索引作为示例
                label.Text = (pageIndex * pageSize + i).ToString();

                // 将Label添加到TableLayoutPanel的特定单元格中
                pictureBox.Controls.Add(label);
                i++;
            }
        }

        private async Task LoadShoes()
        {
            // 分页加载女鞋信息
            shoes = await Task.Run(() => SqlHelper.GetWomenShoesPaged(pageIndex, pageSize));

            //int count = SqlHelper.GetWomenShoesNum();
            //Console.WriteLine(count);

            /*    int i = 1;
                // 异步加载每双鞋的信息和图片
                foreach (Shoe shoe in shoes)
                {
                    PictureBox pictureBox = new PictureBox
                    {
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Size = new Size(100, 100),
                        Tag = shoe,
                    };

                    tableLayoutPanel1.Controls.Add(pictureBox);

                    // 异步加载图片
                    await LoadImageAsync(pictureBox, shoe.ImagePath);
                    pictureBox.Click += ShoePictureBox_Click;



                    Label label = new Label();
                    // 设置Label的文本为数字，这里使用行列的索引作为示例
                    label.Text = (pageIndex * pageSize + i).ToString();
                    // 将Label添加到TableLayoutPanel的特定单元格中
                    pictureBox.Controls.Add(label);
                    i++;
                }*/
            //异步加载PictureBox
            await AddMorePictureBox(shoes);
        }

     /*   //根据每页展示数动态加载网格
        private void LoadMoreGrid()
        {
            int row = 0;
            int col = 0;

            for (int i = 0; i < pageSize; i++)
            {
                if (++col == tableLayoutPanel1.ColumnCount)
                {
                    if (++row == tableLayoutPanel1.RowCount)
                    {
                        tableLayoutPanel1.RowCount++;
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    }
                    col = 0;
                }
            }
        }*/

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

        // 调用示例
        private async void LoadShoesAsync()
        {
            await LoadShoes();
            pageIndex++;
        }


        private async void LoadAllShoes()
        {

            shoes = await Task.Run(() => SqlHelper.GetAllWomenShoes());

            int count = SqlHelper.GetWomenShoesNum();
            Console.WriteLine(count);

            int row = 0;
            int col = 0;

            for (int i = 0; i < count; i++)
            {
                if (++col == tableLayoutPanel1.ColumnCount)
                {
                    if (++row == tableLayoutPanel1.RowCount)
                    {
                        //Console.WriteLine("RowCount++");
                        tableLayoutPanel1.RowCount++;
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    }
                    col = 0;
                }
            }


            await AddMorePictureBox(shoes);

            /*   try
               {
                   PictureBox pictureBox1 = new PictureBox
                   {
                       //Image = Image.FromFile("D:\\Work Files\\C# program\\repo\\ShoeSalesSystem\\Resources\\Images\\Boom.png"),
                       Image = Properties.Resources.Boom,
                       SizeMode = PictureBoxSizeMode.StretchImage,
                       Size = new Size(1000, 1000)
                   };
                   pictureBox1.BringToFront();
                   this.tableLayoutPanel1.Controls.Add(pictureBox1);
               }
               catch (Exception ex)
               {
                   MessageBox.Show("Error loading image: " + ex.Message);
               }*/
            /*    try
                {
                    PictureBox pictureBox1 = new PictureBox
                    {
                        Image = Properties.Resources.Boom,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Size = new Size(1000, 1000)
                    };

                    pictureBox1.Click += ShoePictureBox_Click;

                    int row = tableLayoutPanel1.RowCount;
                    int col = tableLayoutPanel1.ColumnCount;

                    tableLayoutPanel1.Controls.Add(pictureBox1, col, row);

                    if (++col == tableLayoutPanel1.ColumnCount)
                    {
                        tableLayoutPanel1.RowCount++;
                        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                        col = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }*/
        }

        private async Task LoadImageAsync(string imagePath,PictureBox pictureBox)
        {
            pictureBox.Image = await Task.Run(() => Image.FromFile(imagePath));
        }

        private void ShowWindow_RefreshMain(object sender, EventArgs e)
        {
            // 在这里执行刷新MainWindow的代码
            // 例如，调用刷新方法或重新加载数据
            //LoadShoes();
            tableLayoutPanel1.Controls.Clear();
            pageIndex = 0;
            tableLayoutPanel1.RowCount = 0;
            LoadShoesAsync();
        }

        private void ShoePictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox && pictureBox.Tag is Shoe clickedShoe)
            {
                // 创建并显示 ShoeDetailsForm 窗口
                using (ShoeDetailsForm shoeDetailsForm = new ShoeDetailsForm(clickedShoe))
                {
                    shoeDetailsForm.RefreshMain += ShowWindow_RefreshMain;

                    shoeDetailsForm.ShowDialog();
                }
            }
        }

        private void ShoeDetailsForm_DetailsFormClosed(object sender, EventArgs e)
        {
            // 刷新主窗口
            //LoadShoes();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
