namespace ShoeSalesSystem.Forms
{
    partial class ShoeDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelModel = new System.Windows.Forms.Label();
            this.labelOrigin = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelStockQuantity = new System.Windows.Forms.Label();
            this.buttonBuy = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(924, 144);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(82, 24);
            this.labelModel.TabIndex = 0;
            this.labelModel.Text = "label1";
            // 
            // labelOrigin
            // 
            this.labelOrigin.AutoSize = true;
            this.labelOrigin.Location = new System.Drawing.Point(924, 261);
            this.labelOrigin.Name = "labelOrigin";
            this.labelOrigin.Size = new System.Drawing.Size(82, 24);
            this.labelOrigin.TabIndex = 1;
            this.labelOrigin.Text = "label2";
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(924, 388);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(82, 24);
            this.labelPrice.TabIndex = 2;
            this.labelPrice.Text = "label3";
            // 
            // labelStockQuantity
            // 
            this.labelStockQuantity.AutoSize = true;
            this.labelStockQuantity.Location = new System.Drawing.Point(924, 525);
            this.labelStockQuantity.Name = "labelStockQuantity";
            this.labelStockQuantity.Size = new System.Drawing.Size(82, 24);
            this.labelStockQuantity.TabIndex = 3;
            this.labelStockQuantity.Text = "label4";
            // 
            // buttonBuy
            // 
            this.buttonBuy.Location = new System.Drawing.Point(359, 786);
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.Size = new System.Drawing.Size(240, 72);
            this.buttonBuy.TabIndex = 5;
            this.buttonBuy.Text = "购买";
            this.buttonBuy.UseVisualStyleBackColor = true;
            this.buttonBuy.Click += new System.EventHandler(this.buttonBuy_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(928, 660);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 53);
            this.button1.TabIndex = 6;
            this.button1.Text = "显示订购顾客";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(135, 144);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(502, 437);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ShoeDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 1031);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonBuy);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelStockQuantity);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.labelOrigin);
            this.Controls.Add(this.labelModel);
            this.Name = "ShoeDetailsForm";
            this.Text = "ShoeDetailsForm";
            this.Load += new System.EventHandler(this.ShoeDetailsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label labelOrigin;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelStockQuantity;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonBuy;
        private System.Windows.Forms.Button button1;
    }
}