using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdemServico_ADO.Services
{
    public class GerarOrdemServico
    {
        public static string GerarOS()
        {
            int tamanho = 7; 
            string numeroOS = string.Empty;
            for (int i = 0; i < tamanho; i++)
            {
                Random random = new Random();
                int codigo = Convert.ToInt32(random.Next(48, 122).ToString());

                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))
                {
                    string _char = ((char)codigo).ToString();
                    if (!numeroOS.Contains(_char))
                    {
                        numeroOS += _char;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }
            return numeroOS.ToUpper();
        }
    }
}