using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ArticleClientServerCommons;
using ArticleClientServerCommons.Dto;

namespace ArticleGUIClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArticleViewModel _articleViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _articleViewModel = new ArticleViewModel();
            this.DataContext = _articleViewModel;

            var thread = new Thread(WaitForUpdate);
            thread.Start();
        }

        private void WaitForUpdate(object ob)
        {
            var tcpClient = new TcpClient("127.0.0.1", 11001);
            var stream = tcpClient.GetStream();

            while (true)
            {
                var response = Utils.ReadObject<string>(stream);
                if (response == Constants.Update)
                {
                    _articleViewModel.UpdateArticles();
                }
            }
        }
    }
}
