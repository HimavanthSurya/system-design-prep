# Interface Segregation Principle in C# — Interview Preparation Notes

## 1. Topic Overview

### What is the Interface Segregation Principle?

The **Interface Segregation Principle**, commonly called **ISP**, is one of the five **SOLID** design principles.

> **ISP states that clients should not be forced to depend on methods they do not use.**

In simple words:

> Do not create one large interface with many unrelated methods. Instead, create smaller, focused interfaces so classes implement only what they actually need.

---

### Why Is It Important?

ISP is important because it helps create code that is:

- **Flexible**
- **Maintainable**
- **Easy to test**
- **Less coupled**
- **Easier to change**
- **More readable**

When a class is forced to implement methods it does not need, the design becomes fragile and confusing.

---

### What Problem Does ISP Solve?

ISP solves the problem of **fat interfaces**.

A **fat interface** is an interface that contains too many methods, often serving multiple responsibilities.

Example of a bad interface:

~~~csharp
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}
~~~

This may work for a human worker, but what about a robot?

~~~csharp
public class RobotWorker : IWorker
{
    public void Work()
    {
        // Robot works
    }

    public void Eat()
    {
        throw new NotImplementedException();
    }

    public void Sleep()
    {
        throw new NotImplementedException();
    }
}
~~~

This violates ISP because `RobotWorker` is forced to implement `Eat()` and `Sleep()` even though it does not need them.

---

### Where Is ISP Used in Real-World Projects?

ISP is commonly used in:

- Service-oriented architecture
- Dependency injection
- Repository patterns
- Clean Architecture
- Domain-driven design
- API client abstractions
- Plugin systems
- Unit testing and mocking
- Microservices
- Large enterprise applications

Example:

Instead of one large service interface:

~~~csharp
public interface IUserService
{
    void CreateUser();
    void DeleteUser();
    void SendEmail();
    void GenerateReport();
    void ExportToExcel();
}
~~~

Use smaller interfaces:

~~~csharp
public interface IUserCreator
{
    void CreateUser();
}

public interface IUserDeleter
{
    void DeleteUser();
}

public interface IUserEmailSender
{
    void SendEmail();
}

public interface IUserReportGenerator
{
    void GenerateReport();
}
~~~

---

### Very Simple Explanation

Imagine a restaurant menu.

If you only want coffee, you should not be forced to order breakfast, lunch, dinner, and dessert.

Similarly, a class should not be forced to implement methods it does not need.

---

## 2. Core Concepts

## Must Know Concepts

### 2.1 Interface

#### Definition

An **interface** defines a contract that a class must follow.

~~~csharp
public interface ILogger
{
    void Log(string message);
}
~~~

A class implementing this interface must provide the `Log` method.

~~~csharp
public class FileLogger : ILogger
{
    public void Log(string message)
    {
        // Write message to file
    }
}
~~~

#### Purpose

Interfaces are used to:

- Define contracts
- Enable abstraction
- Support dependency injection
- Reduce coupling
- Improve testability

#### Why It Exists

Interfaces allow code to depend on behavior rather than concrete implementation.

#### How It Works

When a class implements an interface, it promises to implement all members declared by that interface.

#### Key Points to Remember

- Interfaces define **what** should be done, not **how**.
- A class can implement multiple interfaces.
- Interfaces help achieve abstraction.
- ISP encourages small, focused interfaces.

#### Simple Example

~~~csharp
public interface IPrintable
{
    void Print();
}

public class Invoice : IPrintable
{
    public void Print()
    {
        // Print invoice
    }
}
~~~

#### Real-World Example

A payment system may define:

~~~csharp
public interface IPaymentProcessor
{
    void ProcessPayment(decimal amount);
}
~~~

Implementations:

~~~csharp
public class CreditCardPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount)
    {
        // Process credit card payment
    }
}

public class UpiPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount)
    {
        // Process UPI payment
    }
}
~~~

---

### 2.2 Fat Interface

#### Definition

A **fat interface** is an interface that contains too many methods, especially unrelated ones.

#### Purpose

A fat interface usually happens when developers try to group too many responsibilities into one contract.

#### Why It Exists

It often exists because of poor design, lack of separation, or attempts to create a "one-size-fits-all" interface.

#### How It Works

Classes implementing the fat interface must implement all methods, even those they do not need.

#### Bad Example

~~~csharp
public interface IMachine
{
    void Print();
    void Scan();
    void Fax();
}
~~~

This is problematic because a simple printer may not support scanning or faxing.

~~~csharp
public class BasicPrinter : IMachine
{
    public void Print()
    {
        // Print document
    }

    public void Scan()
    {
        throw new NotSupportedException();
    }

    public void Fax()
    {
        throw new NotSupportedException();
    }
}
~~~

