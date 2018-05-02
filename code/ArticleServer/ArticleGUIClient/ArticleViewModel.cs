using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            EditingArticleDto = new ArticleDto();
            WriterDto = new WriterDto();
        }

        public IList<ArticleDto> Articles { get; private set; }

        public ICommand AddCommand => new RelayCommand<object>(ExecuteAdd, CanExecuteAdd);
        public ICommand UpdateCommand => new RelayCommand<object>(ExecuteUpdate, CanExecuteUpdate);

        private bool CanExecuteUpdate(object obj)
        {
            return SelectedArticleDto != null;
        }

        private void ExecuteUpdate(object obj)
        {
            var success = _client.UpdateArticle(EditingArticleDto, SelectedArticleDto, WriterDto);
            if (!success)
            {
                MessageBox.Show("Error while updating, check the input");
            }
        }

        private static bool CanExecuteAdd(object o)
        {
            return true;
        }

        private void ExecuteAdd(object o)
        {
            var success = _client.AddArticle(EditingArticleDto, WriterDto);
            if (!success)
            {
                MessageBox.Show("Error While inserting, check the input");
            }

        }

        public void UpdateArticles()
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

        public ArticleDto EditingArticleDto { get; set; }
        public WriterDto WriterDto { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
