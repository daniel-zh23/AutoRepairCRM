namespace AutoRepairCRM.Database.Data.Constants;

/// <summary>
/// Data constants for all DTO models
/// </summary>
public static class DataConstants
{
    /// <summary>
    /// Constants for Car DTO model
    /// </summary>
    public static class Car
    {
        public const int MaxCarMakeLength = 50;
        public const int MaxCarModelLength = 50;
        public const int MaxCarEngineNumberLength = 100;
    }

    /// <summary>
    /// Constants for Person DTO model
    /// </summary>
    public static class Person
    {
        public const int MaxCustomerFNameLength = 50;
        public const int MaxCustomerLNameLength = 50;
        public const int MaxCustomerPhoneLength = 50;
    }
    
    /// <summary>
    /// Constants for Employee DTO model
    /// </summary>
    public static class Employee
    {
        public const string EmployeeSalaryDecimal = "decimal(6,2)";
    }
    
    /// <summary>
    /// Constants for Service DTO model
    /// </summary>
    public static class Service
    {
        public const string ServicePriceDecimal = "decimal(7,2)";
    }
    
    /// <summary>
    /// Constants for ServiceType DTO model
    /// </summary>
    public static class ServiceType
    {
        public const int MaxServiceTypeLength = 25;
    }
    
    /// <summary>
    /// Constants for ServiceType DTO model
    /// </summary>
    public static class ServiceState
    {
        public const int MaxServiceStateLength = 25;
    }
}