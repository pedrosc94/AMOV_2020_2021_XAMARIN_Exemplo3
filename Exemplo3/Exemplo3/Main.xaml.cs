using System;
using System.Collections.Generic;
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
        bool runtask = false;
        int primeiro = 10000;
        int segundo =  99999;

        public Main()
        {
            InitializeComponent();
            Barra.IconImageSource = ImageSource.FromResource("Exemplo3.Resources.play.ico");

            primeiroInicial.Text = primeiro.ToString();
            segundoInicial.Text = segundo.ToString();

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
                    await Task.WhenAll(Incrementa(), Decrementa());
                    await Task.Run(atualizaUI);
                    Thread.Sleep(1000);
                };
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
            });
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

    /*
    async Task Incrementa()
    {
        await Task.Factory.StartNew(() =>
        {
            while (runtask)
            {
                if (primeiro > 99999)
                {
                    s_cts.Cancel();
                    //return Task.CompletedTask;
                }
                else
                {
                    primeiro++;
                    Console.WriteLine("primeiro: " + primeiro);
                }
                System.Threading.Thread.Sleep(1000);
            };
        });
    }

    async Task Decrementa()
    {
        await Task.Factory.StartNew(() =>
        {
            while (runtask)
            {
                if (segundo < 10000)
                {
                    s_cts.Cancel();
                    //return Task.CompletedTask;
                }
                else
                {
                    segundo--;
                    Console.WriteLine("segundo: " + segundo);
                }
                System.Threading.Thread.Sleep(1000);
            };
        });
    }
}*/