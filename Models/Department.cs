namespace webAPI_ASPNET.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string DEPARTMENTNAME { get; set; }
        public string DESCRIPTION { get; set; }

    }

    public class DepartmentRelation
    {
        public int ID { get; set; }
        public int IDUSER { get; set; }
        public int IDDEPARTMENT { get; set; }

    }

    // IDs fixos do DEPARTMENT usados pelo marketplace (Comprador/Vendedor).
    // Separados de propósito dos departamentos administrativos legados
    // (ADM / COMMON USER / ADM BRUNO) que já ocupavam os IDs 1, 2 e 3.
    public static class DepartmentIds
    {
        public const int Comprador = 10;
        public const int Vendedor = 20;
    }
}
