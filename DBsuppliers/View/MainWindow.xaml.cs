using DBsuppliers.Model;
using DBsuppliers.ViewModel;
using DBsuppliers.VMTools;
using System.Collections.ObjectModel;
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

namespace DBsuppliers.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Supplier> Supplers { get; set; } = new();
        DBConnection db;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();

            db = new DBConnection();
            SuppliersDB clientsDB = new SuppliersDB();
            db.SetConnection(clientsDB);

            // пример с добавлением в бд
            //Client client = new Client { FirstName = "Петр", LastName = "Иванов" };
            //clientsDB.Insert(client);

            //// пример с получением данных из бд
            //var clients = clientsDB.SelectAll();
            //// полученную коллекцию выводим в список
            //clients.ForEach(s => Supplers.Add(s));

            // пример изменения записи
            //var edit = Clients.First();
            //edit.FirstName = "Иван";
            //edit.LastName = "Петров";
            //if (!clientsDB.Update(edit))
            //    MessageBox.Show("Сохранение изменений не срослось");
            //else
            //    MessageBox.Show("Запись изменена");

            // пример удаления записи
            //var remove = Clients.Last();
            //clientsDB.Remove(remove);

        }
    }
}