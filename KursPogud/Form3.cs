using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursPogud
{
    public partial class Form3 : Form
    {
        public List<CheckBox> CheckBoxes = new List<CheckBox>();
        int idOrder = 0;
        int randomNumber = 0;
        int i = 0;
        public Form3()
        {
            InitializeComponent();
            Random random = new Random();
            randomNumber = random.Next(1, 4);
            LoadOrders();
        }
        private void LoadOrders()
        {
            panel1.Controls.Clear();
            Logic logic = new Logic();
            
            List<String[]> orders = logic.GetOrders();
            foreach (String[] s in orders)
            {
                if (Convert.ToInt32(s[0]) % randomNumber == 0)
                {
                    Label name = new Label();
                    Label stol = new Label();
                    Label status = new Label();
                    name.Text = "Заказ №" + s[0];

                    switch (Convert.ToInt32(s[1]))
                    {
                        case 0:
                            status.Text = "Передан на кухню";
                            break;
                        case 1:
                            status.Text = "Готовится";
                            break;
                        case 2:
                            status.Text = "Готов";
                            break;
                        case 3:
                            status.Text = "Выдан";
                            break;

                    }
                    stol.Text = "Столик № 3";
                    FlowLayoutPanel element = new FlowLayoutPanel();

                    element.Controls.Add(name);
                    element.Controls.Add(status);
                    element.Controls.Add(stol);
                    element.Dock = DockStyle.Top;
                    int id = Convert.ToInt32(s[0]);
                    name.Click += (labelSender, labelEventArgs) =>
                    {
                        panel2.Controls.Clear();
                        LoadDishes(id);
                        
                        if (status.Text == "Передан на кухню")
                        {
                            button1.Visible = true;
                            button2.Visible = false;
                        }
                        if (status.Text == "Готовится")
                        {
                            button1.Visible = false;
                            button2.Visible = true; ;
                        }
                        if (status.Text == "Готов")
                        {
                            button1.Visible = false;
                            button2.Visible = false; ;
                        }
                    };
                    status.Click += (labelSender, labelEventArgs) =>
                    {
                        panel2.Controls.Clear();
                        LoadDishes(id);
                        if (status.Text == "Передан на кухню")
                        {
                            button1.Visible = true;
                            button2.Visible = false;
                        }
                        if (status.Text == "Готовится")
                        {
                            button1.Visible = false;
                            button2.Visible = true; ;
                        }
                        if (status.Text == "Готов")
                        {
                            button1.Visible = false;
                            button2.Visible = false; ;
                        }

                    };
                   

                    panel1.Controls.Add(element);
                }
            }
        }
        private void LoadDishes(int id)
        {
            label2.Text = "Подробности заказа № " +id;
            CheckBoxes.Clear();
            idOrder = id;
            Logic logic = new Logic();
            List<String[]> dishes = logic.GetDishesFromOrder(id);
            foreach (String[] s in dishes)
            {
                CheckBox check = new CheckBox();
                Label name = new Label();
                Label compounds = new Label();
                Label type = new Label();
                Label mass = new Label();
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
                switch (Convert.ToInt32(s[7]))
                {
                    case 0:
                        check.Checked = false;
                        break;
                    case 1:
                        check.Checked = false;
                        break;
                    case 2:
                        check.Checked = true; ;
                        break;
                    case 3:
                        check.Checked = true; ;
                        break;

                }
                mass.Text = s[4] + " грамм";
                check.CheckedChanged+= (labelSender, labelEventArgs) =>
                {
                    if (check.Checked)
                    {
                        i++;
                        ChangeStatusDishes(Convert.ToInt32(s[0]));
                        logic.ChangeStatusOrder(id, 1);
                        
                    }
                    else check.Checked = true;

                    
                };
                FlowLayoutPanel element = new FlowLayoutPanel();

                element.Controls.Add(name);
                element.Controls.Add(compounds);
                element.Controls.Add(type);
                element.Controls.Add(check);

                element.Dock = DockStyle.Top;



                panel2.Controls.Add(element);
            }
        }
        private void ChangeStatusDishes(int id)
        {
            Logic logic = new Logic();
            logic.ChangeStatusDishes(idOrder, id, $"AND counter={i}");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Logic logic = new Logic();
            logic.ChangeStatusOrder(idOrder,1);
            panel2.Controls.Clear();
            LoadOrders();
            logic.ChangeStatusDishesAll(idOrder, 1,"");
            button2.Visible = true;
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Logic logic = new Logic();
            logic.ChangeStatusOrder(idOrder, 2);
            logic.ChangeStatusDishesAll(idOrder, 2,"AND status <> 3");
            panel2.Controls.Clear();
            panel1.Controls.Clear();
            LoadOrders();
        }
    }
}
