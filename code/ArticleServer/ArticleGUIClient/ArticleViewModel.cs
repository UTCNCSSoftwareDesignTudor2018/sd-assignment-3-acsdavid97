using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ArticleClientServerCommons.Dto;
using ArticleGUIClient.Annotations;

namespace ArticleGUIClient
{
    public class ArticleViewModel : INotifyPropertyChanged
    {
        private readonly ArticleClient _client = new ArticleClient();
        private ArticleDto _selectedArticleDto;

        public ArticleViewModel()
        {
            Articles = _client.GetArticleDtos();
        }

        public IList<ArticleDto> Articles { get; private set; }

        public ICommand UpdateCommand => new RelayCommand<object>(ExecuteUpdate, CanExecuteUpdate);

        private static bool CanExecuteUpdate(object o)
        {
            return true;
        }

        private void ExecuteUpdate(object o)
        {
            Articles = _client.GetArticleDtos();
            OnPropertyChanged(nameof(Articles));
        }

        public ArticleDto SelectedArticleDto
        {
            get => _selectedArticleDto;
            set
            {
                _selectedArticleDto = value; 
                OnPropertyChanged(nameof(SelectedArticleDto));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
