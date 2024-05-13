using SftpXmlTask.Client;
using System;

namespace SftpXmlTask.UIWinForm
{
    public partial class Form1 : Form
    {

        private IClientDefinition _httpClientFactory;

        public Form1(IClientDefinition _httpClientFactory)
        {
            InitializeComponent();
            this._httpClientFactory = _httpClientFactory;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var orders = _httpClientFactory.PurchaseOrdersAsync().GetAwaiter().GetResult();
            OrdersList.Items.Clear();
            var item1 = new ListViewItem();

            OrdersList.Items.Add(item1);
            foreach (var order in orders)
            {
                item1 = new ListViewItem(new[] { order.Customer.FirstName, order.Customer.OIB, string.Join("\n ", order.Product.Select(g => g.ProductName + " x "+ g.Quantity.ToString())) });
                OrdersList.Items.Add(item1);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ImportOrdersButton_Click(object sender, EventArgs e)
        {
            try
            {
                _httpClientFactory.ImportOrdersAsync(PathToFile.Text).GetAwaiter().GetResult().ToString();

                MessageBox.Show("new orders imported. Refresh orders list.", "Orders imported",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occured",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportOrdersButton_Click(object sender, EventArgs e)
        {

            try
            {
                _httpClientFactory.ExportOrdersAsync().GetAwaiter().GetResult().ToString();


                MessageBox.Show("Exported orders", "Orders exported",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occured",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
