
using System.Windows;

namespace JogoGourmet
{
    static class Program
    {

        static void Main()
        {
            var questao = new Questao("massa");
            questao.QuestaoPositivo = new Questao("lasanha", questao);
            questao.QuestaoNegativo = new Questao("bolo de chocolate", questao);

            while (MessageBox.Show("Pense em um prato que você gosta...", "Vou adivinhar seu pensamento, quer ver?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                questao.Perguntar();
            }
        }
    }
}
