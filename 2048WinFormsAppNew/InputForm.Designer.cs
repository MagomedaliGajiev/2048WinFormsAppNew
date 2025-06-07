namespace _2048WinFormsAppNew
{
    partial class InputForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label promptLabel;
        private TextBox inputTextBox;
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
            this.promptLabel = new Label();
            this.inputTextBox = new TextBox();
            this.okButton = new Button();
            this.cancelButton = new Button();
            this.SuspendLayout();

            // promptLabel
            this.promptLabel.AutoSize = true;
            this.promptLabel.Location = new Point(12, 20);
            this.promptLabel.Name = "promptLabel";
            this.promptLabel.Size = new Size(46, 15);
            this.promptLabel.TabIndex = 0;
            this.promptLabel.Text = "Промпт";

            // inputTextBox
            this.inputTextBox.Location = new Point(12, 48);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new Size(260, 23);
            this.inputTextBox.TabIndex = 1;

            // okButton
            this.okButton.Location = new Point(116, 87);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new EventHandler(this.okButton_Click);

            // cancelButton
            this.cancelButton.Location = new Point(197, 87);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new EventHandler(this.cancelButton_Click);

            // InputForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(284, 126);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.promptLabel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Игрок";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}