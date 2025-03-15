using DBsuppliers.Model;
using DBsuppliers.VMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DBsuppliers.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        private DBConnection connection;
        private SuppliersDB suppliersDB;
        public List<Supplier> Suppliers { get; set; } = new List<Supplier>();
        private string title;
        private string adress;
        private string phone;

        public string Title { get => title; set { title = value; Signal(); }}
        public string Adress { get => adress; set { adress = value; Signal(); }}
        public string Phone { get => phone; set { phone = value; Signal(); }}

        public CommandVM Add { get; set; }
        public CommandVM Edit { get; set; }
        public CommandVM Remove { get; set; }
        public MainWindowVM()
        {
            connection = new DBConnection();
            suppliersDB = new SuppliersDB();
            connection.SetConnection(suppliersDB);
            Add = new CommandVM(() =>
            {
                Supplier supplier = new Supplier();
                suppliersDB.Insert(supplier);

            });
        }
    }
}