#### Key Points to Remember

- Fat interfaces violate ISP.
- They increase coupling.
- They make implementations harder to maintain.
- They often lead to dummy methods or exceptions.

#### Real-World Example

A `IUserService` interface containing user creation, email sending, reporting, auditing, and exporting is likely a fat interface.

---

### 2.3 Client-Specific Interface

#### Definition

A **client-specific interface** is an interface designed for a particular client’s needs.

#### Purpose

It allows clients to depend only on methods they actually use.

#### Good Example

~~~csharp
public interface IPrinter
{
    void Print();
}

public interface IScanner
{
    void Scan();
}

public interface IFax
{
    void Fax();
}
~~~

Now classes can implement only the capabilities they support.

~~~csharp
public class BasicPrinter : IPrinter
{
    public void Print()
    {
        // Print document
    }
}

public class MultiFunctionPrinter : IPrinter, IScanner, IFax
{
    public void Print()
    {
        // Print document
    }

    public void Scan()
    {
        // Scan document
    }

    public void Fax()
    {
        // Fax document
    }
}
~~~

#### Key Points to Remember

- Interfaces should be based on client needs.
- Small interfaces are easier to implement.
- Classes can combine multiple small interfaces.

---

### 2.4 Role-Based Interfaces

#### Definition

A **role-based interface** represents a specific role or capability.

Examples:

- `IReadable`
- `IWritable`
- `IPrintable`
- `IScannable`
- `IEmailSender`

#### Purpose

Role-based interfaces make behavior explicit.

#### Example

~~~csharp
public interface IReadable
{
    string Read();
}

public interface IWritable
{
    void Write(string content);
}
~~~

A read-only file can implement only `IReadable`.

~~~csharp
public class ReadOnlyFile : IReadable
{
    public string Read()
    {
        return "File content";
    }
}
~~~

A writable file can implement both:

~~~csharp
public class EditableFile : IReadable, IWritable
{
    public string Read()
    {
        return "File content";
    }

    public void Write(string content)
    {
        // Write content
    }
}
~~~

#### Real-World Example

In .NET, examples of role-based interfaces include:

- `IEnumerable`
- `IDisposable`
- `IComparable`
- `ICloneable`

---

## Good to Know Concepts

### 2.5 Cohesion

#### Definition

**Cohesion** means how closely related the members of a module or interface are.

#### Purpose

High cohesion means an interface has a clear and focused responsibility.

#### Example of Low Cohesion

~~~csharp
public interface IAccountService
{
    void CreateAccount();
    void SendEmail();
    void GeneratePdfReport();
    void BackupDatabase();
}
~~~

These methods are not closely related.

#### Example of High Cohesion

~~~csharp
public interface IAccountCreator
{
    void CreateAccount();
}
~~~

#### Key Points

- ISP improves cohesion.
- Interfaces should represent one clear capability.
- High cohesion makes code easier to understand.

---

### 2.6 Coupling

#### Definition

**Coupling** is the degree of dependency between components.

#### Purpose

Low coupling means one part of the system can change without heavily affecting others.

#### ISP Connection

Fat interfaces increase coupling because clients become dependent on methods they do not use.

#### Example

If a client only needs `Print()`, it should not depend on an interface containing `Scan()` and `Fax()`.

---

### 2.7 Dependency Injection and ISP

#### Definition

**Dependency Injection** is a technique where dependencies are provided from outside instead of being created inside a class.

#### ISP Connection

ISP helps dependency injection by allowing classes to depend on small, focused abstractions.

#### Bad Example

~~~csharp
public interface IUserService
{
    void CreateUser();
    void DeleteUser();
    void SendWelcomeEmail();
    void GenerateUserReport();
}

public class UserController
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    public void Create()
    {
        userService.CreateUser();
    }
}
~~~

`UserController` only needs `CreateUser()` but depends on everything.

#### Better Example

~~~csharp
public interface IUserCreator
{
    void CreateUser();
}

public class UserController
{
    private readonly IUserCreator userCreator;

    public UserController(IUserCreator userCreator)
    {
        this.userCreator = userCreator;
    }

    public void Create()
    {
        userCreator.CreateUser();
    }
}
~~~

---

## Advanced Concepts

### 2.8 Interface Pollution

#### Definition

**Interface pollution** happens when too many tiny interfaces are created unnecessarily.

#### Purpose

ISP does not mean every method must have its own interface.

#### Problem

Too many interfaces can make the design hard to navigate.

#### Bad Example

~~~csharp
public interface IUserNameGetter
{
    string GetUserName();
}

public interface IUserEmailGetter
{
    string GetUserEmail();
}

public interface IUserAgeGetter
{
    int GetUserAge();
}
~~~

