namespace AutoRepairCRM.Database.Common;

public static class DataConstants
{
    public static class Car
    {
        public const int MaxCarMakeLength = 50;
        public const int MaxCarModelLength = 50;
        public const int MaxCarEngineNumberLength = 100;
    }


    public static class Person
    {
        public const int MaxCustomerFNameLength = 50;
        public const int MaxCustomerLNameLength = 50;
        public const int MaxCustomerPhoneLength = 50;
    }
    
    public static class Employee
    {
        public const string EmployeeSalaryDecimal = "decimal(6,2)";
    }
    
    public static class Service
    {
        public const string ServicePriceDecimal = "decimal(7,2)";
    }
    
    public static class ServiceType
    {
        public const int MaxServiceTypeLength = 25;
    }
}