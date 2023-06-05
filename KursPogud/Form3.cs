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
            //LoadOrders();
            LoadDishes();
        }
        
        private void LoadDishes()
        {
           panel2.Controls.Clear();
            // label2.Text = "Подробности заказа № " +id;
            CheckBoxes.Clear();
           
            Logic logic = new Logic();
            List<String[]> orders = logic.GetOrders();
            foreach (String[] order in orders)
            {
                
                List<String[]> dishes = logic.GetDishesFromOrder(Convert.ToInt32(order[0]),"AND status <2");
                foreach (String[] s in dishes)
                {
                    
                    CheckBox check = new CheckBox();
                    Label name = new Label();
                    Label compounds = new Label();
                    Label type = new Label();
                    Label mass = new Label();
                    Label orderId = new Label();
                    orderId.Text = order[0];
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
                    check.CheckedChanged += (labelSender, labelEventArgs) =>
                    {
                        if (check.Checked)
                        {
                            
                            logic.ChangeStatusDishes(Convert.ToInt32(orderId.Text), Convert.ToInt32(s[0]));
                            logic.ChangeStatusOrder(Convert.ToInt32(order[0]), 1);
                            if(logic.CheckIfOrderREady(Convert.ToInt32(orderId.Text))) logic.ChangeStatusOrder(Convert.ToInt32(order[0]), 2);
                            LoadDishes();
                            



                        }
                        else check.Checked = true;


                    };
                    FlowLayoutPanel element = new FlowLayoutPanel();
                   
                    element.Controls.Add(name);
                    element.Controls.Add(compounds);
                    element.Controls.Add(type);
                    element.Controls.Add(check);
                    //element.Controls.Add(orderId);

                    element.Dock = DockStyle.Top;



                    panel2.Controls.Add(element);
                    
                }
            }
           
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
