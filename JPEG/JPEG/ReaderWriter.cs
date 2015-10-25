using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HilfeUpdater
{
    class ReaderWriter
    {
        private string htmlblanko = "error";
        private String filename = "";
        /// <summary>
        /// liest ein txt ein
        /// </summary>
        /// <returns></returns>
        public string einlesen()
        {
            try
            {
                StreamReader readin = new StreamReader(@"C:\Users\s.schlund\Desktop\-AutomatischerHilfe-Updater\TMSHandbuch.htm",
                    System.Text.Encoding.Default);//system.text sorgt dafür das die sonderzeichen auch gelesen werdenin.
                this.htmlblanko = readin.ReadToEnd();
                readin.Close();
                return (htmlblanko);
            }
            catch
            {
                Console.WriteLine("Datei wurde nicht Gefunden");
                return (htmlblanko);
            }
        }
        public string einlesen(String input)
        {
            try
            {
                StreamReader readin = new StreamReader(input,
                    System.Text.Encoding.Default);//system.text sorgt dafür das die sonderzeichen auch gelesen werden
                this.htmlblanko = readin.ReadToEnd();
                readin.Close();
                return (htmlblanko);
            }
            catch
            {
                Console.WriteLine("Datei wurde nicht Gefunden");
                return (htmlblanko);
            }
        }
        /// <summary>
        /// schreibt einen String
        /// </summary>
        /// <param name="tempstring"></param>
        public void schreibString(String contentstring)
        {
            try
            {
                System.IO.File.WriteAllText(@"C:\" + getfilename() + ".txt", contentstring, System.Text.Encoding.Default);
            }
            catch
            {
                Console.WriteLine("DerWriter funkt net");
            }
        }
        public void schreibString(String tempstring, String filename)
        {
            try
            {
                System.IO.File.WriteAllText(filename, tempstring,
                    System.Text.Encoding.Default);
            }
            catch
            {
                Console.WriteLine("DerWriter funkt net");
            }
        }
        public void schreibString(String text, String filename, String zusatz)
        {
            try
            {
                String tempstring = zusatz + text;
                System.IO.File.WriteAllText(filename + ".txt", tempstring,
                    System.Text.Encoding.Default);


            }
            catch
            {
                Console.WriteLine("DerWriter funkt net");
            }
        }
        /// <summary>
        ///  der user tippt den filename ein
        /// </summary>
        /// <returns></returns>
        private string getfilename()
        {
            Console.WriteLine("Name File");
            filename = Console.ReadLine();
            return (filename);
        }
        /// <summary>
        /// verschiebt eine File
        /// </summary>
        /// <param name="start"></param>
        /// <param name="ziel"></param>
        public void verschieben(String start, String ziel)
        {
            try
            {
                System.IO.File.Copy(start, ziel, true);
            }
            catch { }
        }
        private static void verschiebeOrdner(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    verschiebeOrdner(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        //private void extrahieren(String startname, String ziel)
        //{
        //    String Startpfad = @"C:\Users";
        //    Console.WriteLine("welcher beuntzer");
        //    Startpfad = Startpfad + Console.ReadLine() + @"Documents\Calibre-Bibliothek";
        //    Console.WriteLine("welcher Autor");
        //    Startpfad = Startpfad + Console.ReadLine();
        //    Console.WriteLine("der name der vorlage/ordner");
        //    Startpfad = Startpfad + Console.ReadLine();
        //    //Console.WriteLine("der name der vorlage");
        //    //Startpfad = Startpfad + Console.ReadLine()+".hmtlz";
        //    ////Directory.CreateDirectory(@Startpfad);
        //    Startpfad = Startpfad + startname + ".htmlz";
        //    String Zielpfad = @"C:\Users\s.schlund\Desktop\" + ziel;
        //    ZipFile.ExtractToDirectory(@startname, @Zielpfad);
        //}
        /// <summary>
        /// erstellt einen Ordner wenn keiner der gleich hei?t dort vorhanden ist
        /// </summary>
        /// <param name="startname"></param>
        public void erstelleoernder(String startname)
        {
            if (!System.IO.Directory.Exists(@startname))
            {
                try
                {
                    Directory.CreateDirectory(@startname);
                }
                catch { }
            }
        }
        private void Ordnerloschen(String input)
        {
            if (System.IO.Directory.Exists(@input))
            {
                Directory.Delete(input, true);
            }
        }
        private void Fileloschen(String input)
        {
            File.Delete(input);
        }
    }
}
        