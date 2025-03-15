using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DBsuppliers.Model
{
    public class SuppliersDB
    {
        MySqlConnection connection;

        internal void SetConnection(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public bool Insert(Supplier supplier)
        {
            bool result = false;
            if (connection == null)
                return result;

            connection.Open();

            MySqlCommand cmd = new MySqlCommand("insert into `supplier` Values (0, @Title, @Address, @Phone);select LAST_INSERT_ID();",
                 connection);
            // путем добавления значений в запрос через параметры мы используем экранирование опасных символов
            cmd.Parameters.Add(new MySqlParameter("Title", supplier.Title));
            cmd.Parameters.Add(new MySqlParameter("Address", supplier.Address));
            cmd.Parameters.Add(new MySqlParameter("Phone", supplier.Phone));
            try
            {
                // выполняем запрос через ExecuteScalar, получаем id вставленной записи
                // если нам не нужен id, то в запросе убираем часть select LAST_INSERT_ID(); и выполняем команду через ExecuteNonQuery
                int id = (int)(ulong)cmd.ExecuteScalar();
                if (id > 0)
                {
                    MessageBox.Show(id.ToString());
                    // назначаем полученный id обратно в объект для дальнейшей работы
                    supplier.Id = id;
                    result = true;
                }
                else
                {
                    MessageBox.Show("Запись не добавлена");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
            return result;
        }

        internal List<Supplier> SelectAll()
        {
            List<Supplier> supplier = new List<Supplier>();
            if (connection == null)
                return supplier;

            connection.Open();
            var command = new MySqlCommand("select `id`, `title`, `address`, 'phone' from `suppliers` ", connection);
            try
            {
                // выполнение запроса, который возвращает результат-таблицу
                MySqlDataReader dr = command.ExecuteReader();
                // в цикле читаем построчно всю таблицу
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string title = string.Empty;
                    // проверка на то, что столбец имеет значение
                    if (!dr.IsDBNull(1))
                        title = dr.GetString("title");
                    string address = string.Empty;
                    if (!dr.IsDBNull(2))
                        address = dr.GetString("address");
                    string phone = string.Empty;
                    if (!dr.IsDBNull(3))
                        phone = dr.GetString("phone");
                    supplier.Add(new Supplier
                    {
                        Id = id,
                        Title = title,
                        Address = address,
                        Phone = phone
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
            return supplier;
        }

        internal bool Update(Supplier edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            connection.Open();
            var mc = new MySqlCommand($"update `suppliers` set `title`=@Title, `address`=@Address, `phone`=@Phone where `id` = {edit.Id}", connection);
            mc.Parameters.Add(new MySqlParameter("Title", edit.Title));
            mc.Parameters.Add(new MySqlParameter("Address", edit.Address));
            mc.Parameters.Add(new MySqlParameter("Phone", edit.Phone));

            try
            {
                mc.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
            return result;
        }


        internal bool Remove(Supplier remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            connection.Open();
            var mc = new MySqlCommand($"delete from `suppliers` where `id` = {remove.Id}", connection);
            try
            {
                mc.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
            return result;
        }
    }
}