This may be excessive if these methods are always used together.

#### Key Point

> ISP means create focused interfaces, not artificially tiny interfaces.

---

### 2.9 ISP and Liskov Substitution Principle

#### Relationship

ISP often helps avoid violations of the **Liskov Substitution Principle**.

If a class implements methods by throwing `NotImplementedException`, it is often not safely substitutable.

#### Example

~~~csharp
public class RobotWorker : IWorker
{
    public void Work()
    {
        // Work
    }

    public void Eat()
    {
        throw new NotImplementedException();
    }
}
~~~

If code expects any `IWorker` to be able to eat, `RobotWorker` breaks that expectation.

---

### 2.10 ISP and Clean Architecture

In Clean Architecture, interfaces are often placed at boundaries:

- Application layer
- Domain layer
- Infrastructure layer
- Presentation layer

ISP ensures each boundary exposes only what each layer needs.

Example:

~~~csharp
public interface IOrderReader
{
    Order GetById(int orderId);
}

public interface IOrderWriter
{
    void Save(Order order);
}
~~~

A query handler may need only `IOrderReader`, while a command handler may need `IOrderWriter`.

---

## 3. How It Works Internally

### Architecture

ISP is a design principle, not a runtime mechanism. It affects how abstractions are structured.

Typical architecture:

~~~mermaid
flowchart TD
    A[Client Class] --> B[Small Focused Interface]
    B --> C[Concrete Implementation]

    D[Another Client Class] --> E[Different Focused Interface]
    E --> C
~~~

A single implementation can implement multiple small interfaces.

---

### Components

| Component | Description |
|---|---|
| Client | Code that consumes an interface |
| Interface | Contract used by the client |
| Concrete class | Class that implements the interface |
| Method contract | Method signature required by the interface |
| Dependency injection container | Optional component that provides implementation at runtime |

---

### Execution Flow

Example:

~~~csharp
public interface IEmailSender
{
    void SendEmail(string email, string message);
}

public class SmtpEmailSender : IEmailSender
{
    public void SendEmail(string email, string message)
    {
        // Send email using SMTP
    }
}

public class NotificationService
{
    private readonly IEmailSender emailSender;

    public NotificationService(IEmailSender emailSender)
    {
        this.emailSender = emailSender;
    }

    public void Notify(string email)
    {
        emailSender.SendEmail(email, "Welcome");
    }
}
~~~

Execution flow:

1. `NotificationService` depends on `IEmailSender`.
2. A concrete `SmtpEmailSender` is injected.
3. `Notify()` calls `SendEmail()`.
4. The concrete implementation handles the actual email logic.
5. `NotificationService` does not know or care how email is sent.

---

### Behind the Scenes

In C#:

- An interface defines metadata containing method contracts.
- A class implementing the interface must provide compatible method implementations.
- At runtime, interface calls are dispatched to the concrete implementation.
- The caller depends on the interface type, not the concrete class.

~~~mermaid
sequenceDiagram
    participant Client
    participant Interface as IEmailSender
    participant Implementation as SmtpEmailSender

    Client->>Interface: SendEmail()
    Interface->>Implementation: Dispatch call
    Implementation-->>Client: Email sent
~~~

---

### Important Implementation Details

- A class must implement all members of an interface.
- Multiple interfaces can be implemented by one class.
- Interfaces allow polymorphism.
- In C# 7.3, interfaces cannot contain default method implementations.
- If a class throws `NotImplementedException` for interface members, that may indicate ISP violation.
- Smaller interfaces improve test mocking.

---

## 4. Important Terminology

| Term | Meaning | Example |
| ---- | ------- | ------- |
| Interface | A contract defining methods/properties a class must implement | `ILogger` |
| ISP | Clients should not depend on methods they do not use | Split `IMachine` into `IPrinter`, `IScanner` |
| Client | Code that uses an interface | `ReportService` using `IPrinter` |
| Fat interface | Interface with too many unrelated members | `IUserService` with user, email, report, export methods |
| Role interface | Interface representing one capability | `IReadable`, `IWritable` |
| Cohesion | How closely related members are | `IEmailSender` has high cohesion |
| Coupling | Degree of dependency between components | Controller depends on service interface |
| Abstraction | Hiding implementation details behind a contract | `IPaymentProcessor` |
| Contract | Required behavior defined by an interface | `void Pay(decimal amount)` |
| Dependency Injection | Passing dependencies from outside | Constructor injection |
| Interface pollution | Too many unnecessary tiny interfaces | One interface per trivial method |
| Polymorphism | Calling different implementations through same abstraction | `ILogger` implemented by file and database loggers |
| SOLID | Five object-oriented design principles | SRP, OCP, LSP, ISP, DIP |
| LSP | Subtypes should be replaceable for base types | Avoid unsupported methods |
| Adapter | Pattern used to adapt one interface to another | Wrapping third-party API |

