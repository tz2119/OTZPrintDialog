using System.Diagnostics;
using System.Drawing.Printing;

namespace OTZPrintDialog {

    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        /// <summary>
        /// ����{�^���N���b�N��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {
            OTZPrintDialog dialog = new OTZPrintDialog();
            dialog.Document = new PrintDocument();
            // �_�C�A���O�\��
            if (dialog.ShowDialog() == DialogResult.OK) {
                // �v�����^��
                Debug.WriteLine(dialog.Document.PrinterSettings.PrinterName);
            }
        }
    }
}