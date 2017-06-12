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

