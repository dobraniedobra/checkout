namespace Checkout.TechnicalTest.DataAccessLayer.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.DatabaseContext context)
        {

        }
    }
}
