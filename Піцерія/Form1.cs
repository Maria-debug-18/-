using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace PizzaMenu
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
           
            if (textBoxName.Text == "" || textBoxPrice.Text == "" || textBoxDescription.Text == "" || comboBoxSize.SelectedItem == null)
            {
                MessageBox.Show("Будь ласка, заповніть всі поля!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int n = dataGridView1.Rows.Add();
            dataGridView1.Rows[n].Cells[0].Value = textBoxName.Text;
            dataGridView1.Rows[n].Cells[1].Value = comboBoxSize.SelectedItem.ToString();
            dataGridView1.Rows[n].Cells[2].Value = textBoxPrice.Text;
            dataGridView1.Rows[n].Cells[3].Value = textBoxDescription.Text;

            textBoxName.Clear();
            comboBoxSize.SelectedItem = null;
            textBoxPrice.Clear();
            textBoxDescription.Clear();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, оберіть рядок для редагування!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxSize.SelectedItem == null)
            {
                MessageBox.Show("No size!");
                return;
            }

            int rowIndex = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows[rowIndex].Cells[0].Value = textBoxName.Text;
                dataGridView1.Rows[rowIndex].Cells[1].Value = comboBoxSize.SelectedItem.ToString();
                dataGridView1.Rows[rowIndex].Cells[2].Value = textBoxPrice.Text;
                dataGridView1.Rows[rowIndex].Cells[3].Value = textBoxDescription.Text;
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
  
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Будь ласка, оберіть рядок для видалення!", "Примітка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            textBoxName.Clear();
            comboBoxSize.SelectedItem = null;
            textBoxPrice.Clear();
            textBoxDescription.Clear();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
 
            string fileName = "menu.xml";
            XmlWriter writer = XmlWriter.Create(fileName);
            writer.WriteStartDocument();
            writer.WriteStartElement("menu");
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                writer.WriteStartElement("pizza");
                writer.WriteElementString("name", row.Cells[0].Value.ToString());
                writer.WriteElementString("size", row.Cells[1].Value.ToString());
                writer.WriteElementString("price", row.Cells[2].Value.ToString());
                writer.WriteElementString("description", row.Cells[3].Value.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            MessageBox.Show("Дані успішно збережені в файл " + fileName, "Збереження", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
           
            dataGridView1.Rows.Clear();

            string fileName = "menu.xml";
            if (File.Exists(fileName))
            { 
                XmlReader reader = XmlReader.Create(fileName);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "pizza")
                    {
                        string name = "";
                        string size = "";
                        string price = "";
                        string description = "";

                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                switch (reader.Name)
                                {
                                    case "name":
                                        reader.Read();
                                        name = reader.Value;
                                        break;
                                    case "size":
                                        reader.Read();
                                        size = reader.Value;
                                        break;
                                    case "price":
                                        reader.Read();
                                        price = reader.Value;
                                        break;
                                    case "description":
                                        reader.Read();
                                        description = reader.Value;
                                        break;
                                }
                            }

                            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "pizza")
                            {
                                int n = dataGridView1.Rows.Add();
                                dataGridView1.Rows[n].Cells[0].Value = name;
                                dataGridView1.Rows[n].Cells[1].Value = size;
                                dataGridView1.Rows[n].Cells[2].Value = price;
                                dataGridView1.Rows[n].Cells[3].Value = description;
                                break;
                            }
                        }
                    }
                }
                reader.Close();

                MessageBox.Show("Дані успішно завантажені з файлу " + fileName, "Завантаження", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Файл " + fileName + " не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentRow.Index;
            textBoxName.Text = "" + dataGridView1.Rows[rowIndex].Cells[0].Value;
            comboBoxSize.SelectedItem = dataGridView1.Rows[rowIndex].Cells[1].Value;
            textBoxPrice.Text = "" + dataGridView1.Rows[rowIndex].Cells[2].Value;
            textBoxDescription.Text = "" + dataGridView1.Rows[rowIndex].Cells[3].Value;
        }
    }
}
