using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using A01CEP.Services;
using A01CEP.Services.Model;

namespace A01CEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
                string cep = CEP.Text.Trim();

            if (isValidCEP(cep)) {
                try {
                        Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null) {
                        RESULTADO.Text = string.Format("CEP {0}:\n{1}, {2}\nBairro {3}\n{4}", end.cep, end.localidade, end.uf, end.bairro, end.logradouro);
                    }
                    else {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }
                }
                catch(Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;
            
            if(cep.Length != 8){
                DisplayAlert("Erro", "CEP invalido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }

            int novoCEP = 0;

            if (!int.TryParse(cep, out novoCEP)){
                DisplayAlert("Erro", "CEP invalido! O CEP deve ser composto apenas por números", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
