using SalesManagement.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagement
{
    public partial class Form1 : Form
    {
        int CustomerID = 0;
        int cellID = 0;
        string imgLocation = "";
        string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoaddgvInvoices();
            LoadcmbSupplier();
            LoadCmbProSupplier();
            LoadcmbProduct();
            LoadcmbStore();
        }

        private void LoadcmbStore()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                Store obj = new Store();
                obj.StoreNumber = txtStore.Text;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM  Store ";
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr, LoadOption.Upsert);
                cmbStore.DisplayMember = "StoreNumber";
                cmbStore.ValueMember = "StoreId";
                cmbStore.DataSource = dt;
                cmbStore.DisplayMember = "StoreNumber";
                cmbStore.ValueMember = "StoreId";
                cmbStore.DataSource = dt;

            }
        }

        private void LoadcmbProduct()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {

                con.Open();
                Product obj = new Product();
                obj.ProductName = txtPurductName.Text;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM  Product ";
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr, LoadOption.Upsert);
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ProductID";
                cmbProduct.DataSource = dt;
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ProductID";
                cmbProduct.DataSource = dt;

            }
        }

        private void LoadCmbProSupplier()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                Store obj = new Store();
                obj.StoreNumber = txtStore.Text;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM  Supplier ";
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr, LoadOption.Upsert);
                comboBox1.DisplayMember = "SupplierName";
                comboBox1.ValueMember = "SupplierID";
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "SupplierName";
                comboBox1.ValueMember = "SupplierID";
                comboBox1.DataSource = dt;

            }
        }

        private void LoadcmbSupplier()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                Supplier obj = new Supplier();
                obj.SupplierName = txtsupplierName.Text;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM  Supplier ";
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr, LoadOption.Upsert);
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "SupplierID";
                cmbSupplier.DataSource = dt;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "SupplierID";
                cmbSupplier.DataSource = dt;

            }
        }

        private void LoaddgvInvoices()
        {
            //throw new NotImplementedException();
        }
        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {
            int a, b;
            if (int.TryParse(txtUnitprice.Text, out a) && int.TryParse(txtQuantity.Text, out b))
            {
                int c = a * b;
                txtTotalPrice.Text = c.ToString();
            }

        }
        private void AddProduct_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                int count = 0;
                SqlTransaction tran = null;
                con.Open();
                tran = con.BeginTransaction();
                Supplier obj = new Supplier();
                obj.SupplierName = txtsupplierName.Text;
                obj.SupplierPhoneNO = Convert.ToInt32(txtSupplierPhoneNo.Text);
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO  Supplier (SupplierName,SupplierPhoneNO) VALUES ('" + obj.SupplierName + "','" + obj.SupplierPhoneNO + "')";
                cmd.Transaction = tran;
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    tran.Commit();
                    MessageBox.Show("Supplier saved Successfully");
                    LoadcmbSupplier();
                    LoadCmbProSupplier();


                }
                else
                {
                    tran.Rollback();
                    MessageBox.Show("Error Occured");
                }

            }
        }

        private void Product_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                int count = 0;
                SqlTransaction tran = null;
                con.Open();
                tran = con.BeginTransaction();

                Product obj = new Product();
                obj.ProductName = txtPurductName.Text;
                obj.SupplierID = Convert.ToInt32(cmbSupplier.SelectedValue);

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Product (ProductName, SupplierID ) VALUES ('" + obj.ProductName + "','" + obj.SupplierID + "')";
                cmd.Transaction = tran;
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    tran.Commit();
                    MessageBox.Show("Product Added Successfully");
                    LoadcmbProduct();
                    LoadCmbProSupplier();

                }
                else
                {
                    tran.Rollback();
                    MessageBox.Show("Error Occured");
                }

            }
        }

        private void btnStoreAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                int count = 0;
                SqlTransaction tran = null;
                con.Open();
                tran = con.BeginTransaction();
                Store obj = new Store();
                obj.StoreNumber = txtStore.Text;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Store (StoreNumber) VALUES ('" + obj.StoreNumber + "')";
                cmd.Transaction = tran;
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    tran.Commit();
                    MessageBox.Show("Store Number saved Successfully");
                    LoadcmbStore();

                }
                else
                {
                    tran.Rollback();
                    MessageBox.Show("Error Occured");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int count;
            byte[] pic = null;
            FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader brdr = new BinaryReader(stream);
            pic = brdr.ReadBytes((int)stream.Length);
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlTransaction tran = null;
                con.Open();
                string file = imgLocation;
                string[] fArry = file.Split('\\');
                string fn = fArry[fArry.Length - 1];
                string des = @"D:\IDB-IT\Online Class\Second Phase.29thApril\The XML,ADO.net,Crystal Report\ADO.Net\FinalProject\SalesManagementSln\SalesManagement\Images\" + fn;
                File.Copy(file, des, true);
                Customer obj = new Customer();
                obj.CustomerName = txtCustomerName.Text;
                obj.ShippingAddress = txtCaddress.Text;
                obj.OrderDate = Convert.ToDateTime(dateTimePicker1.Text);
                obj.ProductID = Convert.ToInt32(cmbProduct.SelectedValue);
                obj.StoreId = Convert.ToInt32(cmbStore.SelectedValue);
                obj.PhoneNo = textBox1.Text;
                obj.UnitPrice = Convert.ToDecimal(txtUnitprice.Text);
                obj.Quantity = Convert.ToDecimal(txtQuantity.Text);
                obj.TotalPrice = Convert.ToDecimal(txtTotalPrice.Text);
                obj.ImageName = file;
                obj.ImageData = pic;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Customer (CustomerName,ShippingAddress,OrderDate,ProductID,UnitPrice,Quantity,TotalPrice,PhoneNo,StoreID,ImageName,ImageData) VALUES ('" + obj.CustomerName + "','" + obj.ShippingAddress + "','" + obj.OrderDate + "','" + obj.ProductID + "','" + obj.UnitPrice + "','" + obj.Quantity + "','" + obj.TotalPrice + "','" + obj.PhoneNo + "','" + obj.StoreId + "','" + obj.ImageName + "',@img)";
                cmd.Parameters.Add(new SqlParameter("@img", obj.ImageData));
                cmd.Transaction = tran;
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    tran.Commit();
                    MessageBox.Show("Customer Added Successfully");
                    LoadcmbProduct();
                    LoadcmbSupplier();
                    LoadcmbStore();
                    LoaddgvInvoices();
                    CustomerID = 0;
                    dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                }
                else
                {
                    tran.Rollback();
                    MessageBox.Show("Error Occured");
                }

            }
        }
    }
}
