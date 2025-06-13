using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using AsyncLibrary;
using System.Net;
using System.IO;

namespace FTPFileRetrieval
{
    public class Main
    {
        private static Database DB = new Database("spa_portal");
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        private static Chilkat.SFtp sftp = new Chilkat.SFtp();

        private static string execution_id = string.Empty;
        private static List<string> files = new List<string>();

        private static string pHostName = string.Empty;
        private static string pPort = string.Empty;
        private static string pUserName = string.Empty;
        private static string pPassword = string.Empty;
        private static string pRemoteDirectory = string.Empty;
        private static string pFilenameMask = string.Empty;
        private static string pRemoveRemoteFiles = string.Empty;
        private static string pLocalDirectory = string.Empty;


        public static void OnExecute(string[] Args)
        {
            logger.Info("FTPFileRetrieval: Starting");

            execution_id = Args[0];
            logger.Info("FTPFileRetrieval: Execution_id=" + execution_id);

            string context = async.GetJobContext(execution_id);
            logger.Info("FTPFileRetrieval: Context=" + context);

            pHostName = utils.GetParameter(context, "HostName");
            pPort = utils.GetParameter(context, "Port");
            pUserName = utils.GetParameter(context, "UserName");
            pPassword = utils.GetParameter(context, "Password");
            pRemoteDirectory = utils.GetParameter(context, "RemoteDirectory");
            pFilenameMask = utils.GetParameter(context, "FilenameMask");
            pRemoveRemoteFiles = utils.GetParameter(context, "RemoveRemoteFiles");
            pLocalDirectory = utils.GetParameter(context, "LocalDirectory");

            Connect(pHostName, pPort, pUserName, pPassword);

            List<string> files = GetRemoteDirectoryListing(pRemoteDirectory, pFilenameMask);

            bool deleteFiles = false;
            if (pRemoveRemoteFiles == "y")
                deleteFiles = true;

            foreach (string file in files)
            {
                DownloadFile(file, pRemoteDirectory, pLocalDirectory);
                if (deleteFiles) 
                    RemoveRemoteFile(pRemoteDirectory + "/" + file);
            }

            Disconnect();

            string Files = string.Empty;
            foreach(string file in files)
            {
                if (Files.Length < 1)
                    Files = pLocalDirectory + "/" + file;
                else
                    Files += "&" + pLocalDirectory + "/" + file;
            }

            if (Files.Length < 1)
                async.Notify(execution_id, "No Files Retrieved from FTP site");

            async.AddJobContext(execution_id, "RetrievedFiles=" + Files + ";");

            logger.Info("FTPFileRetrieval: Ending");
        }

        private static void Disconnect()
        {
            sftp.Disconnect();
        }

        private static void Connect(string hostname, string port, string username, string password)
        {
            //  Any string automatically begins a fully-functional 30-day trial.
            bool success = sftp.UnlockComponent("focxzcSSH_5MfPXnvG9CnG");
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: Connect " + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: Connect " + sftp.LastErrorText);
            }

            //  Set some timeouts, in milliseconds:
            sftp.ConnectTimeoutMs = 15000;
            sftp.IdleTimeoutMs = 15000;

            int iPort = 22;
            try
            {
                iPort = int.Parse(port);
            }
            catch 
            {
                iPort = 22;
            }

