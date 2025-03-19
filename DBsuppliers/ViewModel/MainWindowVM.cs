using DBsuppliers.Model;
using DBsuppliers.VMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DBsuppliers.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        private DbConnection connection;
        private Supplier selectedSuplier;
        private string title;
        private string address;
        private string phone;
        private ObservableCollection<Supplier> suppliers;
        public ObservableCollection<Supplier> Suppliers { get => suppliers; set { suppliers = value; Signal(); } }
        public string Title { get => title; set { title = value; Signal(); }}
        public string Address { get => address; set { address = value; Signal(); }}
        public string Phone { get => phone; set { phone = value; Signal(); }}
        public Supplier SelectedSuplier { get => selectedSuplier; set { 
                selectedSuplier = value; 
                Title = value.Title;
                Address = value.Address;
                Phone = value.Phone;
                Signal(); } }

        public CommandVM Add { get; set; }
        public CommandVM Edit { get; set; }
        public CommandVM Remove { get; set; }
        public MainWindowVM()
        {
            SelectAll();
            Add = new CommandVM(() =>
            {
                Supplier supplier = new Supplier();
                supplier.Title = Title;
                supplier.Address = Address;
                supplier.Phone = Phone;

                SuppliersDB.GetDb().Insert(supplier);

                SelectAll();
            });
            Edit = new CommandVM(() =>
            {
                if (SelectedSuplier != null)
                {
                    SelectedSuplier.Title = Title;
                    SelectedSuplier.Address = Address;
                    SelectedSuplier.Phone = Phone;
                    SuppliersDB.GetDb().Update(SelectedSuplier);
                }
                else
                    MessageBox.Show("Не выбран объект");
                SelectAll();
            });
            Remove = new CommandVM(() =>
            {
                if (SelectedSuplier != null)
                    SuppliersDB.GetDb().Remove(SelectedSuplier);
                else
                    MessageBox.Show("Не выбран объект");
                SelectAll();
            });
            
        }
        private void SelectAll()
        {
            Suppliers = new ObservableCollection<Supplier>(SuppliersDB.GetDb().SelectAll());
        }
    }
}
