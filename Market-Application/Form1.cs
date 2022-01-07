using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Market_Application
{
    public partial class Form1 : Form
    {
        string[,] products;
        int price = 0;
        int total = 0;
        int amount;
        string productname;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ReadBarcode();
        }

        private void ReadBarcode()
        {
            for (int i = 0; i < products.GetLength(0); i++)
            {
                if (txtBarcode.Text == products[i, 0])
                {
                    textBox2.Text = products[i, 1];
                    textBox3.Text = products[i, 2] + "$";
                    productname = products[i, 1];
                    price = int.Parse(products[i, 2]);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TakeProducts();
        }

        private void TakeProducts()
        {
            string path = Application.StartupPath + "\\products.txt";
            string[] lines = File.ReadAllLines(path);
            products = new string[lines.Length, 3];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] brace = lines[i].Split(';');
                products[i, 0] = brace[0]; // Barcode
                products[i, 1] = brace[1]; // Product
                products[i, 2] = brace[2]; // Price
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bug();
            PrintProduct();
            txtBarcode.Focus();
            Clear();
        }

        private void Clear()
        {
            txtBarcode.Clear();
            textBox2.Clear();
            textBox3.Clear();
            numericUpDown1.Value = 1;
        }

        private void PrintProduct()
        {
            amount = Convert.ToInt32(numericUpDown1.Value);
            lstShoppingCart.Items.Add($"{amount}x - {productname} - {price * amount}$");
            total += price * amount;
            productname = "";
            price = 0;
            amount = 0;
            lblTotal.Text = total.ToString() + "$";
        }

        private void Bug()
        {
            if (txtBarcode.Text == "") MessageBox.Show("Please enter a Barcode...");
        }
    }
}
