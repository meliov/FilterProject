using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using BackEnd;
using BackEnd.db;

namespace FrontEnd
{
    public class ListContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static EntityService entityService = new EntityService();
        private ObservableCollection<String> _entityNames;
        private ObservableCollection<String> _properties;
        private ObservableCollection<String> _operators;
        private String _selectedEntityName;
        private String _selectedProperty;
        private String _selectedOperator;
        private String _selectedFilter;

        private ObservableCollection<string> _pickedFilters;

        private ObservableCollection<Object> _fetchedEntries;

        private String _filterValue;

        private string MAIN_FILTER_SEPARATOR = "___";
        
        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                PropChanged("SelectedFilter");
                OnPropertyChanged();
            }
        }


        public new ICommand AddFilterCommand => new DelegateCommand(() =>
        {
            if (SelectedOperator == null)
            {
                MessageBox.Show("Please select Filter Action");
            }
            else
            {
                // MessageBox.Show("Hello " + SelectedEntityName + " "+ SelectedProperty+" " + SelectedOperator + FilterValue  + "!");
                String itemToAdd = SelectedEntityName + MAIN_FILTER_SEPARATOR + SelectedProperty + MAIN_FILTER_SEPARATOR + SelectedOperator+MAIN_FILTER_SEPARATOR + FilterValue;
                if (!PickedFilters.Contains(itemToAdd))
                {
                    PickedFilters.Add(itemToAdd);
                }
            }

            //FilterValue = "";
        });

        public ICommand RemoveFilterCommand => new DelegateCommand(() =>
        {
            if (SelectedFilter != null)
            {
                PickedFilters.Remove(SelectedFilter);
            }
            else
            {
                MessageBox.Show("Please select Filter");
            }
        });

        public new ICommand ApplyFilterCommand => new DelegateCommand(() =>
        {
            populateTableWithEntries();
        });


        private void populateTableWithEntries()
        {
            Dictionary<string, string> filters = new Dictionary<string, string>();
            foreach (var pickedFilter in PickedFilters)
            {
                string filterValueToAdd = "";
                var elementsSeparator = MAIN_FILTER_SEPARATOR;
                var choppedFilter = pickedFilter.Split(new string[]{elementsSeparator},StringSplitOptions.None);
                //0 SelectedEntityName + MAIN_FILTER_SEPARATOR + 1 SelectedProperty + MAIN_FILTER_SEPARATOR +2 SelectedOperator+ MAIN_FILTER_SEPARATOR + 3 FilterValue;
                string filterPropertyToAdd = choppedFilter[1];
                string filterOperator = choppedFilter[2];
                if (filterOperator.Equals("is"))
                {
                    filterValueToAdd = choppedFilter[3];
                }
                else
                {
                    filterValueToAdd = choppedFilter[2] + choppedFilter[3];
                }
                filters.Add(filterValueToAdd,filterPropertyToAdd);
            }

            FetchedEntries = new ObservableCollection<Object>(entityService
                .FetchEntriesByClassNameAndFilterThem(SelectedEntityName, filters));
        }
        
        public ObservableCollection<String> PickedFilters
        {
            get => _pickedFilters;
            set
            {
                _pickedFilters = value;
                PropChanged("PickedFilters");
            }
        }

        public ObservableCollection<Object> FetchedEntries
        {
            get => _fetchedEntries;
            set
            {
                _fetchedEntries = value;
                PropChanged("FetchedEntries");
            }
        }


        public string FilterValue
        {
            get => _filterValue;
            set
            {
                _filterValue = value;
                PropChanged("FilterValue");
                if (Regex.IsMatch(value, "[a-zA-Z]+"))
                {
                    Operators = new ObservableCollection<string>(new List<string> { "is" });
                }
                else
                {
                    Operators = new ObservableCollection<string>(new List<string> { "==", ">", "<", ">=", "<=" });
                }
                SelectedOperator = null;
            }
        }


        public string SelectedProperty
        {
            get => _selectedProperty;
            set
            {
                _selectedProperty = value;
                PropChanged("SelectedProperty");
                FilterValue = "";
                SelectedOperator = null;
            }
        }

        public string SelectedOperator
        {
            get => _selectedOperator;
            set
            {
                _selectedOperator = value;
                PropChanged("SelectedOperator");
            }
        }

        public string SelectedEntityName
        {
            get => _selectedEntityName;
            set
            {
                _selectedEntityName = value;
                PropChanged("SelectedEntityName");
                Properties =
                    new ObservableCollection<string>(entityService.getSelectedClassFields(value).Select(it => it.Key)
                        .ToList());
                PickedFilters.Clear();
                populateTableWithEntries();
                SelectedOperator = null;
                FilterValue = null; ;
            }
        }

        public ObservableCollection<string> Operators
        {
            get => _operators;
            set
            {
                _operators = value;
                PropChanged("Operators");
            }
        }

        public ObservableCollection<string> Properties
        {
            get => _properties;
            set
            {
                _properties = value;
                PropChanged("Properties");
            }
        }

        public ObservableCollection<string> EntityNames
        {
            get => _entityNames;
            set
            {
                _entityNames = value;
                PropChanged("EntityNames");
            }
        }

        public ListContext()
        {
            EntityNames = new ObservableCollection<string>(entityService.getAllClassesNames());
            Properties =
                new ObservableCollection<string>(new List<string> { "" });
            Operators = new ObservableCollection<string>(new List<string> { "==", ">", "<", ">=", "<=" });
            PickedFilters = new ObservableCollection<String>();
            FetchedEntries = new ObservableCollection<Object>();
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void PropChanged(String propertyName)
        {
            //Did WPF registerd to listen to this event
            if (PropertyChanged != null)
            {
                //Tell WPF that this property changed
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}