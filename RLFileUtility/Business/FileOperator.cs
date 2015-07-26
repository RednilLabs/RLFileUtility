using Microsoft.Win32;
using RLFileUtility.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLFileUtility.Business
{
    class FileOperator
    {
        #region FIELDS

        #endregion


        #region CONSTRUCTORS

        #endregion


        #region PROPERTIES

        #endregion


        #region METHODS
        /// <summary>
        /// <para>Copies an Applications specified Resource File to disc.</para>
        /// <para>Returns false if unsuccessful.</para>
        /// </summary>
        /// <param name="destPathAndFile"></param>
        /// <param name="resourceToCopy"></param>
        /// <returns></returns>
        public static bool CopyApplicationResourceToDisk(string destPathAndFile, byte[] resourceToCopy)
        {
            try
            {
                System.IO.File.WriteAllBytes(destPathAndFile, resourceToCopy);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// Creates the supplied directory, e.g. 'c:\ParentFolder\subFolder'.  Returns False if unable to create directory.
        /// </summary>
        /// <param name="directoryToCreate"></param>
        /// <returns></returns>
        public static bool CreateDirectory(string directoryToCreate)
        {
            if (!DirectoryExists(directoryToCreate))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(directoryToCreate);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        internal static MethodResults CopyFile(string sourcePathAndFile, string destinationPathAndFile)
        {
            try
            {
                File.Copy(sourcePathAndFile, destinationPathAndFile);
                return new MethodResults { TrueFalse = true, Message = "None" };
            }
            catch (Exception copyException)
            {

                return new MethodResults
                {
                    TrueFalse = false,
                    Message = string.Format("Error Copying File. \n\n Error Message:\n{0}\n\nSource: \n{1}", copyException.ToString(), copyException.Source.ToString())
                };
            }

        }
        public static bool DeleteDirectorySubFoldersAndFiles(string parentDirectory)
        {
            bool noFailures = true;
            foreach (var subDirectory in new DirectoryInfo(parentDirectory).GetDirectories())
            {
                try
                {
                    subDirectory.Delete(true);
                }
                catch (Exception)
                {
                    noFailures = false;
                }

            }
            return noFailures;
        }
        /// <summary>
        /// Deletes all files in the specified directory.  Returns false if one or more files are left, or directory doesn't exist.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteFilesInDirectory(string path)
        {
            if (!DirectoryExists(path))
            {
                return false;
            }

            string[] files = System.IO.Directory.GetFiles(path);


            foreach (string file in files)
            {
                // Use static Path methods to extract only the file name from the path.
                try
                {
                    System.IO.File.Delete(file);
                }
                catch (Exception)
                {

                }
            }

            files = System.IO.Directory.GetFiles(path);

            int filesLeft = files.Count();

            return (filesLeft < 1);

        }
        /// <summary>
        /// Deletes the specified file on the file system. Returns false if File still found after delete operation.
        /// </summary>
        /// <param name="pathAndFile"></param>
        /// <returns></returns>
        public static bool DeleteFile(string pathAndFile)
        {
            if (FileExists(pathAndFile))
            {
                try
                {
                    System.IO.File.Delete(pathAndFile);
                }
                catch (Exception e) 
                {
                    string x = e.ToString();
                    return false;
                }

                //  Now confirm the file no longer exists.
                return (!FileExists(pathAndFile));
            }

            return true;
        }
        /// <summary>
        /// Returns True if the specified directory exists.
        /// </summary>
        /// <param name="directoryToCheck"></param>
        /// <returns></returns>
        public static bool DirectoryExists(string directoryToCheck)
        {
            return System.IO.Directory.Exists(directoryToCheck);
        }
        /// <summary>
        /// Given 'C:\SomeDirectory\SomeFile.txt', returns 'Somefile.txt'.  Returns null if error.
        /// </summary>
        /// <param name="filePathAndName"></param>
        /// <returns></returns>
        public static string ExtractFileName(string filePathAndName)
        {
            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePathAndName);
                return fileInfo.Name;
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// Returns true if given file exists. False if not or if error.
        /// </summary>
        /// <param name="filePathAndName">The full path and file name, e.g. 'c:\SomeDirectory\somFileName.ext'</param>
        /// <returns></returns>
        public static bool FileExists(string filePathAndName)
        {
            try
            {
                if (File.Exists(filePathAndName))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Returns True if the file has an extension of '.catdrawing', False if not, or error occurs, or file not found.
        /// </summary>
        /// <param name="filePathAndName"></param>
        /// <returns></returns>
        public static bool FileTypeIsCatiaDrawing(string filePathAndName)
        {
            try
            {
                if (string.Compare(GetFileExtension(filePathAndName), ".CATDRAWING", true) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }


        }
        /// <summary>
        /// Returns True if the file has an extension of '.catpart', False if not, or error occurs, or file not found.
        /// </summary>
        /// <param name="filePathAndName"></param>
        /// <returns></returns>
        public static bool FileTypeIsCatiaPart(string filePathAndName)
        {
            try
            {
                if (string.Compare(GetFileExtension(filePathAndName), ".CATPART", true) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
        /// <summary>
        /// Returns True if the file has an extension of '.catproduct', False if not, or error occurs, or file not found.
        /// </summary>
        /// <param name="filePathAndName"></param>
        /// <returns></returns>
        public static bool FileTypeIsCatiaProduct(string filePathAndName)
        {
            try
            {
                if (string.Compare(GetFileExtension(filePathAndName), ".CATPRODUCT", true) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }


        }
        /// <summary>
        /// Returns True if the file has an extension of '.cgr', False if not, or error occurs, or file not found.
        /// </summary>
        /// <param name="filePathAndName"></param>
        /// <returns></returns>
        public static bool FileTypeIsCGR(string filePathAndName)
        {
            try
            {
                if (string.Compare(GetFileExtension(filePathAndName), ".cgr", true) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
        /// <summary>
        /// Returns 'true' if the file specified has an extension of .pdf, ignoring Case.  
        /// Returns 'false' if not a PDF, or file not found, or error.
        /// </summary>
        /// <param name="filePathAndName"></param>
        /// <returns></returns>
        public static bool FileTypeIsPDF(string filePathAndName)
        {
            if (FileExists(filePathAndName))
            {
                try
                {
                    if (string.Compare(GetFileExtension(filePathAndName), ".PDF", true) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {

                    return false;
                }
            }

            return false;

        }
        /// <summary>
        /// <para>Returns the directory that serves as a common repository for application-specific data that is used by all users.</para>
        /// <para>E.g.  'C:\ProgramData\SomeAppName</para>
        /// <para>Same as 'Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)'.</para>
        /// <para>Returns null if not found or error.</para>
        /// <para>Further info: http://msdn.microsoft.com/en-us/library/system.environment.specialfolder.aspx</para>
        /// </summary>
        /// <returns></returns>
        static public string GetApplicationPublicDataFolder()
        {
            try
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// <para>Returns the directory that serves as a common repository for application-specific data for the current roaming user.</para>
        /// <para>E.g. 'C:\Users\jdoexxxx\AppData\Roaming\SomeAppName</para>
        /// <para>Same as 'Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)'.</para>
        /// <para>Returns null if not found or error.</para>
        /// </summary>
        /// <returns></returns>
        static public string GetApplicationUserDataFolder()
        {
            try
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Returns the extension of the supplied file name, e.g. '.txt'
        /// </summary>
        /// <param name="filePathAndName"></param>
        /// <returns></returns>
        public static string GetFileExtension(string filePathAndName)
        {
            return Path.GetExtension(filePathAndName);
        }
       
        /// <summary>
        /// Returns filename with x number of characters removed from the end (not including the file extension)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="numberOfCharsToRemove"></param>
        /// <returns></returns>
        public static string RemoveLastXCharactersFromFileName(string fileName, int numberOfCharsToRemove=0)
        {
            int lengthOfString = fileName.Length;
            string originalFileName = fileName;
            string newFileName;
            string fileExt = GetFileExtension(fileName);
            newFileName=fileName.Remove(lengthOfString - numberOfCharsToRemove-fileExt.Length);
            newFileName = newFileName + fileExt;
            
            return newFileName;
        }


        
        /// <summary>
        /// <para>Renames a file. Returns True if successful.</para>
        /// <para>Returns False if unsuccessful, or designated file wasn't found.</para>
        /// </summary>
        /// <param name="filePathAndname"></param>
        /// <param name="newFilePathAndName"></param>
        /// <returns></returns>
        public static bool RenameFile(string filePathAndname, string newFilePathAndName)
        {
            if (FileExists(filePathAndname))
            {
                try
                {
                    System.IO.File.Copy(filePathAndname, newFilePathAndName, true);
                }
                catch (Exception)
                {
                    return false;
                }

                return FileExists(newFilePathAndName);
            }
            return false;
        }
        /// <summary>
        /// <para>Returns the string value of FullName from the selected file.</para>
        /// <para>E.g. Returns:  'C:\MyDirectory\Myfile.ext' </para>
        /// <para>Returns null if no file is selected.</para>
        /// </summary>
        /// <param name="preferredStartingDirectory">e.g. 'c:\\MyInitialDirectory\\</param>
        /// <param name="filterString">e.g. 'txt files (*.txt)|*.txt'</param>
        /// <returns></returns>
        public static string SelectFileFromBrowser(string preferredStartingDirectory = null, string filterString = null)
        {

            string fileName;

            OpenFileDialog ofd = new OpenFileDialog();

            //	Initialize OFD's options:
            if (preferredStartingDirectory != null)
            {
                ofd.InitialDirectory = preferredStartingDirectory;
            }
            if (filterString != null)
            {
                ofd.Filter = filterString;
            }

            ofd.Multiselect = false;

            //	Present OFD and get results:
            if (ofd.ShowDialog() == true ) // was:           DialogResult.OK
            {
                fileName = ofd.FileName;
            }
            else
            {
                fileName = null;
            }
            
            //ofd.Dispose();

            return fileName;
        }
        /// <summary>
        /// <para>Returns the string values of FullName from the selected file(s).</para>
        /// <para>e.g. Returns:  'C:\MyDirectory\Myfile1.ext, C:\MyDirectory\Myfile2.ext' </para>
        /// <para>Returns null if no file is selected.</para>
        /// </summary>
        /// <param name="preferredStartingDirectory">e.g. 'c:\\MyInitialDirectory\\</param>
        /// <param name="filterString">e.g. 'txt files (*.txt)|*.txt'</param>
        /// <returns></returns>
        public static Array SelectMultiFilesFromBrowser(string preferredStartingDirectory = null, string filterString = null)
        {
            Array fileNames;    // e.g. "C:\MyDirectory\Myfile1.ext, C:\MyDirectory\Myfile2.ext"

            OpenFileDialog ofd = new OpenFileDialog();

            //	Initialize OFD's options:
            if (preferredStartingDirectory != null)
            {
                ofd.InitialDirectory = preferredStartingDirectory;
            }
            if (filterString != null)
            {
                ofd.Filter = filterString;
            }

            ofd.Multiselect = true;

            //	Present OFD and get results:
            if (ofd.ShowDialog() == true) //was:  ==DialogResult.OK
            {
                fileNames = ofd.FileNames;
            }
            else
            {
                fileNames = null;
            }

           // ofd.Dispose();

            return fileNames;

        }
        
        
        public static string StripUnderscoresToEnd(string fileName)
        {
            int lengthOfString = fileName.Length;
            int location = fileName.IndexOf("_");
            if(location<1)
            {
                return fileName;
            }
            //string originalFileName = fileName;
            string newFileName;
            string fileExt = GetFileExtension(fileName);
           
            newFileName = fileName.Remove(location);
            newFileName = newFileName + fileExt;

            return newFileName;
        }
        
        
        #endregion




    }
}