---

## 5. Important Comparisons

| Concept A | Concept B | Key Difference | When to Use |
| --------- | --------- | -------------- | ----------- |
| ISP | SRP | ISP focuses on interface design; SRP focuses on class responsibility | Use ISP for contracts, SRP for class design |
| ISP | DIP | ISP says keep interfaces focused; DIP says depend on abstractions | Use together in dependency injection |
| Fat Interface | Segregated Interfaces | Fat interface has many unrelated methods; segregated interfaces are focused | Prefer segregated interfaces |
| Interface | Abstract Class | Interface defines contract; abstract class can share base behavior | Use interface for capability, abstract class for shared behavior |
| Small Interface | Too Many Interfaces | Small interface is focused; too many interfaces create complexity | Balance based on client usage |
| Inheritance | Interface Composition | Inheritance shares base behavior; interface composition combines capabilities | Prefer interface composition for flexible behavior |
| `NotImplementedException` | Proper Interface Split | Exception hides bad design; split prevents unsupported members | Split when method does not apply |
| Generic Service Interface | Client-Specific Interface | Generic service may expose too much; client-specific exposes only needed methods | Prefer client-specific dependencies |
| Marker Interface | Behavioral Interface | Marker has no members; behavioral has method contracts | Use marker rarely |
| Composition | Monolithic Interface | Composition combines small contracts; monolithic interface forces all methods | Prefer composition |

---

## 6. Practical Examples

## Example 1: Printer System

### Problem

A basic printer is forced to implement scanning and faxing.

### Bad Design

~~~csharp
public interface IMachine
{
    void Print();
    void Scan();
    void Fax();
}

public class BasicPrinter : IMachine
{
    public void Print()
    {
        // Print document
    }

    public void Scan()
    {
        throw new NotSupportedException();
    }

    public void Fax()
    {
        throw new NotSupportedException();
    }
}
~~~

### Approach

Split the interface into smaller capability-based interfaces.

### Solution

~~~csharp
public interface IPrinter
{
    void Print();
}

public interface IScanner
{
    void Scan();
}

public interface IFax
{
    void Fax();
}

public class BasicPrinter : IPrinter
{
    public void Print()
    {
        // Print document
    }
}

public class MultiFunctionPrinter : IPrinter, IScanner, IFax
{
    public void Print()
    {
        // Print document
    }

    public void Scan()
    {
        // Scan document
    }

    public void Fax()
    {
        // Fax document
    }
}
~~~

### Why This Approach Was Chosen

- Each class implements only supported behavior.
- No dummy methods.
- Easier to maintain and test.

### Alternatives

- Use abstract base classes.
- Use optional methods with exceptions.
- Use feature flags.

### Trade-Offs

| Benefit | Trade-Off |
|---|---|
| Better design | More interfaces |
| Better testability | More files/classes |
| Lower coupling | Requires upfront design |

---

## Example 2: User Management Service

### Problem

A controller only needs to create users but depends on a large service interface.

### Bad Design

~~~csharp
public interface IUserService
{
    void CreateUser();
    void DeleteUser();
    void UpdateUser();
    void SendWelcomeEmail();
    void GenerateUserReport();
}
~~~

### Approach

Split based on use cases.

### Solution

~~~csharp
public interface IUserCreator
{
    void CreateUser();
}

public interface IUserDeleter
{
    void DeleteUser();
}

public interface IUserUpdater
{
    void UpdateUser();
}

public interface IUserReporter
{
    void GenerateUserReport();
}

public class UserService : IUserCreator, IUserDeleter, IUserUpdater, IUserReporter
{
    public void CreateUser()
    {
        // Create user
    }

    public void DeleteUser()
    {
        // Delete user
    }

    public void UpdateUser()
    {
        // Update user
    }

    public void GenerateUserReport()
    {
        // Generate report
    }
}
~~~

Controller:

~~~csharp
public class UserController
{
    private readonly IUserCreator userCreator;

    public UserController(IUserCreator userCreator)
    {
        this.userCreator = userCreator;
    }

    public void Create()
    {
        userCreator.CreateUser();
    }
}
~~~

### Why This Approach Was Chosen

The controller depends only on what it needs.

### Trade-Offs

- Better dependency boundaries
- More interfaces to manage
- More explicit design

---

## Example 3: Payment Gateway

### Problem

Different payment providers support different operations.

Some support refunds. Some do not.

### Bad Design

~~~csharp
public interface IPaymentGateway
{
    void Pay(decimal amount);
    void Refund(decimal amount);
    void SchedulePayment(decimal amount);
}
~~~

