using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace TrayApp
{
    public partial class MainForm : Form
    {

        private string savePath;
        private string logPath;
        private List<string> watchPaths;
        private List<FileSystemWatcher> watcherList;



        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        private System.Windows.Forms.Label watchPathLabel;
        private System.Windows.Forms.Label savePathLabel;
        private System.Windows.Forms.Label logPathLabel;
        private System.Windows.Forms.TextBox watchPathTextBox;
        private System.Windows.Forms.TextBox savePathTextBox;
        private System.Windows.Forms.TextBox logPathTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button closeButton;

        private FileSystemWatcher watcher = new FileSystemWatcher();

        public MainForm()
        {

            InitializeComponent();


            // INI 파일 경로를 지정하고 값을 로드합니다.
            string iniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.ini");
            LoadPathsFromIniFile(iniFilePath);



            // Tray icon setup
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Start", null, Start_Click);
            trayMenu.Items.Add("Close", null, Close_Click);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "Tray Application";
            trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;

            // FileSystemWatcher setup


            watcher.IncludeSubdirectories = true;

            foreach (string path in watchPaths)
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = path;
                watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
                watcher.Filter = "*.*";
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Renamed += new RenamedEventHandler(OnRenamed);
                watcher.Created += new FileSystemEventHandler(OnCreated);
                watcher.EnableRaisingEvents = true;
                
            }

        }




        private void LoadPathsFromIniFile(string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                INIFileParser parser = new INIFileParser(iniFilePath);
                List<string> watchPathList = parser.GetValueList("Paths", "WatchPath", "");
                watchPaths = new List<string>(); // watchPaths 초기화 추가
                foreach (string path in watchPathList)
                {
                    if (!watchPaths.Contains(path))
                    {
                        watchPaths.Add(path);
                    }
                }
                savePath = parser.GetValue("Paths", "SavePath", "");
                logPath = parser.GetValue("Paths", "LogPath", "");

                // WatchPaths를 화면에 표시
                watchPathTextBox.Text = string.Join(", ", watchPaths.ToArray());
                savePathTextBox.Text = savePath;
                logPathTextBox.Text = logPath;
            }
            else
            {
                MessageBox.Show("INI file not found: " + iniFilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Start_Click(object sender, EventArgs e)
        {
            watcher.EnableRaisingEvents = true;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                foreach (var watchPath in watchPaths)
                {
                    if (e.FullPath.StartsWith(watchPath, StringComparison.InvariantCultureIgnoreCase))
                    {
                        string lastWriteTime = File.GetLastWriteTime(e.FullPath).ToString("yy-MM-dd_HH-mm-ss");
                        string backupFolderPath = Path.Combine(savePath, lastWriteTime);

                        // 폴더가 없으면 생성
                        if (!Directory.Exists(backupFolderPath))
                        {
                            Directory.CreateDirectory(backupFolderPath);
                        }

                        if (File.Exists(e.FullPath))
                        {
                            string fileName = Path.GetFileName(e.FullPath);
                            string destinationPath = Path.Combine(backupFolderPath, fileName);

                            if (!File.Exists(destinationPath))
                            {
                                File.Copy(e.FullPath, destinationPath);
                                string logMessage = $"File backed up: {destinationPath}";
                                WriteLog(logMessage, logPath);
                            }
                        }
                        else if (Directory.Exists(e.FullPath))
                        {
                            string sourcePath = e.FullPath;
                            string destinationPath = Path.Combine(backupFolderPath, Path.GetFileName(sourcePath));

                            DirectoryCopy(sourcePath, destinationPath, true);
                            string logMessage = $"Directory backed up: {destinationPath}";
                            WriteLog(logMessage, logPath);
                        }
                    }
                }
            }
            catch (IOException ioException)
            {
                string logMessage = $"Error: {ioException.Message}";
                WriteLog(logMessage, logPath);
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                foreach (string watchPath in watchPaths)
                {
                    if (e.FullPath.StartsWith(watchPath))
                    {
                        string lastWriteTime = File.GetLastWriteTime(e.FullPath).ToString("yy-MM-dd_HH-mm-ss");
                        string backupFolderPath = Path.Combine(savePath, lastWriteTime);

                        // 폴더가 없으면 생성
                        if (!Directory.Exists(backupFolderPath))
                        {
                            Directory.CreateDirectory(backupFolderPath);
                        }

                        string destinationPath = Path.Combine(backupFolderPath, Path.GetFileName(e.FullPath));

                        if (Directory.Exists(e.FullPath)) // 폴더일 경우
                        {
                            DirectoryCopy(e.FullPath, destinationPath, true);
                        }
                        else // 파일일 경우
                        {
                            File.Copy(e.FullPath, destinationPath, true);
                        }

                        string logMessage = $"File backed up: {destinationPath}";
                        WriteLog(logMessage, logPath);
                        break;
                    }
                }
            }
            catch (IOException ioException)
            {
                string logMessage = $"Error: {ioException.Message}";
                WriteLog(logMessage, logPath);
            }
        }




        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                foreach (string watchPath in watchPaths)
                {
                    if (e.FullPath.StartsWith(watchPath))
                    {
                        string lastWriteTime = File.GetLastWriteTime(e.FullPath).ToString("yy-MM-dd_HH-mm-ss");
                        string backupFolderPath = Path.Combine(savePath, lastWriteTime);

                        // 폴더가 없으면 생성
                        if (!Directory.Exists(backupFolderPath))
                        {
                            Directory.CreateDirectory(backupFolderPath);
                        }

                        string fileName = Path.GetFileName(e.FullPath);
                        string destinationPath = Path.Combine(backupFolderPath, fileName);

                        // 파일인 경우
                        if (File.Exists(e.FullPath))
                        {
                            if (!File.Exists(destinationPath))
                            {
                                File.Copy(e.FullPath, destinationPath);
                                string logMessage = $"File backed up: {destinationPath}";
                                WriteLog(logMessage, logPath);
                            }
                        }
                        // 폴더인 경우
                        else if (Directory.Exists(e.FullPath))
                        {
                            if (!Directory.Exists(destinationPath))
                            {
                                DirectoryCopy(e.FullPath, destinationPath, true);
                                string logMessage = $"Folder backed up: {destinationPath}";
                                WriteLog(logMessage, logPath);
                            }
                        }

                        break;
                    }
                }
            }
            catch (IOException ioException)
            {
                string logMessage = $"Error: {ioException.Message}";
                WriteLog(logMessage, logPath);
            }
        }



        private void WriteLog(string logMessage, string logPath)
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string logFileName = $"log_{currentDate}.txt";
            string logFilePath = Path.Combine(logPath, logFileName);

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {logMessage}");
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // 폴더가 없으면 생성
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // 파일 복사
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // 하위 폴더 복사
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, true);
                }
            }
        }






        private void startButton_Click(object sender, EventArgs e)
        {
            watchPaths = watchPathTextBox.Text.Split(',').Select(x => x.Trim()).ToList();
            savePath = savePathTextBox.Text.Trim();
            logPath = logPathTextBox.Text.Trim();

            // 유효성 검사
            if (watchPaths.Any(x => string.IsNullOrEmpty(x)) || string.IsNullOrEmpty(savePath) || string.IsNullOrEmpty(logPath))
            {
                MessageBox.Show("All paths must be filled in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (watchPaths.Any(x => !Directory.Exists(x)))
            {
                MessageBox.Show("Invalid watch path. Please enter a valid directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // FileSystemWatcher 설정
            watcherList = new List<FileSystemWatcher>();
            foreach (string path in watchPaths)
            {
                FileSystemWatcher w = new FileSystemWatcher();
                w.Path = path;
                w.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
                w.Filter = "*.*";
                w.Changed += new FileSystemEventHandler(OnChanged);
                w.Renamed += new RenamedEventHandler(OnRenamed);
                w.Created += new FileSystemEventHandler(OnCreated);
                w.EnableRaisingEvents = false;
                w.IncludeSubdirectories = true;
                watcherList.Add(w);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            // FileSystemWatcher를 정지하고 리소스를 정리하는 코드를 여기에 추가하세요.

            this.Close();
        }



        private void watchPathTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}