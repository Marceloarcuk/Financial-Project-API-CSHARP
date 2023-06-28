using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos
{
    public class DespesaServico : IDespesaServico
    {
        //--------------------------------------------
        //CONTRUTOR DA CLASSE
        //--------------------------------------------
        private readonly InterfaceDespesa _interfaceDespesa;
        public DespesaServico(InterfaceDespesa interfaceDespesa)
        {
            _interfaceDespesa = interfaceDespesa;
        }
        //--------------------------------------------
        public async Task AdicionarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataCadastro = data;
            despesa.DataAlteracao = data;
            despesa.Ano = data.Year;
            despesa.Mes = data.Month;
            var valido = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
            if (valido)
                await _interfaceDespesa.Add(despesa);

        }

        public async Task AtualizarDespesa(Despesa despesa)
        {
            var data = DateTime.UtcNow;
            despesa.DataAlteracao = data;
            if(despesa.Pago)
                despesa.DataPagamento = data;
            var valido = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
            if (valido)
                await _interfaceDespesa.Update(despesa);
        }

        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            var despesaUsuario = await _interfaceDespesa.ListarDespesasUsuario(emailUsuario);
            var despesaAnterior= await _interfaceDespesa.ListarDespesasUsuarioNaoPagasMesAnterior(emailUsuario);
            var despesaUsuarioNaoPagasMesAnterior = despesaAnterior.Any() 
                                                     ? despesaAnterior.ToList().Sum(x => x.Valor) 
                                                     : 0;
            var despesaPaga = despesaUsuario.Where(d => d.Pago && d.TipoDespesa == Entities.Enums.EnumTipoDespesa.Contas)
                .Sum(x => x.Valor);
            var despesaPendentes = despesaUsuario.Where(d => !d.Pago && d.TipoDespesa == Entities.Enums.EnumTipoDespesa.Contas)
                .Sum(x => x.Valor);
            var investimento = despesaUsuario.Where(d => d.TipoDespesa == Entities.Enums.EnumTipoDespesa.Investimento)
                .Sum(x => x.Valor);

            return new
            {
                sucesso = "OK",
                despesaPaga = despesaPaga,
                despesaPendentes = despesaPendentes,
                despesaUsuarioNaoPagasMesAnterior = despesaUsuarioNaoPagasMesAnterior,
                investimento = investimento,

            };
        }

    }
}
