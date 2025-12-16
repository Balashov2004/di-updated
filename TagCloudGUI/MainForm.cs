using Autofac;
using TagsCloudContainer;
using TagsCloudVisualization;

namespace TagCloudGUI;

public partial class MainForm : Form
{
    private AppSettings settings;
    private CloudRunner runner;
    
    public MainForm()
    {
        InitializeComponent();
        settings = new AppSettings();
        
        var builder = new ContainerBuilder();
        builder.RegisterModule(new CompositionRoot());
        builder.RegisterInstance(settings).AsSelf().SingleInstance();
        var container = builder.Build();
        runner = container.Resolve<CloudRunner>();

        LoadSettings();
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
        
        txtExcludePartsSpeech.Text = string.Join(", ", settings.ExcludePartsSpeech);
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
        
        settings.ExcludePartsSpeech = txtExcludePartsSpeech.Text
            .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Distinct()
            .ToList();
    }
    
    private void BtnSelectWordsFile_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Поддерживаемые файлы (*.txt, *.docx, *.doc)|*.txt;*.docx;*.doc|Все файлы (*.*)|*.*";
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
            saveFileDialog.Filter = "PNG Image (*.png)|*.png|";
            saveFileDialog.Title = "Укажите путь для сохранения облака";
            saveFileDialog.FileName = Path.GetFileName(settings.OutputPath);
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtOutputPath.Text = saveFileDialog.FileName;
            }
        }
    }
    
    private void BtnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            SaveSettings();

            runner.Run(); 

            MessageBox.Show($"Облако тегов создано и сохранено в: {settings.OutputPath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при генерации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
}