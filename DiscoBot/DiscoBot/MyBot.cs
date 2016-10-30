using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscoBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random rand;

        string[] freshestMemes;
        string[] freshestQuotes;

        public MyBot()
        {
            rand = new Random();

            freshestMemes = new string[]
                {
                    "mem/mem1.jpg",
                    "mem/mem2.jpg",
                    "mem/mem3.jpg",
                    "mem/mem4.jpg",
                    "mem/mem5.jpg",
                    "mem/mem6.jpg",
                    "mem/mem7.jpg",
                    "mem/mem8.jpg",
                    "mem/mem9.jpg",
                    "mem/mem10.jpg",
                    "mem/mem11.jpg"
                };

            freshestQuotes = new string[]
                {
                    "O primeiro pecado da humanidade foi a fé; a primeira virtude foi a dúvida.",
                    "O mais competente não discute, domina a sua ciência e cala-se.",
                    "A vida sem ciência é uma espécie de morte.",
                    "O meu computador não liga, o que eu faço? Dê o troco e não ligue pra ele também.",
                    "Onde posso baixar um antivírus pro coração?",
                    "O desperdício de papel vai acabar com as florestas. Não imprimo mais nada em casa, lá não se usa papel nenhum, está tudo salvo no computador e só limpo a bunda no tablet.",
                    "Acho que o coração devia ter a opção apagar histórico.",
                    "Digito, logo existo.",
                    "Se a vida fosse um cartão de memória, eu formatava e começaria tudo de novo.",
                    "Em pleno século XXI e nenhum aplicativo para arcar com as consequências dos meus atos.",
                    "99% das pessoas riem quando apertam F13.",
                    "Gata, você não é a fonte do meu PC, mas sem você eu vivo desligado.",
                    "As coisas que amo, deixo livres pra partir. Mas estou tranquilo, nunca vi um computador sair andando por aí.",
                    "Não importa se é virtual, o importante é que é amor.",
                    "Vou no mercado comprar uma caixa de Sucrilhos pra ver se o meu Windows para de pedir serial.",
                    "As senhas são como roupas íntimas: Você não pode deixar ninguém vê-la, você deve alterar regularmente e você não deve compartilhá-la com estranhos.",
                    "A Polícia Federal acaba de divulgar que encontrou provas no apartamento do Lula, mas só até a 4ª série."
                };

            discord = new DiscordClient(x =>
        {
            x.LogLevel = LogSeverity.Info;
            x.LogHandler = Log;
        });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '/';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            RegisterMemeCommand();
            RegisterQuoteCommand();
            RegisterCleanCommand();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("mytoken", TokenType.Bot);
            });
        }

        private void RegisterMemeCommand()
        {
            commands.CreateCommand("meme")
                .Do(async (e) =>
                {
                    int randomMemeIndex = rand.Next(freshestMemes.Length);
                    string memeToPost = freshestMemes[randomMemeIndex];
                    await e.Channel.SendFile(memeToPost);
                });

            commands.CreateCommand("quote")
                .Do(async (e) =>
                {
                    int randomTextIndex = rand.Next(freshestQuotes.Length);
                    string quoteToPost = freshestQuotes[randomTextIndex];
                    await e.Channel.SendMessage(quoteToPost);
                });
        }

        private void RegisterQuoteCommand()
        {
            commands.CreateCommand("quote")
                .Do(async (e) =>
                {
                    int randomTextIndex = rand.Next(freshestQuotes.Length);
                    string quoteToPost = freshestQuotes[randomTextIndex];
                    await e.Channel.SendMessage(quoteToPost);
                });
        }

        private void RegisterCleanCommand()
        {
            commands.CreateCommand("clean")
                .Do(async (e) =>
                {
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(100);

                    await e.Channel.DeleteMessages(messagesToDelete);
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
