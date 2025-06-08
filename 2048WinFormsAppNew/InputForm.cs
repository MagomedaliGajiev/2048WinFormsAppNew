namespace _2048WinFormsAppNew
{
    public partial class InputForm : Form
    {
        public string InputText { get; private set; }
        private readonly string _prompt;
        public InputForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            InputText = inputTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
