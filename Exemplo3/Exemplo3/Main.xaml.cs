using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exemplo3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : ContentPage
    {
        bool runtask = false;
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
            }
        }
    }
}