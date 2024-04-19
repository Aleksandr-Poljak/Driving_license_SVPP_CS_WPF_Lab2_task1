using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SVPP_CS_WPF_Lab2_task1_Driving_license
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal  Driver driver = new Driver();

        public MainWindow()
        {
            InitializeComponent();

            initOtherComponents();
        }
        /// <summary>
        /// Обрабатывает занчения роста полученные из Slider HGT
        /// </summary>
        private void Slider_HGT_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider sl = (Slider) sender;
            TextBlock bl = TextBlock_HGTValue;

            if (bl != null )
            { 
                int value = (int) sl.Value;
                bl.Text = value.ToString(); 
            }
        }

        /// <summary>
        /// Инициализация фото по умолчанию.
        /// </summary>
        private void init_Image_UserPrhoto()
        {
            Image_UserPhoto.Source = new BitmapImage(new Uri(driver.PhotoPath, UriKind.RelativeOrAbsolute));
        }
        /// <summary>
        /// Настраивает диапазоны дат элементов DatePicker
        /// </summary>
        private void init_DatePickers()
        {

            DateTime startRange = new DateTime(1900, 1, 1);

            // Настройка диапазона дат для выбора даты рождения.
            Calendar_DOB.DisplayDateStart = startRange;
            Calendar_DOB.DisplayDateEnd = DateTime.Now.Date.AddYears(-16);
            Calendar_DOB.SelectedDate = null;

            // Настройка диапазона дат для выбора даты выдачи
            Calendar_ISS.DisplayDateStart = startRange;
            Calendar_ISS.DisplayDateEnd = DateTime.Now.Date;
            Calendar_ISS.SelectedDate = null;

            // Настройка диапазона дат для выбора даты окончания лицензии
            Calendar_EXP.DisplayDateStart = startRange;
            Calendar_EXP.DisplayDateEnd = DateTime.Now.Date.AddYears(20);
            Calendar_EXP.SelectedDate = null;
        }

        /// <summary>
        /// Настривает варинаты выбора пола в RadioButtons_Sex
        /// </summary>
        private void init_RadioButtons_Sex()
        {
            foreach (GenderEnum g in Enum.GetValues(typeof(GenderEnum)))
            {
                RadioButton button = new RadioButton();
                button.Content = g;
                button.GroupName = "SEX";

                if (g.ToString() == GenderEnum.other.ToString()) button.IsChecked = true;
                StackPanel_SEX.Children.Add(button);
            }
        }
        /// <summary>
        /// Настаривает варианты выбора цвет глаза в ComboBox
        /// </summary>
        private void init_ComboBox_Eyes()
        {
            foreach (EyesEnum e in Enum.GetValues(typeof(EyesEnum)))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = e;

                if (e.ToString() == EyesEnum.Amber.ToString()) item.IsSelected = true;
                ComboBox_Eyes.Items.Add(item);

            }
        }

        /// <summary>
        /// Заполняет поля пользователя значенями по умолчанию.
        /// </summary>
        private void init_TextBox()
        {
            
            TextBox_Name.Text = driver.NameSurname;
            TextBox_Number.Text = driver.Number.ToString();
            TextBox_Adress.Text = driver.Adress;
        }

        /// <summary>
        /// Заполняет поля значенями по умолчанию.
        /// </summary>
        private void initOtherComponents()
        {
            
            init_Image_UserPrhoto();
            init_DatePickers();
            init_RadioButtons_Sex();
            init_ComboBox_Eyes();
            init_TextBox();
        }

        /// <summary>
        /// Загружает новое изображение
        /// </summary>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Photo (.png,jpg)|*.png;*jpg";
            dlg.FileName = "";
            
            bool? result = dlg.ShowDialog();

            if (result == true) 
            {
                
                Image_UserPhoto.Source = new BitmapImage(new Uri(dlg.FileName, UriKind.RelativeOrAbsolute));
            }
            else { return; }

        }
        /// <summary>
        /// Обработчик кнопки Save. Сохранение введной иноформации во объект
        /// </summary>
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                driver.Number = Int32.Parse(TextBox_Number.Text.ToString());
            }
            catch(ArgumentNullException){MessageBox.Show("Введите номер;", "Error");}
            catch (FormatException) { MessageBox.Show("Не верный формат;", "Error"); }
            catch (OverflowException) { MessageBox.Show("Слишком большое число;", "Error"); }

            driver.NameSurname = TextBox_Name.Text;
            driver.Adress = TextBox_Adress.Text;
            driver.Class_License = TextBox_Class.Text.Length > 0 ? (char) TextBox_Class.Text[0] : driver.Class_License;
            // Проверка выбора даты тренарным операторм. Если дата не указана- используется значение по умолчанию установленное при создании объекта.
            driver.DOB = Calendar_DOB.SelectedDate is not null ?(DateTime) Calendar_DOB.SelectedDate: driver.DOB ;
            driver.ISS = Calendar_ISS.SelectedDate is not null? (DateTime) Calendar_ISS.SelectedDate:driver.ISS ;
            driver.EXP = Calendar_EXP.SelectedDate is not null? (DateTime)Calendar_EXP.SelectedDate: driver.EXP ;
            foreach (RadioButton rb in StackPanel_SEX.Children)
            {
                if (rb.IsChecked == true)
                {
                    driver.Gender = (GenderEnum)  rb.Content;
                }
            }
            driver.PhotoPath = Image_UserPhoto.Source.ToString();
            driver.Eyes =(EyesEnum) ((ComboBoxItem) ComboBox_Eyes.SelectedValue).Content;
            driver.HGT = (int) Slider_HGT.Value;
            driver.OrganDonor = CheckBox_OraganDonor.IsChecked is not null? (bool)CheckBox_OraganDonor.IsChecked : driver.OrganDonor;

            if (!driver.IsFullInfo())
            {
                MessageBox.Show("Профиль заполнен не полностью");
            }

            MessageBox.Show(driver.ToString(), "SAVE");
        }

        /// <summary>
        ///  Обработчик кнопки Load. Извлечение инофрмации из объекта и заполение форм.
        /// </summary>
        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Number.Text = driver.Number.ToString();
            TextBox_Name.Text = driver.NameSurname;
            TextBox_Adress.Text = driver.Adress;
            TextBox_Class.Text = driver.Class_License.ToString();
            Calendar_DOB.SelectedDate = driver.DOB;
            Calendar_ISS.SelectedDate = driver.ISS;
            Calendar_EXP.SelectedDate = driver.EXP;

            foreach (RadioButton rb in StackPanel_SEX.Children)
            {
                if (driver.Gender == (GenderEnum)rb.Content)
                {
                    rb.IsChecked = true;
                }
            }

            foreach (ComboBoxItem ey in ComboBox_Eyes.Items)
            {
                if(((EyesEnum)ey.Content) == driver.Eyes)
                {
                    ey.IsSelected = true;
                }
            }
            Slider_HGT.Value = driver.HGT;
            TextBlock_HGTValue.Text = driver.HGT.ToString();
            CheckBox_OraganDonor.IsChecked = driver.OrganDonor;
            Image_UserPhoto.Source = new BitmapImage(new Uri(driver.PhotoPath, UriKind.RelativeOrAbsolute));

            MessageBox.Show("Driver Загружен!", "LOAD");


        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            driver = new Driver();

            StackPanel_SEX.Children.Clear();
            ComboBox_Eyes.Items.Clear();
            Slider_HGT.Value = 170;
            initOtherComponents();

        }
    }
}
