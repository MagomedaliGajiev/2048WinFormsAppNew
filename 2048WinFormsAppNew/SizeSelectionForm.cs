namespace _2048WinFormsAppNew
{
    public partial class SizeSelectionForm : Form
    {
        public int SelectedSize { get; private set; } = 4;

        public SizeSelectionForm()
        {
            InitializeComponent();
        }

        private void SizeSelectionForm_Load(object sender, EventArgs e)
        {
            sizeComboBox.Items.AddRange(new object[] { 3, 4, 5, 6, 7, 8 });
            sizeComboBox.SelectedItem = SelectedSize;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedSize = (int)sizeComboBox.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}