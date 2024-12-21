namespace WeatherApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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
            CityComboBox = new ComboBox();
            GetWeatherButton = new Button();
            ResulttextBox = new TextBox();
            ResultLabel = new Label();
            SuspendLayout();
            // 
            // CityComboBox
            // 
            CityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            CityComboBox.Font = new Font("Segoe UI", 9F);
            CityComboBox.FormattingEnabled = true;
            CityComboBox.Location = new Point(250, 231);
            CityComboBox.Margin = new Padding(5, 6, 5, 6);
            CityComboBox.Name = "CityComboBox";
            CityComboBox.Size = new Size(264, 33);
            CityComboBox.TabIndex = 0;
            // 
            // GetWeatherButton
            // 
            GetWeatherButton.BackColor = Color.FromArgb(30, 144, 255);
            GetWeatherButton.Font = new Font("Segoe UI", 9F);
            GetWeatherButton.ForeColor = Color.White;
            GetWeatherButton.Location = new Point(567, 231);
            GetWeatherButton.Margin = new Padding(5, 6, 5, 6);
            GetWeatherButton.Name = "GetWeatherButton";
            GetWeatherButton.Size = new Size(200, 58);
            GetWeatherButton.TabIndex = 1;
            GetWeatherButton.Text = "Получить погоду";
            GetWeatherButton.UseVisualStyleBackColor = false;
            GetWeatherButton.Click += GetWeatherButton_Click;
            // 
            // ResulttextBox
            // 
            ResulttextBox.Font = new Font("Segoe UI", 9F);
            ResulttextBox.Location = new Point(250, 346);
            ResulttextBox.Margin = new Padding(5, 6, 5, 6);
            ResulttextBox.Multiline = true;
            ResulttextBox.Name = "ResulttextBox";
            ResulttextBox.ReadOnly = true;
            ResulttextBox.ScrollBars = ScrollBars.Vertical;
            ResulttextBox.Size = new Size(514, 189);
            ResulttextBox.TabIndex = 2;
            // 
            // ResultLabel
            // 
            ResultLabel.AutoSize = true;
            ResultLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ResultLabel.Location = new Point(250, 308);
            ResultLabel.Margin = new Padding(5, 0, 5, 0);
            ResultLabel.Name = "ResultLabel";
            ResultLabel.Size = new Size(188, 28);
            ResultLabel.TabIndex = 3;
            ResultLabel.Text = "Результат погоды:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 673);
            Controls.Add(ResultLabel);
            Controls.Add(ResulttextBox);
            Controls.Add(GetWeatherButton);
            Controls.Add(CityComboBox);
            Margin = new Padding(5, 6, 5, 6);
            Name = "Form1";
            Text = "Погода";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ComboBox CityComboBox;
        private System.Windows.Forms.Button GetWeatherButton;
        private System.Windows.Forms.TextBox ResulttextBox;
        private System.Windows.Forms.Label ResultLabel;
    }
}
