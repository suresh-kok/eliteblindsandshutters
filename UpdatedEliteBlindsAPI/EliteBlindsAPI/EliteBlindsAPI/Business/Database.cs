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

                string query = "INSERT INTO customer (FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM customer WHERE CustomerID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM customer Where CustomerID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Customer()
                            {
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.SafeGetDateTime("DOB"),
                                Gender = reader.SafeGetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.SafeGetBoolean("IsActive"),
                                RoleID = reader.SafeGetInt32("RoleID")
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

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM customer ", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Customer()
                            {
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.SafeGetDateTime("DOB"),
                                Gender = reader.SafeGetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.SafeGetBoolean("IsActive"),
                                RoleID = reader.SafeGetInt32("RoleID")
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

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM customer WHERE CustomerID IN (@CustomerIDs)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerIDs", MySqlDbType.VarString).Value = string.Join(",", IDs.ToArray());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Customer()
                            {
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.SafeGetDateTime("DOB"),
                                Gender = reader.SafeGetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.SafeGetBoolean("IsActive"),
                                RoleID = reader.SafeGetInt32("RoleID")
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

                string query = "UPDATE customer SET " +
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

                using (MySqlCommand command = new MySqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM customer WHERE Email = '" + Email + "' AND Password = '" + Password + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Customer()
                            {
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.SafeGetDateTime("DOB"),
                                Gender = reader.SafeGetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.SafeGetBoolean("IsActive"),
                                RoleID = reader.SafeGetInt32("RoleID")
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

                using (MySqlCommand command = new MySqlCommand("UPDATE customer SET Password = '" + Password + "' WHERE Email = '" + Email + "' AND IsActive = 0 ", (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("UPDATE customer SET IsActive = 0 WHERE Email = '" + Email + "'", (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT Count(1) FROM customer WHERE Email = '" + Email + "'", (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("UPDATE customer SET IsActive = 1 WHERE CustomerID = " + ID, (MySqlConnection)this.Connection))
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

                string query = "INSERT INTO `order` (CustomerID, IsNew,Fault,Evidence, Company, Reference, OrderTypeID,OrderStatusID,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,BlindTypeID,Transport,OrderM2,Notes) " +
                           "VALUES (@CustomerID, @IsNew, @Fault,@Evidence, @Company, @Reference, @OrderTypeID, @OrderStatusID, @OrderDate, @NumbOfBlinds, @ConsignNoteNum, @CompleteDate, @DeliveryDate, @DepartureDate, @ArrivalDate,@BlindTypeID,@Transport, @OrderM2,@Notes); " +
                           "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@IsNew", MySqlDbType.Bit).Value = instance.IsNew;
                    command.Parameters.Add("@Fault", MySqlDbType.VarString).Value = instance.Fault;
                    command.Parameters.Add("@Evidence", MySqlDbType.Bit).Value = instance.Evidence;
                    command.Parameters.Add("@Company", MySqlDbType.VarString).Value = instance.Company;
                    command.Parameters.Add("@Reference", MySqlDbType.VarString).Value = instance.Reference;
                    command.Parameters.Add("@OrderTypeID", MySqlDbType.Int32).Value = instance.OrderTypeID;
                    command.Parameters.Add("@OrderStatusID", MySqlDbType.Int32).Value = instance.OrderStatusID;
                    command.Parameters.Add("@OrderDate", MySqlDbType.DateTime).Value = instance.OrderDate;
                    command.Parameters.Add("@NumbOfBlinds", MySqlDbType.Int32).Value = instance.NumbOfBlinds;
                    command.Parameters.Add("@ConsignNoteNum", MySqlDbType.VarString).Value = instance.ConsignNoteNum;
                    command.Parameters.Add("@CompleteDate", MySqlDbType.DateTime).Value = instance.CompleteDate;
                    command.Parameters.Add("@DeliveryDate", MySqlDbType.DateTime).Value = instance.DeliveryDate;
                    command.Parameters.Add("@DepartureDate", MySqlDbType.DateTime).Value = instance.DepartureDate;
                    command.Parameters.Add("@ArrivalDate", MySqlDbType.DateTime).Value = instance.ArrivalDate;
                    command.Parameters.Add("@BlindTypeID", MySqlDbType.Int32).Value = instance.BlindTypeID;
                    command.Parameters.Add("@Transport", MySqlDbType.VarString).Value = instance.Transport;
                    command.Parameters.Add("@OrderM2", MySqlDbType.Double).Value = instance.OrderM2;
                    command.Parameters.Add("@Notes", MySqlDbType.VarString).Value = instance.Notes;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        query = "Update `order` SET OrderNumber = @OrderNumber WHERE OrderID = " + RetVal;
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM `order` WHERE OrderID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " +
                    "Where OrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Order()
                            {
                                OrderID = reader.SafeGetInt32("OrderID"),
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt32("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt32("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " , (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {
                                OrderID = reader.SafeGetInt32("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt32("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt32("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

                string query = "UPDATE `order` SET " +
                    //"CustomerID = @CustomerID ," +
                    "IsNew = @IsNew ," +
                    "Evidence = @Evidence," +
                    "Company = @Company, " +
                    "Reference = @Reference, " +
                    //"OrderTypeID = @OrderTypeID, " +
                    "OrderStatusID = @OrderStatusID," +
                    "OrderDate = @OrderDate," +
                    "NumbOfBlinds = @NumbOfBlinds," +
                    "ConsignNoteNum = @ConsignNoteNum," +
                    "CompleteDate = @CompleteDate," +
                    "DeliveryDate = @DeliveryDate," +
                    "DepartureDate = @DepartureDate," +
                    "ArrivalDate = @ArrivalDate," +
                    "BlindTypeID = @BlindTypeID," +
                    "Transport = @Transport," +
                    "OrderM2 = @OrderM2," +
                    "Notes = @Notes, " +
                    "IsApproved = @IsApproved " +
                    "WHERE OrderID = @OrderID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderID", MySqlDbType.Int32).Value = instance.OrderID;
                    //command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@IsNew", MySqlDbType.Bit).Value = instance.IsNew;
                    command.Parameters.Add("@Fault", MySqlDbType.VarString).Value = instance.Fault;
                    command.Parameters.Add("@Evidence", MySqlDbType.Bit).Value = instance.Evidence;
                    command.Parameters.Add("@Company", MySqlDbType.VarString).Value = instance.Company;
                    command.Parameters.Add("@Reference", MySqlDbType.VarString).Value = instance.Reference;
                    command.Parameters.Add("@OrderTypeID", MySqlDbType.Int32).Value = instance.OrderTypeID;
                    command.Parameters.Add("@OrderStatusID", MySqlDbType.Int32).Value = instance.OrderStatusID;
                    command.Parameters.Add("@OrderDate", MySqlDbType.DateTime).Value = instance.OrderDate;
                    command.Parameters.Add("@NumbOfBlinds", MySqlDbType.Int32).Value = instance.NumbOfBlinds;
                    command.Parameters.Add("@ConsignNoteNum", MySqlDbType.VarString).Value = instance.ConsignNoteNum;
                    command.Parameters.Add("@CompleteDate", MySqlDbType.DateTime).Value = instance.CompleteDate;
                    command.Parameters.Add("@DeliveryDate", MySqlDbType.DateTime).Value = instance.DeliveryDate;
                    command.Parameters.Add("@DepartureDate", MySqlDbType.DateTime).Value = instance.DepartureDate;
                    command.Parameters.Add("@ArrivalDate", MySqlDbType.DateTime).Value = instance.ArrivalDate;
                    command.Parameters.Add("@BlindTypeID", MySqlDbType.Int32).Value = instance.BlindTypeID;
                    command.Parameters.Add("@Transport", MySqlDbType.VarString).Value = instance.Transport;
                    command.Parameters.Add("@OrderM2", MySqlDbType.Double).Value = instance.OrderM2;
                    command.Parameters.Add("@Notes", MySqlDbType.VarString).Value = instance.Notes;
                    command.Parameters.Add("@IsApproved", MySqlDbType.Bit).Value = instance.IsApproved;

                    int RetVal = Convert.ToInt32(command.ExecuteNonQuery());

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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " +
                    "WHERE OrderID IN (@OrderIDs)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", MySqlDbType.VarString).Value = string.Join(",", IDs.ToArray());
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {
                                OrderID = reader.SafeGetInt32("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt32("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt32("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

        public List<Order> SelectedRole(int RoleID, out Exception exError)
        {
            List<Order> returnValue = new List<Order>();
            exError = null;

            try
            {
                string strFilterBy = "";
                switch (RoleID)
                {
                    case 1:
                        strFilterBy = " WHERE `order`.OrderStatusID IN (1,2) ";
                        break;
                    case 2:
                        strFilterBy = " WHERE `order`.OrderStatusID = 3";
                        break;
                    case 3:
                        strFilterBy = " WHERE `order`.OrderStatusID = 4";
                        break;
                    default:
                        break;
                }
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " +
                    " " + strFilterBy , (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {
                                OrderID = reader.SafeGetInt32("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt32("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt32("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " + 
                    "WHERE CustomerID = @CustomerID ", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = ID;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {

                                OrderID = reader.SafeGetInt32("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt32("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt32("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

                string query = "UPDATE `order` SET " +
                    "IsApproved = 1 " +
                    "WHERE OrderID="+OrderIDs[0];

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    //command.Parameters.Add("@OrderIDs", MySqlDbType.Int32).Value = OrderIDs[0];

                    int RetVal = command.ExecuteNonQuery();

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

        public bool ChangeOrderStatus(List<int> OrderIDs,int StatusID, out Exception exError)
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

                string query = "UPDATE `order` SET " +
                    " OrderStatusID = " +StatusID +
                    " WHERE OrderID="+OrderIDs[0];

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", MySqlDbType.VarString).Value = OrderIDs;
                    command.Parameters.Add("@OrderStatusID", MySqlDbType.Int32).Value = StatusID;


                    int RetVal =command.ExecuteNonQuery();

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
                        strFilterBy = " WHERE OrderTypeDesc = '" + SearchCriteria + "' ";
                        break;
                    case "orderstatus":
                        strFilterBy = " WHERE OrderStatusDesc = '" + SearchCriteria + "' ";
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
                    strOrderBy = " Order By " + OrderBy;
                }

                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " +
                    " " + strFilterBy + " " + strOrderBy, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Order()
                            {

                                OrderID = reader.SafeGetInt32("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt32("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt32("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt32("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

                string query = "INSERT INTO orderdetail (OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize) " +
                           "VALUES (@OrderID, @Width,@Height, @SplPelmetWidth, @WidthMadeBy, @HeightMadeBy, @QualityCheckedBy,@SlatStyleID,@CordStyleID,@ReturnRequired,@MountType,@SquareMeter,@ControlID,@ControlStyle,@OpeningStyle,@PelmetStyle,@ColorID,@MaterialID,@Roll,@ReadyMadeSize); " +
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
                    command.Parameters.Add("@MountType", MySqlDbType.VarString).Value = instance.MountType;
                    command.Parameters.Add("@SquareMeter", MySqlDbType.Double).Value = instance.SquareMeter;
                    command.Parameters.Add("@ControlID", MySqlDbType.Int32).Value = instance.ControlID;
                    command.Parameters.Add("@ControlStyle", MySqlDbType.VarString).Value = instance.ControlStyle;
                    command.Parameters.Add("@OpeningStyle", MySqlDbType.VarString).Value = instance.OpeningStyle;
                    command.Parameters.Add("@PelmetStyle", MySqlDbType.VarString).Value = instance.PelmetStyle;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@Roll", MySqlDbType.VarString).Value = instance.Roll;
                    command.Parameters.Add("@ReadyMadeSize", MySqlDbType.Double).Value = instance.ReadyMadeSize;

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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM orderdetail WHERE OrderDetailID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,OrderDetail.SlatStyleID,SlatStyledesc,OrderDetail.CordStyleID,CordStyleDesc,ReturnRequired,MountType,SquareMeter,OrderDetail.ControlID,ControlDesc,ControlStyle,OpeningStyle,PelmetStyle,OrderDetail.ColorID,ColorsDesc,OrderDetail.MaterialID,MaterialDesc,Roll,ReadyMadeSize FROM orderdetail " +
                                        "LEFT JOIN slatstyle ON slatstyle.SlatStyleID = OrderDetail.SlatStyleID " +
                                        "LEFT JOIN cordstyle ON cordstyle.CordStyleID = OrderDetail.CordStyleID " +
                                        "LEFT JOIN control ON control.ControlID = OrderDetail.ControlID " +
                                        "LEFT JOIN colors ON colors.ColorsID = OrderDetail.ColorID " +
                                        "LEFT JOIN material ON material.MaterialID = OrderDetail.MaterialID " +
                                        "Where OrderDetailID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new OrderDetail()
                            {
                                OrderID = reader.SafeGetInt32("OrderID"),
                                Width = reader.SafeGetDouble("Width"),
                                Height = reader.SafeGetDouble("Height"),
                                SplPelmetWidth = reader.SafeGetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.SafeGetInt32("SlatStyleID"),
                                CordStyleID = reader.SafeGetInt32("CordStyleID"),
                                ReturnRequired = reader.SafeGetBoolean("ReturnRequired"),
                                MountType = reader.SafeGetString("MountType"),
                                SquareMeter = reader.SafeGetDouble("SquareMeter"),
                                ControlID = reader.SafeGetInt32("ControlID"),
                                ControlStyle = reader.SafeGetString("ControlStyle"),
                                OpeningStyle = reader.SafeGetString("OpeningStyle"),
                                PelmetStyle = reader.SafeGetString("PelmetStyle"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                Roll = reader.SafeGetString("Roll"),
                                ReadyMadeSize = reader.SafeGetDouble("ReadyMadeSize"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                ControlName = reader.SafeGetString("ControlDesc"),
                                CordStyleName = reader.SafeGetString("CordStyleDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SlatStyleName = reader.SafeGetString("SlatStyleDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,OrderDetail.SlatStyleID,SlatStyledesc,OrderDetail.CordStyleID,CordStyleDesc,ReturnRequired,MountType,SquareMeter,OrderDetail.ControlID,ControlDesc,ControlStyle,OpeningStyle,PelmetStyle,OrderDetail.ColorID,ColorsDesc,OrderDetail.MaterialID,MaterialDesc,Roll,ReadyMadeSize FROM orderdetail " +
                                        "LEFT JOIN slatstyle ON slatstyle.SlatStyleID = OrderDetail.SlatStyleID " +
                                        "LEFT JOIN cordstyle ON cordstyle.CordStyleID = OrderDetail.CordStyleID " +
                                        "LEFT JOIN control ON control.ControlID = OrderDetail.ControlID " +
                                        "LEFT JOIN colors ON colors.ColorsID = OrderDetail.ColorID " +
                                        "LEFT JOIN material ON material.MaterialID = OrderDetail.MaterialID " , (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderDetail()
                            {
                                OrderID = reader.SafeGetInt32("OrderID"),
                                Width = reader.SafeGetDouble("Width"),
                                Height = reader.SafeGetDouble("Height"),
                                SplPelmetWidth = reader.SafeGetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.SafeGetInt32("SlatStyleID"),
                                CordStyleID = reader.SafeGetInt32("CordStyleID"),
                                ReturnRequired = reader.SafeGetBoolean("ReturnRequired"),
                                MountType = reader.SafeGetString("MountType"),
                                SquareMeter = reader.SafeGetDouble("SquareMeter"),
                                ControlID = reader.SafeGetInt32("ControlID"),
                                ControlStyle = reader.SafeGetString("ControlStyle"),
                                OpeningStyle = reader.SafeGetString("OpeningStyle"),
                                PelmetStyle = reader.SafeGetString("PelmetStyle"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                Roll = reader.SafeGetString("Roll"),
                                ReadyMadeSize = reader.SafeGetDouble("ReadyMadeSize"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                ControlName = reader.SafeGetString("ControlDesc"),
                                CordStyleName = reader.SafeGetString("CordStyleDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SlatStyleName = reader.SafeGetString("SlatStyleDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,OrderDetail.SlatStyleID,SlatStyledesc,OrderDetail.CordStyleID,CordStyleDesc,ReturnRequired,MountType,SquareMeter,OrderDetail.ControlID,ControlDesc,ControlStyle,OpeningStyle,PelmetStyle,OrderDetail.ColorID,ColorsDesc,OrderDetail.MaterialID,MaterialDesc,Roll,ReadyMadeSize FROM orderdetail " +
                                        "LEFT JOIN slatstyle ON slatstyle.SlatStyleID = OrderDetail.SlatStyleID " +
                                        "LEFT JOIN cordstyle ON cordstyle.CordStyleID = OrderDetail.CordStyleID " +
                                        "LEFT JOIN control ON control.ControlID = OrderDetail.ControlID " +
                                        "LEFT JOIN colors ON colors.ColorsID = OrderDetail.ColorID " +
                                        "LEFT JOIN material ON material.MaterialID = OrderDetail.MaterialID " + 
                                        "WHERE OrderDetailID IN (@OrderDetailID)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderDetailIDs", MySqlDbType.VarString).Value = string.Join(",", IDs.ToArray());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderDetail()
                            {
                                OrderID = reader.SafeGetInt32("OrderID"),
                                Width = reader.SafeGetDouble("Width"),
                                Height = reader.SafeGetDouble("Height"),
                                SplPelmetWidth = reader.SafeGetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.SafeGetInt32("SlatStyleID"),
                                CordStyleID = reader.SafeGetInt32("CordStyleID"),
                                ReturnRequired = reader.SafeGetBoolean("ReturnRequired"),
                                MountType = reader.SafeGetString("MountType"),
                                SquareMeter = reader.SafeGetDouble("SquareMeter"),
                                ControlID = reader.SafeGetInt32("ControlID"),
                                ControlStyle = reader.SafeGetString("ControlStyle"),
                                OpeningStyle = reader.SafeGetString("OpeningStyle"),
                                PelmetStyle = reader.SafeGetString("PelmetStyle"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                Roll = reader.SafeGetString("Roll"),
                                ReadyMadeSize = reader.SafeGetDouble("ReadyMadeSize"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                ControlName = reader.SafeGetString("ControlDesc"),
                                CordStyleName = reader.SafeGetString("CordStyleDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SlatStyleName = reader.SafeGetString("SlatStyleDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,OrderDetail.SlatStyleID,SlatStyledesc,OrderDetail.CordStyleID,CordStyleDesc,ReturnRequired,MountType,SquareMeter,OrderDetail.ControlID,ControlDesc,ControlStyle,OpeningStyle,PelmetStyle,OrderDetail.ColorID,ColorsDesc,OrderDetail.MaterialID,MaterialDesc,Roll,ReadyMadeSize FROM orderdetail " +
                                        "LEFT JOIN slatstyle ON slatstyle.SlatStyleID = OrderDetail.SlatStyleID " +
                                        "LEFT JOIN cordstyle ON cordstyle.CordStyleID = OrderDetail.CordStyleID " +
                                        "LEFT JOIN control ON control.ControlID = OrderDetail.ControlID " +
                                        "LEFT JOIN colors ON colors.ColorsID = OrderDetail.ColorID " +
                                        "LEFT JOIN material ON material.MaterialID = OrderDetail.MaterialID " +
                                        "WHERE OrderID IN (@OrderIDs)", (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", MySqlDbType.VarString).Value = string.Join(",", IDs.ToArray());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderDetail()
                            {
                                OrderID = reader.SafeGetInt32("OrderID"),
                                Width = reader.SafeGetDouble("Width"),
                                Height = reader.SafeGetDouble("Height"),
                                SplPelmetWidth = reader.SafeGetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.SafeGetInt32("SlatStyleID"),
                                CordStyleID = reader.SafeGetInt32("CordStyleID"),
                                ReturnRequired = reader.SafeGetBoolean("ReturnRequired"),
                                MountType = reader.SafeGetString("MountType"),
                                SquareMeter = reader.SafeGetDouble("SquareMeter"),
                                ControlID = reader.SafeGetInt32("ControlID"),
                                ControlStyle = reader.SafeGetString("ControlStyle"),
                                OpeningStyle = reader.SafeGetString("OpeningStyle"),
                                PelmetStyle = reader.SafeGetString("PelmetStyle"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                Roll = reader.SafeGetString("Roll"),
                                ReadyMadeSize = reader.SafeGetDouble("ReadyMadeSize"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                ControlName = reader.SafeGetString("ControlDesc"),
                                CordStyleName = reader.SafeGetString("CordStyleDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SlatStyleName = reader.SafeGetString("SlatStyleDesc")
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

                string query = "UPDATE orderdetail SET " +
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
                    command.Parameters.Add("@MountType", MySqlDbType.VarString).Value = instance.MountType;
                    command.Parameters.Add("@SquareMeter", MySqlDbType.Double).Value = instance.SquareMeter;
                    command.Parameters.Add("@ControlID", MySqlDbType.Int32).Value = instance.ControlID;
                    command.Parameters.Add("@ControlStyle", MySqlDbType.VarString).Value = instance.ControlStyle;
                    command.Parameters.Add("@OpeningStyle", MySqlDbType.VarString).Value = instance.OpeningStyle;
                    command.Parameters.Add("@PelmetStyle", MySqlDbType.VarString).Value = instance.PelmetStyle;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@Roll", MySqlDbType.VarString).Value = instance.Roll;
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

                string query = "INSERT INTO utilityorder (CustomerID, OrderTypeID, OrderDate, CompleteDate) " +
                           "VALUES (@CustomerID, @OrderTypeID, @OrderDate, @CompleteDate); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@OrderTypeID", MySqlDbType.Int32).Value = instance.OrderTypeID;
                    command.Parameters.Add("@OrderDate", MySqlDbType.DateTime).Value = instance.OrderDate;
                    command.Parameters.Add("@CompleteDate", MySqlDbType.DateTime).Value = instance.CompleteDate;

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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM utilityorder WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, `utilityorder`.OrderTypeID, OrderTypeDesc,  OrderDate, CompleteDate, IsApproved FROM utilityorder " +
                                                            "LEFT JOIN ordertype ON ordertype.OrderTypeID = `utilityorder`.OrderTypeID " +
                                                            "Where UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new UtilityOrder()
                            {
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, `utilityorder`.OrderTypeID, OrderTypeDesc,  OrderDate, CompleteDate, IsApproved FROM utilityorder " +
                                                                "LEFT JOIN ordertype ON ordertype.OrderTypeID = `utilityorder`.OrderTypeID " , (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new UtilityOrder()
                            {
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, `utilityorder`.OrderTypeID, OrderTypeDesc,  OrderDate, CompleteDate, IsApproved FROM utilityorder " +
                                                                "LEFT JOIN ordertype ON ordertype.OrderTypeID = `utilityorder`.OrderTypeID " +
                                                                "WHERE CustomerID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new UtilityOrder()
                            {
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

        public List<UtilityOrder> SelectedRole(int RoleID, out Exception exError)
        {
            List<UtilityOrder> returnValue = new List<UtilityOrder>();
            exError = null;

            try
            {
                string strFilterBy = "";
                switch (RoleID)
                {
                    case 1:
                        strFilterBy = " WHERE OrderStatusID IN (1,2,3) ";
                        break;
                    case 2:
                        strFilterBy = " WHERE OrderStatusID = 3";
                        break;
                    case 3:
                        strFilterBy = " WHERE OrderStatusID = 4";
                        break;
                    default:
                        break;
                }
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, `utilityorder`.OrderTypeID, OrderTypeDesc,  OrderDate, CompleteDate, IsApproved FROM utilityorder" +
                                                                "LEFT JOIN ordertype ON ordertype.OrderTypeID = `utilityorder`.OrderTypeID " +
                                                                " " + strFilterBy, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new UtilityOrder()
                            {
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

                string query = "UPDATE utilityorder SET " +
                    //"CustomerID = @CustomerID, " +
                    "OrderTypeID = @OrderTypeID," +
                    "OrderDate = @OrderDate, " +
                    "CompleteDate = @CompleteDate, " +
                    "IsApproved = @IsApproved " +
                    "WHERE UtilityOrderID = @UtilityOrderID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    //command.Parameters.Add("@CustomerID", MySqlDbType.Int32).Value = instance.CustomerID;
                    command.Parameters.Add("@OrderTypeID", MySqlDbType.Int32).Value = instance.OrderTypeID;
                    command.Parameters.Add("@OrderDate", MySqlDbType.DateTime).Value = instance.OrderDate;
                    command.Parameters.Add("@CompleteDate", MySqlDbType.DateTime).Value = instance.CompleteDate;
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

                string query = "UPDATE `utilityorder` SET " +
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

        public bool ChangeUtilityOrderStatus(List<int> OrderIDs, int StatusID, out Exception exError)
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

                string query = "UPDATE `utilityorder` SET " +
                    "OrderStatusID = @OrderStatusID " +
                    "WHERE UtilityOrder IN (@OrderIDs)";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", MySqlDbType.VarString).Value = OrderIDs;
                    command.Parameters.Add("@OrderStatusID", MySqlDbType.Int32).Value = StatusID;


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
                    strOrderBy = " Order By " + OrderBy;
                }

                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, CustomerID, `utilityorder`.OrderTypeID, OrderTypeDesc,  OrderDate, CompleteDate, IsApproved FROM utilityorder" +
                                                                "LEFT JOIN ordertype ON ordertype.OrderTypeID = `utilityorder`.OrderTypeID " +
                                                                " " + strFilterBy + " " + strOrderBy, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new UtilityOrder()
                            {
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                CustomerID = reader.SafeGetInt32("CustomerID"),
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
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

                string query = "INSERT INTO fabric (UtilityOrderID, FabricType, ColorID, SizeID, Boxes) " +
                           "VALUES (@UtilityOrderID, @FabricType, @ColorID, @SizeID, @Boxes); " +
                            "SELECT LAST_INSERT_ID();";


                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.Int32).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@FabricType", MySqlDbType.VarString).Value = instance.FabricType;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@SizeID", MySqlDbType.Int32).Value = instance.SizeID;
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM fabric WHERE FabricID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, FabricID, FabricType, fabric.ColorID, ColorsDesc, fabric.SizeID, SizeDesc, Boxes FROM fabric " +
                                                                "LEFT JOIN size ON size.SizeID = fabric.SizeID " +
                                                                "LEFT JOIN colors ON colors.ColorsID = fabric.ColorID " +
                                                                "Where FabricID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Fabric()
                            {
                                FabricID = reader.SafeGetInt32("FabricID"),
                                FabricType = reader.SafeGetString("FabricType"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                Boxes = reader.SafeGetInt32("Boxes"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                SizeID = reader.SafeGetInt32("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                SizeValue = reader.SafeGetString("SizeDesc"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, FabricID, FabricType, fabric.ColorID, ColorsDesc, fabric.SizeID, SizeDesc, Boxes FROM fabric " +
                                                "LEFT JOIN size ON size.SizeID = fabric.SizeID " +
                                                "LEFT JOIN colors ON colors.ColorsID = fabric.ColorID " , (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Fabric()
                            {
                                FabricID = reader.SafeGetInt32("FabricID"),
                                FabricType = reader.SafeGetString("FabricType"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                Boxes = reader.SafeGetInt32("Boxes"),
                                SizeID = reader.SafeGetInt32("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                SizeValue = reader.SafeGetString("SizeDesc"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT UtilityOrderID, FabricID, FabricType, fabric.ColorID, ColorsDesc, fabric.SizeID, SizeDesc, Boxes FROM fabric " +
                                 "LEFT JOIN size ON size.SizeID = fabric.SizeID " +
                                 "LEFT JOIN colors ON colors.ColorsID = fabric.ColorID " +
                                 "WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Fabric()
                            {
                                FabricID = reader.SafeGetInt32("FabricID"),
                                FabricType = reader.SafeGetString("FabricType"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                Boxes = reader.SafeGetInt32("Boxes"),
                                SizeID = reader.SafeGetInt32("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                SizeValue = reader.SafeGetString("SizeDesc"),
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

                string query = "UPDATE fabric SET " +
                    "FabricType = @FabricType, " +
                    "ColorID = @ColorID," +
                    "Boxes = @Boxes," +
                    "SizeID = @SizeID " +
                    "WHERE FabricID = @FabricID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FabricType", MySqlDbType.VarString).Value = instance.FabricType;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@SizeID", MySqlDbType.Int32).Value = instance.SizeID;
                    command.Parameters.Add("@FabricID", MySqlDbType.Int32).Value = instance.FabricID;
                    command.Parameters.Add("@Boxes", MySqlDbType.Int32).Value = instance.Boxes;

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

                string query = "INSERT INTO rollerblindtype (Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM rollerblindtype WHERE RollerBlindsID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype Where RollerBlindTypeID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new RollerBlindType()
                            {
                                RollerBlindTypeID = reader.SafeGetInt32("RollerBlindTypeID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new RollerBlindType()
                            {
                                RollerBlindTypeID = reader.SafeGetInt32("RollerBlindTypeID"),
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

                string query = "UPDATE rollerblindtype SET " +
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

                string query = "INSERT INTO rollerblinds (UtilityOrderID, RollerBlindTypeID, Boxes) " +
                           "VALUES (@UtilityOrderID, @RollerBlindTypeID, @Boxes); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@RollerBlindTypeID", MySqlDbType.VarString).Value = instance.RollerBlindTypeID;
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM rollerblinds WHERE RollerBlindsID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindsID, UtilityOrderID, RollerBlindTypeID, Boxes FROM rollerblinds Where RollerBlindsID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new RollerBlinds()
                            {
                                RollerBlindsID = reader.SafeGetInt32("RollerBlindsID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                RollerBlindTypeID = reader.SafeGetInt32("RollerBlindTypeID"),
                                Boxes = reader.SafeGetInt32("Boxes")
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

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindsID, UtilityOrderID, RollerBlindTypeID, Boxes FROM rollerblinds", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new RollerBlinds()
                            {
                                RollerBlindsID = reader.SafeGetInt32("RollerBlindsID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                RollerBlindTypeID = reader.SafeGetInt32("RollerBlindTypeID"),
                                Boxes = reader.SafeGetInt32("Boxes")
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

                using (MySqlCommand command = new MySqlCommand("SELECT RollerBlindsID, UtilityOrderID, RollerBlindTypeID, Boxes FROM rollerblinds WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new RollerBlinds()
                            {
                                RollerBlindsID = reader.SafeGetInt32("RollerBlindsID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                RollerBlindTypeID = reader.SafeGetInt32("RollerBlindTypeID"),
                                Boxes = reader.SafeGetInt32("Boxes")
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

                string query = "UPDATE rollerblinds SET " +
                    "UtilityOrderID = @UtilityOrderID, " +
                    "RollerBlindTypeID = @RollerBlindTypeID " +
                    "Boxes = @Boxes " +
                    "WHERE RollerBlindsID = @RollerBlindsID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@RollerBlindTypeID", MySqlDbType.VarString).Value = instance.RollerBlindTypeID;
                    command.Parameters.Add("@RollerBlindsID", MySqlDbType.Int32).Value = instance.RollerBlindsID;
                    command.Parameters.Add("@Boxes", MySqlDbType.Int32).Value = instance.Boxes;

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

                string query = "INSERT INTO valance (MaterialID, ColorID, SizeID, UtilityOrderID, Boxes) " +
                           "VALUES (@MaterialID, @ColorID, @SizeID, @UtilityOrderID, @Boxes); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@SizeID", MySqlDbType.Int32).Value = instance.SizeID;
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@Boxes", MySqlDbType.VarString).Value = instance.Boxes;

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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM valance WHERE ValanceID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT ValanceID, valance.MaterialID, valance.ColorID, valance.SizeID, UtilityOrderID, Boxes, ColorsDesc,MaterialDesc,SizeDesc FROM valance" +
                    "LEFT JOIN size ON size.SizeID = valance.SizeID " +
                    "LEFT JOIN material ON size.MaterialID = valance.MaterialID " +
                    "LEFT JOIN colors ON colors.ColorsID = valance.ColorID " +
                    "Where ValanceID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Valance()
                            {
                                ValanceID = reader.SafeGetInt32("ValanceID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                Boxes = reader.SafeGetInt32("Boxes"),
                                SizeID = reader.SafeGetInt32("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SizeVal = reader.SafeGetString("SizeDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT ValanceID, valance.MaterialID, valance.ColorID, valance.SizeID, UtilityOrderID, Boxes, ColorsDesc,MaterialDesc,SizeDesc FROM valance" +
                    "LEFT JOIN size ON size.SizeID = valance.SizeID " +
                    "LEFT JOIN material ON size.MaterialID = valance.MaterialID " +
                    "LEFT JOIN colors ON colors.ColorsID = valance.ColorID " , (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Valance()
                            {
                                ValanceID = reader.SafeGetInt32("ValanceID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                Boxes = reader.SafeGetInt32("Boxes"),
                                SizeID = reader.SafeGetInt32("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SizeVal = reader.SafeGetString("SizeDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT ValanceID, valance.MaterialID, valance.ColorID, valance.SizeID, UtilityOrderID, Boxes, ColorsDesc,MaterialDesc,SizeDesc FROM valance" +
                    "LEFT JOIN size ON size.SizeID = valance.SizeID " +
                    "LEFT JOIN material ON size.MaterialID = valance.MaterialID " +
                    "LEFT JOIN colors ON colors.ColorsID = valance.ColorID " + 
                    "WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Valance()
                            {
                                ValanceID = reader.SafeGetInt32("ValanceID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                Boxes = reader.SafeGetInt32("Boxes"),
                                SizeID = reader.SafeGetInt32("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SizeVal = reader.SafeGetString("SizeDesc")
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

                string query = "UPDATE valance SET " +
                    "MaterialID = @MaterialID, " +
                    "ColorID = @ColorID," +
                    "UtilityOrderID = @UtilityOrderID," +
                    "Boxes = @Boxes," +
                    "SizeID = @SizeID " +
                    "WHERE ValanceID = @ValanceID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@SizeID", MySqlDbType.VarString).Value = instance.SizeID;
                    command.Parameters.Add("@ValanceID", MySqlDbType.Int32).Value = instance.ValanceID;
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@Boxes", MySqlDbType.VarString).Value = instance.Boxes;

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

                string query = "INSERT INTO bottomrail (MaterialID, ColorID, SizeID, UtilityOrderID, Boxes) " +
                           "VALUES (@MaterialID, @ColorID, @SizeID, @UtilityOrderID, @Boxes); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@SizeID", MySqlDbType.Int32).Value = instance.SizeID;
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@Boxes", MySqlDbType.VarString).Value = instance.Boxes;

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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM bottomrail WHERE BottomRailID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT BottomRailID, MaterialID, ColorID, SizeID, UtilityOrderID, Boxes, ColorsDesc, MaterialDesc, SizeDesc FROM bottomrail " +
                                                                "LEFT JOIN size ON size.SizeID = bottomrail.SizeID " +
                                                                "LEFT JOIN material ON size.MaterialID = bottomrail.MaterialID " +
                                                                "LEFT JOIN colors ON colors.ColorsID = bottomrail.ColorID " +
                                                                " Where BottomRailID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new BottomRail()
                            {
                                BottomRailID = reader.SafeGetInt32("BottomRailID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                Boxes = reader.SafeGetInt32("Boxes"),
                                SizeID = reader.SafeGetInt32("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SizeVal = reader.SafeGetString("SizeDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT BottomRailID, MaterialID, ColorID, SizeID, UtilityOrderID, Boxes, ColorsDesc, MaterialDesc, SizeDesc FROM bottomrail " +
                                                "LEFT JOIN size ON size.SizeID = bottomrail.SizeID " +
                                                "LEFT JOIN material ON size.MaterialID = bottomrail.MaterialID " +
                                                "LEFT JOIN colors ON colors.ColorsID = bottomrail.ColorID " , (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new BottomRail()
                            {
                                BottomRailID = reader.SafeGetInt32("BottomRailID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                Boxes = reader.SafeGetInt32("Boxes"),
                                SizeID = reader.SafeGetInt32("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SizeVal = reader.SafeGetString("SizeDesc")
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

                using (MySqlCommand command = new MySqlCommand("SELECT BottomRailID, MaterialID, ColorID, SizeID, UtilityOrderID, Boxes, ColorsDesc, MaterialDesc, SizeDesc FROM bottomrail " +
                                                "LEFT JOIN size ON size.SizeID = bottomrail.SizeID " +
                                                "LEFT JOIN material ON size.MaterialID = bottomrail.MaterialID " +
                                                "LEFT JOIN colors ON colors.ColorsID = bottomrail.ColorID " +
                                                "WHERE UtilityOrderID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new BottomRail()
                            {
                                BottomRailID = reader.SafeGetInt32("BottomRailID"),
                                MaterialID = reader.SafeGetInt32("MaterialID"),
                                ColorID = reader.SafeGetInt32("ColorID"),
                                UtilityOrderID = reader.SafeGetInt32("UtilityOrderID"),
                                Boxes = reader.SafeGetInt32("Boxes"),
                                SizeID = reader.SafeGetInt32("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SizeVal = reader.SafeGetString("SizeDesc")
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

                string query = "UPDATE bottomrail SET " +
                    "MaterialID = @MaterialID, " +
                    "ColorID = @ColorID," +
                    "UtilityOrderID = @UtilityOrderID," +
                    "Boxes = @Boxes," +
                    "SizeID = @SizeID " +
                    "WHERE BottomRailID = @BottomRailID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@MaterialID", MySqlDbType.Int32).Value = instance.MaterialID;
                    command.Parameters.Add("@ColorID", MySqlDbType.Int32).Value = instance.ColorID;
                    command.Parameters.Add("@SizeID", MySqlDbType.Int32).Value = instance.SizeID;
                    command.Parameters.Add("@BottomRailID", MySqlDbType.Int32).Value = instance.BottomRailID;
                    command.Parameters.Add("@UtilityOrderID", MySqlDbType.VarString).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@Boxes", MySqlDbType.VarString).Value = instance.Boxes;

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

                string query = "INSERT INTO slatstyle (SlatStyleDesc,For) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM slatstyle WHERE SlatStyleID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT SlatStyleID, SlatStyleDesc, `For` FROM slatstyle Where SlatStyleID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new SlatStyle()
                            {
                                SlatStyleID = reader.SafeGetInt32("SlatStyleID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT SlatStyleID, SlatStyleDesc, `For` FROM slatstyle", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new SlatStyle()
                            {
                                SlatStyleID = reader.SafeGetInt32("SlatStyleID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT SlatStyleID, SlatStyleDesc, `For` FROM slatstyle WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new SlatStyle()
                            {
                                SlatStyleID = reader.SafeGetInt32("SlatStyleID"),
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

                string query = "UPDATE slatstyle SET " +
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

                string query = "INSERT INTO cordstyle (CordStyleDesc, `For`) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM cordstyle WHERE CordStyleID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT CordStyleID, CordStyleDesc, `For` FROM cordstyle Where CordStyleID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new CordStyle()
                            {
                                CordStyleID = reader.SafeGetInt32("CordStyleID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT CordStyleID, CordStyleDesc, `For` FROM cordstyle", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new CordStyle()
                            {
                                CordStyleID = reader.SafeGetInt32("CordStyleID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT CordStyleID, CordStyleDesc, `For` FROM cordstyle WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new CordStyle()
                            {
                                CordStyleID = reader.SafeGetInt32("CordStyleID"),
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

                string query = "UPDATE cordstyle SET " +
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

                string query = "INSERT INTO control (ControlDesc, `For`) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM control WHERE ControlID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT ControlID, ControlDesc, `For` FROM control Where ControlID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Control()
                            {
                                ControlID = reader.SafeGetInt32("ControlID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT ControlID, ControlDesc, `For` FROM control", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Control()
                            {
                                ControlID = reader.SafeGetInt32("ControlID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT ControlID, ControlDesc, `For` FROM control WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Control()
                            {
                                ControlID = reader.SafeGetInt32("ControlID"),
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

                string query = "UPDATE control SET " +
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

                string query = "INSERT INTO material (MaterialDesc, `For`) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM material WHERE MaterialID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT MaterialID, MaterialDesc, `For` FROM material Where MaterialID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Material()
                            {
                                MaterialID = reader.SafeGetInt32("MaterialID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT MaterialID, MaterialDesc, `For` FROM material", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Material()
                            {
                                MaterialID = reader.SafeGetInt32("MaterialID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT MaterialID, MaterialDesc, `For` FROM material WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Material()
                            {
                                MaterialID = reader.SafeGetInt32("MaterialID"),
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

                string query = "UPDATE material SET " +
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

                string query = "INSERT INTO colors (ColorsDesc, `For`) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM colors WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT ColorsID, ColorsDesc, `For` FROM colors Where ColorsID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Colors()
                            {
                                ColorsID = reader.SafeGetInt32("ColorsID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT ColorsID, ColorsDesc, `For` FROM colors", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Colors()
                            {
                                ColorsID = reader.SafeGetInt32("ColorsID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT ColorsID, ColorsDesc, `For` FROM colors WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Colors()
                            {
                                ColorsID = reader.SafeGetInt32("ColorsID"),
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

                string query = "UPDATE colors SET " +
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

                string query = "INSERT INTO size (SizeDesc, `For`) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM size WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT SizeID, SizeDesc, `For` FROM size Where SizeID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Size()
                            {
                                SizeID = reader.SafeGetInt32("SizeID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT SizeID, SizeDesc, `For` FROM size", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Size()
                            {
                                SizeID = reader.SafeGetInt32("SizeID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT SizeID, SizeDesc, `For` FROM size WHERE `For` = '" + For + "'", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Size()
                            {
                                SizeID = reader.SafeGetInt32("SizeID"),
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

                string query = "UPDATE size SET " +
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

                string query = "INSERT INTO blindtype (BlindTypeDesc, Val) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM blindtype WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT BlindTypeID, BlindTypeDesc, Val FROM blindtype Where BlindTypeID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new BlindType()
                            {
                                BlindTypeID = reader.SafeGetInt32("BlindTypeID"),
                                BlindTypeDesc = reader.SafeGetString("BlindTypeDesc"),
                                Val = reader.SafeGetInt32("Val")
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

                using (MySqlCommand command = new MySqlCommand("SELECT BlindTypeID, BlindTypeDesc, Val FROM blindtype", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new BlindType()
                            {
                                BlindTypeID = reader.SafeGetInt32("BlindTypeID"),
                                BlindTypeDesc = reader.SafeGetString("BlindTypeDesc"),
                                Val = reader.SafeGetInt32("Val")
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

                string query = "UPDATE blindtype SET " +
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

                string query = "INSERT INTO ordertype (OrderTypeDesc) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM ordertype WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderTypeID, OrderTypeDesc FROM ordertype Where OrderTypeID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new OrderType()
                            {
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderTypeID, OrderTypeDesc FROM ordertype", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderType()
                            {
                                OrderTypeID = reader.SafeGetInt32("OrderTypeID"),
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

                string query = "UPDATE ordertype SET " +
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

                string query = "INSERT INTO orderstatus (OrderStatusDesc) " +
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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM orderstatus WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderStatusID, OrderStatusDesc FROM orderstatus Where OrderStatusID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new OrderStatus()
                            {
                                OrderStatusID = reader.SafeGetInt32("OrderStatusID"),
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

                using (MySqlCommand command = new MySqlCommand("SELECT OrderStatusID, OrderStatusDesc FROM orderstatus", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new OrderStatus()
                            {
                                OrderStatusID = reader.SafeGetInt32("OrderStatusID"),
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

                string query = "UPDATE orderstatus SET " +
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
    public class EliteRoles_Mapper : IDataMapper<EliteRoles>
    {
        public override EliteRoles Create(EliteRoles instance, out Exception exError)
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

                string query = "INSERT INTO eliteroles (EliteRolesDesc) " +
                           "VALUES (@EliteRolesDesc); " +
                            "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@EliteRolesDesc", MySqlDbType.VarString).Value = instance.EliteRolesDesc;

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

                using (MySqlCommand command = new MySqlCommand("DELETE FROM eliteroles WHERE ColorID = " + ID, (MySqlConnection)this.Connection))
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

        public override EliteRoles Select(int ID, out Exception exError)
        {
            EliteRoles returnValue = new EliteRoles();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT EliteRolesID, EliteRolesDesc FROM eliteroles Where EliteRolesID = " + ID, (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new EliteRoles()
                            {
                                EliteRolesID = reader.SafeGetInt32("EliteRolesID"),
                                EliteRolesDesc = reader.SafeGetString("EliteRolesDesc")
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

        public override List<EliteRoles> SelectAll(out Exception exError)
        {
            List<EliteRoles> returnValue = new List<EliteRoles>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT EliteRolesID, EliteRolesDesc FROM eliteroles", (MySqlConnection)this.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new EliteRoles()
                            {
                                EliteRolesID = reader.SafeGetInt32("EliteRolesID"),
                                EliteRolesDesc = reader.SafeGetString("EliteRolesDesc")
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

        public override EliteRoles Update(EliteRoles instance, out Exception exError)
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

                string query = "UPDATE eliteroles SET " +
                    "EliteRolesDesc = @EliteRolesDesc" +
                    "WHERE EliteRolesID = @EliteRolesID";

                using (MySqlCommand command = new MySqlCommand(query, (MySqlConnection)this.Connection))
                {
                    command.Parameters.Add("@EliteRolesDesc", MySqlDbType.VarString).Value = instance.EliteRolesDesc;
                    command.Parameters.Add("@EliteRolesID", MySqlDbType.Int32).Value = instance.EliteRolesID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.EliteRolesID, out custException);
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
    public static class ExtMethods
    {
        public static string SafeGetString(this MySqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    
        public static int SafeGetInt32(this MySqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetInt32(colIndex);
            return 0;
        }

        public static Double SafeGetDouble(this MySqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetDouble(colIndex);
            return 0;
        }

        public static DateTime SafeGetDateTime(this MySqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetDateTime(colIndex);
            return DateTime.MinValue;
        }

        public static Boolean SafeGetBoolean(this MySqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetBoolean(colIndex);
            return false;
        }

    }
}
