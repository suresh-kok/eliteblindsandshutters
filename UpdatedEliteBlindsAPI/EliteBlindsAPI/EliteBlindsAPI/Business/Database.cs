using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text;
using EliteBlindsAPI.Models;
using System.Data;

namespace EliteBlindsAPI.Business
{
    public class Customer_Mapper : IDataMapper<Models.Customer>
    {
        public override Customer Create(Customer instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO Customer (FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive) " +
                           "VALUES (@FirstName, @MiddleName,@Lastname, @Email, @Password, @DOB, @Gender,@Mobile,@Address,@City,@Country,@Pincode,@RoleID,0); " +
                           "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FirstName", MySqlDbType.VarString).Value = instance.FirstName;
                    command.Parameters.Add("@MiddleName", MySqlDbType.VarString).Value = instance.MiddleName;
                    command.Parameters.Add("@Lastname", MySqlDbType.VarString).Value = instance.LastName;
                    command.Parameters.Add("@Email", MySqlDbType.VarString).Value = instance.Email;
                    command.Parameters.Add("@Password", MySqlDbType.VarString).Value = instance.Password;
                    command.Parameters.Add("@DOB", MySqlDbType.DateTime).Value = instance.DOB;
                    command.Parameters.Add("@Gender", MySqlDbType.Bit).Value = instance.Gender;
                    command.Parameters.Add("@Mobile", MySqlDbType.VarString).Value = instance.Mobile;
                    command.Parameters.Add("@Address", MySqlDbType.VarString).Value = instance.Address;
                    command.Parameters.Add("@City", MySqlDbType.VarString).Value = instance.City;
                    command.Parameters.Add("@Country", MySqlDbType.VarString).Value = instance.Country;
                    command.Parameters.Add("@Pincode", MySqlDbType.VarString).Value = instance.Pincode;
                    command.Parameters.Add("@RoleID", MySqlDbType.Int32).Value = instance.RoleID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM Customer WHERE CustomerID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override Customer Select(int ID, out Exception exError)
        {
            Customer returnValue = new Customer();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM Customer Where CustomerID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Customer()
                            {
                                CustomerID = reader.GetInt32("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.GetDateTime("DOB"),
                                Gender = reader.GetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.GetBoolean("IsActive"),
                                RoleID = reader.GetInt32("RoleID")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<Customer> SelectAll(out Exception exError)
        {
            List<Customer> returnValue = new List<Customer>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM Customer ", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Customer()
                            {
                                CustomerID = reader.GetInt32("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.GetDateTime("DOB"),
                                Gender = reader.GetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.GetBoolean("IsActive"),
                                RoleID = reader.GetInt32("RoleID")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<Customer> SelectedID(List<int> IDs, out Exception exError)
        {
            List<Customer> returnValue = new List<Customer>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM Customer WHERE CustomerID IN (@CustomerIDs)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerIDs", MySqlDbType.VarString).Value = string.Join(",", IDs.ToArray());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Customer()
                            {
                                CustomerID = reader.GetInt32("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.GetDateTime("DOB"),
                                Gender = reader.GetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.GetBoolean("IsActive"),
                                RoleID = reader.GetInt32("RoleID")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override Customer Update(Customer instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE Customer SET " +
                    "FirstName = @FirstName, " +
                    "MiddleName = @MiddleName," +
                    "Lastname = @Lastname," +
                    "Email = @Email," +
                    "Password = @Password," +
                    "DOB = @DOB," +
                    "Gender = @Gender," +
                    "Mobile = @Mobile," +
                    "Address = @Address," +
                    "City = @City," +
                    "Country = @Country," +
                    "Pincode = @Pincode, " +
                    "RoleID = @RoleID " +
                    "WHERE CustomerID = @CustomerID ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FirstName", MySqlDbType.VarString).Value = instance.FirstName;
                    command.Parameters.Add("@MiddleName", MySqlDbType.VarString).Value = instance.MiddleName;
                    command.Parameters.Add("@Lastname", MySqlDbType.VarString).Value = instance.LastName;
                    command.Parameters.Add("@Email", MySqlDbType.VarString).Value = instance.Email;
                    command.Parameters.Add("@Password", MySqlDbType.VarString).Value = instance.Password;
                    command.Parameters.Add("@DOB", MySqlDbType.DateTime).Value = instance.DOB;
                    command.Parameters.Add("@Gender", MySqlDbType.Bit).Value = instance.Gender;
                    command.Parameters.Add("@Mobile", MySqlDbType.VarString).Value = instance.Mobile;
                    command.Parameters.Add("@Address", MySqlDbType.VarString).Value = instance.Address;
                    command.Parameters.Add("@City", MySqlDbType.VarString).Value = instance.City;
                    command.Parameters.Add("@Country", MySqlDbType.VarString).Value = instance.Country;
                    command.Parameters.Add("@Pincode", MySqlDbType.VarString).Value = instance.Pincode;
                    command.Parameters.Add("@RoleID", MySqlDbType.Int32).Value = instance.RoleID;
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.CustomerID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public Customer LoginCheck(string Email, string Password, out Exception exError)
        {
            Customer returnValue = new Models.Customer();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM Customer WHERE Email = '" + Email + "' AND Password = '" + Password + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Customer()
                            {
                                CustomerID = reader.GetInt32("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.GetDateTime("DOB"),
                                Gender = reader.GetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.GetBoolean("IsActive"),
                                RoleID = reader.GetInt32("RoleID")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return returnValue;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool ResetPassword(string Email, string Password, out Exception exError)
        {
            bool returnValue = false;
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("UPDATE Customer SET Password = '" + Password + "' WHERE Email = '" + Email + "' AND IsActive = 0 ", (MySqlConnection)this.Connection))
                {
                    int Rows = Convert.ToInt32(command.ExecuteScalar());
                    returnValue = Rows > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool ForgotPassword(string Email, out Exception exError)
        {
            bool returnValue = false;
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("UPDATE Customer SET IsActive = 0 WHERE Email = '" + Email + "'", (MySqlConnection)this.Connection))
                {
                    int Rows = Convert.ToInt32(command.ExecuteScalar());
                    returnValue = Rows > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool UserCheck(string Email, out Exception exError)
        {
            bool returnValue = false;
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT Count(1) FROM Customer WHERE Email = '" + Email + "'", (MySqlConnection)this.Connection))
                {
                    int Rows = Convert.ToInt32(command.ExecuteScalar());
                    returnValue = Rows > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool SetUserActive(int ID, out Exception exError)
        {
            bool returnValue = false;
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("UPDATE Customer SET IsActive = 1 WHERE CustomerID = " + ID, (MySqlConnection)this.Connection))
                {
                    int Rows = Convert.ToInt32(command.ExecuteScalar());
                    returnValue = Rows > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }
    }

    public class Order_Mapper : IDataMapper<Order>
    {
        public override Order Create(Order instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO `Order` (CustomerID, IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2,Notes) " +
                           "VALUES (@CustomerID, @IsNew, @Fault,@Evidence, @Company, @Reference, @OrderType, @OrderStatus, @OrderDate, @NumbOfBlinds, @ConsignNoteNum, @CompleteDate, @DeliveryDate, @DepartureDate, @ArrivalDate, @OrderM2,@Notes); " +
                           "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@IsNew", MySqlDbType.Bit).Value = instance.IsNew;
                    command.Parameters.Add("@Fault", MySqlDbType.Int32).Value = instance.Fault;
                    command.Parameters.Add("@Evidence", MySqlDbType.Bit).Value = instance.Evidence;
                    command.Parameters.Add("@Company", MySqlDbType.VarString).Value = instance.Company;
                    command.Parameters.Add("@Reference", MySqlDbType.VarString).Value = instance.Reference;
                    command.Parameters.Add("@OrderType", MySqlDbType.Int32).Value = instance.OrderType;
                    command.Parameters.Add("@OrderStatus", MySqlDbType.VarString).Value = instance.OrderStatus;
                    command.Parameters.Add("@OrderDate", MySqlDbType.DateTime).Value = instance.OrderDate;
                    command.Parameters.Add("@NumbOfBlinds", MySqlDbType.Int32).Value = instance.NumbOfBlinds;
                    command.Parameters.Add("@ConsignNoteNum", MySqlDbType.VarString).Value = instance.ConsignNoteNum;
                    command.Parameters.Add("@CompleteDate", MySqlDbType.DateTime).Value = instance.CompleteDate;
                    command.Parameters.Add("@DeliveryDate", MySqlDbType.DateTime).Value = instance.DeliveryDate;
                    command.Parameters.Add("@DepartureDate", MySqlDbType.DateTime).Value = instance.DepartureDate;
                    command.Parameters.Add("@ArrivalDate", MySqlDbType.DateTime).Value = instance.ArrivalDate;
                    command.Parameters.Add("@OrderM2", MySqlDbType.Double).Value = instance.OrderM2;
                    command.Parameters.Add("@Notes", MySqlDbType.VarString).Value = instance.Notes;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        query = "Update `Order` SET OrderNumber = @OrderNumber WHERE OrderID = " + RetVal;
                        using (MySqlCommand command1 = new MySqlCommand(query, (MySqlConnection)this.Connection))
                        {
                            command1.Parameters.Add("@OrderNumber", MySqlDbType.VarString).Value = RetVal;
                            command1.ExecuteScalar();
                        }
                        Exception custException = new Exception();
                        instance.OrderID = RetVal;
                        instance.OrderNumber = RetVal.ToString();
                        return instance ;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM Order WHERE OrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override Order Select(int ID, out Exception exError)
        {
            Order returnValue = new Order();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2,Notes,IsApproved FROM Order Where OrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Order()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                Fault = reader.GetInt32("Fault"),
                                IsNew = reader.GetBoolean("IsNew"),
                                Evidence = reader.GetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderType = reader.GetInt32("OrderType"),
                                OrderStatus = reader.SafeGetString("OrderStatus"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                NumbOfBlinds = reader.GetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.GetDateTime("CompleteDate"),
                                DeliveryDate = reader.GetDateTime("DeliveryDate"),
                                DepartureDate = reader.GetDateTime("DepartureDate"),
                                ArrivalDate = reader.GetDateTime("ArrivalDate"),
                                OrderM2 = reader.GetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.GetBoolean("IsApproved")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<Order> SelectAll(out Exception exError)
        {
            List<Order> returnValue = new List<Order>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2,Notes,IsApproved FROM Order", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Fault = reader.GetInt32("Fault"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                IsNew = reader.GetBoolean("IsNew"),
                                Evidence = reader.GetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderType = reader.GetInt32("OrderType"),
                                OrderStatus = reader.SafeGetString("OrderStatus"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                NumbOfBlinds = reader.GetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.GetDateTime("CompleteDate"),
                                DeliveryDate = reader.GetDateTime("DeliveryDate"),
                                DepartureDate = reader.GetDateTime("DepartureDate"),
                                ArrivalDate = reader.GetDateTime("ArrivalDate"),
                                OrderM2 = reader.GetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.GetBoolean("IsApproved")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override Order Update(Order instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE Order SET " +
                    "CustomerID = @CustomerID ," +
                    "IsNew = @IsNew ," +
                    "Evidence = @Evidence," +
                    "Company = @Company, " +
                    "Reference = @Reference, " +
                    "OrderType = @OrderType, " +
                    "OrderStatus = @OrderStatus," +
                    "OrderDate = @OrderDate," +
                    "NumbOfBlinds = @NumbOfBlinds," +
                    "ConsignNoteNum = @ConsignNoteNum," +
                    "CompleteDate = @CompleteDate," +
                    "DeliveryDate = @DeliveryDate," +
                    "DepartureDate = @DepartureDate," +
                    "ArrivalDate = @ArrivalDate," +
                    "OrderM2 = @OrderM2,"+
                    "Notes = @Notes, " +
                    "IsApproved = @IsApproved " +
                    "WHERE OrderID = @OrderID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderID", MySqlDbType.Int32).Value = instance.OrderID;
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@IsNew", MySqlDbType.Bit).Value = instance.IsNew;
                    command.Parameters.Add("@Fault", MySqlDbType.Int32).Value = instance.Fault;
                    command.Parameters.Add("@Evidence", MySqlDbType.Bit).Value = instance.Evidence;
                    command.Parameters.Add("@Company", MySqlDbType.VarString).Value = instance.Company;
                    command.Parameters.Add("@Reference", MySqlDbType.VarString).Value = instance.Reference;
                    command.Parameters.Add("@OrderType", MySqlDbType.Int32).Value = instance.OrderType;
                    command.Parameters.Add("@OrderStatus", MySqlDbType.VarString).Value = instance.OrderStatus;
                    command.Parameters.Add("@OrderDate", MySqlDbType.DateTime).Value = instance.OrderDate;
                    command.Parameters.Add("@NumbOfBlinds", MySqlDbType.Int32).Value = instance.NumbOfBlinds;
                    command.Parameters.Add("@ConsignNoteNum", MySqlDbType.VarString).Value = instance.ConsignNoteNum;
                    command.Parameters.Add("@CompleteDate", MySqlDbType.DateTime).Value = instance.CompleteDate;
                    command.Parameters.Add("@DeliveryDate", MySqlDbType.DateTime).Value = instance.DeliveryDate;
                    command.Parameters.Add("@DepartureDate", MySqlDbType.DateTime).Value = instance.DepartureDate;
                    command.Parameters.Add("@ArrivalDate", MySqlDbType.DateTime).Value = instance.ArrivalDate;
                    command.Parameters.Add("@OrderM2", MySqlDbType.Double).Value = instance.OrderM2;
                    command.Parameters.Add("@Notes", MySqlDbType.VarString).Value = instance.Notes;
                    command.Parameters.Add("@IsApproved", MySqlDbType.Bit).Value = instance.IsApproved;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.OrderID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<Order> SelectedID(List<int> IDs, out Exception exError)
        {
            List<Order> returnValue = new List<Order>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2,Notes,IsApproved FROM Order WHERE OrderID IN (@OrderIDs)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", MySqlDbType.VarString).Value = string.Join(",", IDs.ToArray());
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Fault = reader.GetInt32("Fault"),
                                IsNew = reader.GetBoolean("IsNew"),
                                Evidence = reader.GetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderType = reader.GetInt32("OrderType"),
                                OrderStatus = reader.SafeGetString("OrderStatus"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                NumbOfBlinds = reader.GetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.GetDateTime("CompleteDate"),
                                DeliveryDate = reader.GetDateTime("DeliveryDate"),
                                DepartureDate = reader.GetDateTime("DepartureDate"),
                                ArrivalDate = reader.GetDateTime("ArrivalDate"),
                                OrderM2 = reader.GetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.GetBoolean("IsApproved")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<Order> SelectedCustomer(int ID, out Exception exError)
        {
            List<Order> returnValue = new List<Order>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2,Notes,IsApproved FROM Order WHERE CustomerID = @CustomerID ", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                Fault = reader.GetInt32("Fault"),
                                IsNew = reader.GetBoolean("IsNew"),
                                Evidence = reader.GetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderType = reader.GetInt32("OrderType"),
                                OrderStatus = reader.SafeGetString("OrderStatus"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                NumbOfBlinds = reader.GetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.GetDateTime("CompleteDate"),
                                DeliveryDate = reader.GetDateTime("DeliveryDate"),
                                DepartureDate = reader.GetDateTime("DepartureDate"),
                                ArrivalDate = reader.GetDateTime("ArrivalDate"),
                                OrderM2 = reader.GetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.GetBoolean("IsApproved")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool ApproveOrders(List<int> OrderIDs, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE `Order` SET " +
                    "IsApproved = 'True' " +
                    "WHERE OrderID IN (@OrderIDs)";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", MySqlDbType.VarString).Value = OrderIDs;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<Order> GetCustomerOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy,out Exception exError)
        {
            List<Order> returnValue = new List<Order>();
            exError = null;

            try
            {
                string strFilterBy = "",strOrderBy = "";
                switch (FilterBy.ToLower())
                {
                    case "company":
                        strFilterBy = " WHERE company = '" + SearchCriteria + "' ";
                        break;
                    case "ordertype":
                        strFilterBy = " WHERE ordertype = '" + SearchCriteria + "' ";
                        break;
                    case "orderstatus":
                        strFilterBy = " WHERE orderstatus = '" + SearchCriteria + "' ";
                        break;
                    case "completedate":
                        strFilterBy = " WHERE completedate = '" + SearchCriteria + "' ";
                        break;
                    case "deliverydate":
                        strFilterBy = " WHERE deliverydate = '" + SearchCriteria + "' ";
                        break;
                    case "departuredate":
                        strFilterBy = " WHERE departuredate = '" + SearchCriteria + "' ";
                        break;
                    case "arrivaldate":
                        strFilterBy = " WHERE arrivaldate = '" + SearchCriteria + "' ";
                        break;
                    case "isapproved":
                        strFilterBy = " WHERE isapproved = " + SearchCriteria ;
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrWhiteSpace(OrderBy))
                {
                    strOrderBy = "Order By " + OrderBy;
                }

                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2,Notes,IsApproved FROM Order" +
                    " " + strFilterBy + " " + strOrderBy, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Fault = reader.GetInt32("Fault"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                IsNew = reader.GetBoolean("IsNew"),
                                Evidence = reader.GetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderType = reader.GetInt32("OrderType"),
                                OrderStatus = reader.SafeGetString("OrderStatus"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                NumbOfBlinds = reader.GetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.GetDateTime("CompleteDate"),
                                DeliveryDate = reader.GetDateTime("DeliveryDate"),
                                DepartureDate = reader.GetDateTime("DepartureDate"),
                                ArrivalDate = reader.GetDateTime("ArrivalDate"),
                                OrderM2 = reader.GetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.GetBoolean("IsApproved")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

    }
    public class OrderDetail_Mapper : IDataMapper<OrderDetail>
    {
        public override OrderDetail Create(OrderDetail instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO OrderDetail (OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter) " +
                           "VALUES (@OrderID, @Width,@Height, @SplPelmetWidth, @WidthMadeBy, @HeightMadeBy, @QualityCheckedBy,@SlatStyleID,@CordStyleID,@ReturnRequired,@MountType,@SquareMeter); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderID", MySqlDbType.Int32).Value = instance.OrderID;
                    command.Parameters.Add("@Width", MySqlDbType.Double).Value = instance.Width;
                    command.Parameters.Add("@Height", MySqlDbType.Double).Value = instance.Height;
                    command.Parameters.Add("@SplPelmetWidth", MySqlDbType.Double).Value = instance.SplPelmetWidth;
                    command.Parameters.Add("@WidthMadeBy", MySqlDbType.VarString).Value = instance.WidthMadeBy;
                    command.Parameters.Add("@HeightMadeBy", MySqlDbType.VarString).Value = instance.HeightMadeBy;
                    command.Parameters.Add("@QualityCheckedBy", MySqlDbType.VarString).Value = instance.QualityCheckedBy;
                    command.Parameters.Add("@SlatStyleID", MySqlDbType.Int32).Value = instance.SlatStyleID;
                    command.Parameters.Add("@CordStyleID", MySqlDbType.Int32).Value = instance.CordStyleID;
                    command.Parameters.Add("@ReturnRequired", MySqlDbType.Bit).Value = instance.ReturnRequired;
                    command.Parameters.Add("@MountType", MySqlDbType.Bit).Value = instance.MountType;
                    command.Parameters.Add("@SquareMeter", MySqlDbType.Double).Value = instance.SquareMeter;
                    //command.Parameters.Add("@ControlID", MySqlDbType.Int32).Value = instance.ControlID;
                    //command.Parameters.Add("@ControlStyle", MySqlDbType.Int32).Value = instance.ControlStyle;
                    //command.Parameters.Add("@OpeningStyle", MySqlDbType.Int32).Value = instance.OpeningStyle;
                    //command.Parameters.Add("@PelmetStyle", MySqlDbType.Int32).Value = instance.PelmetStyle;
                    //command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    //command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    //command.Parameters.Add("@Roll", MySqlDbType.Bit).Value = instance.Roll;
                    //command.Parameters.Add("@ReadyMadeSize", MySqlDbType.Double).Value = instance.ReadyMadeSize;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM OrderDetail WHERE OrderDetailID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override OrderDetail Select(int ID, out Exception exError)
        {
            OrderDetail returnValue = new OrderDetail();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize FROM OrderDetail Where OrderDetailID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new OrderDetail()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Width = reader.GetDouble("Width"),
                                Height = reader.GetDouble("Height"),
                                SplPelmetWidth = reader.GetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.GetInt32("SlatStyleID"),
                                CordStyleID = reader.GetInt32("CordStyleID"),
                                ReturnRequired = reader.GetBoolean("ReturnRequired"),
                                MountType = reader.GetBoolean("MountType"),
                                SquareMeter = reader.GetDouble("SquareMeter"),
                                ControlID = reader.GetInt32("ControlID"),
                                ControlStyle = reader.GetInt32("ControlStyle"),
                                OpeningStyle = reader.GetInt32("OpeningStyle"),
                                PelmetStyle = reader.GetInt32("PelmetStyle"),
                                ColorID = reader.GetInt32("ColorID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                Roll = reader.GetBoolean("Roll"),
                                ReadyMadeSize = reader.GetDouble("ReadyMadeSize")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<OrderDetail> SelectAll(out Exception exError)
        {
            List<OrderDetail> returnValue = new List<OrderDetail>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize FROM OrderDetail ", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderDetail()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Width = reader.GetDouble("Width"),
                                Height = reader.GetDouble("Height"),
                                SplPelmetWidth = reader.GetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.GetInt32("SlatStyleID"),
                                CordStyleID = reader.GetInt32("CordStyleID"),
                                ReturnRequired = reader.GetBoolean("ReturnRequired"),
                                MountType = reader.GetBoolean("MountType"),
                                SquareMeter = reader.GetDouble("SquareMeter"),
                                ControlID = reader.GetInt32("ControlID"),
                                ControlStyle = reader.GetInt32("ControlStyle"),
                                OpeningStyle = reader.GetInt32("OpeningStyle"),
                                PelmetStyle = reader.GetInt32("PelmetStyle"),
                                ColorID = reader.GetInt32("ColorID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                Roll = reader.GetBoolean("Roll"),
                                ReadyMadeSize = reader.GetDouble("ReadyMadeSize")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<OrderDetail> SelectedID(List<int> IDs, out Exception exError)
        {
            List<OrderDetail> returnValue = new List<OrderDetail>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize FROM OrderDetail WHERE OrderDetailID IN (@OrderDetailID)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderDetailIDs", MySqlDbType.VarString).Value = string.Join(",", IDs.ToArray());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderDetail()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Width = reader.GetDouble("Width"),
                                Height = reader.GetDouble("Height"),
                                SplPelmetWidth = reader.GetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.GetInt32("SlatStyleID"),
                                CordStyleID = reader.GetInt32("CordStyleID"),
                                ReturnRequired = reader.GetBoolean("ReturnRequired"),
                                MountType = reader.GetBoolean("MountType"),
                                SquareMeter = reader.GetDouble("SquareMeter"),
                                ControlID = reader.GetInt32("ControlID"),
                                ControlStyle = reader.GetInt32("ControlStyle"),
                                OpeningStyle = reader.GetInt32("OpeningStyle"),
                                PelmetStyle = reader.GetInt32("PelmetStyle"),
                                ColorID = reader.GetInt32("ColorID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                Roll = reader.GetBoolean("Roll"),
                                ReadyMadeSize = reader.GetDouble("ReadyMadeSize")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<OrderDetail> SelectedOrderID(List<int> IDs, out Exception exError)
        {
            List<OrderDetail> returnValue = new List<OrderDetail>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize FROM OrderDetail WHERE OrderID IN (@OrderIDs)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", MySqlDbType.VarString).Value = string.Join(",", IDs.ToArray());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderDetail()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Width = reader.GetDouble("Width"),
                                Height = reader.GetDouble("Height"),
                                SplPelmetWidth = reader.GetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.GetInt32("SlatStyleID"),
                                CordStyleID = reader.GetInt32("CordStyleID"),
                                ReturnRequired = reader.GetBoolean("ReturnRequired"),
                                MountType = reader.GetBoolean("MountType"),
                                SquareMeter = reader.GetDouble("SquareMeter"),
                                ControlID = reader.GetInt32("ControlID"),
                                ControlStyle = reader.GetInt32("ControlStyle"),
                                OpeningStyle = reader.GetInt32("OpeningStyle"),
                                PelmetStyle = reader.GetInt32("PelmetStyle"),
                                ColorID = reader.GetInt32("ColorID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                Roll = reader.GetBoolean("Roll"),
                                ReadyMadeSize = reader.GetDouble("ReadyMadeSize")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override OrderDetail Update(OrderDetail instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE OrderDetail SET " +
                    "OrderID = @OrderID," +
                    "Width = @Width," +
                    "Height = @Height," +
                    "SplPelmetWidth = @SplPelmetWidth," +
                    "WidthMadeBy = @WidthMadeBy," +
                    "HeightMadeBy = @HeightMadeBy," +
                    "QualityCheckedBy = @QualityCheckedBy," +
                    "SlatStyleID = @SlatStyleID," +
                    "CordStyleID = @CordStyleID," +
                    "ReturnRequired = @ReturnRequired," +
                    "MountType = @MountType," +
                    "SquareMeter = @SquareMeter," +
                    "ControlID = @ControlID," +
                    "ControlStyle = @ControlStyle," +
                    "OpeningStyle = @OpeningStyle," +
                    "PelmetStyle = @PelmetStyle," +
                    "ColorID = @ColorID," +
                    "MaterialID = @MaterialID," +
                    "Roll = @Roll," +
                    "ReadyMadeSize = @ReadyMadeSize " +
                    "WHERE OrderDetailID = @OrderDetailID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderID", MySqlDbType.Int32).Value = instance.OrderID;
                    command.Parameters.Add("@Width", MySqlDbType.Double).Value = instance.Width;
                    command.Parameters.Add("@Height", MySqlDbType.Double).Value = instance.Height;
                    command.Parameters.Add("@SplPelmetWidth", MySqlDbType.Double).Value = instance.SplPelmetWidth;
                    command.Parameters.Add("@WidthMadeBy", MySqlDbType.VarString).Value = instance.WidthMadeBy;
                    command.Parameters.Add("@HeightMadeBy", MySqlDbType.VarString).Value = instance.HeightMadeBy;
                    command.Parameters.Add("@QualityCheckedBy", MySqlDbType.VarString).Value = instance.QualityCheckedBy;
                    command.Parameters.Add("@SlatStyleID", MySqlDbType.Int32).Value = instance.SlatStyleID;
                    command.Parameters.Add("@CordStyleID", MySqlDbType.Int32).Value = instance.CordStyleID;
                    command.Parameters.Add("@ReturnRequired", MySqlDbType.Bit).Value = instance.ReturnRequired;
                    command.Parameters.Add("@MountType", MySqlDbType.Bit).Value = instance.MountType;
                    command.Parameters.Add("@SquareMeter", MySqlDbType.Double).Value = instance.SquareMeter;
                    command.Parameters.Add("@ControlID", MySqlDbType.Int32).Value = instance.ControlID;
                    command.Parameters.Add("@ControlStyle", MySqlDbType.Int32).Value = instance.ControlStyle;
                    command.Parameters.Add("@OpeningStyle", MySqlDbType.Int32).Value = instance.OpeningStyle;
                    command.Parameters.Add("@PelmetStyle", MySqlDbType.Int32).Value = instance.PelmetStyle;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@Roll", MySqlDbType.Bit).Value = instance.Roll;
                    command.Parameters.Add("@ReadyMadeSize", MySqlDbType.Double).Value = instance.ReadyMadeSize;
                    command.Parameters.Add("@OrderDetailID", MySqlDbType.Int32).Value = instance.OrderDetailID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.OrderDetailID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class UtilityOrder_Mapper : IDataMapper<UtilityOrder>
    {
        public override UtilityOrder Create(UtilityOrder instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO UtilityOrder (CustomerID, OrderType, Boxes) " +
                           "VALUES (@CustomerID, @OrderType, @Boxes); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@OrderType", MySqlDbType.Int32).Value = instance.OrderType;
                    command.Parameters.Add("@Boxes", MySqlDbType.Int32).Value = instance.Boxes;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM UtilityOrder WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override UtilityOrder Select(int ID, out Exception exError)
        {
            UtilityOrder returnValue = new UtilityOrder();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, OrderType, Boxes, IsApproved FROM UtilityOrder Where UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new UtilityOrder()
                            {
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                OrderType = reader.GetInt32("OrderType"),
                                Boxes = reader.GetInt32("Boxes"),
                                IsApproved = reader.GetBoolean("IsApproved")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<UtilityOrder> SelectAll(out Exception exError)
        {
            List<UtilityOrder> returnValue = new List<UtilityOrder>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, OrderType, Boxes, IsApproved FROM UtilityOrder", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new UtilityOrder()
                            {
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                OrderType = reader.GetInt32("OrderType"),
                                Boxes = reader.GetInt32("Boxes"),
                                IsApproved = reader.GetBoolean("IsApproved")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<UtilityOrder> SelectedCustomer(int ID, out Exception exError)
        {
            List<UtilityOrder> returnValue = new List<UtilityOrder>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, OrderType, Boxes, IsApproved FROM UtilityOrder WHERE CustomerID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new UtilityOrder()
                            {
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                OrderType = reader.GetInt32("OrderType"),
                                Boxes = reader.GetInt32("Boxes"),
                                IsApproved = reader.GetBoolean("IsApproved")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override UtilityOrder Update(UtilityOrder instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE UtilityOrder SET " +
                    "CustomerID = @CustomerID, " +
                    "OrderType = @OrderType," +
                    "Boxes = @Boxes, " +
                    "IsApproved = @IsApproved " +
                    "WHERE UtilityOrderID = @UtilityOrderID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@OrderType", MySqlDbType.Int32).Value = instance.OrderType;
                    command.Parameters.Add("@Boxes", MySqlDbType.Int32).Value = instance.Boxes;
                    command.Parameters.Add("@IsApproved", MySqlDbType.Bit).Value = instance.IsApproved;
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.Int32).Value = instance.UtilityOrderID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.UtilityOrderID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public bool ApproveUtilityOrders(List<int> OrderIDs, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE `UtilityOrder` SET " +
                    "IsApproved = 'True' " +
                    "WHERE UtilityOrderID IN (@OrderIDs)";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", MySqlDbType.VarString).Value = OrderIDs;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<UtilityOrder> GetCustomerUtilityOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy, out Exception exError)
        {
            List<UtilityOrder> returnValue = new List<UtilityOrder>();
            exError = null;
            try
            {
                string strFilterBy = "", strOrderBy = "";
                switch (FilterBy.ToLower())
                {
                    case "customerid":
                        strFilterBy = " WHERE customerid = '" + SearchCriteria + "' ";
                        break;
                    case "ordertype":
                        strFilterBy = " WHERE ordertype = '" + SearchCriteria + "' ";
                        break;
                    case "isapproved":
                        strFilterBy = " WHERE isapproved = " + SearchCriteria;
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrWhiteSpace(OrderBy))
                {
                    strOrderBy = "Order By " + OrderBy;
                }

                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, OrderType, Boxes, IsApproved FROM UtilityOrder" +
                    " " + strFilterBy + " " + strOrderBy, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new UtilityOrder()
                            {
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                OrderType = reader.GetInt32("OrderType"),
                                Boxes = reader.GetInt32("Boxes"),
                                IsApproved = reader.GetBoolean("IsApproved")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }
    }
    public class Fabric_Mapper : IDataMapper<Fabric>
    {
        public override Fabric Create(Fabric instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO Fabric (FabricType, ColorID, FabricSize) " +
                           "VALUES (@FabricType, @ColorID, @FabricSize); " +
                            "SELECT LAST_INSERT_ID();";


                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FabricType", MySqlDbType.Int32).Value = instance.FabricType;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@FabricSize", MySqlDbType.Int32).Value = instance.FabricSize;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM Fabric WHERE FabricID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override Fabric Select(int ID, out Exception exError)
        {
            Fabric returnValue = new Fabric();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT FabricID, FabricType, ColorID, FabricSize FROM Fabric Where FabricID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Fabric()
                            {
                                FabricID = reader.GetInt32("FabricID"),
                                FabricType = reader.GetInt32("FabricType"),
                                ColorID = reader.GetInt32("ColorID"),
                                FabricSize = reader.GetInt32("FabricSize")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<Fabric> SelectAll(out Exception exError)
        {
            List<Fabric> returnValue = new List<Fabric>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT FabricID, FabricType, ColorID, FabricSize FROM Fabric", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Fabric()
                            {
                                FabricID = reader.GetInt32("FabricID"),
                                FabricType = reader.GetInt32("FabricType"),
                                ColorID = reader.GetInt32("ColorID"),
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                FabricSize = reader.GetInt32("FabricSize")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<Fabric> SelectAll(int ID, out Exception exError)
        {
            List<Fabric> returnValue = new List<Fabric>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT FabricID, FabricType, ColorID, FabricSize FROM Fabric WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Fabric()
                            {
                                FabricID = reader.GetInt32("FabricID"),
                                FabricType = reader.GetInt32("FabricType"),
                                ColorID = reader.GetInt32("ColorID"),
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                FabricSize = reader.GetInt32("FabricSize")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override Fabric Update(Fabric instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE Fabric SET " +
                    "FabricType = @FabricType, " +
                    "ColorID = @ColorID," +
                    "FabricSize = @FabricSize " +
                    "WHERE FabricID = @FabricID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FabricType", MySqlDbType.Int32).Value = instance.FabricType;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@FabricSize", MySqlDbType.Int32).Value = instance.FabricSize;
                    command.Parameters.Add("@FabricID", MySqlDbType.Int32).Value = instance.FabricID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.FabricID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class RollerBlindType_Mapper : IDataMapper<RollerBlindType>
    {
        public override RollerBlindType Create(RollerBlindType instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO RollerBlinds (Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ) " +
                           "VALUES (@Description, @Profile, @RollerColor, @DLXCODE, @PCSCTN, @MOQ); " +
                            "SELECT LAST_INSERT_ID();";
                
                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", MySqlDbType.VarString).Value = instance.Description;
                    command.Parameters.Add("@Profile", MySqlDbType.VarString).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", MySqlDbType.VarString).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", MySqlDbType.VarString).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", MySqlDbType.VarString).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", MySqlDbType.DateTime).Value = instance.MOQ;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM RollerBlinds WHERE RollerBlindsID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override RollerBlindType Select(int ID, out Exception exError)
        {
            RollerBlindType returnValue = new RollerBlindType();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM RollerBlinds Where RollerBlindTypeID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new RollerBlindType()
                            {
                                RollerBlindTypeID = reader.GetInt32("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<RollerBlindType> SelectAll(out Exception exError)
        {
            List<RollerBlindType> returnValue = new List<RollerBlindType>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM RollerBlinds", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new RollerBlindType()
                            {
                                RollerBlindTypeID = reader.GetInt32("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override RollerBlindType Update(RollerBlindType instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE RollerBlinds SET " +
                    "Description = @Description, " +
                    "Profile = @Profile," +
                    "RollerColor = @RollerColor," +
                    "DLXCODE = @DLXCODE," +
                    "PCSCTN = @PCSCTN," +
                    "MOQ = @MOQ " +
                    "WHERE RollerBlindTypeID = @RollerBlindTypeID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", MySqlDbType.VarString).Value = instance.Description;
                    command.Parameters.Add("@Profile", MySqlDbType.VarString).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", MySqlDbType.VarString).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", MySqlDbType.VarString).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", MySqlDbType.VarString).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", MySqlDbType.VarString).Value = instance.MOQ;
                    command.Parameters.Add("@RollerBlindTypeID", MySqlDbType.Int32).Value = instance.RollerBlindTypeID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.RollerBlindTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class RollerBlinds_Mapper : IDataMapper<RollerBlinds>
    {
        public override RollerBlinds Create(RollerBlinds instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO RollerBlinds (UtilityOrderID, RollerBlindTypeID) " +
                           "VALUES (@UtilityOrderID, @RollerBlindTypeID); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@RollerBlindTypeID", MySqlDbType.VarString).Value = instance.RollerBlindTypeID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM RollerBlinds WHERE RollerBlindsID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override RollerBlinds Select(int ID, out Exception exError)
        {
            RollerBlinds returnValue = new RollerBlinds();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindsID, UtilityOrderID, RollerBlindTypeID FROM RollerBlinds Where RollerBlindsID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new RollerBlinds()
                            {
                                RollerBlindsID = reader.GetInt32("RollerBlindsID"),
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                RollerBlindTypeID = reader.GetInt32("RollerBlindTypeID")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<RollerBlinds> SelectAll(out Exception exError)
        {
            List<RollerBlinds> returnValue = new List<RollerBlinds>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindsID, UtilityOrderID, RollerBlindTypeID FROM RollerBlinds", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new RollerBlinds()
                            {
                                RollerBlindsID = reader.GetInt32("RollerBlindsID"),
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                RollerBlindTypeID = reader.GetInt32("RollerBlindTypeID")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<RollerBlinds> SelectAll(int ID, out Exception exError)
        {
            List<RollerBlinds> returnValue = new List<RollerBlinds>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindsID, UtilityOrderID, RollerBlindTypeID FROM RollerBlinds WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new RollerBlinds()
                            {
                                RollerBlindsID = reader.GetInt32("RollerBlindsID"),
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                RollerBlindTypeID = reader.GetInt32("RollerBlindTypeID")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override RollerBlinds Update(RollerBlinds instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE RollerBlinds SET " +
                    "UtilityOrderID = @UtilityOrderID, " +
                    "RollerBlindTypeID = @RollerBlindTypeID " +
                    "WHERE RollerBlindsID = @RollerBlindsID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@RollerBlindTypeID", MySqlDbType.VarString).Value = instance.RollerBlindTypeID;
                    command.Parameters.Add("@RollerBlindsID", MySqlDbType.Int32).Value = instance.RollerBlindsID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.RollerBlindsID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class Valance_Mapper : IDataMapper<Valance>
    {
        public override Valance Create(Valance instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO Valance (MaterialID, ColorID, Size) " +
                           "VALUES (@MaterialID, @ColorID, @Size); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@Size", MySqlDbType.VarString).Value = instance.Size;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM Valance WHERE ValanceID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override Valance Select(int ID, out Exception exError)
        {
            Valance returnValue = new Valance();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT ValanceID, MaterialID, ColorID, Size FROM Valance Where ValanceID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Valance()
                            {
                                ValanceID = reader.GetInt32("ValanceID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                ColorID = reader.GetInt32("ColorID"),
                                Size = reader.SafeGetString("Size")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<Valance> SelectAll(out Exception exError)
        {
            List<Valance> returnValue = new List<Valance>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT ValanceID, MaterialID, ColorID, Size FROM Valance", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Valance()
                            {
                                ValanceID = reader.GetInt32("ValanceID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                ColorID = reader.GetInt32("ColorID"),
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                Size = reader.SafeGetString("Size")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<Valance> SelectAll(int ID, out Exception exError)
        {
            List<Valance> returnValue = new List<Valance>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT ValanceID, MaterialID, ColorID, Size FROM Valance WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Valance()
                            {
                                ValanceID = reader.GetInt32("ValanceID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                ColorID = reader.GetInt32("ColorID"),
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                Size = reader.SafeGetString("Size")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override Valance Update(Valance instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE Valance SET " +
                    "MaterialID = @MaterialID, " +
                    "ColorID = @ColorID," +
                    "Size = @Size " +
                    "WHERE ValanceID = @ValanceID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@Size", MySqlDbType.VarString).Value = instance.Size;
                    command.Parameters.Add("@ValanceID", MySqlDbType.Int32).Value = instance.ValanceID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.ValanceID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class BottomRail_Mapper : IDataMapper<BottomRail>
    {
        public override BottomRail Create(BottomRail instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO BottomRail (MaterialID, ColorID, Size) " +
                           "VALUES (@MaterialID, @ColorID, @Size); "+
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@Size", MySqlDbType.VarString).Value = instance.Size;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM BottomRail WHERE BottomRailID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override BottomRail Select(int ID, out Exception exError)
        {
            BottomRail returnValue = new BottomRail();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT BottomRailID, MaterialID, ColorID, Size FROM BottomRail Where BottomRailID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new BottomRail()
                            {
                                BottomRailID = reader.GetInt32("BottomRailID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                ColorID = reader.GetInt32("ColorID"),
                                Size = reader.SafeGetString("Size")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<BottomRail> SelectAll(out Exception exError)
        {
            List<BottomRail> returnValue = new List<BottomRail>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT BottomRailID, MaterialID, ColorID, Size FROM BottomRail", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new BottomRail()
                            {
                                BottomRailID = reader.GetInt32("BottomRailID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                ColorID = reader.GetInt32("ColorID"),
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                Size = reader.SafeGetString("Size")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<BottomRail> SelectAll(int ID, out Exception exError)
        {
            List<BottomRail> returnValue = new List<BottomRail>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT BottomRailID, MaterialID, ColorID, Size FROM BottomRail WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new BottomRail()
                            {
                                BottomRailID = reader.GetInt32("BottomRailID"),
                                MaterialID = reader.GetInt32("MaterialID"),
                                ColorID = reader.GetInt32("ColorID"),
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                Size = reader.SafeGetString("Size")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override BottomRail Update(BottomRail instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE BottomRail SET " +
                    "MaterialID = @MaterialID, " +
                    "ColorID = @ColorID," +
                    "Size = @Size " +
                    "WHERE BottomRailID = @BottomRailID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@Size", MySqlDbType.VarString).Value = instance.Size;
                    command.Parameters.Add("@BottomRailID", MySqlDbType.Int32).Value = instance.BottomRailID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.BottomRailID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class SlatStyle_Mapper : IDataMapper<SlatStyle>
    {
        public override SlatStyle Create(SlatStyle instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO SlatStyle (SlatStyleDesc,For) " +
                           "VALUES (@SlatStyleDesc,@For); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@SlatStyleDesc", MySqlDbType.VarString).Value = instance.SlatStyleDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM SlatStyle WHERE SlatStyleID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override SlatStyle Select(int ID, out Exception exError)
        {
            SlatStyle returnValue = new SlatStyle();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT SlatStyleID, SlatStyleDesc, `For` FROM SlatStyle Where SlatStyleID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new SlatStyle()
                            {
                                SlatStyleID = reader.GetInt32("SlatStyleID"),
                                SlatStyleDesc = reader.SafeGetString("SlatStyleDesc"),
                                For = reader.SafeGetString("For")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<SlatStyle> SelectAll(out Exception exError)
        {
            List<SlatStyle> returnValue = new List<SlatStyle>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT SlatStyleID, SlatStyleDesc, `For` FROM SlatStyle", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new SlatStyle()
                            {
                                SlatStyleID = reader.GetInt32("SlatStyleID"),
                                SlatStyleDesc = reader.SafeGetString("SlatStyleDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<SlatStyle> SelectFor(string For, out Exception exError)
        {
            List<SlatStyle> returnValue = new List<SlatStyle>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT SlatStyleID, SlatStyleDesc, `For` FROM SlatStyle WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new SlatStyle()
                            {
                                SlatStyleID = reader.GetInt32("SlatStyleID"),
                                SlatStyleDesc = reader.SafeGetString("SlatStyleDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }
        public override SlatStyle Update(SlatStyle instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE SlatStyle SET " +
                    "SlatStyleDesc = @SlatStyleDesc," +
                    "`For` = @For " +
                    "WHERE SlatStyleID = @SlatStyleID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@SlatStyleDesc", MySqlDbType.VarString).Value = instance.SlatStyleDesc;
                    command.Parameters.Add("@SlatStyleID", MySqlDbType.Int32).Value = instance.SlatStyleID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.SlatStyleID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class CordStyle_Mapper : IDataMapper<CordStyle>
    {
        public override CordStyle Create(CordStyle instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO CordStyle (CordStyleDesc, `For`) " +
                           "VALUES (@CordStyleDesc, @For); "+
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CordStyleDesc", MySqlDbType.VarString).Value = instance.CordStyleDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM CordStyle WHERE CordStyleID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override CordStyle Select(int ID, out Exception exError)
        {
            CordStyle returnValue = new CordStyle();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT CordStyleID, CordStyleDesc, `For` FROM CordStyle Where CordStyleID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new CordStyle()
                            {
                                CordStyleID = reader.GetInt32("CordStyleID"),
                                CordStyleDesc = reader.SafeGetString("CordStyleDesc"),
                                For = reader.SafeGetString("For")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<CordStyle> SelectAll(out Exception exError)
        {
            List<CordStyle> returnValue = new List<CordStyle>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT CordStyleID, CordStyleDesc, `For` FROM CordStyle", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new CordStyle()
                            {
                                CordStyleID = reader.GetInt32("CordStyleID"),
                                CordStyleDesc = reader.SafeGetString("CordStyleDesc"),
                                For = reader.SafeGetString("FOr")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<CordStyle> SelectFor(string For, out Exception exError)
        {
            List<CordStyle> returnValue = new List<CordStyle>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT CordStyleID, CordStyleDesc, `For` FROM CordStyle WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new CordStyle()
                            {
                                CordStyleID = reader.GetInt32("CordStyleID"),
                                CordStyleDesc = reader.SafeGetString("CordStyleDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override CordStyle Update(CordStyle instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE CordStyle SET " +
                    "CordStyleDesc = @CordStyleDesc," +
                    "`For` = @For " +
                    "WHERE CordStyleID = @CordStyleID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CordStyleDesc", MySqlDbType.VarString).Value = instance.CordStyleDesc;
                    command.Parameters.Add("@CordStyleID", MySqlDbType.Int32).Value = instance.CordStyleID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.CordStyleID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class Control_Mapper : IDataMapper<Control>
    {
        public override Control Create(Control instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO Control (ControlDesc, `For`) " +
                           "VALUES (@ControlDesc, @For); "+
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@ControlDesc", MySqlDbType.VarString).Value = instance.ControlDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM Control WHERE ControlID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override Control Select(int ID, out Exception exError)
        {
            Control returnValue = new Control();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT ControlID, ControlDesc, `For` FROM Control Where ControlID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Control()
                            {
                                ControlID = reader.GetInt32("ControlID"),
                                ControlDesc = reader.SafeGetString("ControlDesc"),
                                For = reader.SafeGetString("For")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<Control> SelectAll(out Exception exError)
        {
            List<Control> returnValue = new List<Control>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT ControlID, ControlDesc, `For` FROM Control", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Control()
                            {
                                ControlID = reader.GetInt32("ControlID"),
                                ControlDesc = reader.SafeGetString("ControlDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<Control> SelectFor(string For, out Exception exError)
        {
            List<Control> returnValue = new List<Control>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT ControlID, ControlDesc, `For` FROM Control WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Control()
                            {
                                ControlID = reader.GetInt32("ControlID"),
                                ControlDesc = reader.SafeGetString("ControlDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }
        public override Control Update(Control instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE Control SET " +
                    "ControlDesc = @ControlDesc," +
                    "`For` = @For " +
                    "WHERE ControlID = @ControlID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@ControlDesc", MySqlDbType.VarString).Value = instance.ControlDesc;
                    command.Parameters.Add("@ControlID", MySqlDbType.Int32).Value = instance.ControlID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.ControlID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class Material_Mapper : IDataMapper<Material>
    {
        public override Material Create(Material instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO Material (MaterialDesc, `For`) " +
                           "VALUES (@MaterialDesc, @For); "+
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialDesc", MySqlDbType.VarString).Value = instance.MaterialDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM Material WHERE MaterialID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override Material Select(int ID, out Exception exError)
        {
            Material returnValue = new Material();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT MaterialID, MaterialDesc, `For` FROM Material Where MaterialID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Material()
                            {
                                MaterialID = reader.GetInt32("MaterialID"),
                                MaterialDesc = reader.SafeGetString("MaterialDesc"),
                                For = reader.SafeGetString("For")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<Material> SelectAll(out Exception exError)
        {
            List<Material> returnValue = new List<Material>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT MaterialID, MaterialDesc, `For` FROM Material", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Material()
                            {
                                MaterialID = reader.GetInt32("MaterialID"),
                                MaterialDesc = reader.SafeGetString("MaterialDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<Material> SelectFor(string For, out Exception exError)
        {
            List<Material> returnValue = new List<Material>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT MaterialID, MaterialDesc, `For` FROM Material WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Material()
                            {
                                MaterialID = reader.GetInt32("MaterialID"),
                                MaterialDesc = reader.SafeGetString("MaterialDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override Material Update(Material instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE Material SET " +
                    "MaterialDesc = @MaterialDesc," +
                    "`For` = @For " +
                    "WHERE MaterialID = @MaterialID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialDesc", MySqlDbType.VarString).Value = instance.MaterialDesc;
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.MaterialID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class Colors_Mapper : IDataMapper<Colors>
    {
        public override Colors Create(Colors instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO Colors (ColorsDesc, `For`) " +
                           "VALUES (@ColorsDesc, @For); "+
                            "SELECT LAST_INSERT_ID();";
                
                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@ColorsDesc", MySqlDbType.VarString).Value = instance.ColorsDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM Colors WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override Colors Select(int ID, out Exception exError)
        {
            Colors returnValue = new Colors();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT ColorsID, ColorsDesc, `For` FROM Colors Where ColorsID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Colors()
                            {
                                ColorsID = reader.GetInt32("ColorsID"),
                                ColorsDesc = reader.SafeGetString("ColorsDesc"),
                                For = reader.SafeGetString("For")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<Colors> SelectAll(out Exception exError)
        {
            List<Colors> returnValue = new List<Colors>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT ColorsID, ColorsDesc, `For` FROM Colors", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Colors()
                            {
                                ColorsID = reader.GetInt32("ColorsID"),
                                ColorsDesc = reader.SafeGetString("ColorsDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<Colors> SelectFor(string For, out Exception exError)
        {
            List<Colors> returnValue = new List<Colors>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT ColorsID, ColorsDesc, `For` FROM Colors WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Colors()
                            {
                                ColorsID = reader.GetInt32("ColorsID"),
                                ColorsDesc = reader.SafeGetString("ColorsDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override Colors Update(Colors instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE Colors SET " +
                    "ColorsDesc = @ColorsDesc," +
                    "`For` = @For " +
                    "WHERE ColorsID = @ColorsID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@ColorsDesc", MySqlDbType.VarString).Value = instance.ColorsDesc;
                    command.Parameters.Add("@ColorsID", MySqlDbType.Int32).Value = instance.ColorsID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.ColorsID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class Size_Mapper : IDataMapper<Size>
    {
        public override Size Create(Size instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO Size (SizeDesc, `For`) " +
                           "VALUES (@SizeDesc, @For); "+
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@SizeDesc", MySqlDbType.VarString).Value = instance.SizeDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM Size WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override Size Select(int ID, out Exception exError)
        {
            Size returnValue = new Size();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT SizeID, SizeDesc, `For` FROM Size Where SizeID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Size()
                            {
                                SizeID = reader.GetInt32("SizeID"),
                                SizeDesc = reader.SafeGetString("SizeDesc"),
                                For = reader.SafeGetString("For")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<Size> SelectAll(out Exception exError)
        {
            List<Size> returnValue = new List<Size>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT SizeID, SizeDesc, `For` FROM Size", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Size()
                            {
                                SizeID = reader.GetInt32("SizeID"),
                                SizeDesc = reader.SafeGetString("SizeDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<Size> SelectFor(string For, out Exception exError)
        {
            List<Size> returnValue = new List<Size>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT SizeID, SizeDesc, `For` FROM Size WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Size()
                            {
                                SizeID = reader.GetInt32("SizeID"),
                                SizeDesc = reader.SafeGetString("SizeDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override Size Update(Size instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE Size SET " +
                    "SizeDesc = @SizeDesc," +
                    "`For` = @For " +
                    "WHERE SizeID = @SizeID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@SizeDesc", MySqlDbType.VarString).Value = instance.SizeDesc;
                    command.Parameters.Add("@SizeID", MySqlDbType.Int32).Value = instance.SizeID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.SizeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class BlindType_Mapper : IDataMapper<BlindType>
    {
        public override BlindType Create(BlindType instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO BlindType (BlindTypeDesc, Val) " +
                           "VALUES (@BlindTypeDesc, @Val); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@BlindTypeDesc", MySqlDbType.VarString).Value = instance.BlindTypeDesc;
                    command.Parameters.Add("@Val", MySqlDbType.Int32).Value = instance.Val;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM BlindType WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override BlindType Select(int ID, out Exception exError)
        {
            BlindType returnValue = new BlindType();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT BlindTypeID, BlindTypeDesc, Val FROM BlindType Where BlindTypeID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new BlindType()
                            {
                                BlindTypeID = reader.GetInt32("BlindTypeID"),
                                BlindTypeDesc = reader.SafeGetString("BlindTypeDesc"),
                                Val = reader.GetInt32("Val")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<BlindType> SelectAll(out Exception exError)
        {
            List<BlindType> returnValue = new List<BlindType>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT BlindTypeID, BlindTypeDesc, Val FROM BlindType", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new BlindType()
                            {
                                BlindTypeID = reader.GetInt32("BlindTypeID"),
                                BlindTypeDesc = reader.SafeGetString("BlindTypeDesc"),
                                Val = reader.GetInt32("Val")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override BlindType Update(BlindType instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE BlindType SET " +
                    "BlindTypeDesc = @BlindTypeDesc," +
                    "Val = @Val " +
                    "WHERE BlindTypeID = @BlindTypeID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@BlindTypeDesc", MySqlDbType.VarString).Value = instance.BlindTypeDesc;
                    command.Parameters.Add("@BlindTypeID", MySqlDbType.Int32).Value = instance.BlindTypeID;
                    command.Parameters.Add("@Val", MySqlDbType.Int32).Value = instance.Val;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.BlindTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }

    public class OrderType_Mapper : IDataMapper<OrderType>
    {
        public override OrderType Create(OrderType instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO OrderType (OrderTypeDesc) " +
                           "VALUES (@OrderTypeDesc); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderTypeDesc", MySqlDbType.VarString).Value = instance.OrderTypeDesc;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM OrderType WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override OrderType Select(int ID, out Exception exError)
        {
            OrderType returnValue = new OrderType();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderTypeID, OrderTypeDesc FROM OrderType Where OrderTypeID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new OrderType()
                            {
                                OrderTypeID = reader.GetInt32("OrderTypeID"),
                                OrderTypeDesc = reader.SafeGetString("OrderTypeDesc")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<OrderType> SelectAll(out Exception exError)
        {
            List<OrderType> returnValue = new List<OrderType>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderTypeID, OrderTypeDesc FROM OrderType", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderType()
                            {
                                OrderTypeID = reader.GetInt32("OrderTypeID"),
                                OrderTypeDesc = reader.SafeGetString("OrderTypeDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override OrderType Update(OrderType instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE OrderType SET " +
                    "OrderTypeDesc = @OrderTypeDesc" +
                    "WHERE OrderTypeID = @OrderTypeID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderTypeDesc", MySqlDbType.VarString).Value = instance.OrderTypeDesc;
                    command.Parameters.Add("@OrderTypeID", MySqlDbType.Int32).Value = instance.OrderTypeID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.OrderTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }

    public class OrderStatus_Mapper : IDataMapper<OrderStatus>
    {
        public override OrderStatus Create(OrderStatus instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO OrderStatus (OrderStatusDesc) " +
                           "VALUES (@OrderStatusDesc); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderStatusDesc", MySqlDbType.VarString).Value = instance.OrderStatusDesc;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("DELETE FROM OrderStatus WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override OrderStatus Select(int ID, out Exception exError)
        {
            OrderStatus returnValue = new OrderStatus();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderStatusID, OrderStatusDesc FROM OrderStatus Where OrderStatusID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new OrderStatus()
                            {
                                OrderStatusID = reader.GetInt32("OrderStatusID"),
                                OrderStatusDesc = reader.SafeGetString("OrderStatusDesc")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<OrderStatus> SelectAll(out Exception exError)
        {
            List<OrderStatus> returnValue = new List<OrderStatus>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderStatusID, OrderStatusDesc FROM OrderStatus", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderStatus()
                            {
                                OrderStatusID = reader.GetInt32("OrderStatusID"),
                                OrderStatusDesc = reader.SafeGetString("OrderStatusDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override OrderStatus Update(OrderStatus instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE OrderStatus SET " +
                    "OrderStatusDesc = @OrderStatusDesc" +
                    "WHERE OrderStatusID = @OrderStatusID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderStatusDesc", MySqlDbType.VarString).Value = instance.OrderStatusDesc;
                    command.Parameters.Add("@OrderStatusID", MySqlDbType.Int32).Value = instance.OrderStatusID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.OrderStatusID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }

    public static class StringExt
    {
        public static string SafeGetString(this MySqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}
