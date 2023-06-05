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
    public partial class Form5 : Form
    {
        public Form5(int idOrder)
        {
            InitializeComponent();
            this.idOrder = idOrder;
        }
        int idOrder=0;
        int Finalcost = 0;
        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Text = "Заказ №" + idOrder;
            label3.Text = "Иванов Иван Иванович";
            label4.Text = "Поваров Повар Поварович";
            LoadDishes(idOrder);
            label6.Text = Finalcost + " рублей";
        }
        private void LoadDishes(int id)
        {
            
            idOrder = id;
            Logic logic = new Logic();
            List<String[]> dishes = logic.GetDishesFromOrder(id,"");
            foreach (String[] s in dishes)
            {

                Label name = new Label();
                Label compounds = new Label();
                Label type = new Label();
                Label mass = new Label();
                Label kkal = new Label();
                Label cost = new Label();
                Finalcost += Convert.ToInt32(s[6]);
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
                element.Controls.Add(cost);
                element.Dock = DockStyle.Top;



                panel1.Controls.Add(element);
            }
        }
    }
}
