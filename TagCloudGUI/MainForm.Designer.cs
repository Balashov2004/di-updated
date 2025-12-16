namespace TagCloudGUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        
        private System.Windows.Forms.Label lblWordsFile;
        private System.Windows.Forms.TextBox txtWordsFilePath;
        private System.Windows.Forms.Button btnSelectWordsFile;
        
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnSelectOutputFile;

        private System.Windows.Forms.Label lblImageSize;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.NumericUpDown numHeight;

        private System.Windows.Forms.Label lblFontName;
        private System.Windows.Forms.TextBox txtFontName;
        private System.Windows.Forms.Label lblMinFontSize;
        private System.Windows.Forms.NumericUpDown numMinFontSize;
        private System.Windows.Forms.Label lblMaxFontSize;
        private System.Windows.Forms.NumericUpDown numMaxFontSize;
        
        private System.Windows.Forms.Label lblPadding;
        private System.Windows.Forms.NumericUpDown numPadding;
        private System.Windows.Forms.Label lblDensity;
        private System.Windows.Forms.NumericUpDown numSpiralDensity;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.NumericUpDown numAngle;

        private System.Windows.Forms.Label lblBackgroundColor;
        private System.Windows.Forms.Button btnBackgroundColor;
        private System.Windows.Forms.Label lblWordColor;
        private System.Windows.Forms.Button btnWordColor;
        private System.Windows.Forms.Label lblContourColor;
        private System.Windows.Forms.Button btnContourColor;
        private System.Windows.Forms.CheckedListBox clbExcludeParts;
        private Button btnSelectFont;

        private System.Windows.Forms.Label lblExcludeParts;
        private System.Windows.Forms.TextBox txtExcludePartsSpeech;

        private System.Windows.Forms.Button btnGenerate;
        
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
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 700);
            this.Text = "Генератор Облака Тегов";
            
            int yOffset = 20;
            int lineHeight = 30;
            int xOffset = 20;

            // руппа Файл и Пути
            GroupBox gbFile = new GroupBox();
            gbFile.Text = "1. Файл и Пути";
            gbFile.Location = new Point(xOffset, yOffset);
            gbFile.Size = new Size(560, 100);
            this.Controls.Add(gbFile);

            yOffset = 20;
            
            // Путь к входному файлу
            this.lblWordsFile = new System.Windows.Forms.Label();
            this.lblWordsFile.Text = "Входной файл:";
            this.lblWordsFile.Location = new Point(10, yOffset);
            this.lblWordsFile.AutoSize = true;
            gbFile.Controls.Add(this.lblWordsFile);

            this.txtWordsFilePath = new System.Windows.Forms.TextBox();
            this.txtWordsFilePath.Location = new Point(150, yOffset - 3);
            this.txtWordsFilePath.Size = new Size(300, 20);
            gbFile.Controls.Add(this.txtWordsFilePath);

            this.btnSelectWordsFile = new System.Windows.Forms.Button();
            this.btnSelectWordsFile.Text = "...";
            this.btnSelectWordsFile.Location = new Point(460, yOffset - 5);
            this.btnSelectWordsFile.Size = new Size(30, 25);
            this.btnSelectWordsFile.Click += new EventHandler(this.BtnSelectWordsFile_Click);
            gbFile.Controls.Add(this.btnSelectWordsFile);

            yOffset += lineHeight;

            // Путь для сохранения
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.lblOutputPath.Text = "Путь сохранения:";
            this.lblOutputPath.Location = new Point(10, yOffset);
            this.lblOutputPath.AutoSize = true;
            gbFile.Controls.Add(this.lblOutputPath);

            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.txtOutputPath.Location = new Point(150, yOffset - 3);
            this.txtOutputPath.Size = new Size(300, 20);
            gbFile.Controls.Add(this.txtOutputPath);
            
            this.btnSelectOutputFile = new System.Windows.Forms.Button();
            this.btnSelectOutputFile.Text = "...";
            this.btnSelectOutputFile.Location = new Point(460, yOffset - 5);
            this.btnSelectOutputFile.Size = new Size(30, 25);
            this.btnSelectOutputFile.Click += new EventHandler(this.BtnSelectOutputFile_Click);
            gbFile.Controls.Add(this.btnSelectOutputFile);
            
            // Размер слова и шрифт
            yOffset = 130;
            GroupBox gbSizeFont = new GroupBox();
            gbSizeFont.Text = "2. Размер Изображения и Шрифт";
            gbSizeFont.Location = new Point(xOffset, yOffset);
            gbSizeFont.Size = new Size(560, 150);
            this.Controls.Add(gbSizeFont);
            
            yOffset = 20;
            
            // Размер изображения
            this.lblImageSize = new System.Windows.Forms.Label();
            this.lblImageSize.Text = "Размер (ШxВ):";
            this.lblImageSize.Location = new Point(10, yOffset);
            this.lblImageSize.AutoSize = true;
            gbSizeFont.Controls.Add(this.lblImageSize);

            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.numWidth.Location = new Point(150, yOffset - 3);
            this.numWidth.Size = new Size(60, 20);
            this.numWidth.Minimum = 100;
            this.numWidth.Maximum = 5000;
            this.numWidth.Value = 1500;
            gbSizeFont.Controls.Add(this.numWidth);

            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.numHeight.Location = new Point(220, yOffset - 3);
            this.numHeight.Size = new Size(60, 20);
            this.numHeight.Minimum = 100;
            this.numHeight.Maximum = 5000;
            this.numHeight.Value = 1500;
            gbSizeFont.Controls.Add(this.numHeight);
            
            yOffset += lineHeight;

            // Имя шрифта
            this.lblFontName = new System.Windows.Forms.Label();
            this.lblFontName.Text = "Имя шрифта:";
            this.lblFontName.Location = new Point(10, yOffset);
            this.lblFontName.AutoSize = true;
            gbSizeFont.Controls.Add(this.lblFontName);
            
            this.txtFontName = new System.Windows.Forms.TextBox();
            this.txtFontName.Location = new Point(150, yOffset - 3);
            this.txtFontName.Size = new Size(200, 20);
            gbSizeFont.Controls.Add(this.txtFontName);
            
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.btnSelectFont.Text = "Выбрать шрифт...";
            this.btnSelectFont.Location = new Point(360, yOffset - 5);
            this.btnSelectFont.Size = new Size(150, 25);
            this.btnSelectFont.Click += new EventHandler(this.BtnSelectFont_Click); // Подписка на событие
            gbSizeFont.Controls.Add(this.btnSelectFont);

            yOffset += lineHeight;

            // Мин размер шрифта
            this.lblMinFontSize = new System.Windows.Forms.Label();
            this.lblMinFontSize.Text = "Мин. размер (px):";
            this.lblMinFontSize.Location = new Point(10, yOffset);
            this.lblMinFontSize.AutoSize = true;
            gbSizeFont.Controls.Add(this.lblMinFontSize);
            
            this.numMinFontSize = new System.Windows.Forms.NumericUpDown();
            this.numMinFontSize.Location = new Point(150, yOffset - 3);
            this.numMinFontSize.Size = new Size(60, 20);
            this.numMinFontSize.Minimum = 1;
            this.numMinFontSize.Maximum = 100;
            gbSizeFont.Controls.Add(this.numMinFontSize);

            yOffset += lineHeight;

            // Макс размер шрифта
            this.lblMaxFontSize = new System.Windows.Forms.Label();
            this.lblMaxFontSize.Text = "Макс. размер (px):";
            this.lblMaxFontSize.Location = new Point(10, yOffset);
            this.lblMaxFontSize.AutoSize = true;
            gbSizeFont.Controls.Add(this.lblMaxFontSize);

            this.numMaxFontSize = new System.Windows.Forms.NumericUpDown();
            this.numMaxFontSize.Location = new Point(150, yOffset - 3);
            this.numMaxFontSize.Size = new Size(60, 20);
            this.numMaxFontSize.Minimum = 1;
            this.numMaxFontSize.Maximum = 100;
            gbSizeFont.Controls.Add(this.numMaxFontSize);
            
            // Группа Алгоритм и Цвета 
            yOffset = 290;
            GroupBox gbAlgoColors = new GroupBox();
            gbAlgoColors.Text = "3. Алгоритм и Цвета";
            gbAlgoColors.Location = new Point(xOffset, yOffset);
            gbAlgoColors.Size = new Size(560, 180);
            this.Controls.Add(gbAlgoColors);
            
            int colorButtonX = 150;
            int colorLabelX = 10;
            yOffset = 20;

            // Цвет фона
            this.lblBackgroundColor = new System.Windows.Forms.Label();
            this.lblBackgroundColor.Text = "Цвет фона:";
            this.lblBackgroundColor.Location = new Point(colorLabelX, yOffset);
            this.lblBackgroundColor.AutoSize = true;
            gbAlgoColors.Controls.Add(this.lblBackgroundColor);
            
            this.btnBackgroundColor = new System.Windows.Forms.Button();
            this.btnBackgroundColor.Location = new Point(colorButtonX, yOffset - 5);
            this.btnBackgroundColor.Size = new Size(100, 25);
            this.btnBackgroundColor.Click += new EventHandler(this.BtnColorSelect_Click);
            gbAlgoColors.Controls.Add(this.btnBackgroundColor);

            yOffset += lineHeight;

            // Цвет слова
            this.lblWordColor = new System.Windows.Forms.Label();
            this.lblWordColor.Text = "Цвет слова:";
            this.lblWordColor.Location = new Point(colorLabelX, yOffset);
            this.lblWordColor.AutoSize = true;
            gbAlgoColors.Controls.Add(this.lblWordColor);
            
            this.btnWordColor = new System.Windows.Forms.Button();
            this.btnWordColor.Location = new Point(colorButtonX, yOffset - 5);
            this.btnWordColor.Size = new Size(100, 25);
            this.btnWordColor.Click += new EventHandler(this.BtnColorSelect_Click);
            gbAlgoColors.Controls.Add(this.btnWordColor);

            yOffset += lineHeight;

            // Цвет контура
            this.lblContourColor = new System.Windows.Forms.Label();
            this.lblContourColor.Text = "Цвет контура:";
            this.lblContourColor.Location = new Point(colorLabelX, yOffset);
            this.lblContourColor.AutoSize = true;
            gbAlgoColors.Controls.Add(this.lblContourColor);
            
            this.btnContourColor = new System.Windows.Forms.Button();
            this.btnContourColor.Location = new Point(colorButtonX, yOffset - 5);
            this.btnContourColor.Size = new Size(100, 25);
            this.btnContourColor.Click += new EventHandler(this.BtnColorSelect_Click);
            gbAlgoColors.Controls.Add(this.btnContourColor);

            yOffset += lineHeight;
            
            // Отступы
            this.lblPadding = new System.Windows.Forms.Label();
            this.lblPadding.Text = "Отступ (Padding):";
            this.lblPadding.Location = new Point(colorLabelX, yOffset);
            this.lblPadding.AutoSize = true;
            gbAlgoColors.Controls.Add(this.lblPadding);

            this.numPadding = new System.Windows.Forms.NumericUpDown();
            this.numPadding.Location = new Point(colorButtonX, yOffset - 3);
            this.numPadding.Size = new Size(60, 20);
            this.numPadding.Minimum = 0;
            this.numPadding.Maximum = 10;
            gbAlgoColors.Controls.Add(this.numPadding);

            yOffset += lineHeight;
            
            // Плотность спирали
            this.lblDensity = new System.Windows.Forms.Label();
            this.lblDensity.Text = "Плотность спирали:";
            this.lblDensity.Location = new Point(colorLabelX, yOffset);
            this.lblDensity.AutoSize = true;
            gbAlgoColors.Controls.Add(this.lblDensity);

            this.numSpiralDensity = new System.Windows.Forms.NumericUpDown();
            this.numSpiralDensity.Location = new Point(colorButtonX, yOffset - 3);
            this.numSpiralDensity.Size = new Size(60, 20);
            this.numSpiralDensity.DecimalPlaces = 2;
            this.numSpiralDensity.Minimum = 0.01M;
            this.numSpiralDensity.Maximum = 10.0M;
            gbAlgoColors.Controls.Add(this.numSpiralDensity);

            // Угол
            this.lblAngle = new System.Windows.Forms.Label();
            this.lblAngle.Text = "Угол поворота:";
            this.lblAngle.Location = new Point(250, yOffset);
            this.lblAngle.AutoSize = true;
            gbAlgoColors.Controls.Add(this.lblAngle);

            this.numAngle = new System.Windows.Forms.NumericUpDown();
            this.numAngle.Location = new Point(360, yOffset - 3);
            this.numAngle.Size = new Size(60, 20);
            this.numAngle.DecimalPlaces = 2;
            this.numAngle.Minimum = 0.1M;
            this.numAngle.Maximum = 10.0M;
            gbAlgoColors.Controls.Add(this.numAngle);


            int group4YOffset = 480;
            
            GroupBox gbFilter = new GroupBox(); 
            gbFilter.Text = "4. Фильтрация (Части речи)";
            gbFilter.Location = new Point(xOffset, group4YOffset);
            gbFilter.Size = new Size(560, 150);
            this.Controls.Add(gbFilter);

            int currentY = 20;

            this.lblExcludeParts = new System.Windows.Forms.Label();
            this.lblExcludeParts.Text = "Исключить части речи:";
            this.lblExcludeParts.Location = new Point(10, currentY);
            this.lblExcludeParts.AutoSize = true;
            gbFilter.Controls.Add(this.lblExcludeParts);
            
            currentY += lineHeight;

            this.clbExcludeParts = new System.Windows.Forms.CheckedListBox();
            this.clbExcludeParts.FormattingEnabled = true;
            this.clbExcludeParts.Location = new Point(10, currentY);
            this.clbExcludeParts.Size = new Size(540, 90);
            gbFilter.Controls.Add(this.clbExcludeParts); 
            
            // Кнопка генерации
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnGenerate.Text = "Сгенерировать Облако Тегов";
            this.btnGenerate.Location = new Point(xOffset, 650);
            this.btnGenerate.Size = new Size(560, 50);
            this.btnGenerate.Click += new EventHandler(this.BtnGenerate_Click);
            this.Controls.Add(this.btnGenerate);
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}