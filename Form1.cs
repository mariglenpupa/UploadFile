using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEMP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string url = "http://localhost/Panel/upload.php?";
        string uuid = "uuuuuiiiiddddd";
        string file = @"C:\users\marig\Desktop\1.txt";

        private void button1_Click(object sender, EventArgs e)
        {
            UploadFile(file);
        }

        public async Task<string> UploadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File [{filePath}] not found.");
            }
            using var form = new MultipartFormDataContent();
            using var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            form.Add(fileContent, "user_file", Path.GetFileName(filePath));
            form.Add(new StringContent("789"), "uuid");

            var response = await new HttpClient().PostAsync(url, form);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseContent);
            return responseContent;
        }
    }
}
