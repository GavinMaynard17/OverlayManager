using OverlayManager.Commands;
using OverlayManager.Models;
using OverlayManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OverlayManager.ViewModels
{
    public class CasterListViewModel : ViewModelBase
    {
        private readonly Match _match;

        public ICommand GoBackCommand { get; } 
		public ICommand AddCasterCommand { get; }
		public ICommand RemoveCasterCommand { get; }
        public CasterListViewModel(Match match, NavigationService selectGameNavigationService) 
        {
            _match = match;
            GoBackCommand = new NavigateCommand(selectGameNavigationService);
			AddCasterCommand = new AddCasterCommand(_match, this);
			RemoveCasterCommand = new RemoveCasterCommand(_match, this);

			CasterList = _match.Casters;
        }

		private ObservableCollection<Caster> casterList;
		public ObservableCollection<Caster> CasterList
		{
			get
			{
				return casterList;
			}
			set
			{
				casterList = value;
				OnPropertyChanged(nameof(CasterList));
			}
		}

		private string casterName;
		public string CasterName
		{
			get
			{
				return casterName;
			}
			set
			{
				casterName = value;
				OnPropertyChanged(nameof(CasterName));
			}
		}

		private string casterUsername;
		public string CasterUsername
		{
			get
			{
				return casterUsername;
			}
			set
			{
				casterUsername = value;
				OnPropertyChanged(nameof(CasterUsername));
			}
		}

	}
}
