using System.Diagnostics;
using System.Drawing.Printing;

namespace OTZPrintDialog {

    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        /// <summary>
        /// 印刷ボタンクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {
            OTZPrintDialog dialog = new OTZPrintDialog();
            dialog.Document = new PrintDocument();
            // ダイアログ表示
            if (dialog.ShowDialog() == DialogResult.OK) {
                // プリンタ名
                Debug.WriteLine(dialog.Document.PrinterSettings.PrinterName);
            }
        }
    }
}