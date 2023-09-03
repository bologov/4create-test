# 4create-test

C# .NET Developer test for 4Create.

## Description

Using .NET 6 and MySql and DDD-like approach (aggregate roots are not isolated) I implemented the API according to the specification.

Features of the API:
1. Has Swagger in the development environment.
2. Uses FluentValidation with FluentValidation.AutoValidation package to enable asynchronous validation of the input DTOs in the controllers. Validators are only making sure that the DTOs data is correct - properties are set, enum values are in the correct range, and employees of a new company have either Id set or all other properties as well.
3. Uses Hellang.Middleware.ProblemDetails to wrap exceptions into a ProblemDetails response. Custom BusinessException used for domain and application use-cases exceptions are wrapped into 409 Conflict response.
  1. In the development environment the ProblemDetails response includes the stack trace and the detail of the exception that has occured.
  2. In the production environment the ProblemDetails response does not include the stack trace and detail is included only for BusinessExceptions.
4. Uses EntityFramework and the Code-First approach to store the data in the MySQL database. In the development environment, the database would be created and migrated if it doesn't exist.
5. SaveChangesAsync() method is overriden to implement writing a SystemLog record of all classes derived from Entity<T> (except for SystemLog itself).
6. Guid is used as entity identifier thus entity id can be generated in code and that allows to avoid an extra round-trip to database.

## Task assumptions

Api-level:
1. On the creation of a company either employee attributes or employee id could be send along with the company dto. Sending both will produce an exception as updating of the existing employee was not requested.
2. An employee can not be created without a company (because that what employment means) but company can be created without employees.

Application- and Domain-level:
1. The CreatedBy field is mentioned only once in the task, but is not present in the entities description, so taking into account that authorisation hasn't been added to the API, I decided to omit it to for now.
2. While domain-level restrictions such as "employee title is unique within a company" could potentially be implemented as part of FluentValidation of input DTOs, I decided to leave them in the Domain level as it is where they should belong.
3. "Employee title is unique within a company" rule is implemented only on the application level and not enforced at the database level as the current data structure wouldn't allow to achieve that without overcomplicating database (using triggers or check constraint that would use a DB function) or duplicating the data (storing the title both on employee and in the many-to-many table). It would make sense to implement this constraint on the database level, if there was Person->Employment(with title)<-Company structure - in that case it would be possible to put a unique index on title and company id.
4. It was not requsted to write a SystemLog for changes in Employee<->Company relationship.

## Running

1. Make sure that the .net sdk has been installed. If not, you could find it here https://dotnet.microsoft.com/en-us/download/dotnet/6.0.
2. Make sure that the Docker Desktop has been installed.
3. Open cmd, navigate to the solution folder and run the following command :
```
	docker compose up
```
4. The command will build a docker image from the API solution and will start a container with it as well as an instance of MySQL server.

There is a [Postman collection](https://github.com/bologov/4create-test/blob/main/4create-test.postman_collection.json) available in the solution root.

Swagger UI would be accessible at http://localhost:7120/swagger/index.html

MySQL instance would be accessible at localhost:33061  (password is p4ssw0rd and the user is root).
