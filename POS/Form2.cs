using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class Form2 : Form
    {
        Form1 form1;
        // Form1 Form1;
      
        public Form2()
        {
           // form1 = incomingform;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            loadnocalculat();
        }

        private void loadnocalculat()
        {
            System.Random rand = new System.Random((int)System.DateTime.Now.Ticks);
            int random = rand.Next(1, 1000);
            textBoxnoone.Text = Convert.ToString(random);
            System.Random randm = new System.Random((int)System.DateTime.Now.Ticks);
            int randoms = randm.Next(1, 100);
            textBoxno2.Text = Convert.ToString(randoms);
        }

        private void calculat()
        {
            decimal no1 = Convert.ToDecimal(textBoxnoone.Text);
            decimal no2 = Convert.ToDecimal(textBoxno2.Text);
            decimal ans = no1 + no2;
            decimal ans2 = Convert.ToDecimal(textBoxanswer.Text);

            if(ans.Equals(ans2))
            {
                loadnocalculat();
                textBoxanswer.Text = "";
            }

            else
            {
                textBoxanswer.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            calculat();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            //if ( textBoxanswer.Text ==" ")
            //{
            //    this.Hide();
            //    Form1 form1 = new Form1();
            //    form1.Show();
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
