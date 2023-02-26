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
using System.Xml.Serialization;
using System.Windows.Forms.VisualStyles;

namespace InventoryProX
{
    public partial class InvProX : Form
    {
        List<Item> items = new List<Item>();
        string name;
        string description;
        int quantity;
        float price;

        public InvProX()
        {
            InitializeComponent();
            this.Load += OpenExistingItems;
            this.FormClosing += SaveChangesOnExit;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void OpenExistingItems(object sender, EventArgs e)
        {
            items = new List<Item>();
            if (File.Exists("items.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
                using (FileStream stream = File.OpenRead("items.xml"))
                {
                    items = (List<Item>)serializer.Deserialize(stream);
                }
                if (items.Count != 0)
                {
                    dataGridView1.DataSource = items;
                }
            }
        }

        private void SaveChangesOnExit(object sender, FormClosingEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
            using (TextWriter writer = new StreamWriter("items.xml"))
            {
                serializer.Serialize(writer, items);
            }
        }

        private int ParseQuantity()
        {
            if (int.TryParse(txtQuantity.Text, out quantity))
            {
                return quantity;
            }
            return quantity;
        }

        private float ParsePrice()
        {
            if (float.TryParse(txtPrice.Text, out price))
            {
                return price;
            }
            return price;
        }

        private void ResetDataSource()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = items;
        }

        private void AddEVT(object sender, EventArgs e)
        {
            name = txtName.Text;
            description = txtDescription.Text;
            quantity = ParseQuantity();
            price = ParsePrice();
            items.Add(new Item(name, description, quantity, price));
            ResetDataSource();
        }

        private void DeleteEVT(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Item item = (Item)row.DataBoundItem;
                items.Remove(item);
            }
            ResetDataSource();
        }
    }
}
