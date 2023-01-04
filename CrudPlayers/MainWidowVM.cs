using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
namespace CrudPlayers
{
    public class MainWidowVM : INotifyPropertyChanged
    {
        public ObservableCollection<Player> listaPlayer { get; set; }

        public RelayCommand Add { get; private set; }
        public RelayCommand Remove { get; private set; }
        public RelayCommand Alterar { get; private set; }
        public Player PlayerSelecionado { get; set; }
        private IDatabase conexaoDB { get; set; }

        public MainWidowVM()
        {
            conexaoDB = new DataBase();
            listaPlayer = new ObservableCollection<Player>(conexaoDB.Listar()) { };
            IniciaComandos();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notifica(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void IniciaComandos()
        {
            Add = new RelayCommand((object param) =>
            {
                Player playerCadastro = new Player();


                CadastroPlayer adicionarPlayer = new CadastroPlayer();
                adicionarPlayer.DataContext = playerCadastro;
                adicionarPlayer.ShowDialog();

                conexaoDB.Inserir(playerCadastro);
                MessageBox.Show("Player Registrado com sucesso");
                atualizaLista();
            });

            Remove = new RelayCommand((object param) =>
            {
                conexaoDB.Deletar(PlayerSelecionado);
                MessageBox.Show("Player Removido com sucesso");
                atualizaLista();
            });

            Alterar = new RelayCommand((object param) =>
            {

                if(PlayerSelecionado != null)
                {
                    Player editarPlayer = PlayerSelecionado.CopiarPlayer();
                    
                    CadastroPlayer cadastroPlayer = new CadastroPlayer();
                    cadastroPlayer.DataContext = PlayerSelecionado;

                    bool? verificaAtualizacao = cadastroPlayer.ShowDialog();
                    if(verificaAtualizacao.HasValue && verificaAtualizacao.Value)
                    {
                        //PlayerSelecionado = editarPlayer;
                        conexaoDB.Atualizar(PlayerSelecionado);
                    }
                }
           
            });
        }

        private void atualizaLista()
        {
            listaPlayer.Clear();
            try
            {
                List<Player> list = new List<Player>(conexaoDB.Listar());
                for (int i = 0; i < list.Count; i++)
                    listaPlayer.Add(list[i]);
            }
            catch (Exception)
            {
                MessageBox.Show("Nao foi possivel carregar a lista");
            }
        }

    }
}
