using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KursPogud
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Logic logic = new Logic();
            switch(logic.Logging(textBox1.Text, textBox2.Text,"offic"))
            //switch (class1.GetLogFromBD("admin","admin"))
            {
                case 0:
                    Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
            break;
                case 1:
                    label5.Visible = true;
            label5.Text = "Пользователь не найден";
            break;
                case 2:
                    label5.Visible = true;
            label5.Text = "Неверный пароль";
            break;


        }
    }

        private void button3_Click(object sender, EventArgs e)
        {
            
            LogicMain logicMain = new LogicMain();

            switch (logicMain.ValidateLoging(textBox1.Text, textBox2.Text,"povar"))
            {
                case 0:
                    Form3 form3 = new Form3();
                    form3.Show();
                    this.Hide();
                    break;
                case 1:
                    label5.Visible = true;
                    label5.Text = "Пользователь не найден";
                    break;
                case 2:
                    label5.Visible = true;
                    label5.Text = "Неверный пароль";
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
