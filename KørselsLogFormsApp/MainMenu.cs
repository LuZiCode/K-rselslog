using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KørselsLogClassLibrary;


namespace KørselsLogFormsApp
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();

        }


        private void MainMenu_Load(object sender, EventArgs e)
        {
            ShowDataInComboBox();
        }

        private void FornavnTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            RedigerIDTextBox.Text = selectedRow.Cells[0].Value.ToString();
            PersonComboBox.Text = selectedRow.Cells[1].Value.ToString();
            NrPladeTextBox2.Text = selectedRow.Cells[2].Value.ToString();
        }

        private void NrPladeTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PersonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NrPladeTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void OKButton1_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(FornavnTextBox.Text))
            //{

            //}
            var f = new Requirements(FornavnTextBox.Text, FuldeNavnTextBox.Text, NrPladeTextBox1.Text);
            var k = f.CheckRequirements();
            if (!String.IsNullOrEmpty(k))
            {
                MessageBox.Show(k, "Opfyldelse af krav fejlbesked");
            }
            else
            {
                SqlConnection con = new SqlConnection("Data Source=WIN-NDGDCIETDGL;Initial Catalog=KørselLogC#Datebase;Integrated Security=True;Pooling=False");
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into UserTable values (@BrugerID,@Navn,@Nummerplade,@DatoOprettet, @DatoRedigeret)", con);
                cmd.Parameters.AddWithValue("@BrugerID", Convert.ToString(FornavnTextBox.Text));
                cmd.Parameters.AddWithValue("@Navn", Convert.ToString(FuldeNavnTextBox.Text));
                cmd.Parameters.AddWithValue("@Nummerplade", Convert.ToString(NrPladeTextBox1.Text));
                cmd.Parameters.AddWithValue("@DatoOprettet", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@DatoRedigeret", dateTimePicker1.Value);
                 cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show(string.Format("{0} er nu oprettet i systemet", FuldeNavnTextBox.Text), "Person oprettet");

                FornavnTextBox.ResetText();
                FuldeNavnTextBox.ResetText();
                NrPladeTextBox1.ResetText();
                dateTimePicker1.ResetText();

                ShowDataInComboBox();
            }
        }

        private void FuldeNavnTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-NDGDCIETDGL;Initial Catalog=KørselLogC#Datebase;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand checkForDublicates = new SqlCommand("select BrugerID from UserTable where BrugerID='" + RedigerIDTextBox.Text + "'", con);
            int? bid = (int?)checkForDublicates.ExecuteScalar();

            if (bid == int.Parse(RedigerIDTextBox.Text))
            {
                if (MessageBox.Show("Bekræft venlist, før du fortsætter" + "\n" + "Ønsker du at Fortsætte ?", "Bekræft Ændring", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    SqlCommand cmd = new SqlCommand("UPDATE UserTable SET Navn=@Navn, Nummerplade=@Nummerplade, DatoRedigeret=@DatoRedigeret where BrugerID=@BrugerID", con);
                    cmd.Parameters.AddWithValue("@BrugerID", Convert.ToString(RedigerIDTextBox.Text));
                    cmd.Parameters.AddWithValue("@Navn", Convert.ToString(PersonComboBox.Text));
                    cmd.Parameters.AddWithValue("@Nummerplade", Convert.ToString(NrPladeTextBox2.Text));
                    cmd.Parameters.AddWithValue("@DatoRedigeret", dateTimePicker2.Value);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show(string.Format("BrugerID: {0} er nu redigeret i systemet", RedigerIDTextBox.Text), "Person redigering");

                    RedigerIDTextBox.ResetText();
                    PersonComboBox.ResetText();
                    NrPladeTextBox2.ResetText();
                    dateTimePicker2.ResetText();

                    ShowDataInComboBox();
                }

                else

                {

                }
            }
            else
            {
                MessageBox.Show("BrugerID findes ikke" + "\n" + "Tjek nedenfor", "Fejl");
            }

        }

        private void RedigerIDTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PersonSletTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SletPersonButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-NDGDCIETDGL;Initial Catalog=KørselLogC#Datebase;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand checkForDublicates = new SqlCommand("select BrugerID from UserTable where BrugerID='"+ SletPersonComboBox.Text + "'", con);
            int? bid = (int?)checkForDublicates.ExecuteScalar();

            if (bid == int.Parse(SletPersonComboBox.Text))
            {
                if (MessageBox.Show("Bekræft venlist, før du fortsætter" + "\n" + "Ønsker du at Fortsætte ?", "Bekræft Slettelse", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    SqlCommand cmd = new SqlCommand("Delete UserTable where BrugerID=@BrugerID", con);
                    cmd.Parameters.AddWithValue("@BrugerID", Convert.ToString(SletPersonComboBox.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show(string.Format("BrugerID: {0} er nu slettet fra systemet", SletPersonComboBox.Text), "Person fjernelse");
                    ShowDataInComboBox();
                }

                else

                {

                }
            }
            else
            {
                MessageBox.Show("BrugerID findes ikke" + "\n" + "Tjek nedenfor", "Fejl");
            }


        }

        private void CancelButton1_Click(object sender, EventArgs e)
        {
            FornavnTextBox.ResetText();
            FuldeNavnTextBox.ResetText();
            NrPladeTextBox1.ResetText();
            dateTimePicker1.ResetText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RedigerIDTextBox.ResetText();
            PersonComboBox.ResetText();
            NrPladeTextBox2.ResetText();
            dateTimePicker2.ResetText();
        }

        private void MainMenu_Activated(object sender, EventArgs e)
        {
            ShowDataInDataGrid();
            ShowDataInDataGrid2();



            // Set your desired AutoSize Mode:
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Now that DataGridView has calculated it's Widths; we can now store each column Width values.
            for (int i = 0; i <= dataGridView1.Columns.Count - 1; i++)
            {
                // Store Auto Sized Widths:
                int colw = dataGridView1.Columns[i].Width;

                // Remove AutoSizing:
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // Set Width to calculated AutoSize value:
                dataGridView1.Columns[i].Width = colw;
            }



        }

        private void ShowDataInComboBox()
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-NDGDCIETDGL;Initial Catalog=KørselLogC#Datebase;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select BrugerID, Navn from UserTable", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            DataTable table1 = new DataTable();
            DataTable table2 = new DataTable();
            DataTable table3 = new DataTable();


            da.Fill(table1);
            da.Fill(table2);
            da.Fill(table3);

            DataRow itemrow = table1.NewRow();
            itemrow[1] = "Vælg Person";
            table1.Rows.InsertAt(itemrow, 0);

            PersonComboBox.DataSource = table1;
            PersonComboBox.DisplayMember = "Navn";
            PersonComboBox.ValueMember = "BrugerID";

            SletPersonComboBox.DataSource = table2;
            SletPersonComboBox.DisplayMember = "BrugerID";

            OpretOpgaveComboBox.DataSource = table3;
            OpretOpgaveComboBox.DisplayMember = "BrugerID";
        }

        private void ShowDataInDataGrid()
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-NDGDCIETDGL;Initial Catalog=KørselLogC#Datebase;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from UserTable", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void ShowDataInDataGrid2()
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-NDGDCIETDGL;Initial Catalog=KørselLogC#Datebase;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from OpgaveTable", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SletPersonComboBox.ResetText();
        }

        private void OKOpgaveButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-NDGDCIETDGL;Initial Catalog=KørselLogC#Datebase;Integrated Security=True;Pooling=False");
            con.Open();
            var f = new ORequirements(OpgaveNummerpladeTextBox.Text, IndtastOpgaveTextBox.Text, AntalKMnumericUpDown.Value);
            var k = f.CheckRequirements();

            if (String.IsNullOrEmpty(k))
            {
                if (MessageBox.Show("Bekræft venlist, før du fortsætter" + "\n" + "Ønsker du at Fortsætte ?", "Bekræft Opgave Oprettelse", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                {
                    SqlCommand cmd = new SqlCommand("insert into OpgaveTable values (@BrugerID,@Nummerplade,@Opgave, @KM, @DatoOprettet)", con);
                    cmd.Parameters.AddWithValue("@BrugerID", Convert.ToString(OpretOpgaveComboBox.Text));
                    cmd.Parameters.AddWithValue("@Nummerplade", Convert.ToString(OpgaveNummerpladeTextBox.Text));
                    cmd.Parameters.AddWithValue("@Opgave", Convert.ToString(IndtastOpgaveTextBox.Text));
                    cmd.Parameters.AddWithValue("@KM", AntalKMnumericUpDown.Value);
                    cmd.Parameters.AddWithValue("@DatoOprettet", BrugerIDOpretOpgaveDatetime.Value);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show(string.Format("Opgaven: '{0}' er oprettet i systemet", IndtastOpgaveTextBox.Text), "Opgave oprettelse");
                    ShowDataInComboBox();
                }

                else

                {

                }
            }
            else
            {
                MessageBox.Show(k, "Opfyldelse af krav fejlbesked");
            }

            //DataSet objDS = new DataSet();
            //objDS.Tables.Add(UserTable);
            //objDS.Tables.Add(OpgaveTable);
            //DataRelation objRelation = new DataRelation("objRelation", UserTable.Columns["BrugerID"], OpgaveTable.Columns["BrugerID"]);

            //objDS.Relations.Add(objRelation);
        }

        private void BrugerIDOpretOpgaveTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void BrugerIDOpretOpgaveDatetime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void IndtastOpgaveTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PersonComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            RedigerIDTextBox.Text = PersonComboBox.SelectedValue.ToString();        }

        private void SletPersonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OpretOpgaveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OpgaveNummerpladeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AntalKMnumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
