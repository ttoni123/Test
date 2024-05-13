namespace SftpXmlTask.UIWinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RefreshOrdersButton = new Button();
            OrdersList = new ListView();
            CustomerName = new ColumnHeader();
            CustomerOIB = new ColumnHeader();
            OrderedProducts = new ColumnHeader();
            PathToFile = new TextBox();
            ImportOrdersButton = new Button();
            ExportOrdersButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // RefreshOrdersButton
            // 
            RefreshOrdersButton.Location = new Point(547, 131);
            RefreshOrdersButton.Name = "RefreshOrdersButton";
            RefreshOrdersButton.Size = new Size(156, 42);
            RefreshOrdersButton.TabIndex = 0;
            RefreshOrdersButton.Text = "Refresh orders";
            RefreshOrdersButton.UseVisualStyleBackColor = true;
            RefreshOrdersButton.Click += button1_Click;
            // 
            // OrdersList
            // 
            OrdersList.Location = new Point(48, 48);
            OrdersList.Name = "OrdersList";
            OrdersList.Size = new Size(455, 290);
            OrdersList.TabIndex = 1;
            OrdersList.UseCompatibleStateImageBehavior = false;
            OrdersList.View = View.Details;
            OrdersList.Columns.Add("name").Width = 75;
            OrdersList.Columns.Add("Oib").Width = 125;
            OrdersList.Columns.Add("products").Width = 255;
            // 
            // CustomerName
            // 
            CustomerName.Tag = "Customer name";
            // 
            // PathToFile
            // 
            PathToFile.Location = new Point(48, 361);
            PathToFile.Name = "PathToFile";
            PathToFile.Size = new Size(311, 27);
            PathToFile.TabIndex = 2;
            PathToFile.Text = "Path to file";
            // 
            // ImportOrdersButton
            // 
            ImportOrdersButton.Location = new Point(547, 346);
            ImportOrdersButton.Name = "ImportOrdersButton";
            ImportOrdersButton.Size = new Size(156, 42);
            ImportOrdersButton.TabIndex = 3;
            ImportOrdersButton.Text = "Import orders";
            ImportOrdersButton.UseVisualStyleBackColor = true;
            ImportOrdersButton.Click += ImportOrdersButton_Click;
            // 
            // ExportOrdersButton
            // 
            ExportOrdersButton.Location = new Point(547, 231);
            ExportOrdersButton.Name = "ExportOrdersButton";
            ExportOrdersButton.Size = new Size(156, 42);
            ExportOrdersButton.TabIndex = 4;
            ExportOrdersButton.Text = "Export orders";
            ExportOrdersButton.UseVisualStyleBackColor = true;
            ExportOrdersButton.Click += ExportOrdersButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 25);
            label1.Name = "label1";
            label1.Size = new Size(53, 20);
            label1.TabIndex = 5;
            label1.Text = "Orders";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(ExportOrdersButton);
            Controls.Add(ImportOrdersButton);
            Controls.Add(PathToFile);
            Controls.Add(OrdersList);
            Controls.Add(RefreshOrdersButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button RefreshOrdersButton;
        private ListView OrdersList;
        private TextBox PathToFile;
        private Button ImportOrdersButton;
        private Button ExportOrdersButton;
        private Label label1;
        private ColumnHeader CustomerName;
        private ColumnHeader CustomerOIB;
        private ColumnHeader OrderedProducts;
    }
}
