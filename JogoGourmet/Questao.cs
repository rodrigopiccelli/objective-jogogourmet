using Microsoft.VisualBasic;
using System.Windows;

namespace JogoGourmet
{
    public class Questao
    {

        public Questao QuestaoAnterior { get; private set; }
        public Questao QuestaoPositivo { get; set; }
        public Questao QuestaoNegativo { get; set; }

        private readonly string conteudoQuestao;

        public Questao(string conteudoQuestao, Questao questaoAnterior = null)
        {
            this.conteudoQuestao = conteudoQuestao;
            QuestaoAnterior = questaoAnterior;
        }

        public void Perguntar()
        {
            string texto = QuestaoNegativo == null ? $"O prato que você pensou só pode ser {conteudoQuestao}! Acertei?" : $"O prato que você pensou é {conteudoQuestao}?";
            if (MessageBox.Show(texto, "Hmmmm...", MessageBoxButton.YesNo, QuestaoNegativo == null ? MessageBoxImage.Information : MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(QuestaoPositivo != null)
                {
                    QuestaoPositivo.Perguntar();
                }
                else
                {
                    MessageBox.Show("Viu só? Consegui adivinhar!", "Eu sou um gênio!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                if (QuestaoNegativo != null)
                {
                    QuestaoNegativo.Perguntar();
                }
                else
                {
                    var novaResposta = Interaction.InputBox("Me conta, em qual prato você pensou?", "Tudo bem, eu desisto!");
                    if (string.IsNullOrEmpty(novaResposta?.Trim()))
                        return;

                    var conteudoNovaQuestao = Interaction.InputBox($"{novaResposta} é _______ mas {conteudoQuestao} não é.", "Me explica...");
                    if (string.IsNullOrEmpty(conteudoNovaQuestao?.Trim()))
                        return;

                    var novaQuestao = new Questao(conteudoNovaQuestao, QuestaoAnterior);
                    
                    if(QuestaoAnterior.QuestaoPositivo == this)
                    {
                        QuestaoAnterior.QuestaoPositivo = novaQuestao;
                    }
                    else
                    {
                        QuestaoAnterior.QuestaoNegativo = novaQuestao;
                    }

                    novaQuestao.QuestaoPositivo = new Questao(novaResposta, novaQuestao);
                    novaQuestao.QuestaoNegativo = this;
                    QuestaoAnterior = novaQuestao;

                    MessageBox.Show("Admito minha derrota, vamos de novo!", "Ok, entendi...", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }

    }
}
