namespace _2048WinFormsAppNew
{
    partial class InputForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label promptLabel;
        public TextBox inputTextBox;
        private Button okButton;
        private Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            promptLabel = new Label();
            inputTextBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // promptLabel
            // 
            promptLabel.AutoSize = true;
            promptLabel.Location = new Point(12, 20);
            promptLabel.Name = "promptLabel";
            promptLabel.Size = new Size(75, 15);
            promptLabel.TabIndex = 0;
            promptLabel.Text = "Введите имя";
            // 
            // inputTextBox
            // 
            inputTextBox.Location = new Point(12, 48);
            inputTextBox.Name = "inputTextBox";
            inputTextBox.Size = new Size(260, 23);
            inputTextBox.TabIndex = 1;
            // 
            // okButton
            // 
            okButton.Location = new Point(116, 87);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(197, 87);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // InputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 126);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(inputTextBox);
            Controls.Add(promptLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Игрок";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}