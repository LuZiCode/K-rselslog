using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KørselsLogFormsApp;

namespace KørselsLogClassLibrary
{
    public class Opgave
    {
        public string BrugerIdInput;
        public string OpgaveTekst;
        //private DataDto _datadto;
        StringBuilder sb = new StringBuilder();
        public Opgave(string BrugerID, string Opgave)
        {
            BrugerIdInput = BrugerID;
            OpgaveTekst = Opgave;
        }

        public string CheckOpgaveRequirements()
        {
            TjekEfterID();
            TjekOpgave();
            return sb.ToString();
        }

        private void TjekOpgave()
        {
            if (String.IsNullOrEmpty(OpgaveTekst))
            {
                sb.AppendLine("Indtast en opgave");
            }
        }

        private void TjekEfterID()
        {

            SqlConnection con = new SqlConnection("Data Source=WIN-NDGDCIETDGL;Initial Catalog=KørselLogC#Datebase;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand checkForDublicates = new SqlCommand("select BrugerID from UserTable where BrugerID='" + BrugerIdInput + "'", con);
            int? bid = (int?)checkForDublicates.ExecuteScalar();

            if (bid != int.Parse(BrugerIdInput))
            {
                sb.AppendLine("BrugerID findes ikke");
            }
        }
    }
}
