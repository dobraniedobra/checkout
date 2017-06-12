# checkout

## tools
- Database: MS SQL file 
- ORM: Entity Framework (code first)
- Dependency injection: Autofac
- Unit tests: Nunit, Moq, Specsfor

## routes
POST `/shoppingList/` add new item to shopping list

GET `/shoppingList/` get shopping list

GET `/shoppingList/{name}`get shopping item by name

DELETE `/shoppingList/{name}` remove item from shopping list

PUT `/shoppingList/{name}?quantity={quantity}` update item's quantity

## project details
Connection string points to my local database file, so please modify path to run project.

Database was created using code first approach.

Solution consists of 5 projects 
- DataAccessLayer - models, DTOs, db context, migrations, extention methods and generic repository.
- Checkout.TechnicalTest.BusinessLayer - interface for service and its implementation.
- Checkout.TechnicalTest.BusinessLayer.Tests - unit tests for service created with BDD approach.
- Checkout.TechnicalTest.WebApi - APIcontroller and filters, dependency injection setup (see AutofacWebApiConfig.cs).
- Checkout.TechnicalTest.WebApi.Tests - unit test for controller and filters.
