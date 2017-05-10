﻿using rabi_splitter_WPF.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace rabi_splitter_WPF
{
    public class ExportableVariable
    {
        // Do not make these properties public.
        private static int nextAvailableId = 0;
        private readonly int _id;
        private readonly string _displayName;

        private ExportableVariable(string displayName)
        {
            _id = nextAvailableId++;
            _displayName = displayName;
        }

        public int Id
        {
            get { return _id; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public static List<ExportableVariable> GetAll()
        {
            return new List<ExportableVariable>()
            {
                new ExportableVariable("TestVariable")
            };
        }

        #region Equals, GetHashCode
        public override bool Equals(object obj)
        {
            var otherValue = obj as ExportableVariable;
            if (otherValue == null) return false;
            return _id.Equals(otherValue.Id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        #endregion
    }

    public class VariableExportSetting : INotifyPropertyChanged
    {
        private ExportableVariable _selectedVariable;
        private string _outputFileName;
        private string _outputFormat;
        private bool _isExporting;
        private bool _isPreviewingFormat;

        #region Dictionaries
        private static Dictionary<ExportableVariable, string> _variableCaptions;
        
        public Dictionary<ExportableVariable, string> VariableCaptions
        {
            get
            {
                if (_variableCaptions == null)
                {
                    _variableCaptions = ExportableVariable.GetAll().ToDictionary(ev => ev, ev => ev.DisplayName);
                }
                return _variableCaptions;
            }
        }

        internal void DefaultButton_Click()
        {
            OutputFormat = "DEFAULT OUTPUT FORMAT: " + OutputFileName;
        }

        #endregion

        public VariableExportSetting()
        {
            // Default values
            _selectedVariable = null;
            _outputFileName = "";
            _outputFormat = "";
            _isExporting = false;
            _isPreviewingFormat = false;
        }

        #region Parameters

        public ExportableVariable SelectedVariable
        {
            get { return _selectedVariable; }
            set
            {
                if (value.Equals(_selectedVariable)) return;
                _selectedVariable = value;
                OnPropertyChanged(nameof(SelectedVariable));
            }
        }

        public string OutputFileName
        {
            get { return _outputFileName; }
            set
            {
                if (value.Equals(_outputFileName)) return;
                _outputFileName = value;
                OnPropertyChanged(nameof(OutputFileName));
            }
        }

        public string OutputFormat
        {
            get { return _outputFormat; }
            set
            {
                if (value.Equals(_outputFormat)) return;
                _outputFormat = value;
                OnPropertyChanged(nameof(OutputFormat));
            }
        }

        public bool IsPreviewingFormat
        {
            get { return _isPreviewingFormat; }
            set
            {
                if (value.Equals(_isPreviewingFormat)) return;
                _isPreviewingFormat = value;
                OnPropertyChanged(nameof(IsPreviewingFormat));
            }
        }

        public bool IsExporting
        {
            get { return _isExporting; }
            set
            {
                if (value.Equals(_isExporting)) return;
                _isExporting = value;
                OnPropertyChanged(nameof(IsExporting));
            }
        }


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
