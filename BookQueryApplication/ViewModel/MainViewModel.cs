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
        public ICommand TitleAuthorByAuthorCommand { get; private set; }
        public ICommand AuthorForTitleCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            bookDataSource = new BooksEntities();
            TitleAuthorCommand = new RelayCommand(SetTitleAuthorListing);
            TitleAuthorByAuthorCommand = new RelayCommand(
                SetTitleAuthorListingOrderByAuthor
                );
            AuthorForTitleCommand = new RelayCommand(
                SetAuthorTitleListing);
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
            var titleAuthorList = from title in bookDataSource.Titles
                                  from author in title.Authors
                                  orderby title.Title1
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

        private void SetTitleAuthorListingOrderByAuthor()
        {
            var titleAuthorList = from title in bookDataSource.Titles
                                  from author in title.Authors
                                  orderby title.Title1, author.FirstName, author.LastName
                                  select new { title.Title1, author.FirstName,
                                               author.LastName };
            SearchResult = "";
            string tempResult = "";
            foreach (var titleAuthor in titleAuthorList)
            {
                tempResult += string.Format("Title: {0}\t\t\tAuthor: {1}{2}\r\n",
                    titleAuthor.Title1, titleAuthor.FirstName, titleAuthor.LastName);
            }
            SearchResult = tempResult;
        }

        private void SetAuthorTitleListing()
        {
            var authorTitleListQuery = from title in bookDataSource.Titles
                                       orderby title.Title1
                                       select new { title.Title1, title.Authors };

            SearchResult = "";
            string tempResult = "";
            foreach( var authorTitle in authorTitleListQuery )
            {
                tempResult += string.Format("{0}\r\n", authorTitle.Title1);
                var sortedAuthors = from author in authorTitle.Authors
                                orderby author.LastName, author.FirstName
                                select author;
                                
                foreach ( var author in sortedAuthors )
                {
                    tempResult += string.Format("{0} {1}\r\n", author.FirstName,
                        author.LastName);
                }
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