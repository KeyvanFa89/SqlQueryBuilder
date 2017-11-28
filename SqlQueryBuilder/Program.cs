using System;


namespace SqlQueryBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Query query = new Query();

            query
                .Select("Id", "FirstName")
                .From("Persons")
                .BeginScope()
                .Where("Id", Operators.Equals)
                .And("FirstName", Operators.Equals)
                .Or("Id", Operators.GratherEquals)
                .EndScope();

            Query insertQuery = new Query();

            

            Console.WriteLine(query.TSql);

            Console.WriteLine("--------------------------------------");

            insertQuery
                .Insert("Categories", "Id", "CategoryName");

            Console.WriteLine(insertQuery.TSql);


            Console.WriteLine("--------------------------------------");

            Query UpdateQuery = new Query();

            UpdateQuery
                .Update("Products", "ProductName", "CategoryId")
                .Where("CategroyId",Operators.Equals);

            Console.WriteLine(UpdateQuery.TSql);

            Console.WriteLine("--------------------------------------");

            Query groupByHaving = new Query();

            groupByHaving
                .Select("COUNT(Msisdn)", "Msisdn")
                .From("Subscribers")
                .GroupBy("Msisdn")
                .Having("Msisdn",Operators.Grather);

            Console.WriteLine(groupByHaving.ToString());

            Console.ReadKey();
        }
    }
}
