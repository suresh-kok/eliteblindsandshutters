﻿using System;
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
        public override bool Create(Customer instance, out Exception exError)
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

                string query = "INSERT INTO Customer (FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,IsActive) " +
                           "VALUES (@FirstName, @MiddleName,@Lastname, @Email, @Password, @DOB, @Gender,@Mobile,@Address,@City,@Country,@Pincode,1) ";

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

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,IsActive,TotalOrders FROM Customer Where CustomerID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Customer()
                            {
                                CustomerID = reader.GetInt32("CustomerID"),
                                FirstName = reader.GetString("FirstName"),
                                MiddleName = reader.GetString("MiddleName"),
                                LastName = reader.GetString("Lastname"),
                                Email = reader.GetString("Email"),
                                Password = reader.GetString("Password"),
                                DOB = reader.GetDateTime("DOB"),
                                Gender = reader.GetBoolean("Gender"),
                                Mobile = reader.GetString("Mobile"),
                                Address = reader.GetString("Address"),
                                City = reader.GetString("City"),
                                Country = reader.GetString("Country"),
                                Pincode = reader.GetString("Pincode"),
                                IsActive = reader.GetBoolean("IsActive"),
                                TotalOrders = reader.GetInt32("TotalOrders")
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

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,IsActive,TotalOrders FROM Customer ", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Customer()
                            {
                                CustomerID = reader.GetInt32("CustomerID"),
                                FirstName = reader.GetString("FirstName"),
                                MiddleName = reader.GetString("MiddleName"),
                                LastName = reader.GetString("Lastname"),
                                Email = reader.GetString("Email"),
                                Password = reader.GetString("Password"),
                                DOB = reader.GetDateTime("DOB"),
                                Gender = reader.GetBoolean("Gender"),
                                Mobile = reader.GetString("Mobile"),
                                Address = reader.GetString("Address"),
                                City = reader.GetString("City"),
                                Country = reader.GetString("Country"),
                                Pincode = reader.GetString("Pincode"),
                                IsActive = reader.GetBoolean("IsActive"),
                                TotalOrders = reader.GetInt32("TotalOrders")
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

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,IsActive,TotalOrders FROM Customer WHERE CustomerID IN (@CustomerIDs)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerIDs", MySqlDbType.VarString).Value = string.Join(',',IDs.ToArray());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Customer()
                            {
                                CustomerID = reader.GetInt32("CustomerID"),
                                FirstName = reader.GetString("FirstName"),
                                MiddleName = reader.GetString("MiddleName"),
                                LastName = reader.GetString("Lastname"),
                                Email = reader.GetString("Email"),
                                Password = reader.GetString("Password"),
                                DOB = reader.GetDateTime("DOB"),
                                Gender = reader.GetBoolean("Gender"),
                                Mobile = reader.GetString("Mobile"),
                                Address = reader.GetString("Address"),
                                City = reader.GetString("City"),
                                Country = reader.GetString("Country"),
                                Pincode = reader.GetString("Pincode"),
                                IsActive = reader.GetBoolean("IsActive"),
                                TotalOrders = reader.GetInt32("TotalOrders")
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

        public override bool Update(Customer instance, out Exception exError)
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
                    "Pincode = @Pincode," +
                    "IsActive = @IsActive" +
                    "WHERE CustomerID = @CustomerID";

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
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }

        public bool LoginCheck(string Email, string Password, out Exception exError)
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

                using (MySqlCommand command = new MySqlCommand("SELECT Count(1) FROM Customer WHERE Email = " + Email + " AND Password = " + Password, (MySqlConnection)this.Connection))
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
        public override bool Create(Order instance, out Exception exError)
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

                string query = "INSERT INTO Order (CustomerID, IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2) " +
                           "VALUES (@CustomerID, @IsNew, @Fault,@Evidence, @Company, @Reference, @OrderType, @OrderStatus, @OrderDate, @NumbOfBlinds, @ConsignNoteNum, @CompleteDate, @DeliveryDate, @DepartureDate, @ArrivalDate, @OrderM2) ";

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

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2 FROM Order Where OrderID = " + ID, (MySqlConnection)this.Connection))
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
                                Company = reader.GetString("Company"),
                                Reference = reader.GetString("Reference"),
                                OrderType = reader.GetInt32("OrderType"),
                                OrderStatus = reader.GetString("OrderStatus"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                NumbOfBlinds = reader.GetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.GetString("ConsignNoteNum"),
                                CompleteDate = reader.GetDateTime("CompleteDate"),
                                DeliveryDate = reader.GetDateTime("DeliveryDate"),
                                DepartureDate = reader.GetDateTime("DepartureDate"),
                                ArrivalDate = reader.GetDateTime("ArrivalDate"),
                                OrderM2 = reader.GetDouble("OrderM2")
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2 FROM Order", (MySqlConnection)this.Connection))
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
                                Company = reader.GetString("Company"),
                                Reference = reader.GetString("Reference"),
                                OrderType = reader.GetInt32("OrderType"),
                                OrderStatus = reader.GetString("OrderStatus"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                NumbOfBlinds = reader.GetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.GetString("ConsignNoteNum"),
                                CompleteDate = reader.GetDateTime("CompleteDate"),
                                DeliveryDate = reader.GetDateTime("DeliveryDate"),
                                DepartureDate = reader.GetDateTime("DepartureDate"),
                                ArrivalDate = reader.GetDateTime("ArrivalDate"),
                                OrderM2 = reader.GetDouble("OrderM2")
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

        public override bool Update(Order instance, out Exception exError)
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
                    "OrderM2 = @OrderM2," +
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

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2 FROM Order WHERE OrderID IN (@OrderIDs)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", MySqlDbType.VarString).Value = string.Join(',', IDs.ToArray());
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Fault = reader.GetInt32("Fault"),
                                IsNew = reader.GetBoolean("IsNew"),
                                Evidence = reader.GetBoolean("Evidence"),
                                Company = reader.GetString("Company"),
                                Reference = reader.GetString("Reference"),
                                OrderType = reader.GetInt32("OrderType"),
                                OrderStatus = reader.GetString("OrderStatus"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                NumbOfBlinds = reader.GetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.GetString("ConsignNoteNum"),
                                CompleteDate = reader.GetDateTime("CompleteDate"),
                                DeliveryDate = reader.GetDateTime("DeliveryDate"),
                                DepartureDate = reader.GetDateTime("DepartureDate"),
                                ArrivalDate = reader.GetDateTime("ArrivalDate"),
                                OrderM2 = reader.GetDouble("OrderM2")
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, OrderType,OrderStatus,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,OrderM2 FROM Order WHERE CustomerID = @CustomerID ", (MySqlConnection)this.Connection))
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
                                Company = reader.GetString("Company"),
                                Reference = reader.GetString("Reference"),
                                OrderType = reader.GetInt32("OrderType"),
                                OrderStatus = reader.GetString("OrderStatus"),
                                OrderDate = reader.GetDateTime("OrderDate"),
                                NumbOfBlinds = reader.GetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.GetString("ConsignNoteNum"),
                                CompleteDate = reader.GetDateTime("CompleteDate"),
                                DeliveryDate = reader.GetDateTime("DeliveryDate"),
                                DepartureDate = reader.GetDateTime("DepartureDate"),
                                ArrivalDate = reader.GetDateTime("ArrivalDate"),
                                OrderM2 = reader.GetDouble("OrderM2")
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
        public override bool Create(OrderDetail instance, out Exception exError)
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

                string query = "INSERT INTO OrderDetail (OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize,Notes) " +
                           "VALUES (@OrderID, @Width,@Height, @SplPelmetWidth, @WidthMadeBy, @HeightMadeBy, @QualityCheckedBy,@SlatStyleID,@CordStyleID,@ReturnRequired,@MountType,@SquareMeter,@ControlID,@ControlStyle,@OpeningStyle,@PelmetStyle,@ColorID,@MaterialID,@Roll,@ReadyMadeSize,@Notes) ";

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
                    command.Parameters.Add("@Notes", MySqlDbType.VarString).Value = instance.Notes;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize,Notes FROM OrderDetail Where OrderDetailID = " + ID, (MySqlConnection)this.Connection))
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
                                WidthMadeBy = reader.GetString("WidthMadeBy"),
                                HeightMadeBy = reader.GetString("HeightMadeBy"),
                                QualityCheckedBy = reader.GetString("QualityCheckedBy"),
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
                                ReadyMadeSize = reader.GetDouble("ReadyMadeSize"),
                                Notes = reader.GetString("Notes"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize,Notes FROM OrderDetail ", (MySqlConnection)this.Connection))
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
                                WidthMadeBy = reader.GetString("WidthMadeBy"),
                                HeightMadeBy = reader.GetString("HeightMadeBy"),
                                QualityCheckedBy = reader.GetString("QualityCheckedBy"),
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
                                ReadyMadeSize = reader.GetDouble("ReadyMadeSize"),
                                Notes = reader.GetString("Notes"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize,Notes FROM OrderDetail WHERE OrderDetailID IN (@OrderDetailID)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderDetailIDs", MySqlDbType.VarString).Value = string.Join(',',IDs.ToArray());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderDetail()
                            {
                                OrderID = reader.GetInt32("OrderID"),
                                Width = reader.GetDouble("Width"),
                                Height = reader.GetDouble("Height"),
                                SplPelmetWidth = reader.GetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.GetString("WidthMadeBy"),
                                HeightMadeBy = reader.GetString("HeightMadeBy"),
                                QualityCheckedBy = reader.GetString("QualityCheckedBy"),
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
                                ReadyMadeSize = reader.GetDouble("ReadyMadeSize"),
                                Notes = reader.GetString("Notes"),
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

        public override bool Update(OrderDetail instance, out Exception exError)
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
                    "ReadyMadeSize = @ReadyMadeSize," +
                    "Notes = @Notes," +
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
                    command.Parameters.Add("@Notes", MySqlDbType.VarString).Value = instance.Notes;
                    command.Parameters.Add("@OrderDetailID", MySqlDbType.Int32).Value = instance.OrderDetailID;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class UtilityOrder_Mapper : IDataMapper<UtilityOrder>
    {
        public override bool Create(UtilityOrder instance, out Exception exError)
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
                           "VALUES (@CustomerID, @OrderType, @Boxes) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@OrderType", MySqlDbType.Int32).Value = instance.OrderType;
                    command.Parameters.Add("@Boxes", MySqlDbType.Int32).Value = instance.Boxes;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, OrderType, Boxes FROM UtilityOrder Where UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
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
                                Boxes = reader.GetInt32("Boxes")
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

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, OrderType, Boxes FROM UtilityOrder", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new UtilityOrder()
                            {
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                OrderType = reader.GetInt32("OrderType"),
                                Boxes = reader.GetInt32("Boxes")
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

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, OrderType, Boxes FROM UtilityOrder WHERE CustomerID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new UtilityOrder()
                            {
                                UtilityOrderID = reader.GetInt32("UtilityOrderID"),
                                CustomerID = reader.GetInt32("CustomerID"),
                                OrderType = reader.GetInt32("OrderType"),
                                Boxes = reader.GetInt32("Boxes")
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

        public override bool Update(UtilityOrder instance, out Exception exError)
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
                    "Boxes = @Boxes," +
                    "WHERE UtilityOrderID = @UtilityOrderID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@OrderType", MySqlDbType.Int32).Value = instance.OrderType;
                    command.Parameters.Add("@Boxes", MySqlDbType.Int32).Value = instance.Boxes;
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.Int32).Value = instance.UtilityOrderID;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class Fabric_Mapper : IDataMapper<Fabric>
    {
        public override bool Create(Fabric instance, out Exception exError)
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
                           "VALUES (@FabricType, @ColorID, @FabricSize) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FabricType", MySqlDbType.Int32).Value = instance.FabricType;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@FabricSize", MySqlDbType.Int32).Value = instance.FabricSize;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

        public override bool Update(Fabric instance, out Exception exError)
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
                    "FabricSize = @FabricSize," +
                    "WHERE FabricID = @FabricID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FabricType", MySqlDbType.Int32).Value = instance.FabricType;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@FabricSize", MySqlDbType.Int32).Value = instance.FabricSize;
                    command.Parameters.Add("@FabricID", MySqlDbType.Int32).Value = instance.FabricID;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class RollerBlindType_Mapper : IDataMapper<RollerBlindType>
    {
        public override bool Create(RollerBlindType instance, out Exception exError)
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
                           "VALUES (@Description, @Profile, @RollerColor, @DLXCODE, @PCSCTN, @MOQ) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", MySqlDbType.VarString).Value = instance.Description;
                    command.Parameters.Add("@Profile", MySqlDbType.VarString).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", MySqlDbType.VarString).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", MySqlDbType.VarString).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", MySqlDbType.VarString).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", MySqlDbType.DateTime).Value = instance.MOQ;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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
                                Description = reader.GetString("Description"),
                                Profile = reader.GetString("Profile"),
                                RollerColor = reader.GetString("RollerColor"),
                                DLXCODE = reader.GetString("DLXCODE"),
                                PCSCTN = reader.GetString("PCSCTN"),
                                MOQ = reader.GetString("MOQ")
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
                                Description = reader.GetString("Description"),
                                Profile = reader.GetString("Profile"),
                                RollerColor = reader.GetString("RollerColor"),
                                DLXCODE = reader.GetString("DLXCODE"),
                                PCSCTN = reader.GetString("PCSCTN"),
                                MOQ = reader.GetString("MOQ")
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

        public override bool Update(RollerBlindType instance, out Exception exError)
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
                    "MOQ = @MOQ," +
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

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class RollerBlinds_Mapper : IDataMapper<RollerBlinds>
    {
        public override bool Create(RollerBlinds instance, out Exception exError)
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
                           "VALUES (@UtilityOrderID, @RollerBlindTypeID) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@RollerBlindTypeID", MySqlDbType.VarString).Value = instance.RollerBlindTypeID;
                    
                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

        public override bool Update(RollerBlinds instance, out Exception exError)
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
                    "RollerBlindTypeID = @RollerBlindTypeID," +
                    "WHERE RollerBlindsID = @RollerBlindsID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@RollerBlindTypeID", MySqlDbType.VarString).Value = instance.RollerBlindTypeID;
                    command.Parameters.Add("@RollerBlindsID", MySqlDbType.Int32).Value = instance.RollerBlindsID;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class Valance_Mapper : IDataMapper<Valance>
    {
        public override bool Create(Valance instance, out Exception exError)
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
                           "VALUES (@MaterialID, @ColorID, @Size) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@Size", MySqlDbType.VarString).Value = instance.Size;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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
                                Size = reader.GetString("Size")
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
                                Size = reader.GetString("Size")
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

        public override bool Update(Valance instance, out Exception exError)
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
                    "Size = @Size," +
                    "WHERE ValanceID = @ValanceID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@Size", MySqlDbType.VarString).Value = instance.Size;
                    command.Parameters.Add("@ValanceID", MySqlDbType.Int32).Value = instance.ValanceID;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class BottomRail_Mapper : IDataMapper<BottomRail>
    {

        public override bool Create(BottomRail instance, out Exception exError)
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
                           "VALUES (@MaterialID, @ColorID, @Size) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@Size", MySqlDbType.VarString).Value = instance.Size;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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
                                Size = reader.GetString("Size")
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
                                Size = reader.GetString("Size")
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

        public override bool Update(BottomRail instance, out Exception exError)
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
                    "Size = @Size," +
                    "WHERE BottomRailID = @BottomRailID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@Size", MySqlDbType.VarString).Value = instance.Size;
                    command.Parameters.Add("@BottomRailID", MySqlDbType.Int32).Value = instance.BottomRailID;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class SlatStyle_Mapper : IDataMapper<SlatStyle>
    {
        public override bool Create(SlatStyle instance, out Exception exError)
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
                           "VALUES (@SlatStyleDesc,@For) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@SlatStyleDesc", MySqlDbType.VarString).Value = instance.SlatStyleDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT SlatStyleID, SlatStyleDesc, For FROM SlatStyle Where SlatStyleID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new SlatStyle()
                            {
                                SlatStyleID = reader.GetInt32("SlatStyleID"),
                                SlatStyleDesc = reader.GetString("SlatStyleDesc"),
                                For = reader.GetString("For")
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

                using (MySqlCommand command = new MySqlCommand("SELECT SlatStyleID, SlatStyleDesc, For FROM SlatStyle", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new SlatStyle()
                            {
                                SlatStyleID = reader.GetInt32("SlatStyleID"),
                                SlatStyleDesc = reader.GetString("SlatStyleDesc"),
                                For = reader.GetString("For")
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

        public override bool Update(SlatStyle instance, out Exception exError)
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
                    "SlatStyleDesc = @SlatStyleDesc" +
                    "For = @For" +
                    "WHERE SlatStyleID = @SlatStyleID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@SlatStyleDesc", MySqlDbType.VarString).Value = instance.SlatStyleDesc;
                    command.Parameters.Add("@SlatStyleID", MySqlDbType.Int32).Value = instance.SlatStyleID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class CordStyle_Mapper : IDataMapper<CordStyle>
    {
        public override bool Create(CordStyle instance, out Exception exError)
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

                string query = "INSERT INTO CordStyle (CordStyleDesc, For) " +
                           "VALUES (@CordStyleDesc, @For) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CordStyleDesc", MySqlDbType.VarString).Value = instance.CordStyleDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT CordStyleID, CordStyleDesc, For FROM CordStyle Where CordStyleID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new CordStyle()
                            {
                                CordStyleID = reader.GetInt32("CordStyleID"),
                                CordStyleDesc = reader.GetString("CordStyleDesc"),
                                For = reader.GetString("For")
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

                using (MySqlCommand command = new MySqlCommand("SELECT CordStyleID, CordStyleDesc, For FROM CordStyle", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new CordStyle()
                            {
                                CordStyleID = reader.GetInt32("CordStyleID"),
                                CordStyleDesc = reader.GetString("CordStyleDesc"),
                                For = reader.GetString("FOr")
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

        public override bool Update(CordStyle instance, out Exception exError)
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
                    "CordStyleDesc = @CordStyleDesc" +
                    "For = @For" +
                    "WHERE CordStyleID = @CordStyleID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CordStyleDesc", MySqlDbType.VarString).Value = instance.CordStyleDesc;
                    command.Parameters.Add("@CordStyleID", MySqlDbType.Int32).Value = instance.CordStyleID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;
                    
                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class Control_Mapper : IDataMapper<Control>
    {
        public override bool Create(Control instance, out Exception exError)
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

                string query = "INSERT INTO Control (ControlDesc, For) " +
                           "VALUES (@ControlDesc, @For) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@ControlDesc", MySqlDbType.VarString).Value = instance.ControlDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT ControlID, ControlDesc, For FROM Control Where ControlID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Control()
                            {
                                ControlID = reader.GetInt32("ControlID"),
                                ControlDesc = reader.GetString("ControlDesc"),
                                For = reader.GetString("For")
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

                using (MySqlCommand command = new MySqlCommand("SELECT ControlID, ControlDesc, For FROM Control", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Control()
                            {
                                ControlID = reader.GetInt32("ControlID"),
                                ControlDesc = reader.GetString("ControlDesc"),
                                For = reader.GetString("For")
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

        public override bool Update(Control instance, out Exception exError)
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
                    "ControlDesc = @ControlDesc" +
                    "For = @For" +
                    "WHERE ControlID = @ControlID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@ControlDesc", MySqlDbType.VarString).Value = instance.ControlDesc;
                    command.Parameters.Add("@ControlID", MySqlDbType.Int32).Value = instance.ControlID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class Material_Mapper : IDataMapper<Material>
    {
        public override bool Create(Material instance, out Exception exError)
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

                string query = "INSERT INTO Material (MaterialDesc, For) " +
                           "VALUES (@MaterialDesc, @For) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialDesc", MySqlDbType.VarString).Value = instance.MaterialDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT MaterialID, MaterialDesc, For FROM Material Where MaterialID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Material()
                            {
                                MaterialID = reader.GetInt32("MaterialID"),
                                MaterialDesc = reader.GetString("MaterialDesc"),
                                For = reader.GetString("For")
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

                using (MySqlCommand command = new MySqlCommand("SELECT MaterialID, MaterialDesc, For FROM Material", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Material()
                            {
                                MaterialID = reader.GetInt32("MaterialID"),
                                MaterialDesc = reader.GetString("MaterialDesc"),
                                For = reader.GetString("For")
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

        public override bool Update(Material instance, out Exception exError)
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
                    "MaterialDesc = @MaterialDesc" +
                    "For = @For" +
                    "WHERE MaterialID = @MaterialID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialDesc", MySqlDbType.VarString).Value = instance.MaterialDesc;
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
    public class Colors_Mapper : IDataMapper<Colors>
    {
        public override bool Create(Colors instance, out Exception exError)
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

                string query = "INSERT INTO Colors (ColorsDesc, For) " +
                           "VALUES (@ColorsDesc, @For) ";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@ColorsDesc", MySqlDbType.VarString).Value = instance.ColorsDesc;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
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

                using (MySqlCommand command = new MySqlCommand("SELECT ColorsID, ColorsDesc, For FROM Colors Where ColorsID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Colors()
                            {
                                ColorsID = reader.GetInt32("ColorsID"),
                                ColorsDesc = reader.GetString("ColorsDesc"),
                                For = reader.GetString("For")
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

                using (MySqlCommand command = new MySqlCommand("SELECT ColorsID, ColorsDesc, For FROM Colors", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Colors()
                            {
                                ColorsID = reader.GetInt32("ColorsID"),
                                ColorsDesc = reader.GetString("ColorsDesc"),
                                For = reader.GetString("For")
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

        public override bool Update(Colors instance, out Exception exError)
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
                    "ColorsDesc = @ColorsDesc" +
                    "For = @For" +
                    "WHERE ColorsID = @ColorsID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@ColorsDesc", MySqlDbType.VarString).Value = instance.ColorsDesc;
                    command.Parameters.Add("@ColorsID", MySqlDbType.Int32).Value = instance.ColorsID;
                    command.Parameters.Add("@For", MySqlDbType.VarString).Value = instance.For;

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return true;
            }
            finally
            {
                Connection.Close();
            }
            return true;
        }
    }
}
