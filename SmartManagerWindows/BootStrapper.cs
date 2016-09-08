using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartManagerWindows
{
    static class BootStrapper
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Initialize();

                Application.Run(new Home());
                //Application.Run(new TestForm());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                /*using (System.IO.StreamWriter sr = new System.IO.StreamWriter(new System.IO.FileStream("c:\\abc.txt", System.IO.FileMode.OpenOrCreate)))
                {
                    sr.WriteLine(ex.Message);
                    sr.WriteLine(ex.StackTrace);
                    if (ex.InnerException != null)
                    {
                        sr.WriteLine(ex.InnerException.Message);
                    }
                }*/
            }
        }

        private static void Initialize()
        {
            System.Net.ICredentials cred = System.Net.CredentialCache.DefaultCredentials;
            //System.Net.ICredentials cred = new System.Net.NetworkCredential("Asia\\Raghuthama.Vemparala", "passwordhere");

            ClientApplication.Initialize(new Uri("http://tfs.kofax.com:8080/tfs/products"), "KTA", cred);
        }
    }
}
