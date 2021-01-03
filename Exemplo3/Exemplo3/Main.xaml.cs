using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exemplo3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : ContentPage
    {
        readonly CancellationTokenSource s_cts = new CancellationTokenSource();
        //private string filename = Path.Combine(Xamarin.Essentials.FileSystem.CacheDirectory, "numeros.txt");
        private string filename = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "numeros.txt");

        bool runtask = false;
        int primeiro = 10000;
        int segundo =  99999;

        string numeros_ficheiro;

        public Main()
        {
            InitializeComponent();
            Barra.IconImageSource = ImageSource.FromResource("Exemplo3.Resources.play.ico");

            primeiroInicial.Text = primeiro.ToString();
            segundoInicial.Text = segundo.ToString();

            Task.Run(leFicheiro);
            Task.Run(atualizaUI);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (runtask)
            {
                //a tarefa vai parar e mostra botao para iniciar
                Console.WriteLine("Carreguei no Stop");
                Barra.IconImageSource = ImageSource.FromResource("Exemplo3.Resources.play.ico");
                runtask = false;
            }
            else
            {
                //a tarefa vai ser executada e mostra botao para parar
                Console.WriteLine("Carreguei no Start");
                Barra.IconImageSource = ImageSource.FromResource("Exemplo3.Resources.stop.ico");
                runtask = true;
                Task.Run(Tarefa);
            }
        }

async Task Tarefa()
        {
            try
            {
                while (runtask)
                {
                    var tarefaIncrementa = Incrementa();
                    var tarefaDecrementa = Decrementa();
                    await Task.WhenAll(tarefaIncrementa, tarefaDecrementa);
                    await Task.Run(escreveFicheiro);
                    await Task.Run(leFicheiro);
                    await Task.Run(atualizaUI);
                    Thread.Sleep(1000);
                };
                await Task.Run(escreveFicheiro);
            }
            catch
            {
                Console.WriteLine("Erro na Task Tarefa");
            }
        }
        Task atualizaUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                // UI interaction here
                primeiroIncrementa.Text = primeiro.ToString();
                segundoDecrementa.Text = segundo.ToString();
                numerosFicheiro.Text = numeros_ficheiro;
            });
            return Task.CompletedTask;
        }
        Task leFicheiro()
        {
            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                numeros_ficheiro = content.ToString();
            }
            return Task.CompletedTask;
        }
        Task escreveFicheiro()
        {
            using (var sw = new StreamWriter(filename, false))
            {
                Console.WriteLine(filename);
                sw.WriteLine(primeiro);
                sw.WriteLine(segundo);
            }
            return Task.CompletedTask;
        }
        Task Incrementa()
        {
            primeiro++;
            return Task.CompletedTask;
        }
        Task Decrementa()
        {
            segundo--;
            return Task.CompletedTask;
        }
    }
}