A provider that does not support scheduled payments may throw exceptions.

### Better Design

~~~csharp
public interface IPaymentProcessor
{
    void Pay(decimal amount);
}

public interface IRefundProcessor
{
    void Refund(decimal amount);
}

public interface IScheduledPaymentProcessor
{
    void SchedulePayment(decimal amount);
}
~~~

Implementation:

~~~csharp
public class CreditCardGateway : IPaymentProcessor, IRefundProcessor
{
    public void Pay(decimal amount)
    {
        // Process payment
    }

    public void Refund(decimal amount)
    {
        // Process refund
    }
}

public class BankTransferGateway : IPaymentProcessor, IScheduledPaymentProcessor
{
    public void Pay(decimal amount)
    {
        // Process bank transfer
    }

    public void SchedulePayment(decimal amount)
    {
        // Schedule bank transfer
    }
}
~~~

### Why This Works

Each gateway exposes only its supported capabilities.

---

## 7. Interview Questions

# Beginner Questions

## Question 1

### Question

What is the Interface Segregation Principle?

### Short Interview Answer

The Interface Segregation Principle says that a class should not be forced to implement methods it does not use. Interfaces should be small and focused.

### Detailed Explanation

Instead of creating one large interface with many unrelated methods, ISP recommends creating multiple smaller interfaces based on client needs.

### Example

~~~csharp
public interface IPrinter
{
    void Print();
}

public interface IScanner
{
    void Scan();
}
~~~

### Possible Follow-up Question

Why are large interfaces harmful?

---

## Question 2

### Question

What is a fat interface?

### Short Interview Answer

A fat interface is an interface with too many methods, especially methods that are unrelated or not required by all implementers.

### Detailed Explanation

Fat interfaces force classes to implement unnecessary methods, often leading to empty methods or `NotImplementedException`.

### Example

~~~csharp
public interface IMachine
{
    void Print();
    void Scan();
    void Fax();
}
~~~

### Possible Follow-up Question

How would you refactor a fat interface?

---

## Question 3

### Question

Why is throwing `NotImplementedException` often a sign of ISP violation?

### Short Interview Answer

Because it usually means the class was forced to implement a method it does not actually support.

### Detailed Explanation

If a method does not make sense for a class, the interface is probably too broad and should be split.

### Example

~~~csharp
public void Fax()
{
    throw new NotImplementedException();
}
~~~

### Possible Follow-up Question

Is `NotImplementedException` always bad?

---

# Intermediate Questions

## Question 4

### Question

How does ISP improve unit testing?

### Short Interview Answer

ISP improves unit testing by allowing tests to mock only the small interface required by the class under test.

### Detailed Explanation

When a class depends on a large interface, mocks must handle many unnecessary members. Small interfaces make tests simpler and more focused.

### Example

~~~csharp
public class ReportService
{
    private readonly IPrinter printer;

    public ReportService(IPrinter printer)
    {
        this.printer = printer;
    }

    public void PrintReport()
    {
        printer.Print();
    }
}
~~~

### Possible Follow-up Question

How does ISP relate to dependency injection?

---

## Question 5

### Question

How is ISP different from Single Responsibility Principle?

### Short Interview Answer

SRP focuses on classes having one reason to change, while ISP focuses on keeping interfaces small and client-specific.

### Detailed Explanation

SRP is about implementation responsibility. ISP is about abstraction responsibility.

### Example

A class may follow SRP but still implement a fat interface, causing ISP violation.

### Possible Follow-up Question

Can a class violate ISP but follow SRP?

---

## Question 6

### Question

Can one class implement multiple small interfaces?

### Short Interview Answer

Yes. That is a common way to follow ISP.

### Detailed Explanation

A class can support multiple capabilities by implementing multiple focused interfaces.

### Example

~~~csharp
public class MultiFunctionPrinter : IPrinter, IScanner, IFax
{
    public void Print()
    {
    }

    public void Scan()
    {
    }

    public void Fax()
    {
    }
}
~~~

### Possible Follow-up Question

Does this increase complexity?

---

# Advanced Questions

## Question 7

### Question

Can too many small interfaces be a problem?

### Short Interview Answer

Yes. Too many unnecessary interfaces can cause interface pollution and make the codebase harder to navigate.

### Detailed Explanation

ISP means interfaces should be focused, not artificially tiny. Related methods that are always used together can remain in one interface.

### Example

Creating separate interfaces for every property getter may be excessive.

### Possible Follow-up Question

How do you decide interface boundaries?

---

## Question 8

### Question

How does ISP relate to Liskov Substitution Principle?

### Short Interview Answer

ISP helps avoid LSP violations by preventing classes from implementing unsupported behavior.

### Detailed Explanation

