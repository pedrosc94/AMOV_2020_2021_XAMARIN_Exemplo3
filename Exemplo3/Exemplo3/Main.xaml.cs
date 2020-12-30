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
        int segundo  = 99999;

        public Main()
        {
            InitializeComponent();
            Barra.IconImageSource = ImageSource.FromResource("Exemplo3.Resources.play.ico");
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e) {
            if (runtask) {
                //a tarefa esta parada
                Barra.IconImageSource = ImageSource.FromResource("Exemplo3.Resources.play.ico");
                runtask = false;
            }
            else{
                //a tarefa esta a ser executada
                Barra.IconImageSource = ImageSource.FromResource("Exemplo3.Resources.stop.ico");
                runtask = true;
                //Task tarefa = 
            }
        }

        async Task Tarefa()
        {
            if (!runtask) s_cts.Cancel();
            primeiro++;
            segundo--;
        }
    }
}