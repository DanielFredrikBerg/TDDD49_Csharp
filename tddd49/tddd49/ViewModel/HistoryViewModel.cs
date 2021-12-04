using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tddd49.ViewModel
{
    class HistoryViewModel : ViewModelBase
    {

        private String searchWord;

        public String SearchWord { get => searchWord; set { searchWord = value; OnPropertyChanged("SearchWord"); } }

        public HistoryViewModel()
        {

        }

    }
}