If a class implements an interface but throws exceptions for some methods, it cannot safely substitute other implementations of that interface.

### Example

A `RobotWorker` implementing `Eat()` but throwing an exception violates substitutability.

### Possible Follow-up Question

Can ISP violation cause runtime bugs?

---

## Question 9

### Question

How would you apply ISP in a Clean Architecture project?

### Short Interview Answer

Define small interfaces around use cases and layer boundaries, such as separate reader and writer interfaces.

### Detailed Explanation

Query handlers can depend on read interfaces, while command handlers can depend on write interfaces.

### Example

~~~csharp
public interface IOrderReader
{
    Order GetById(int orderId);
}

public interface IOrderWriter
{
    void Save(Order order);
}
~~~

### Possible Follow-up Question

Should repositories always be split into read/write interfaces?

---

## 8. Tricky Interview Questions

## Question 1

### Question

Does ISP mean every method should have its own interface?

### Answer

No. ISP means interfaces should be focused around client needs. Creating one interface per method can cause interface pollution.

---

## Question 2

### Question

Is a large interface always bad?

### Answer

Not always. A large interface is bad when clients are forced to depend on members they do not use. If all members are cohesive and always used together, it may be acceptable.

---

## Question 3

### Question

Can an interface with only two methods violate ISP?

### Answer

Yes. If those two methods represent unrelated responsibilities and some clients need only one, it can violate ISP.

---

## Question 4

### Question

Can ISP be violated even if all methods are implemented?

### Answer

Yes. ISP is about unnecessary dependency, not only missing implementation. A class may implement all methods correctly but still be forced to depend on methods it does not need.

---

## Question 5

### Question

What is the hidden cost of ISP?

### Answer

More interfaces, more files, more dependency registrations, and potentially more complexity if overused.

---

## Question 6

### Question

Can ISP improve performance?

### Answer

Usually not directly. ISP mainly improves maintainability, testability, and design clarity. Performance impact is typically negligible.

---

## Question 7

### Question

Should service interfaces always be split by method?

### Answer

No. They should be split by client role, use case, or cohesive capability.

---

## Question 8

### Question

What if two clients need overlapping methods?

### Answer

Create interfaces based on meaningful capabilities. A class can implement multiple interfaces, and clients can depend on the smallest suitable abstraction.

---

## 9. Scenario-Based Questions

## Scenario 1: Large Repository Interface

### Scenario

You have this interface:

~~~csharp
public interface ICustomerRepository
{
    Customer GetById(int id);
    IEnumerable<Customer> GetAll();
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(int id);
    void ExportToCsv();
}
~~~

A reporting service only needs `GetAll()`.

### Approach

Split read, write, and export responsibilities.

### Solution

~~~csharp
public interface ICustomerReader
{
    Customer GetById(int id);
    IEnumerable<Customer> GetAll();
}

public interface ICustomerWriter
{
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(int id);
}

public interface ICustomerExporter
{
    void ExportToCsv();
}
~~~

### Why It Works

The reporting service depends only on `ICustomerReader`.

### Trade-Offs

- More focused dependencies
- More interfaces to maintain

### Alternatives

- Keep repository as-is if all clients truly need all methods.
- Use CQRS for larger systems.

### What the Interviewer Is Testing

- Ability to identify fat interfaces
- Understanding of client-specific abstractions
- Practical design judgment

---

## Scenario 2: Third-Party Payment API

### Scenario

A third-party payment provider supports payment but not refunds.

### Approach

Do not force all payment providers to implement refund methods.

### Solution

~~~csharp
public interface IPaymentProcessor
{
    void Pay(decimal amount);
}

public interface IRefundProcessor
{
    void Refund(decimal amount);
}
~~~

### Why It Works

Providers implement only supported operations.

### Trade-Offs

- Cleaner contracts
- Runtime feature discovery may be needed if capabilities vary dynamically

### Alternatives

- Capability flags
- Strategy pattern
- Adapter pattern

### What the Interviewer Is Testing

- Real-world ISP application
- Handling unsupported operations
- API abstraction design

---

## Scenario 3: Notification System

### Scenario

An application sends email, SMS, and push notifications.

### Bad Interface

~~~csharp
public interface INotificationService
{
    void SendEmail();
    void SendSms();
    void SendPush();
}
~~~

### Better Design

~~~csharp
public interface IEmailNotificationSender
{
    void SendEmail();
}

public interface ISmsNotificationSender
{
    void SendSms();
}

public interface IPushNotificationSender
{
    void SendPush();
}
~~~

### What the Interviewer Is Testing

- Role-based interface design
- Avoiding unsupported methods
- Dependency injection design

---

## 10. Real-World Usage

### Common Use Cases

