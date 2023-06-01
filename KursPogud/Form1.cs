using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace KursPogud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }
        public List<CheckBox> CheckBoxes = new List<CheckBox>();
        public List<NumericUpDown> numericUpDowns = new List<NumericUpDown>();
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LoadMenu(String filter)
        {
            panel1.Controls.Clear();
            CheckBoxes.Clear();
            Logic logic = new Logic();
            List<String[]> dishes = logic.GetAllDishes(filter);
            foreach (String[] s in dishes)
            {
                CheckBox check = new CheckBox();
                
                Label name = new Label();
                Label compounds = new Label();
                Label type = new Label();
                Label mass = new Label();
                Label kkal = new Label();
                Label cost= new Label();
                check.Tag = s[0];
                CheckBoxes.Add(check);
                
                compounds.Width = 260;
                compounds.Height = 140;
                name.Text = s[1];
                compounds.Text = s[2];
                switch (Convert.ToInt32(s[3]))
                {
                    case 1:
                        type.Text = "Первое";
                        break;
                    case 2:
                        type.Text = "Второе";
                        break;
                    case 3:
                        type.Text = "Десерт";
                        break;

                }
                mass.Text = s[4] + " грамм";
                kkal.Text = s[5] + " ккал";
                cost.Text = s[6] + " рублей";
                FlowLayoutPanel element = new FlowLayoutPanel();
               

                element.Controls.Add(name);
                element.Controls.Add(compounds);
                element.Controls.Add(type);
                element.Controls.Add(mass);
                element.Controls.Add(kkal);
                element.Controls.Add(cost);
                element.Controls.Add(check);
                
                element.Dock = DockStyle.Top;

                int id = Convert.ToInt32(s[0]);
                
                panel1.Controls.Add(element);

            }
        }
        private void LoadOrder()
        {
            
            List<String[]> dishes = new List<String[]>();
            Logic logic = new Logic();
            int i = 0;
            foreach (CheckBox check in CheckBoxes)
            {  
                String[] s = new string[CheckBoxes.Count];
                if (check.Checked == true)
                {


                        s = logic.GetOrderDishes(Convert.ToInt32(check.Tag));
                        dishes.Add(s);
                    

                }
                i++;
            }
            CheckBoxes.Clear();
            numericUpDowns.Clear();
            foreach (String[] s in dishes)
            {
                NumericUpDown number = new NumericUpDown();
                CheckBox check = new CheckBox();
                Label name = new Label();
                Label compounds = new Label();
                Label type = new Label();
                Label mass = new Label();
                Label kkal = new Label();
                Label cost = new Label();
                check.Checked= true;
                numericUpDowns.Add(number);
                check.Tag = s[0];
                CheckBoxes.Add(check);
                compounds.Width = 260;
                compounds.Height = 140;
                name.Text = s[1];
                compounds.Text = s[2];
                switch (Convert.ToInt32(s[3]))
                {
                    case 1:
                        type.Text = "Первое";
                        break;
                    case 2:
                        type.Text = "Второе";
                        break;
                    case 3:
                        type.Text = "Десерт";
                        break;

                }
                mass.Text = s[4] + " грамм";
                kkal.Text = s[5] + " ккал";
                cost.Text = s[6] + " рублей";
                FlowLayoutPanel element = new FlowLayoutPanel();

                element.Controls.Add(name);
                element.Controls.Add(compounds);
                element.Controls.Add(type);
                element.Controls.Add(mass);
                element.Controls.Add(kkal);
                element.Controls.Add(cost);
                element.Controls.Add(check);
                element.Controls.Add(number);
                element.Dock = DockStyle.Top;

                int id = Convert.ToInt32(s[0]);

                panel1.Controls.Add(element);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logic logic = new Logic();
            
            label1.Text = $"Заказ № {logic.GetLastIdOrder()+1}";
            panel1.Controls.Clear();
            LoadOrder();
            button2.Visible = true;
            button3.Visible = true;
            button1.Visible = false;
            

        }


        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Меню";
            panel1.Controls.Clear();
            LoadMenu("");
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {int i=0;
            Logic logic = new Logic();
            int idLastOrder = logic.GetLastIdOrder() + 1;
            logic.CreateNewOrder(idLastOrder);
            foreach(CheckBox check in CheckBoxes)
            {
                if (check.Checked == true)
                {
                    if (numericUpDowns[i].Value > 1)
                    {

                        for (int j = 0; j < numericUpDowns[i].Value; j++)
                        {
                            logic.CreateNewOrderDishes(Convert.ToInt32(check.Tag), idLastOrder,j+1);
                        }
                    }
                    else
                    {
                        logic.CreateNewOrderDishes(Convert.ToInt32(check.Tag), idLastOrder,1);
                    }
                    
                }
                i++;
            }
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            comboBox1.SelectedIndex = 1;
            comboBox1.SelectedIndex = 0;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    LoadMenu("");
                    break;
                case 1:
                    LoadMenu("WHERE typeOfDishes = 1");
                    break;
                case 2:
                    LoadMenu("WHERE typeOfDishes = 2");
                    break;
                case 3:
                    LoadMenu("WHERE typeOfDishes = 3");
                    break;
            }
        }
    }
}
