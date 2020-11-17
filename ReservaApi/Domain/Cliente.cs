using System;

namespace TestesHttpClient.ReservaApi.Domain
{
    public class Cliente
    {
        public long NumeroCpfCnpj { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public int Tipo { get; set; }

        public bool EhVip() => Tipo == 1;

        public bool PodeReservar(DateTime dataEntrada)
        {
            var diasAntecedencia = (dataEntrada.Date - DateTime.Today).Days;
            if ((EhVip() && diasAntecedencia < 1) || (!EhVip() && diasAntecedencia < 10))
            {
                return false;
            }
            return true;
        }
    }
}