- Logging abstractions
- Payment gateways
- Notification systems
- File storage providers
- Reporting systems
- Read/write repositories
- External API clients
- Authentication and authorization services

---

### When to Use ISP

Use ISP when:

- Interfaces have unrelated methods.
- Classes throw `NotImplementedException`.
- Clients depend on methods they do not use.
- Unit tests require mocking unnecessary methods.
- Implementations support different capabilities.
- Changes to one method force unrelated clients to rebuild or retest.

---

### When NOT to Use ISP

Avoid overusing ISP when:

- Methods are always used together.
- Splitting creates meaningless interfaces.
- The interface is already cohesive.
- The project is very small and added abstraction brings no value.
- The split is based on speculation, not real client needs.

---

### Design Considerations

- Prefer capability-based interfaces.
- Keep interfaces cohesive.
- Split based on client usage.
- Avoid premature abstraction.
- Avoid one-method interfaces unless the capability is meaningful.
- Use naming that clearly expresses behavior.

---

### Scalability

ISP improves design scalability because new capabilities can be added without forcing all existing implementations to change.

---

### Reliability

ISP improves reliability by reducing unsupported operations and unexpected runtime exceptions.

---

### Maintainability

ISP improves maintainability because changes affect fewer clients and fewer implementations.

---

## 11. Performance & Optimization

### Time Complexity

ISP does not usually affect algorithmic time complexity.

Calling an interface method is generally:

- Time complexity: **O(1)**
- Space complexity: **O(1)**

---

### Performance Considerations

- Interface dispatch has a small runtime cost.
- In most business applications, this cost is negligible.
- Maintainability benefits usually outweigh micro-performance concerns.

---

### Bottlenecks

ISP itself is rarely a performance bottleneck.

Common actual bottlenecks are:

- Database calls
- Network calls
- File I/O
- Serialization
- Large object allocations

---

### Optimization Techniques

- Do not split interfaces excessively.
- Avoid unnecessary abstraction in performance-critical hot paths.
- Keep interface boundaries meaningful.
- Use profiling before optimizing.

> Interview tip: ISP is mainly a design and maintainability principle, not a performance optimization technique.

---

## 12. Common Mistakes

### Conceptual Mistakes

- Thinking ISP means one method per interface.
- Thinking every large interface violates ISP.
- Confusing ISP with SRP.
- Thinking ISP applies only to interfaces, not abstract contracts.
- Ignoring client needs when designing interfaces.

---

### Implementation Mistakes

- Creating fat service interfaces.
- Throwing `NotImplementedException`.
- Adding unrelated methods to existing interfaces.
- Making controllers depend on broad services.
- Creating too many meaningless tiny interfaces.
- Splitting interfaces based on implementation instead of client usage.

---

### Interview Mistakes

- Giving only textbook definition.
- Not providing practical examples.
- Not explaining trade-offs.
- Saying small interfaces are always better.
- Forgetting to mention client-specific design.

---

## 13. Best Practices

### 1. Design Interfaces Around Client Needs

Clients should depend only on what they use.

### 2. Prefer Small, Cohesive Interfaces

Each interface should represent a clear capability.

### 3. Avoid Fat Interfaces

Large unrelated interfaces increase coupling and maintenance cost.

### 4. Avoid Dummy Implementations

Empty methods or `NotImplementedException` often indicate bad interface design.

### 5. Use Interface Composition

A class can implement multiple small interfaces.

### 6. Split by Capability, Not Randomly

Good interface names describe behavior, such as `IReadable`, `IWritable`, `IEmailSender`.

### 7. Keep Interfaces Stable

Changing public interfaces can affect many clients.

### 8. Avoid Interface Pollution

Do not create interfaces that do not provide real design value.

### 9. Combine ISP With Dependency Injection

Small interfaces make dependency injection cleaner and easier to test.

### 10. Refactor When Pain Appears

Apply ISP when there is evidence: unused methods, hard tests, broad dependencies, or unsupported operations.

---

## 14. Interview Cheat Sheet

### Key Definition

**Interface Segregation Principle:**  
Clients should not be forced to depend on methods they do not use.

---

### Core Concepts

- Avoid fat interfaces.
- Create client-specific interfaces.
- Keep interfaces cohesive.
- Split by role or capability.
- Use multiple small interfaces instead of one large interface.
- Avoid `NotImplementedException` for unsupported members.
- Balance ISP with simplicity.

---

### Important Differences

| Topic | Key Point |
|---|---|
| ISP vs SRP | ISP is about interfaces; SRP is about class responsibility |
| ISP vs DIP | ISP designs focused abstractions; DIP depends on abstractions |
| Small interface vs interface pollution | Small is good when meaningful; excessive splitting is harmful |
| Interface vs abstract class | Interface defines capability; abstract class shares behavior |

---

