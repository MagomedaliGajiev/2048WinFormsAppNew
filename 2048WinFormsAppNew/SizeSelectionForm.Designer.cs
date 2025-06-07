namespace _2048WinFormsAppNew
{
    partial class SizeSelectionForm
    {
        private ComboBox sizeComboBox;
        private Button okButton;
        private Label label1;

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.sizeComboBox = new ComboBox();
            this.okButton = new Button();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(136, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите размер поля:";

            // sizeComboBox
            this.sizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.sizeComboBox.FormattingEnabled = true;
            this.sizeComboBox.Location = new Point(12, 48);
            this.sizeComboBox.Name = "sizeComboBox";
            this.sizeComboBox.Size = new Size(260, 23);
            this.sizeComboBox.TabIndex = 1;

            // okButton
            this.okButton.Location = new Point(197, 87);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new EventHandler(this.okButton_Click);

            // SizeSelectionForm
            this.ClientSize = new Size(284, 126);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.sizeComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle =  FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SizeSelectionForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Размер поля";
            this.Load += new EventHandler(this.SizeSelectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}