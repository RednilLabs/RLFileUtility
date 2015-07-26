using Microsoft.Win32;
using RLFileUtility.Business;
using RLFileUtility.Models;
using RLFileUtility.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RLFileUtility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private ObservableCollection<FileObject> _filesCollection;
        private List<string> _filesToDelete;
        public ObservableCollection<FileObject> FilesCollection
        {
            get
            {
                //FileOperator.DeleteFile("pathAndFile");
                return _filesCollection;

            }
            set { }
        }

        public MainWindow()
        {
            InitializeComponent();


            MainWindowViewModel mainWindowVM = new MainWindowViewModel();
            mainWindowVM.MainWindowTitleText = "File Rename Utility     - ver  " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - © 2015 Rednil Labs.       ";
            //this.DataContext = mainWindowVM;
            mygrid.DataContext = this;
            mygrid.ItemsSource = FilesCollection;
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            mygrid.Items.Refresh();
        }

        private void CleanUp(object sender, RoutedEventArgs e)
        {

            _filesToDelete = new List<string>();

            foreach (FileObject fileObject in FilesCollection)
            {
                

                string originalFileName = fileObject.FileName;

                if (!FileOperator.FileExists(originalFileName))
                {
                    continue;
                }



                string newFileName = FileOperator.StripUnderscoresToEnd(fileObject.FileName);
                FileOperator.RenameFile(originalFileName, newFileName);


                if (FileOperator.FileExists(newFileName))
                {

                   // FileObject newFileObject = new FileObject();
                    //newFileObject.FileName = newFileName;

                    

                    if (originalFileName.ToUpper().Contains("_"))
                    {

                        var attr = File.GetAttributes(originalFileName);
                        attr = attr & ~FileAttributes.ReadOnly;
                        File.SetAttributes(originalFileName,attr);

                        FileOperator.DeleteFile(originalFileName);

                       
                    }
                    

                }

                fileObject.FileName = newFileName;



                mygrid.Items.Refresh();
            }   //end foreach

           
        }

        private void SelectFilesClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;


            if (openFileDialog.ShowDialog() == true)
            {

                _filesCollection = new ObservableCollection<FileObject>();

                foreach (string fileName in openFileDialog.FileNames)
                {
                    FileObject fileObject = new FileObject();
                    fileObject.FileName = fileName;

                    _filesCollection.Add(fileObject);
                }
                mygrid.ItemsSource = FilesCollection;
                mygrid.Items.Refresh();

            }
        }


    }
}
