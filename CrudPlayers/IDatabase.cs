using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudPlayers
{
    public interface IDatabase
    {
        List<Player> Listar();
        void Inserir(Player player);
        void Deletar(Player player);
        void Atualizar(Player player);
    }
}