### Common Questions

- What is ISP?
- What is a fat interface?
- How does ISP improve testing?
- How does ISP relate to SOLID?
- Can too many interfaces be bad?
- How do you refactor a fat interface?
- How does ISP relate to dependency injection?

---

### Must-Remember Points

- ISP is about **client-specific contracts**.
- Classes should not implement methods they do not need.
- Fat interfaces increase coupling.
- Small interfaces improve testability.
- Do not over-split interfaces.
- Use practical judgment.

---

## 15. Rapid-Fire Interview Questions

1. **What is ISP?**  
   ISP says clients should not depend on methods they do not use.

2. **What is a fat interface?**  
   An interface with too many unrelated methods.

3. **Why are fat interfaces bad?**  
   They force classes to implement unnecessary methods and increase coupling.

4. **What is a client-specific interface?**  
   An interface designed around what a particular client needs.

5. **Does ISP mean one method per interface?**  
   No. It means interfaces should be focused and cohesive.

6. **Can one class implement multiple interfaces?**  
   Yes, and that is common when following ISP.

7. **How does ISP help testing?**  
   It allows mocking smaller, focused dependencies.

8. **What is a sign of ISP violation?**  
   Methods throwing `NotImplementedException`.

9. **Is a large interface always bad?**  
   No, only if clients are forced to depend on unused members.

10. **How does ISP relate to DIP?**  
    DIP depends on abstractions; ISP keeps those abstractions focused.

11. **How does ISP relate to SRP?**  
    SRP focuses on class responsibility; ISP focuses on interface responsibility.

12. **What is interface pollution?**  
    Creating too many unnecessary interfaces.

13. **What is a role-based interface?**  
    An interface representing a specific capability.

14. **Give an example of a role-based interface.**  
    `IReadable`, `IWritable`, `IPrintable`.

15. **Can ISP reduce runtime exceptions?**  
    Yes, by avoiding unsupported methods.

16. **Does ISP improve performance?**  
    Usually not directly; it improves design quality.

17. **When should you split an interface?**  
    When clients use different subsets of its methods.

18. **What is the main benefit of ISP?**  
    Lower coupling and better maintainability.

19. **What is a common ISP mistake?**  
    Splitting interfaces too aggressively.

20. **How would you refactor `IMachine` with `Print`, `Scan`, and `Fax`?**  
    Split it into `IPrinter`, `IScanner`, and `IFax`.

---

## 16. Mock Interview

Use this section interactively.

I will ask one question at a time. After you answer, I will:

1. Rate your answer from **1–10**
2. Tell you what you got right
3. Tell you what you missed
4. Correct mistakes
5. Give an ideal interview answer
6. Ask the next question

### Mock Interview Question 1

What is the Interface Segregation Principle, and why is it important?

> Answer this question first. Do not look for the ideal answer before attempting it.

---

## 17. Final Interview Revision

### 20–30 Most Important Things to Remember

1. ISP stands for **Interface Segregation Principle**.
2. It is one of the **SOLID** principles.
3. ISP says clients should not depend on methods they do not use.
4. The main problem ISP solves is **fat interfaces**.
5. A fat interface contains too many unrelated methods.
6. Classes should not be forced to implement unsupported behavior.
7. `NotImplementedException` often indicates ISP violation.
8. Small interfaces improve maintainability.
9. Small interfaces improve testability.
10. Small interfaces reduce coupling.
11. ISP helps dependency injection by creating focused abstractions.
12. ISP does not mean one method per interface.
13. Too many small interfaces can cause interface pollution.
14. Interfaces should be split by client need or capability.
15. A class can implement multiple small interfaces.
16. Role-based interfaces are common in good design.
17. Examples include `IReadable`, `IWritable`, `IPrinter`, and `ILogger`.
18. ISP is closely related to SRP, DIP, and LSP.
19. ISP focuses on interface design, while SRP focuses on class responsibility.
20. ISP helps avoid LSP violations caused by unsupported methods.
21. ISP is mostly about maintainability, not performance.
22. Large interfaces are acceptable only if they are cohesive and all clients need the members.
23. Avoid adding unrelated methods to existing interfaces.
24. Refactor fat interfaces into focused contracts.
25. Use interface composition for flexible design.
26. Do not create abstractions without real need.
27. In real projects, ISP is useful in services, repositories, notifications, payments, and API clients.
28. Good ISP design makes code easier to change.
29. Interviewers expect practical examples, not only definitions.
30. The best interview answer includes definition, problem, example, and trade-off.

---

## Final One-Line Interview Answer

The **Interface Segregation Principle** means that clients should depend only on the methods they actually use, so instead of creating large fat interfaces, we design small, focused, client-specific interfaces that improve maintainability, testability, and flexibility.