using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlindsAPI.Models
{
    public class Customer
    {
        private int _CustomerID;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _Email;
        private DateTime? _DOB;
        private bool _Gender;
        private bool _IsActive;
        private string _Mobile;
        private string _Address;
        private string _City;
        private string _Country;
        private string _Pincode;
        private string _Password;
        private string _ConfirmPassword;
        private int _TotalOrders;
        private int _RoleID;
        public List<Order> _Orders;
        
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public DateTime? DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public bool Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string Pincode
        {
            get { return _Pincode; }
            set { _Pincode = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }
        public int TotalOrders
        {
            get { return _TotalOrders; }
            set { _TotalOrders = value; }
        }
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

    }



    public class Order
    {
        private int _OrderID;

        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        private string _OrderNumber;

        public string OrderNumber
        {
            get { return _OrderNumber; }
            set { _OrderNumber = value; }
        }

        private int _CustomerID;

        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        private bool _IsNew;

        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }

        private int _Fault;

        public int Fault
        {
            get { return _Fault; }
            set { _Fault = value; }
        }

        private bool _Evidence;

        public bool Evidence
        {
            get { return _Evidence; }
            set { _Evidence = value; }
        }
        
        private string _Company;

        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }

        private string _Reference;

        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        private int _OrderType;

        public int OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }

        private string _OrderStatus;

        public string OrderStatus
        {
            get { return _OrderStatus; }
            set { _OrderStatus = value; }
        }

        private DateTime _OrderDate;

        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }

        private int _NumbOfBlinds;

        public int NumbOfBlinds
        {
            get { return _NumbOfBlinds; }
            set { _NumbOfBlinds = value; }
        }

        private string _ConsignNoteNum;

        public string ConsignNoteNum
        {
            get { return _ConsignNoteNum; }
            set { _ConsignNoteNum = value; }
        }

        private DateTime? _CompleteDate;

        public DateTime? CompleteDate
        {
            get { return _CompleteDate; }
            set { _CompleteDate = value; }
        }

        private DateTime? _DeliveryDate;

        public DateTime? DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }

        private DateTime? _DepartureDate;

        public DateTime? DepartureDate
        {
            get { return _DepartureDate; }
            set { _DepartureDate = value; }
        }

        private DateTime? _ArrivalDate;

        public DateTime? ArrivalDate
        {
            get { return _ArrivalDate; }
            set { _ArrivalDate = value; }
        }

        private int _BlindTypeID;

        public int BlindTypeID
        {
            get { return _BlindTypeID; }
            set { _BlindTypeID = value; }
        }

        private int _Transport;

        public int Transport
        {
            get { return _Transport; }
            set { _Transport = value; }
        }


        private string _Notes;

        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }

        private double _OrderM2;

        public double OrderM2
        {
            get { return _OrderM2; }
            set { _OrderM2 = value; }
        }

        private bool _IsApproved;

        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
    }

    public class OrderDetail
    {
        private int _OrderDetailID;

        public int OrderDetailID
        {
            get { return _OrderDetailID; }
            set { _OrderDetailID = value; }
        }

        private int _OrderID;

        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        private double _Width;

        public double Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        private double _Height;

        public double Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        private double _SplPelmetWidth;

        public double SplPelmetWidth
        {
            get { return _SplPelmetWidth; }
            set { _SplPelmetWidth = value; }
        }

        private string _WidthMadeBy;

        public string WidthMadeBy
        {
            get { return _WidthMadeBy; }
            set { _WidthMadeBy = value; }
        }

        private string _HeightMadeBy;

        public string HeightMadeBy
        {
            get { return _HeightMadeBy; }
            set { _HeightMadeBy = value; }
        }

        private string _QualityCheckedBy;

        public string QualityCheckedBy
        {
            get { return _QualityCheckedBy; }
            set { _QualityCheckedBy = value; }
        }

        private int _SlatStyleID;

        public int SlatStyleID
        {
            get { return _SlatStyleID; }
            set { _SlatStyleID = value; }
        }

        private int _CordStyleID;

        public int CordStyleID
        {
            get { return _CordStyleID; }
            set { _CordStyleID = value; }
        }

        private bool _ReturnRequired;

        public bool ReturnRequired
        {
            get { return _ReturnRequired; }
            set { _ReturnRequired = value; }
        }

        private bool _MountType;

        public bool MountType
        {
            get { return _MountType; }
            set { _MountType = value; }
        }

        private double _SquareMeter;

        public double SquareMeter
        {
            get { return _SquareMeter; }
            set { _SquareMeter = value; }
        }

        private int _ControlID;

        public int ControlID
        {
            get { return _ControlID; }
            set { _ControlID = value; }
        }

        private int _ControlStyle;

        public int ControlStyle
        {
            get { return _ControlStyle; }
            set { _ControlStyle = value; }
        }

        private int _OpeningStyle;

        public int OpeningStyle
        {
            get { return _OpeningStyle; }
            set { _OpeningStyle = value; }
        }
        
        private int _PelmetStyle;

        public int PelmetStyle
        {
            get { return _PelmetStyle; }
            set { _PelmetStyle = value; }
        }

        private int _ColorID;

        public int ColorID
        {
            get { return _ColorID; }
            set { _ColorID = value; }
        }

        private int _MaterialID;

        public int MaterialID
        {
            get { return _MaterialID; }
            set { _MaterialID = value; }
        }

        private bool _Roll;

        public bool Roll
        {
            get { return _Roll; }
            set { _Roll = value; }
        }

        private double _ReadyMadeSize;

        public double ReadyMadeSize
        {
            get { return _ReadyMadeSize; }
            set { _ReadyMadeSize = value; }
        }

    }

    public class UtilityOrder
    {
        private int _UtilityOrderID;

        public int UtilityOrderID
        {
            get { return _UtilityOrderID; }
            set { _UtilityOrderID = value; }
        }

        private int _UtilityOrderNumber;

        public int UtilityOrderNumber
        {
            get { return _UtilityOrderNumber; }
            set { _UtilityOrderNumber = value; }
        }

        private int _CustomerID;

        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        private int _OrderType;

        public int OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }

        private DateTime _OrderDate;

        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }

        private DateTime? _CompleteDate;

        public DateTime? CompleteDate
        {
            get { return _CompleteDate; }
            set { _CompleteDate = value; }
        }

        private bool _IsApproved;

        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
    }

    public class Fabric
    {
        private int _FabricID;

        public int FabricID
        {
            get { return _FabricID; }
            set { _FabricID = value; }
        }

        private int _UtilityOrderID;

        public int UtilityOrderID
        {
            get { return _UtilityOrderID; }
            set { _UtilityOrderID = value; }
        }

        private int _FabricType;

        public int FabricType
        {
            get { return _FabricType; }
            set { _FabricType = value; }
        }

        private int _ColorID;

        public int ColorID
        {
            get { return _ColorID; }
            set { _ColorID = value; }
        }

        private int _FabricSize;

        public int FabricSize
        {
            get { return _FabricSize; }
            set { _FabricSize = value; }
        }

        private int _Boxes;

        public int Boxes
        {
            get { return _Boxes; }
            set { _Boxes = value; }
        }
        
    }

    public class RollerBlinds
    {
        private int _RollerBlindsID;

        public int RollerBlindsID
        {
            get { return _RollerBlindsID; }
            set { _RollerBlindsID = value; }
        }

        private int _UtilityOrderID;

        public int UtilityOrderID
        {
            get { return _UtilityOrderID; }
            set { _UtilityOrderID = value; }
        }

        private int _RollerBlindTypeID;

        public int RollerBlindTypeID
        {
            get { return _RollerBlindTypeID; }
            set { _RollerBlindTypeID = value; }
        }

        private int _Boxes;

        public int Boxes
        {
            get { return _Boxes; }
            set { _Boxes = value; }
        }

    }

    public class Valance
    {
        private int _ValanceID;

        public int ValanceID
        {
            get { return _ValanceID; }
            set { _ValanceID = value; }
        }

        private int _UtilityOrderID;

        public int UtilityOrderID
        {
            get { return _UtilityOrderID; }
            set { _UtilityOrderID = value; }
        }

        private int _MaterialID;

        public int MaterialID
        {
            get { return _MaterialID; }
            set { _MaterialID = value; }
        }

        private int _ColorID;

        public int ColorID
        {
            get { return _ColorID; }
            set { _ColorID = value; }
        }

        private string _Size;

        public string Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        private int _Boxes;

        public int Boxes
        {
            get { return _Boxes; }
            set { _Boxes = value; }
        }

    }

    public class BottomRail
    {
        private int _BottomRailID;

        public int BottomRailID
        {
            get { return _BottomRailID; }
            set { _BottomRailID = value; }
        }

        private int _UtilityOrderID;

        public int UtilityOrderID
        {
            get { return _UtilityOrderID; }
            set { _UtilityOrderID = value; }
        }

        private int _MaterialID;

        public int MaterialID
        {
            get { return _MaterialID; }
            set { _MaterialID = value; }
        }

        private int _ColorID;

        public int ColorID
        {
            get { return _ColorID; }
            set { _ColorID = value; }
        }

        private string _Size;

        public string Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        private int _Boxes;

        public int Boxes
        {
            get { return _Boxes; }
            set { _Boxes = value; }
        }

    }

    public class RollerBlindType
    {

        private int _RollerBlindTypeID;

        public int RollerBlindTypeID
        {
            get { return _RollerBlindTypeID; }
            set { _RollerBlindTypeID = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Profile;

        public string Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }

        private string _RollerColor;

        public string RollerColor
        {
            get { return _RollerColor; }
            set { _RollerColor = value; }
        }

        private string _DLXCODE;

        public string DLXCODE
        {
            get { return _DLXCODE; }
            set { _DLXCODE = value; }
        }

        private string _PCSCTN;

        public string PCSCTN
        {
            get { return _PCSCTN; }
            set { _PCSCTN = value; }
        }

        private string _MOQ;

        public string MOQ
        {
            get { return _MOQ; }
            set { _MOQ = value; }
        }
    }

    public class SlatStyle
    {
        private int _SlatStyleID;

        public int SlatStyleID
        {
            get { return _SlatStyleID; }
            set { _SlatStyleID = value; }
        }

        private string _SlatStyleDesc;

        public string SlatStyleDesc
        {
            get { return _SlatStyleDesc; }
            set { _SlatStyleDesc = value; }
        }

        private string _For;

        public string For
        {
            get { return _For; }
            set { _For = value; }
        }

    }

    public class CordStyle
    {
        private int _CordStyleID;

        public int CordStyleID
        {
            get { return _CordStyleID; }
            set { _CordStyleID = value; }
        }

        private string _CordStyleDesc;

        public string CordStyleDesc
        {
            get { return _CordStyleDesc; }
            set { _CordStyleDesc = value; }
        }

        private string _For;

        public string For
        {
            get { return _For; }
            set { _For = value; }
        }

    }

    public class Control
    {
        private int _ControlID;

        public int ControlID
        {
            get { return _ControlID; }
            set { _ControlID = value; }
        }

        private string _ControlDesc;

        public string ControlDesc
        {
            get { return _ControlDesc; }
            set { _ControlDesc = value; }
        }

        private string _For;

        public string For
        {
            get { return _For; }
            set { _For = value; }
        }

    }

    public class Material
    {
        private int _MaterialID;

        public int MaterialID
        {
            get { return _MaterialID; }
            set { _MaterialID = value; }
        }

        private string _MaterialDesc;

        public string MaterialDesc
        {
            get { return _MaterialDesc; }
            set { _MaterialDesc = value; }
        }

        private string _For;

        public string For
        {
            get { return _For; }
            set { _For = value; }
        }

    }

    public class Colors
    {
        private int _ColorsID;

        public int ColorsID
        {
            get { return _ColorsID; }
            set { _ColorsID = value; }
        }

        private string _ColorsDesc;

        public string ColorsDesc
        {
            get { return _ColorsDesc; }
            set { _ColorsDesc = value; }
        }

        private string _For;

        public string For
        {
            get { return _For; }
            set { _For = value; }
        }

    }
    public class Size
    {
        private int _SizeID;

        public int SizeID
        {
            get { return _SizeID; }
            set { _SizeID = value; }
        }

        private string _SizeDesc;

        public string SizeDesc
        {
            get { return _SizeDesc; }
            set { _SizeDesc = value; }
        }

        private string _For;

        public string For
        {
            get { return _For; }
            set { _For = value; }
        }

    }
    public class BlindType
    {
        private int _BlindTypeID;

        public int BlindTypeID
        {
            get { return _BlindTypeID; }
            set { _BlindTypeID = value; }
        }

        private string _BlindTypeDesc;

        public string BlindTypeDesc
        {
            get { return _BlindTypeDesc; }
            set { _BlindTypeDesc = value; }
        }

        private int _Val;

        public int Val
        {
            get { return _Val; }
            set { _Val = value; }
        }

    }
    public class OrderType
    {
        private int _OrderTypeID;

        public int OrderTypeID
        {
            get { return _OrderTypeID; }
            set { _OrderTypeID = value; }
        }

        private string _OrderTypeDesc;

        public string OrderTypeDesc
        {
            get { return _OrderTypeDesc; }
            set { _OrderTypeDesc = value; }
        }
        
    }
    public class OrderStatus
    {
        private int _OrderStatusID;

        public int OrderStatusID
        {
            get { return _OrderStatusID; }
            set { _OrderStatusID = value; }
        }

        private string _OrderStatusDesc;

        public string OrderStatusDesc
        {
            get { return _OrderStatusDesc; }
            set { _OrderStatusDesc = value; }
        }

    }

    public class EliteRoles
    {
        private int _EliteRolesID;

        public int EliteRolesID
        {
            get { return _EliteRolesID; }
            set { _EliteRolesID = value; }
        }

        private string _EliteRolesDesc;

        public string EliteRolesDesc
        {
            get { return _EliteRolesDesc; }
            set { _EliteRolesDesc = value; }
        }

    }

}
