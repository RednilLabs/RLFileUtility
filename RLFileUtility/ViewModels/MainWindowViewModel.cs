using Microsoft.Win32;
using RLFileUtility.Business;
using RLFileUtility.Models;
using RLFIleUtility.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RLFileUtility.ViewModels
{
    class MainWindowViewModel:ViewModelBase
    {
        #region Fields
        private ObservableCollection<FileObject> _filesCollection;
        private ObservableCollection<FileObject> _newFilesCollection;

        private string _mainWindowTitleText;
        private ICommand _selectFilesCommand;
        private ICommand _stripUnderscoresCommand;

        private ICollectionView _filesCollectionView;
        #endregion

        #region Properties
        public string MainWindowTitleText
        {
            get { return _mainWindowTitleText; }
            set { _mainWindowTitleText = value; }
        }
        public ObservableCollection<FileObject> FilesCollection
        {
            get
            { FileOperator.DeleteFile("pathAndFile");
                return _filesCollection;

            }
             set { }
        }

        #endregion


        #region Commands
        public ICommand SelectFilesCommand
        {
            get
            {
                if (_selectFilesCommand == null)
                    _selectFilesCommand = new RelayCommand(param => this.DoAllowDuplicatesCommand(), param => this.CanDoAllowDuplicatesCommand);

                return _selectFilesCommand;
            }
        }
        private bool CanDoAllowDuplicatesCommand
        {
            get
            {
                return true;
             
            }

        }
        private void DoAllowDuplicatesCommand()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            

            if(openFileDialog.ShowDialog()==true)
            {

                _filesCollection = new ObservableCollection<FileObject>();

                foreach(string fileName in openFileDialog.FileNames)
                {
                    FileObject fileObject = new FileObject();
                    fileObject.FileName = fileName;
                    
                    _filesCollection.Add(fileObject);
                }

                OnPropertyChanged("FilesCollection");
            }

        }

        public ICommand StripUnderscoresCommand
        {
            get
            {
                if (_stripUnderscoresCommand == null)
                    _stripUnderscoresCommand = new RelayCommand(param => this.DoStripUnderscoresCommand(), param => this.CanDoStripUnderscoresCommand);

                return _stripUnderscoresCommand;
            }
        }
        private bool CanDoStripUnderscoresCommand
        {
            get
            {
                return true;

            }

        }
        private void DoStripUnderscoresCommand()
        {

            _newFilesCollection = new ObservableCollection<FileObject>();

            foreach(FileObject fileObject in FilesCollection)
            {
                //string newFileName = FileOperator.RemoveLastXCharactersFromFileName(fileObject.FileName, 2);
                string originalFileName = fileObject.FileName;

                if(!FileOperator.FileExists(originalFileName))
                {
                    continue;
                }

                string newFileName = FileOperator.StripUnderscoresToEnd(fileObject.FileName);
                FileOperator.RenameFile(fileObject.FileName, newFileName);

                if(FileOperator.FileExists(newFileName))
                {

                    FileObject newFileObject = new FileObject();
                    newFileObject.FileName = newFileName;

                    _newFilesCollection.Add(newFileObject);

                    if(originalFileName.ToUpper().Contains("_"))
                    {
                        FileOperator.DeleteFile(originalFileName);
                    }
                    OnPropertyChanged("FilesCollection");

                }

                fileObject.FileName = newFileName;
                             

            }

            //FilesCollection.items
            //FilesCollection.Add(_newFilesCollection[0]);


        }


        #endregion

    }
}