            success = sftp.Connect(hostname, iPort);
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: Connect " + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: Connect " + sftp.LastErrorText);
            }

            success = sftp.AuthenticatePw(username, password);
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: Connect " + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: Connect " + sftp.LastErrorText);
            }

            //  After authenticating, the SFTP subsystem must be initialized:
            success = sftp.InitializeSftp();
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: Connect " + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: Connect " + sftp.LastErrorText);
            }

        }

        private static void DownloadFile(string file, string remoteFilePath, string localFilePath)
        {
            //  Download the file:
            bool success = sftp.DownloadFileByName(remoteFilePath + "/" + file, localFilePath + "/" + file);
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: DownloadFile: Remote File=" + remoteFilePath + "/" + file + " Error=" + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: DownloadFile: Remote File=" + remoteFilePath + "/" + file + " Error=" + sftp.LastErrorText);
            }

            async.Notify(execution_id, file + " Retrieved from FTP Location");
        }

        private static void DownloadFolder(string remoteDirectory, string pattern, string localDirectory, string recursive)
        {
            //  Download all the files from the remote directory 

            //  Mode 0 causes SyncTreeDownload to download all files.
            int mode = 0;
            if (pattern == string.Empty)
                sftp.SyncMustMatch = pattern;

            //  Do not recursively descend the remote directory tree.  Just download all the files in specified directory.
            bool Recursive = false;
            if (recursive.ToUpper() == "Y")
            {
                Recursive = true;
            }

            bool success = sftp.SyncTreeDownload(remoteDirectory, localDirectory, mode, Recursive);
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: DownloadFolder: Error=" + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: DownloadFolder: Error=" + sftp.LastErrorText);
            }
        }

        private static List<string> GetRemoteDirectoryListing(string remoteDirectory, string pattern)
        {
            List<string> files = new List<string>();

            //  Open a directory on the server...
            //  Paths starting with a slash are "absolute", and are relative
            //  to the root of the file system. Names starting with any other
            //  character are relative to the user's default directory (home directory).
            //  A path component of ".." refers to the parent directory,
            //  and "." refers to the current directory.
            string handle;
            handle = sftp.OpenDir(remoteDirectory);
            if (sftp.LastMethodSuccess != true)
            {
                logger.Error("FTPFileRetrieval: GetRemoteDirectoryListing: Error=" + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: GetRemoteDirectoryListing: Error=" + sftp.LastErrorText);
            }

            if (pattern != string.Empty)
                sftp.ReadDirMustMatch = pattern;

            //  Download the directory listing:
            Chilkat.SFtpDir dirListing = null;
            dirListing = sftp.ReadDir(handle);
            if (sftp.LastMethodSuccess != true)
            {
                logger.Error("FTPFileRetrieval: GetRemoteDirectoryListing: Error=" + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: GetRemoteDirectoryListing: Error=" + sftp.LastErrorText);
            }

            //  Iterate over the files.
            int i;
            int n = dirListing.NumFilesAndDirs;
            if (n == 0)
            {
                logger.Error("FTPFileRetrieval: GetRemoteDirectoryListing: No entries found in this directory.");
            }

            for (i = 0; i <= n - 1; i++)
            {
                Chilkat.SFtpFile fileObj = null;
                fileObj = dirListing.GetFileObject(i);

                files.Add(fileObj.Filename);
                logger.Info("FTPFileRetrieval: GetRemoteDirectoryListing: File: " + fileObj.Filename + " Size: " + Convert.ToString(fileObj.Size32));
            }

            //  Close the directory
            bool success = sftp.CloseHandle(handle);
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: GetRemoteDirectoryListing: Close Handle Error=" + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: GetRemoteDirectoryListing: Close Handle Error=" + sftp.LastErrorText);
            }

            return files;
        }

        private static void RemoveRemoteFile(string file)
        {
            bool success = sftp.RemoveFile(file);
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: Delete Remote File: Error=" + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: Delete Remote File: Error=" + sftp.LastErrorText);
            }
            success = sftp.RemoveFile(file + ".done");
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: Done file not found");
            }
        }

        private static void RenameRemoteFile(string oldfile, string newfile)
        {
            //  Rename the file or directory:
            bool success = sftp.RenameFileOrDir(oldfile, newfile);
            if (success != true)
            {
                logger.Error("FTPFileRetrieval: Renaming File: from=" + oldfile + " to=" + newfile + " Error=" + sftp.LastErrorText);
                throw new Exception("FTPFileRetrieval: Renaming File: from=" + oldfile + " to=" + newfile + " Error=" + sftp.LastErrorText);
            }
        }
    }
}
