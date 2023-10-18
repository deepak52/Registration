using System;
using System.Data;
using System.Data.SqlClient;


namespace Registration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=LAPTOP-8MDS7I11\\SQLEXPRESS;Initial Catalog=Registration_db;Integrated Security=True");

        private void label7_Click(object sender, EventArgs e)
        {

        }

        //INSERT

        private void button1_Click(object sender, EventArgs e)
        {
            int phone = int.Parse(textBox3.Text), age = int.Parse(textBox5.Text);
            string name = textBox1.Text, email = textBox2.Text, gender = "", state = comboBox1.Text, district = comboBox2.Text, userName = textBox6.Text, password = textBox7.Text;

            if (radioButton1.Checked == true) { gender = "Female"; } else { gender = "Male"; }
            con.Open();
            SqlCommand cmd = new SqlCommand("EXEC InsertPer_SP '" + null + "','" + name + "','" + email + "','" + phone + "','" + age + "','" + gender + "','" + state + "','" + district + "','" + userName + "','" + password + "'", con);

            try
            {
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                con.Close();
            }


            MessageBox.Show("inserted sucessfully");
            GetPersonList();
            con.Close();
        }

        void GetPersonList()
        {
            SqlCommand cmd = new SqlCommand("EXEC DisplayPer_SP", con);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetPersonList();
        }

        //UPDATE

        private void button2_Click(object sender, EventArgs e)
        {
            int phone = int.Parse(textBox3.Text), pID = int.Parse(textBox4.Text), age = int.Parse(textBox5.Text);
            string name = textBox1.Text, email = textBox2.Text, gender = "", state = comboBox1.Text, district = comboBox2.Text, userName = textBox6.Text, password = textBox7.Text;
            if (radioButton1.Checked == true) { gender = "Female"; } else { gender = "Male"; }
            con.Open();
            SqlCommand cmd = new SqlCommand("EXEC UpdatePer_SP '" + pID + "','" + name + "','" + email + "','" + phone + "','" + age + "','" + gender + "','" + state + "','" + district + "','" + userName + "','" + password + "'", con);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }


            MessageBox.Show("Updated sucessfully");
            GetPersonList();
            con.Close();

        }

        //Delete
        private void button3_Click(object sender, EventArgs e)
        {
            int pID = int.Parse(textBox4.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("EXEC DeletePer_SP '" + pID + "'", con);

            try
            {
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                con.Close();
            }


            MessageBox.Show("Deleted sucessfully");
            GetPersonList();
            con.Close();
        }

        //Search

        private void button5_Click(object sender, EventArgs e)
        {
            int pID = int.Parse(textBox4.Text);
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("EXEC SearchPer_SP '" + pID + "'", con);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
                con.Close();
            }


            MessageBox.Show("Search Sucess");
            con.Close();

        }
    }
}