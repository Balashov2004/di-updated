using Autofac;
using TagsCloudContainer;
using TagsCloudContainer.DTO;
using TagsCloudVisualization;

namespace TagCloudGUI;

public partial class MainForm : Form
{
    private AppSettings settings;
    private CloudRunner runner;
    private IContainer container;
    
    public MainForm()
    {
        InitializeComponent();
        settings = new AppSettings();
        
        var builder = new ContainerBuilder();
        builder.RegisterModule(new CompositionRoot());
        builder.RegisterInstance(settings).AsSelf().SingleInstance();
        container = builder.Build();

        LoadSettings();
        
        if (Path.GetFileName(settings.OutputPath) == "result.png") 
        {
            GenerateUniqueOutputPath(Path.GetDirectoryName(settings.OutputPath));
        }
    }

    private void LoadSettings()
    {
        txtWordsFilePath.Text = settings.WordsFilePath;
        txtOutputPath.Text = settings.OutputPath;
        
        txtFontName.Text = settings.FontName;
        numMinFontSize.Value = settings.MinFontSize;
        numMaxFontSize.Value = settings.MaxFontSize;
        
        numPadding.Value = settings.Padding;
        numSpiralDensity.Value = (decimal)settings.SpiralDensity;
        numAngle.Value = (decimal)settings.Angle;

        btnBackgroundColor.BackColor = settings.BackgroundColor;
        btnWordColor.BackColor = settings.WordColor;
        btnContourColor.BackColor = settings.ContourColor;
        
        clbExcludeParts.Items.Clear();
        foreach (var russianName in PartsSpeech.RussianToTag.Keys)
        {
            clbExcludeParts.Items.Add(russianName);
        }

        for (int i = 0; i < clbExcludeParts.Items.Count; i++)
        {
            var russianName = clbExcludeParts.Items[i].ToString();
            
            if (PartsSpeech.ToTags.TryGetValue(russianName, out var tag)) 
            {
                var isChecked = settings.ExcludePartsSpeech.Contains(tag);
                clbExcludeParts.SetItemChecked(i, isChecked);
            }
        }
    }
    
    private void SaveSettings()
    {
        settings.WordsFilePath = txtWordsFilePath.Text;
        settings.OutputPath = txtOutputPath.Text;
        
        settings.ImageSize = new Size((int)numWidth.Value, (int)numHeight.Value);
        settings.MinFontSize = (int)numMinFontSize.Value;
        settings.MaxFontSize = (int)numMaxFontSize.Value;
        settings.FontName = txtFontName.Text;
        
        settings.Padding = (int)numPadding.Value;
        settings.SpiralDensity = (double)numSpiralDensity.Value;
        settings.Angle = (double)numAngle.Value;
        
        settings.BackgroundColor = btnBackgroundColor.BackColor;
        settings.WordColor = btnWordColor.BackColor;
        settings.ContourColor = btnContourColor.BackColor;
        
        var newExcludeList = new List<string>();

        foreach (var checkedItem in clbExcludeParts.CheckedItems)
        {
            var russianName = checkedItem.ToString();
            
            if (PartsSpeech.ToTags.TryGetValue(russianName, out var tag))
            {
                newExcludeList.Add(tag);
            }
        }
        settings.ExcludePartsSpeech = newExcludeList;
    }
    
    private void BtnSelectWordsFile_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Поддерживаемые файлы (*.txt, *.docx)|*.txt;*.docx;|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите файл для анализа";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtWordsFilePath.Text = openFileDialog.FileName;
            }
        }
    }
    
    private void BtnSelectOutputFile_Click(object sender, EventArgs e)
    {
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = "PNG Image (*.png)|*.png";
            saveFileDialog.Title = "Укажите путь для сохранения облака";
            
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(settings.OutputPath);
            saveFileDialog.FileName = Path.GetFileName(settings.OutputPath);
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = saveFileDialog.FileName;
                
                if (Path.GetExtension(selectedPath).Equals(".png", StringComparison.OrdinalIgnoreCase))
                {
                    settings.OutputPath = selectedPath;
                }
                else
                {
                    GenerateUniqueOutputPath(Path.GetDirectoryName(selectedPath));
                }
                
                txtOutputPath.Text = settings.OutputPath;
            }
        }
    }
    
    private void BtnGenerate_Click(object sender, EventArgs e)
    {
        SaveSettings();
        var validator = new SettingsValidator();
        var validatorResult = validator.Validate(settings);
        if (!validatorResult.IsSuccess)
        {
            MessageBox.Show(validatorResult.ErrorMessage, 
                "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        using (var scope = container.BeginLifetimeScope())
        {
            var runner = scope.Resolve<CloudRunner>();
            var result = runner.Run();
            
            if (result.IsSuccess)
            {
                MessageBox.Show($"Облако сохранено: {result.Value}");
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
    private void BtnColorSelect_Click(object sender, EventArgs e)
    {
        ColorDialog colorDialog = new ColorDialog();
        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            ((Button)sender).BackColor = colorDialog.Color;
        }
    }
    
    private void BtnSelectFont_Click(object sender, EventArgs e)
    {
        using (FontDialog fontDialog = new FontDialog())
        {
            try
            {
                fontDialog.Font = new Font(settings.FontName, settings.MinFontSize); 
            }
            catch
            {
                fontDialog.Font = new Font(FontFamily.GenericSansSerif, 12);
            }

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                settings.FontName = fontDialog.Font.Name;
                txtFontName.Text = fontDialog.Font.Name;
            }
        }
    }
    
    private void GenerateUniqueOutputPath(string directory)
    {
        Directory.CreateDirectory(directory);
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var fileName = $"TagCloud_{timestamp}.png";
        
        settings.OutputPath = Path.Combine(directory, fileName);
        txtOutputPath.Text = settings.OutputPath;
    }
    
}