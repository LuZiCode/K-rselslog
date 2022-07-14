using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KørselsLogFormsApp;

namespace KørselsLogClassLibrary
{
    public class Requirements
    {
        public string BrugerIdInput;
        public string NavnInput;
        public string NrPladeInput;
        //private DataDto _datadto;
        StringBuilder sb = new StringBuilder();
        public Requirements(string BrugerID, string Navn, string Nummerplade)
        {
            BrugerIdInput = BrugerID;
            NavnInput = Navn;
            NrPladeInput = Nummerplade;
        }


        public string CheckRequirements()
        {
            TjekBrugerID();
            //TjekOmDetAlleredeEksisterer();
            TjekNavn();
            TjekNrPlade();
            return sb.ToString();
        }

        private void TjekOmDetAlleredeEksisterer()
        {
            SqlConnection con = new SqlConnection("Data Source=WIN-NDGDCIETDGL;Initial Catalog=KørselLogC#Datebase;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand checkForDublicates = new SqlCommand("select BrugerID from UserTable where BrugerID='"+ BrugerIdInput +"'", con);
            int? bid = (int?)checkForDublicates.ExecuteScalar();

            if (bid == int.Parse(BrugerIdInput))
            {
                sb.AppendLine("BrugerID findes allerede");
            }
        }

        private void TjekBrugerID()
        {
            string mellemrum = " "; bool indholderBogstaver = false;
            //if (BrugerIdInput.Length < 8 || BrugerIdInput.Length > 8)
            //{
            //    if (String.IsNullOrEmpty(BrugerIdInput))
            //    {
            //        sb.AppendLine("Indtast brugerID!");
            //    }
            //    else
            //    {
            //        sb.AppendLine("BrugerID skal være 8 karakterer lang");
            //    }
            //}
            if (string.IsNullOrEmpty(BrugerIdInput))
            {
                sb.AppendLine("Indtast BrugerID!");
            }
            else
            {
                if (BrugerIdInput.Length < 8 || BrugerIdInput.Length > 8)
                {
                    sb.AppendLine("BrugerID må kun være 8 karakterer lang");
                }
                else
                {
                    TjekOmDetAlleredeEksisterer();
                }
            }
            if (BrugerIdInput.Contains(mellemrum))
            {
                sb.AppendLine("BrugerID må ikke indholde mellemrum");
            }

            for (int i = 0; i < BrugerIdInput.Length; i++)
                if ((BrugerIdInput[i] ^ '0') > 9)
                {
                    indholderBogstaver = true;
                }

            if (indholderBogstaver == true) sb.AppendLine("BrugerID må kun indholde tal");
        }

        private void TjekNavn()
        {
            if (String.IsNullOrEmpty(NavnInput))
            {
                sb.AppendLine("Indtast et navn!");
            }
        }

        private void TjekNrPlade()
        {
            if (NrPladeInput.Length < 7)
            {
                if (String.IsNullOrEmpty(NrPladeInput))
                {
                    sb.AppendLine("Indtast en nummerplade!");
                }
                else
                {
                    sb.AppendLine("Venlist indtast en dansk nummerplade");
                }
            }

        }
    }
}
