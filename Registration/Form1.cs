using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

        // INSERT

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string email = textBox2.Text;
            string phoneText = textBox3.Text;
            string ageText = textBox5.Text;
            string userName = textBox6.Text;
            string password = textBox7.Text;
            string confirmPassword = textBox8.Text; // Assuming you have a confirmation password textbox
            string gender = radioButton1.Checked ? "Female" : "Male";
            string state = comboBox1.Text;
            string district = comboBox2.Text;

            // Validation for email
            if (!IsValidEmail(email))
            {
                labelErrorEmail.Visible = true;
                labelErrorEmail.Text = "Invalid email address";
                return;
            }

            // Validation for phone number
            if (!IsValidPhoneNumber(phoneText))
            {
                labelErrorPhone.Visible = true;
                labelErrorPhone.Text = "Invalid phone number";
                return;
            }

            // Validation for age (age should be at least 18)
            if (!IsValidAge(ageText))
            {
                labelErrorAge.Visible = true;
                labelErrorAge.Text = "Age must be at least 18";
                return;
            }

            // Validation for password
            if (!IsValidPassword(password))
            {
                labelErrorPassword.Visible = true;
                labelErrorPassword.Text = "Invalid password";
                return;
            }

            // Check if passwords match
            if (password != confirmPassword)
            {
                labelErrorPassword.Visible = true;
                labelErrorPassword.Text = "Passwords do not match";
                return;
            }

            // All validations passed; proceed with database insertion
            con.Open();
            SqlCommand cmd = new SqlCommand("EXEC InsertPer_SP '" + null + "','" + name + "','" + email + "','" + phoneText + "','" + ageText + "','" + gender + "','" + state + "','" + district + "','" + userName + "','" + password + "'", con);

            try
            {
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inserted successfully");
                GetPersonList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        // Leave event for email
        private void textBox2_Leave(object sender, EventArgs e)
        {
            string email = textBox2.Text;

            if (!IsValidEmail(email))
            {
                labelErrorEmail.Visible = true;
                labelErrorEmail.Text = "Invalid email address";
            }
            else
            {
                labelErrorEmail.Visible = false;
            }
        }

        // Leave event for phone number
        private void textBox3_Leave(object sender, EventArgs e)
        {
            string phoneText = textBox3.Text;

            if (!IsValidPhoneNumber(phoneText))
            {
                labelErrorPhone.Visible = true;
                labelErrorPhone.Text = "Invalid phone number";
            }
            else
            {
                labelErrorPhone.Visible = false;
            }
        }

        // Leave event for age
        private void textBox5_Leave(object sender, EventArgs e)
        {
            string ageText = textBox5.Text;

            if (!IsValidAge(ageText))
            {
                labelErrorAge.Visible = true;
                labelErrorAge.Text = "Age must be at least 18";
            }
            else
            {
                labelErrorAge.Visible = false;
            }
        }

        // Leave event for password
        private void textBox7_Leave(object sender, EventArgs e)
        {
            string password = textBox7.Text;
            string confirmPassword = textBox8.Text; // Assuming you have a confirmation password textbox

            if (!string.IsNullOrEmpty(password))
            {
                if (!IsValidPassword(password))
                {
                    labelErrorPassword.Visible = true;
                    labelErrorPassword.Text = "Invalid password";
                }
                else if (password != confirmPassword)
                {
                    labelErrorPassword.Visible = true;
                    labelErrorPassword.Text = "Passwords do not match";
                }
                else
                {
                    labelErrorPassword.Visible = false;
                }
            }
        }

        // Update button click event
        private void button2_Click(object sender, EventArgs e)
        {
            int phone = int.Parse(textBox3.Text), pID = int.Parse(textBox4.Text), age = int.Parse(textBox5.Text);
            string name = textBox1.Text, email = textBox2.Text, userName = textBox6.Text, password = textBox7.Text;
            string confirmPassword = textBox8.Text; // Assuming you have a confirmation password textbox
            string gender = radioButton1.Checked ? "Female" : "Male";
            string state = comboBox1.Text;
            string district = comboBox2.Text;

            // Validation for email
            if (!IsValidEmail(email))
            {
                labelErrorEmail.Visible = true;
                labelErrorEmail.Text = "Invalid email address";
                return;
            }

            // Validation for phone number
            if (!IsValidPhoneNumber(phone.ToString()))
            {
                labelErrorPhone.Visible = true;
                labelErrorPhone.Text = "Invalid phone number";
                return;
            }

            // Validation for age (age should be at least 18)
            if (!IsValidAge(age.ToString()))
            {
                labelErrorAge.Visible = true;
                labelErrorAge.Text = "Age must be at least 18";
                return;
            }

            // Validation for password
            if (!string.IsNullOrEmpty(password))
            {
                if (!IsValidPassword(password))
                {
                    labelErrorPassword.Visible = true;
                    labelErrorPassword.Text = "Invalid password";
                    return;
                }

                // Check if passwords match
                if (password != confirmPassword)
                {
                    labelErrorPassword.Visible = true;
                    labelErrorPassword.Text = "Passwords do not match";
                    return;
                }
            }

            // All validations passed; proceed with database update
            con.Open();
            SqlCommand cmd = new SqlCommand("EXEC UpdatePer_SP '" + pID + "','" + name + "','" + email + "','" + phone + "','" + age + "','" + gender + "','" + state + "','" + district + "','" + userName + "','" + password + "'", con);

            try
            {
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Updated successfully");
                GetPersonList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        // Delete button click event
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

            MessageBox.Show("Deleted successfully");
            GetPersonList();
            con.Close();
        }

        // Search button click event
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

            MessageBox.Show("Search Success");
            con.Close();
        }

        // Get list of persons and populate the DataGridView
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

        // Refresh the DataGridView
        private void button4_Click(object sender, EventArgs e)
        {
            GetPersonList();
        }

        // Method to validate email
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Method to validate phone number (assuming a 10-digit number)
        private bool IsValidPhoneNumber(string phoneText)
        {
            return Regex.IsMatch(phoneText, @"^\d{10}$");
        }

        // Method to validate age (age should be at least 18)
        private bool IsValidAge(string ageText)
        {
            if (int.TryParse(ageText, out int age))
            {
                return age >= 18;
            }
            return false;
        }

        // Method to validate password with regex
        private bool IsValidPassword(string password)
        {
            // Password must be at least 8 characters long and contain at least one uppercase letter,
            // one lowercase letter, one digit, and one special character.
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,}$");
        }
    }
}