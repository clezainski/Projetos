using CadastroCorrentista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroCorrentista.Controllers
{
    public class ClienteController : BasePage
    {
        private static Cliente temp;
        public static bool Adicionar(Cliente cliente)
        {
            temp = new Cliente();

            if ((temp = BuscaDeCliente(cliente.Cpf)) == null)
            {
                try
                {
                    cliente.Ativo = true;
                    contexto.Clientes.Add(cliente);
                    contexto.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                if (temp.Ativo == false)
                {
                    try
                    {
                        temp.AgenciaId = cliente.AgenciaId;
                        temp.Ativo = true;
                        temp.Conta = cliente.Conta;
                        temp.Nome = cliente.Nome;
                        temp.Sexo = cliente.Sexo;

                        contexto.Entry(temp).State =
                        System.Data.Entity.EntityState.Modified;
                        contexto.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Editar(Cliente cliente)
        {
            //if ((cliente = BuscaDeClienteAtivos(cliente.Cpf)) != null)
            if (BuscaDeClienteAtivos(cliente.Cpf) != null)
            {
                try
                {
                    contexto.Entry(cliente).State =
                        System.Data.Entity.EntityState.Modified;
                    contexto.SaveChanges();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
            else return false;
        }

        public static bool Excluir(Cliente cliente)
        {
            if ((cliente = BuscaDeCliente(cliente.Cpf)) != null)
            {
                try
                {
                    cliente.Ativo = false;
                    contexto.Entry(cliente).State =
                        System.Data.Entity.EntityState.Modified;
                    contexto.SaveChanges();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
            else return false;
        }

        public static Cliente BuscaDeCliente(string cpf)
        {
            return contexto.Clientes.FirstOrDefault(x=>x.Cpf.Equals(cpf));
        }

        public static Cliente BuscaDeClienteAtivos(string cpf)
        {
            temp = new Cliente();
            if((temp = BuscaDeCliente(cpf)) != null)
            {
                if (temp.Ativo == true)
                {
                    return temp;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static bool VerificaAgenciaVinculadaACliente(int agenciaid)
        {
            if(contexto.Clientes.FirstOrDefault(x => x.AgenciaId == agenciaid)!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Cliente> ListarClientes()
        {
            List<Cliente> temp = new List<Cliente>();

            foreach (Cliente itemAtivo in contexto.Clientes)
            {
                if (itemAtivo.Ativo == true)
                {
                    temp.Add(itemAtivo);
                }
            }
            return temp;
        }
    }
}