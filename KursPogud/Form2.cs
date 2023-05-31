using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace KursPogud
{
    public partial class Form2 : Form
    {
        public List<System.Windows.Forms.CheckBox> CheckBoxes = new List<System.Windows.Forms.CheckBox>();
        public Form2()
        {
            InitializeComponent();
        }
        int idOrder = 0;
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }
        private void LoadOrders()
        {
            panel1.Controls.Clear();
            Logic logic = new Logic();
            List<String[]> orders = logic.GetOrders();
            foreach (String[] s in orders)
            {

                Label name = new Label();
                Label stol = new Label();
                Label status = new Label();
                name.Text = "Заказ №" + s[0];
                string se = s[1];
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
                    LoadDishes(id);
                };
                status.Click += (labelSender, labelEventArgs) =>
                {
                    LoadDishes(id);
                };

                panel1.Controls.Add(element);
            }
        }
        private void LoadDishes(int id)
        {
            label2.Text = "Состав заказа № " + id;
            int i = 1;
            CheckBoxes.Clear();
            panel2.Controls.Clear();
            idOrder = id;
            Logic logic = new Logic();
            List<String[]> dishes = logic.GetDishesFromOrder(id);
            foreach (String[] s in dishes)
            {
                System.Windows.Forms.CheckBox check = new System.Windows.Forms.CheckBox();
                Label name = new Label();
                Label compounds = new Label();
                Label type = new Label();
                Label mass = new Label();
                Label kkal = new Label();
                Label cost = new Label();
                Label status = new Label();
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
                        status.Text = "Отправлен на кухню";
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
                if (status.Text=="Выдан") check.Checked = true;
                check.CheckedChanged += (labelSender, labelEventArgs) =>
                    {
                        if (status.Text == "Готов")
                        {
                            ChangeStatusDishes(Convert.ToInt32(s[0]), i);
                            i++;
                        }
                        else
                        {
                            check.Checked = false;
                        }
                    };
                mass.Text = s[4] + " грамм";
                kkal.Text = s[5] + " ккал";
                cost.Text = s[6] + " рублей";
                status.Width = 110;
                FlowLayoutPanel element = new FlowLayoutPanel();

                element.Controls.Add(name);
                element.Controls.Add(compounds);
                element.Controls.Add(type);
                element.Controls.Add(status);
                element.Controls.Add(check);
                element.Dock = DockStyle.Top;

                

                panel2.Controls.Add(element);
            }
        }
        private void ChangeStatusDishes(int id, int counter)
        {
            Logic logic = new Logic();
            logic.ChangeStatusDishesEaten(idOrder, id,counter);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ready = true;
            foreach(System.Windows.Forms.CheckBox check in CheckBoxes)
            {
                if (!check.Checked) ready = false;
            }
            if (ready)
            {
                Logic logic = new Logic();
                logic.ChangeStatusOrder(idOrder, 3);
                Form5 form5 = new Form5(idOrder);
                form5.ShowDialog();
                panel2.Controls.Clear();
                panel1.Controls.Clear();
                LoadOrders();
            }
        }
    }
}
