using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KørselsLogFormsApp;

namespace KørselsLogClassLibrary
{
    public class ORequirements
    {
        public string OpgaveInput;
        public decimal KMInput;
        public string NrPladeInput;
        //private DataDto _datadto;
        StringBuilder sb = new StringBuilder();
        public ORequirements(string Nummerplade, string Opgave, decimal KM)
        {
            NrPladeInput = Nummerplade;
            OpgaveInput = Opgave;
            KMInput = KM;
        }


        public string CheckRequirements()
        {
            TjekNrPlade();
            TjekOpgave();
            TjekKM();
            return sb.ToString();
        }

        private void TjekKM()
        {
            if (KMInput <= 0)
            {
                sb.AppendLine("Indtast antal KM!");
            }
        }

        private void TjekOpgave()
        {
            if (String.IsNullOrEmpty(OpgaveInput))
            {
                sb.AppendLine("Indtast en opgave!");
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
