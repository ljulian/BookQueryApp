using GalaSoft.MvvmLight;
using BookQueryApplication.Model;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace BookQueryApplication.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {



        private string _searchResult;
        private BooksEntities bookDataSource;

        public ICommand TitleAuthorCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            bookDataSource = new BooksEntities();
            TitleAuthorCommand = new RelayCommand(SetTitleAuthorListing);
        }

        public string SearchResult
        {
            get => _searchResult;

            set
            {
                _searchResult = value;
                RaisePropertyChanged("SearchResult");
            }
        }

        private void SetTitleAuthorListing()
        {
            string test = "ehlo";
            var titleAuthorList = from title in bookDataSource.Titles
                                  from author in title.Authors
                                  select new { title.Title1, author };
            SearchResult = "";
            string tempResult = "";
            foreach (var titleAuthor in titleAuthorList)
            {
                tempResult += string.Format("Title: {0}\t\t Author: {1} {2}\r\n", 
                                              titleAuthor.Title1, titleAuthor.author.FirstName,
                                              titleAuthor.author.LastName); 
            }
            SearchResult = tempResult;
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}