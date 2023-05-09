using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace FrontEnd
{
    public class ListContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
                String itemToAdd = SelectedEntityName + Utils.MAIN_FILTER_SEPARATOR + SelectedProperty + Utils.MAIN_FILTER_SEPARATOR + SelectedOperator+Utils.MAIN_FILTER_SEPARATOR + FilterValue;
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
           FetchedEntries = Utils.populateTableWithEntries(SelectedEntityName, PickedFilters);
        });


       
        
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
                if (value != null)
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
                    Utils.getSelectedClassFields(value);
                PickedFilters.Clear();
                FetchedEntries = Utils.populateTableWithEntries(SelectedEntityName, PickedFilters);
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
            EntityNames = Utils.getAllClassesNames();
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