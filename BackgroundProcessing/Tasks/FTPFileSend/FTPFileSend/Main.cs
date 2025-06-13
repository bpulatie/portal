using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using AsyncLibrary;
using System.Net;
using System.IO;

namespace FTPFileSend
{
    public class Main
    {
        private static Database DB = new Database("spa_portal");
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        private static Chilkat.SFtp sftp = new Chilkat.SFtp();

        private static string pHostName = string.Empty;
        private static string pPort = string.Empty;
        private static string pUserName = string.Empty;
        private static string pPassword = string.Empty;
        private static string pRenameRemoteFiles = string.Empty;
        private static string pFilenameMask = string.Empty;
        private static string pDeleteLocalFile = string.Empty;
        private static string pLocalDirectory = string.Empty;

        private static string pRemoteDirectory = string.Empty;
        
        public static void OnExecute(string[] Args)
        {
            logger.Info("FTPFileSend: Starting");

            string context = async.GetJobContext(Args[0]);
            logger.Info("FTPFileSend: Context=" + context);

            pHostName = utils.GetParameter(context, "HostName");
            pPort = utils.GetParameter(context, "Port");
            pUserName = utils.GetParameter(context, "UserName");
            pPassword = utils.GetParameter(context, "Password");
            pRenameRemoteFiles = utils.GetParameter(context, "RenameRemoteFiles");
            pFilenameMask = utils.GetParameter(context, "FilenameMask");
            pDeleteLocalFile = utils.GetParameter(context, "DeleteLocalFile");
            pLocalDirectory = utils.GetParameter(context, "LocalDirectory");
            pRemoteDirectory = utils.GetParameter(context, "RemoteDirectory");

            Connect(pHostName, pPort, pUserName, pPassword);

            List<string> files = GetFilesInFolder(pLocalDirectory, pFilenameMask);

            bool deleteFiles = false;
            if (pDeleteLocalFile == "y")
                deleteFiles = true;

            bool renameFiles = false;
            if (pRenameRemoteFiles == "y")
                renameFiles = true;

            foreach (string file in files)
            {
                string filename = Path.GetFileName(file);
                UploadFile(filename, pLocalDirectory, pRemoteDirectory);

                if (renameFiles)
                    RenameRemoteFile(pRemoteDirectory + "/" + filename, pRemoteDirectory + "/" + filename + ".done");

                if (deleteFiles)
                    RemoveLocalFile(pLocalDirectory + "/" + filename);
            }

            Disconnect();
            logger.Info("FTPFileSend: Ending");
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
                logger.Error("FTPFileSend: Connect " + sftp.LastErrorText);
                throw new Exception("FTPFileSend: Connect " + sftp.LastErrorText);
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
                logger.Error("FTPFileSend: Connect " + sftp.LastErrorText);
                throw new Exception("FTPFileSend: Connect " + sftp.LastErrorText);
            }

            success = sftp.AuthenticatePw(username, password);
            if (success != true)
            {
                logger.Error("FTPFileSend: Connect " + sftp.LastErrorText);
                throw new Exception("FTPFileSend: Connect " + sftp.LastErrorText);
            }

            //  After authenticating, the SFTP subsystem must be initialized:
            success = sftp.InitializeSftp();
            if (success != true)
            {
                logger.Error("FTPFileSend: Connect " + sftp.LastErrorText);
                throw new Exception("FTPFileSend: Connect " + sftp.LastErrorText);
            }
        }

        private static List<string> GetFilesInFolder(string localFolder, string filenameMask)
        {
            List<string> files = new List<string>();

            string[] array2 = Directory.GetFiles(localFolder, filenameMask);
            foreach (string f in array2)
            {
                files.Add(f);
            }

            return files;
        }

        private static void RemoveLocalFile(string filename)
        {
            try
            {
                File.Delete(filename);
            }
            catch (IOException ex)
            {
                logger.Error("FTPFileRetrieval: RemoveLocalFile: Unable to remove local file " + ex.Message);
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

        private static void UploadFile(string file, string localFilePath, string remoteFilePath)
        {
            //  Open a file for writing on the SSH server.
            //  If the file already exists, it is overwritten.
            //  (Specify "createNew" instead of "createTruncate" to
            //  prevent overwriting existing files.)
            string handle = sftp.OpenFile(remoteFilePath + "/" + file, "writeOnly", "createTruncate");
            if (sftp.LastMethodSuccess != true)
            {
                logger.Error("FTPFileSend: UploadFile " + sftp.LastErrorText);
                throw new Exception("FTPFileSend: UploadFile " + sftp.LastErrorText);
            }

            //  Upload from the local file to the SSH server.
            bool success = sftp.UploadFile(handle, localFilePath + "/" + file);
            if (success != true)
            {
                logger.Error("FTPFileSend: UploadFile " + sftp.LastErrorText);
                throw new Exception("FTPFileSend: UploadFile " + sftp.LastErrorText);
            }

            //  Close the file.
            success = sftp.CloseHandle(handle);
            if (success != true)
            {
                logger.Error("FTPFileSend: UploadFile " + sftp.LastErrorText);
                throw new Exception("FTPFileSend: UploadFile " + sftp.LastErrorText);
            }

        }

    }
}
