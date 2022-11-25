namespace AutoRepairCRM.Areas.Admin.Constants;

public static class DataConstants
{
    /// <summary>
    /// Constants for Car input model
    /// </summary>
    public static class Car
    {
        public const int MaxCarMakeLength = 50;
        public const int MaxCarModelLength = 50;
        public const int MaxCarYearLength = 15;
    }

    /// <summary>
    /// Constants for Person input model
    /// </summary>
    public static class Person
    {
        public const int MaxCustomerFNameLength = 50;
        public const int MinCustomerFNameLength = 2;
        public const int MaxCustomerLNameLength = 50;
        public const int MinCustomerLNameLength = 2;
        public const int MaxCustomerPhoneLength = 50;
    }
    
    /// <summary>
    /// Constants for Employee input model
    /// </summary>
    public static class Employee
    {
        public const string EmployeeSalaryDecimal = "decimal(6,2)";
    }
    
    /// <summary>
    /// Constants for Service input model
    /// </summary>
    public static class Service
    {
        public const string ServicePriceDecimal = "decimal(7,2)";
    }
    
    /// <summary>
    /// Constants for ServiceType input model
    /// </summary>
    public static class ServiceType
    {
        public const int MaxServiceTypeLength = 25;
    }
}