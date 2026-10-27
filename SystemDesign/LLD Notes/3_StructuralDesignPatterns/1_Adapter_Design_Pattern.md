# Adapter Design Pattern in C# — Interview Preparation Notes

## 1. Topic Overview

### What is the Adapter Design Pattern?

The **Adapter Design Pattern** is a **structural design pattern** that allows objects with incompatible interfaces to work together.

It acts like a **translator** between two systems.

### Simple Explanation

Imagine a mobile charger plug that does not fit a wall socket. You use a travel adapter. The adapter does not change the phone or the socket; it simply makes them compatible.

In software, an adapter does the same thing:

- Existing class has one interface.
- Client expects another interface.
- Adapter converts calls from the expected interface to the existing interface.

### Why Is It Important?

The Adapter pattern is important because it helps:

- Reuse existing code.
- Integrate legacy systems.
- Work with third-party libraries.
- Reduce changes to existing tested code.
- Follow the **Open/Closed Principle**.

### What Problem Does It Solve?

It solves the problem of **interface incompatibility**.

Example:

A payment system expects this interface:

~~~csharp
public interface IPaymentProcessor
{
    void Pay(decimal amount);
}
~~~

But a third-party payment gateway exposes this:

~~~csharp
public class StripeGateway
{
    public void MakePayment(double amountInDollars)
    {
    }
}
~~~

The method names and parameter types do not match. An adapter can bridge them.

### Where Is It Used in Real-World Projects?

Common real-world uses:

- Wrapping third-party APIs.
- Integrating old legacy code.
- Converting external DTOs to internal models.
- Making different payment gateways follow one common interface.
- Adapting logging frameworks.
- Adapting file storage providers like local disk, Azure Blob, AWS S3.
- Wrapping external services behind internal interfaces.
- Making old code testable by adapting it to interfaces.

---

## 2. Core Concepts

## Must Know Concepts

### 2.1 Target Interface

#### Definition

The **Target Interface** is the interface expected by the client code.

#### Purpose

It defines how the client wants to interact with a service.

#### Why It Exists

It allows client code to depend on a stable abstraction instead of depending on incompatible external classes.

#### How It Works

The client calls methods on the target interface. The adapter implements this interface and internally delegates the call to the incompatible class.

#### Key Points

- Client knows only the target interface.
- Adapter implements the target interface.
- Helps reduce coupling.

#### Simple Example

~~~csharp
public interface IPaymentProcessor
{
    void ProcessPayment(decimal amount);
}
~~~

#### Real-World Example

In an e-commerce application, the application may define `IPaymentProcessor`, while Stripe, PayPal, and Razorpay each have different APIs.

---

### 2.2 Adaptee

#### Definition

The **Adaptee** is the existing class whose interface is incompatible with what the client expects.

#### Purpose

It contains useful existing functionality that should be reused.

#### Why It Exists

Often, the adaptee is:

- Legacy code.
- Third-party library.
- External SDK.
- Old internal component.

#### How It Works

The adapter calls the adaptee’s existing methods and converts input/output if necessary.

#### Key Points

- Usually cannot or should not be modified.
- Has a useful implementation.
- Has an incompatible interface.

#### Simple Example

~~~csharp
public class LegacyPaymentGateway
{
    public void MakePayment(double amount)
    {
    }
}
~~~

#### Real-World Example

A banking application has an old SOAP service, but the new application expects a REST-like service interface.

---

### 2.3 Adapter

#### Definition

The **Adapter** is a class that implements the target interface and internally uses the adaptee.

#### Purpose

It translates client requests into calls that the adaptee understands.

#### Why It Exists

It avoids changing client code or adaptee code.

#### How It Works

1. Client calls adapter through target interface.
2. Adapter converts the call.
3. Adapter invokes adaptee.
4. Adapter returns the result in the expected format.

#### Key Points

- Implements the target interface.
- Holds a reference to the adaptee.
- Performs translation or conversion.
- Keeps integration logic isolated.

#### Simple Example

~~~csharp
public class LegacyPaymentAdapter : IPaymentProcessor
{
    private readonly LegacyPaymentGateway legacyPaymentGateway;

    public LegacyPaymentAdapter(LegacyPaymentGateway legacyPaymentGateway)
    {
        this.legacyPaymentGateway = legacyPaymentGateway;
    }

    public void ProcessPayment(decimal amount)
    {
        this.legacyPaymentGateway.MakePayment((double)amount);
    }
}
~~~

#### Real-World Example

A payment adapter converts your internal payment request to the format required by a payment gateway SDK.

---

### 2.4 Client

#### Definition

The **Client** is the code that uses the target interface.

#### Purpose

It performs business operations without depending on concrete external implementations.

#### Why It Exists

The client should not know about incompatible classes or third-party details.

#### How It Works

The client depends on the target interface and receives an adapter object.

#### Key Points

- Depends on abstraction.
- Does not know the adaptee.
- Easier to test.

#### Simple Example

~~~csharp
public class CheckoutService
{
    private readonly IPaymentProcessor paymentProcessor;

    public CheckoutService(IPaymentProcessor paymentProcessor)
    {
        this.paymentProcessor = paymentProcessor;
    }

    public void Checkout(decimal amount)
    {
        this.paymentProcessor.ProcessPayment(amount);
    }
}
~~~

---

## Good to Know Concepts

### 2.5 Object Adapter

#### Definition

An **Object Adapter** uses composition to wrap an adaptee object.

#### Purpose

It adapts behavior without inheritance.

#### How It Works

The adapter contains a private field of the adaptee type.

#### Key Points

- Uses composition.
- More flexible.
- Preferred in C#.
- Works even when adaptee is sealed.

#### Example

~~~csharp
public class ExternalEmailServiceAdapter : IEmailService
{
    private readonly ExternalEmailClient externalEmailClient;

    public ExternalEmailServiceAdapter(ExternalEmailClient externalEmailClient)
    {
        this.externalEmailClient = externalEmailClient;
    }

    public void Send(string to, string subject, string body)
    {
        this.externalEmailClient.SendEmail(to, subject, body);
    }
}
~~~

---

### 2.6 Class Adapter

#### Definition

A **Class Adapter** uses inheritance to adapt one interface to another.

#### Purpose

It adapts behavior by inheriting from the adaptee and implementing the target interface.

#### Key Points

- Uses inheritance.
- Less common in C#.
- C# does not support multiple class inheritance.
- Works only when inheritance is possible.

#### Example

~~~csharp
public class LegacyLogger
{
    public void WriteLog(string message)
    {
    }
}

public interface ILogger
{
    void Log(string message);
}

public class LoggerAdapter : LegacyLogger, ILogger
{
    public void Log(string message)
    {
        this.WriteLog(message);
    }
}
~~~

> Interview Tip: In C#, **object adapters are usually preferred** because composition is more flexible than inheritance.

---

### 2.7 Two-Way Adapter

#### Definition

A **Two-Way Adapter** supports both the target interface and the adaptee interface.

#### Purpose

It allows old and new systems to communicate in both directions.

#### Key Points

- Useful during migration.
- More complex.
- Can increase coupling.

#### Example Use Case

During migration from an old notification system to a new one, both old and new modules may need to communicate using different interfaces.

---

## Advanced Concepts

### 2.8 Adapter vs Anti-Corruption Layer

#### Definition

An **Anti-Corruption Layer** is a broader architectural boundary that protects a domain model from external systems.

#### Purpose

It prevents external models, naming, rules, and assumptions from leaking into core business logic.

#### Key Points

- Adapter is usually a class-level pattern.
- Anti-corruption layer is usually an architectural pattern.
- Anti-corruption layer may contain multiple adapters, mappers, validators, and translators.

#### Real-World Example

A domain model uses `CustomerAccount`, but an external banking API returns `CustAcctInfoDto`. An anti-corruption layer converts the external model into a clean domain model.

---

### 2.9 Adapter and Dependency Injection

#### Definition

Dependency Injection allows adapters to be injected behind interfaces.

#### Purpose

It helps switch implementations without changing business logic.

#### Example

~~~csharp
public interface ICurrencyConverter
{
    decimal Convert(string fromCurrency, string toCurrency, decimal amount);
}

public class ExternalCurrencyApiAdapter : ICurrencyConverter
{
    private readonly ExternalCurrencyApi externalCurrencyApi;

    public ExternalCurrencyApiAdapter(ExternalCurrencyApi externalCurrencyApi)
    {
        this.externalCurrencyApi = externalCurrencyApi;
    }

    public decimal Convert(string fromCurrency, string toCurrency, decimal amount)
    {
        double result = this.externalCurrencyApi.ConvertAmount(fromCurrency, toCurrency, (double)amount);
        return (decimal)result;
    }
}
~~~

---

## 3. How It Works Internally

### Architecture

The Adapter pattern has four main parts:

1. **Client**
2. **Target Interface**
3. **Adapter**
4. **Adaptee**

~~~mermaid
classDiagram
    class Client {
        +Execute()
    }

    class ITarget {
        <<interface>>
        +Request()
    }

    class Adapter {
        -Adaptee adaptee
        +Request()
    }

    class Adaptee {
        +SpecificRequest()
    }

    Client --> ITarget
    Adapter ..|> ITarget
    Adapter --> Adaptee
~~~

### Execution Flow

~~~mermaid
sequenceDiagram
    participant Client
    participant Adapter
    participant Adaptee

    Client->>Adapter: Calls Target method
    Adapter->>Adapter: Converts input if required
    Adapter->>Adaptee: Calls incompatible method
    Adaptee-->>Adapter: Returns result
    Adapter->>Adapter: Converts output if required
    Adapter-->>Client: Returns expected result
~~~

### Step-by-Step Internal Working

1. Client depends on a target interface.
2. Adapter implements the target interface.
3. Adapter receives the client request.
4. Adapter converts arguments if necessary.
5. Adapter calls the adaptee’s method.
6. Adaptee performs the actual work.
7. Adapter converts the result if needed.
8. Client receives data in the expected format.

### Behind the Scenes

The adapter may perform:

- Method name translation.
- Parameter conversion.
- Return type conversion.
- Exception translation.
- Data mapping.
- Format conversion.
- Protocol conversion.
- Unit conversion.
- Authentication wrapping.
- Logging or retry handling.

Example:

- Client uses `decimal`.
- Third-party API uses `double`.
- Adapter converts `decimal` to `double`.

---

## 4. Important Terminology

| Term | Meaning | Example |
| --- | --- | --- |
| Adapter | Class that converts one interface to another | `StripePaymentAdapter` |
| Target Interface | Interface expected by client | `IPaymentProcessor` |
| Adaptee | Existing incompatible class | `StripeGateway` |
| Client | Code that uses the target interface | `CheckoutService` |
| Object Adapter | Adapter using composition | Adapter has `StripeGateway` field |
| Class Adapter | Adapter using inheritance | Adapter inherits `LegacyLogger` |
| Wrapper | Common informal name for adapter | `HttpClientWrapper` |
| Translation | Converting one call format to another | `Pay()` to `MakePayment()` |
| Composition | Holding another object inside a class | `private readonly LegacyService service` |
| Legacy System | Existing old system being adapted | SOAP service |
| Third-Party SDK | External library with its own interface | Payment gateway SDK |
| Interface Incompatibility | Interfaces do not match | Different method names |
| Anti-Corruption Layer | Architectural boundary around external systems | Domain-to-API mapping layer |

---

## 5. Important Comparisons

| Concept A | Concept B | Key Difference | When to Use |
| --- | --- | --- | --- |
| Adapter | Facade | Adapter changes interface; Facade simplifies interface | Use Adapter for compatibility; Facade for simplification |
| Adapter | Decorator | Adapter changes interface; Decorator adds behavior without changing interface | Use Decorator for logging, caching, validation |
| Adapter | Proxy | Proxy controls access; Adapter converts interface | Use Proxy for lazy loading, security, remote access |
| Adapter | Bridge | Adapter works after incompatible interfaces exist; Bridge is designed upfront to separate abstraction and implementation | Use Bridge for planned extensibility |
| Object Adapter | Class Adapter | Object adapter uses composition; class adapter uses inheritance | Prefer Object Adapter in C# |
| Adapter | Mapper | Adapter adapts behavior; Mapper converts data | Use Mapper for DTO transformations |
| Adapter | Anti-Corruption Layer | Adapter is smaller; ACL is broader architectural boundary | Use ACL for complex external system integration |

### Adapter vs Facade Example

#### Adapter

Used when the interface is incompatible.

~~~csharp
public interface ILogger
{
    void Log(string message);
}

public class ThirdPartyLogger
{
    public void Write(string text)
    {
    }
}

public class LoggerAdapter : ILogger
{
    private readonly ThirdPartyLogger logger;

    public LoggerAdapter(ThirdPartyLogger logger)
    {
        this.logger = logger;
    }

    public void Log(string message)
    {
        this.logger.Write(message);
    }
}
~~~

#### Facade

Used to simplify a complex subsystem.

~~~csharp
public class OrderFacade
{
    public void PlaceOrder()
    {
        // Validate order
        // Reserve stock
        // Process payment
        // Send confirmation
    }
}
~~~

> Interview Tip: If the problem is **incompatible interfaces**, think Adapter. If the problem is **too many complex subsystem calls**, think Facade.

---

## 6. Practical Examples

## Example 1: Payment Gateway Adapter

### 1. Problem

The application expects all payment providers to follow `IPaymentProcessor`, but each provider has a different SDK.

### 2. Approach

Create an adapter for each payment gateway.

### 3. Solution

~~~csharp
using System;

public interface IPaymentProcessor
{
    PaymentResult ProcessPayment(PaymentRequest request);
}

public class PaymentRequest
{
    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public string CustomerId { get; set; }
}

public class PaymentResult
{
    public bool IsSuccess { get; set; }

    public string TransactionId { get; set; }

    public string Message { get; set; }
}

public class StripeGateway
{
    public StripeChargeResponse CreateCharge(double amount, string currency, string customerReference)
    {
        return new StripeChargeResponse
        {
            Success = true,
            ChargeId = "ch_123",
            StatusMessage = "Payment completed"
        };
    }
}

public class StripeChargeResponse
{
    public bool Success { get; set; }

    public string ChargeId { get; set; }

    public string StatusMessage { get; set; }
}

public class StripePaymentAdapter : IPaymentProcessor
{
    private readonly StripeGateway stripeGateway;

    public StripePaymentAdapter(StripeGateway stripeGateway)
    {
        this.stripeGateway = stripeGateway;
    }

    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        StripeChargeResponse response = this.stripeGateway.CreateCharge(
            (double)request.Amount,
            request.Currency,
            request.CustomerId);

        return new PaymentResult
        {
            IsSuccess = response.Success,
            TransactionId = response.ChargeId,
            Message = response.StatusMessage
        };
    }
}
~~~

### 4. Why This Approach Was Chosen

- Business code depends on `IPaymentProcessor`.
- Stripe-specific code stays isolated.
- Another provider can be added with another adapter.

### 5. Alternatives

- Directly call Stripe SDK everywhere.
- Modify business logic for each gateway.
- Use a facade over all gateways.

### 6. Trade-Offs

| Benefit | Trade-Off |
| --- | --- |
| Loose coupling | More classes |
| Easier testing | Mapping logic must be maintained |
| Easier provider replacement | Adapter can become complex |

---

## Example 2: Legacy XML Service to Modern Interface

### 1. Problem

A modern service expects a C# object, but a legacy service accepts XML strings.

### 2. Approach

Use an adapter that converts objects to XML and XML back to objects.

### 3. Solution

~~~csharp
public interface ICustomerService
{
    Customer GetCustomer(int customerId);
}

public class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; }
}

public class LegacyCustomerXmlService
{
    public string GetCustomerXml(string requestXml)
    {
        return "<Customer><CustomerId>101</CustomerId><Name>John</Name></Customer>";
    }
}

public class CustomerServiceAdapter : ICustomerService
{
    private readonly LegacyCustomerXmlService legacyService;

    public CustomerServiceAdapter(LegacyCustomerXmlService legacyService)
    {
        this.legacyService = legacyService;
    }

    public Customer GetCustomer(int customerId)
    {
        string requestXml = "<CustomerRequest><CustomerId>" + customerId + "</CustomerId></CustomerRequest>";
        string responseXml = this.legacyService.GetCustomerXml(requestXml);

        return new Customer
        {
            CustomerId = customerId,
            Name = "John"
        };
    }
}
~~~

### 4. Why This Approach Was Chosen

It keeps XML-specific legacy behavior away from modern business logic.

### 5. Alternatives

- Rewrite the legacy service.
- Expose XML throughout the application.
- Use a full anti-corruption layer.

### 6. Trade-Offs

- Adapter simplifies client code.
- XML parsing and validation must be handled carefully.
- Error handling can become complex.

---

## Example 3: Logger Adapter

### Problem

Application uses `ILogger`, but a third-party logger exposes `WriteLog`.

### Solution

~~~csharp
using System;

public interface ILogger
{
    void LogInfo(string message);

    void LogError(Exception exception);
}

public class ThirdPartyLogger
{
    public void WriteLog(string level, string message)
    {
    }
}

public class ThirdPartyLoggerAdapter : ILogger
{
    private readonly ThirdPartyLogger thirdPartyLogger;

    public ThirdPartyLoggerAdapter(ThirdPartyLogger thirdPartyLogger)
    {
        this.thirdPartyLogger = thirdPartyLogger;
    }

    public void LogInfo(string message)
    {
        this.thirdPartyLogger.WriteLog("INFO", message);
    }

    public void LogError(Exception exception)
    {
        this.thirdPartyLogger.WriteLog("ERROR", exception.ToString());
    }
}
~~~

---

## 7. Interview Questions

## Beginner Questions

### Question 1

**Question:** What is the Adapter Design Pattern?

**Short Interview Answer:**  
The Adapter pattern allows two incompatible interfaces to work together by wrapping one object with another object that exposes the expected interface.

**Detailed Explanation:**  
It is a structural design pattern. The adapter implements the interface expected by the client and internally calls the incompatible class.

**Example:**  
A `StripePaymentAdapter` implements `IPaymentProcessor` and internally calls `StripeGateway`.

**Possible Follow-up Question:**  
What are the main components of the Adapter pattern?

---

### Question 2

**Question:** What problem does the Adapter pattern solve?

**Short Interview Answer:**  
It solves interface incompatibility between existing code and client expectations.

**Detailed Explanation:**  
When a class has useful behavior but does not match the required interface, an adapter translates one interface into another.

**Example:**  
A legacy XML service can be adapted to a modern object-based service interface.

**Possible Follow-up Question:**  
Why not modify the existing class directly?

---

### Question 3

**Question:** What are the main components of Adapter pattern?

**Short Interview Answer:**  
Client, Target Interface, Adapter, and Adaptee.

**Detailed Explanation:**  
The client uses the target interface. The adapter implements the target interface. The adapter internally calls the adaptee.

**Example:**  
`CheckoutService` → `IPaymentProcessor` → `StripePaymentAdapter` → `StripeGateway`.

**Possible Follow-up Question:**  
Which component does the client depend on?

---

## Intermediate Questions

### Question 4

**Question:** Why is composition preferred over inheritance in Adapter pattern?

**Short Interview Answer:**  
Composition is more flexible, supports sealed classes, and avoids tight coupling to the adaptee’s inheritance hierarchy.

**Detailed Explanation:**  
Object adapters can wrap any compatible object and can be changed at runtime. Class adapters require inheritance and are limited in C# because C# does not support multiple class inheritance.

**Example:**  
`StripePaymentAdapter` has a `StripeGateway` field instead of inheriting from it.

**Possible Follow-up Question:**  
When would a class adapter be acceptable?

---

### Question 5

**Question:** How is Adapter different from Facade?

**Short Interview Answer:**  
Adapter changes an incompatible interface into an expected one. Facade simplifies a complex subsystem.

**Detailed Explanation:**  
Adapter is about compatibility. Facade is about simplification.

**Example:**  
Adapter: `ThirdPartyLoggerAdapter`.  
Facade: `OrderProcessingFacade`.

**Possible Follow-up Question:**  
Can Adapter and Facade be used together?

---

### Question 6

**Question:** How does Adapter support the Open/Closed Principle?

**Short Interview Answer:**  
It allows new integrations to be added through new adapter classes without modifying existing client code.

**Detailed Explanation:**  
The client depends on a stable interface. New external systems can be integrated by creating new adapters.

**Example:**  
Add `PayPalPaymentAdapter` without changing `CheckoutService`.

**Possible Follow-up Question:**  
Does Adapter always guarantee Open/Closed Principle?

---

## Advanced Questions

### Question 7

**Question:** What are the risks of putting too much logic inside an adapter?

**Short Interview Answer:**  
The adapter can become a god object, mixing translation, business rules, validation, retry logic, and orchestration.

**Detailed Explanation:**  
Adapters should focus mainly on interface translation. Complex business logic should remain in domain or application services.

**Example:**  
A payment adapter should translate payment calls, not decide discount eligibility.

**Possible Follow-up Question:**  
Where should validation logic be placed?

---

### Question 8

**Question:** How would you handle exceptions in an adapter?

**Short Interview Answer:**  
Catch external-specific exceptions and translate them into application-specific exceptions or result types.

**Detailed Explanation:**  
External exceptions should not leak into business logic. The adapter acts as a boundary.

**Example:**  
Convert `StripeApiException` into `PaymentFailedException`.

**Possible Follow-up Question:**  
Should adapters swallow exceptions?

---

### Question 9

**Question:** Is Adapter pattern useful in microservices?

**Short Interview Answer:**  
Yes. It is commonly used to isolate external APIs, message formats, protocols, and service contracts from internal business logic.

**Detailed Explanation:**  
Adapters help protect internal models from external service changes.

**Example:**  
A service consumes an external KYC API through `IKycVerificationService`.

**Possible Follow-up Question:**  
How is this related to hexagonal architecture?

---

## 8. Tricky Interview Questions

### Question 1

**Question:** Is an adapter just a wrapper?

**Answer:**  
Not always. A wrapper simply surrounds another object, but an adapter specifically converts one interface into another expected interface.

---

### Question 2

**Question:** Does Adapter modify the adaptee?

**Answer:**  
No. The adapter usually wraps the adaptee without modifying it.

---

### Question 3

**Question:** Can Adapter hide bad design?

**Answer:**  
Yes. Overusing adapters can hide poor abstractions or unnecessary incompatibility. If you own both interfaces, redesigning the interface may be better.

---

### Question 4

**Question:** Can an adapter call multiple adaptees?

**Answer:**  
Yes, but if it starts coordinating multiple systems heavily, it may become a facade, service, or anti-corruption layer.

---

### Question 5

**Question:** Should an adapter contain business logic?

**Answer:**  
Usually no. It should contain translation logic. Business decisions should stay in business services or domain logic.

---

### Question 6

**Question:** What happens if the external API changes?

**Answer:**  
Ideally, only the adapter changes. Client code remains stable because it depends on the target interface.

---

### Question 7

**Question:** Can Adapter improve testability?

**Answer:**  
Yes. Client code can be tested against the target interface using mocks or stubs instead of real external systems.

---

### Question 8

**Question:** Is DTO mapping the same as Adapter?

**Answer:**  
No. DTO mapping converts data. Adapter converts behavior/interface. However, adapters often contain mapping code.

---

## 9. Scenario-Based Questions

## Scenario 1: Multiple Payment Providers

### Scenario

An application supports Stripe today and needs to support PayPal next month.

### Approach

Define a common `IPaymentProcessor` and create one adapter per payment provider.

### Why It Works

Business logic stays provider-independent.

### Trade-Offs

- More classes.
- Provider-specific features may not fit the common interface.

### Alternatives

- Direct provider-specific calls.
- Payment gateway facade.
- Strategy pattern with adapters.

### What the Interviewer Is Testing

- Interface design.
- Loose coupling.
- Extensibility.
- Practical design judgment.

---

## Scenario 2: Legacy SOAP Service

### Scenario

A modern Web API needs data from an old SOAP service.

### Approach

Create an adapter that implements a clean application interface and internally calls the SOAP client.

### Why It Works

SOAP details do not leak into the application layer.

### Trade-Offs

- Adapter must handle XML, SOAP faults, and retries.
- Mapping logic may become large.

### Alternatives

- Rewrite SOAP service.
- Generate SOAP client and use it directly.
- Use an anti-corruption layer.

### What the Interviewer Is Testing

- Legacy integration.
- Error handling.
- Boundary design.

---

## Scenario 3: External Logging Library Replacement

### Scenario

A project wants to replace one logging library with another.

### Approach

Application code should use an internal `ILogger` abstraction. Each external logger gets an adapter.

### Why It Works

Only adapter registration changes when switching libraries.

### Trade-Offs

- Internal abstraction may not expose every feature of the external logger.
- Over-abstraction may be unnecessary for small apps.

### Alternatives

- Use external logger directly.
- Use built-in .NET logging abstractions.

### What the Interviewer Is Testing

- Dependency inversion.
- Maintainability.
- Avoiding vendor lock-in.

---

## 10. Real-World Usage

### Common Use Cases

- Payment gateway integration.
- Email service providers.
- SMS gateways.
- Cloud storage providers.
- Logging frameworks.
- Legacy database access.
- SOAP-to-REST integration.
- XML-to-object conversion.
- External API clients.
- Hardware/device communication.

### When to Use Adapter

Use Adapter when:

- Existing class has useful behavior but wrong interface.
- You cannot modify the existing class.
- You want to isolate third-party SDKs.
- You need stable internal interfaces.
- You are migrating legacy systems.
- You want better testability.

### When NOT to Use Adapter

Avoid Adapter when:

- Interfaces are already compatible.
- You control both interfaces and can redesign them.
- It adds unnecessary indirection.
- The adapter would only pass calls without meaningful translation.
- A facade, strategy, decorator, or mapper is more appropriate.

### Design Considerations

- Keep adapters thin.
- Avoid business rules inside adapters.
- Translate external exceptions.
- Keep external DTOs outside domain logic.
- Use dependency injection.
- Write unit tests for mapping and error handling.
- Avoid leaking third-party types through internal interfaces.

### Scalability

Adapter itself does not directly improve scalability, but it helps by:

- Isolating external services.
- Supporting provider replacement.
- Allowing retry/caching policies around external calls.
- Making failures easier to contain.

### Reliability

Adapters can improve reliability by centralizing:

- Timeout handling.
- Retry handling.
- Circuit breaker integration.
- Exception translation.
- Logging.
- Input/output validation.

### Maintainability

Adapters improve maintainability by:

- Reducing duplicated integration code.
- Keeping client code clean.
- Localizing external changes.
- Supporting easier testing.

---

## 11. Performance & Optimization

### Time Complexity

The Adapter pattern usually adds only a small constant-time overhead.

| Operation | Complexity |
| --- | --- |
| Method forwarding | `O(1)` |
| Simple parameter conversion | `O(1)` |
| Mapping a list of `n` items | `O(n)` |
| XML/JSON serialization | Depends on payload size, usually `O(n)` |

### Space Complexity

Usually minimal unless the adapter performs:

- Object mapping.
- Serialization.
- Large response buffering.
- Caching.
- Data transformation.

### Performance Considerations

Potential bottlenecks:

- Heavy object mapping.
- Reflection-based serialization.
- XML/JSON parsing.
- Network calls to external services.
- Repeated adapter creation.
- Synchronous blocking over asynchronous APIs.
- Excessive logging.

### Optimization Techniques

- Keep adapters stateless where possible.
- Reuse expensive clients like `HttpClient`.
- Avoid unnecessary serialization.
- Avoid reflection-heavy mapping in hot paths.
- Cache static mappings.
- Use async APIs for I/O-bound operations.
- Measure before optimizing.

> Interview Tip: Adapter pattern overhead is usually negligible. Real bottlenecks usually come from the adaptee, such as network calls, database calls, or serialization.

---

## 12. Common Mistakes

### Conceptual Mistakes

- Thinking Adapter and Facade are the same.
- Thinking Adapter always means inheritance.
- Thinking Adapter should contain business logic.
- Thinking every wrapper is an adapter.
- Confusing Adapter with DTO mapper.

### Implementation Mistakes

- Leaking external SDK types into internal interfaces.
- Putting too much logic in the adapter.
- Not handling external exceptions.
- Not validating external responses.
- Creating one large adapter for unrelated operations.
- Hardcoding credentials or configuration.
- Making adapters stateful unnecessarily.
- Not unit testing conversion logic.

### Interview Mistakes

- Giving only textbook definitions.
- Not explaining real-world usage.
- Not mentioning target, adapter, adaptee, and client.
- Confusing Adapter with Decorator or Facade.
- Not explaining object adapter vs class adapter.
- Ignoring trade-offs.

---

## 13. Best Practices

### 1. Depend on Internal Interfaces

Use internal target interfaces like `IPaymentProcessor`.

**Why it matters:**  
Business code stays independent of third-party APIs.

---

### 2. Prefer Object Adapter in C#

Use composition instead of inheritance.

**Why it matters:**  
It is more flexible and avoids inheritance limitations.

---

### 3. Keep Adapters Focused

Adapters should mainly translate interfaces.

**Why it matters:**  
Prevents adapters from becoming large and hard to maintain.

---

### 4. Do Not Leak External Types

Avoid returning third-party SDK objects from internal interfaces.

**Why it matters:**  
Leaking external types couples the application to the provider.

---

### 5. Translate Exceptions

Convert external exceptions into application-specific exceptions or result objects.

**Why it matters:**  
Client code should not depend on provider-specific exception types.

---

### 6. Test Mapping Logic

Write unit tests for input conversion, output conversion, and error handling.

**Why it matters:**  
Most adapter bugs happen in mapping.

---

### 7. Use Dependency Injection

Inject adapters through interfaces.

**Why it matters:**  
It improves testability and replaceability.

---

### 8. Avoid Over-Abstraction

Do not create adapters without a real incompatibility or boundary problem.

**Why it matters:**  
Unnecessary adapters add complexity.

---

## 14. Interview Cheat Sheet

### Key Definition

**Adapter Pattern:** A structural design pattern that allows incompatible interfaces to work together by converting one interface into another expected by the client.

### Core Components

- **Client:** Uses the target interface.
- **Target Interface:** Expected interface.
- **Adapter:** Implements target and wraps adaptee.
- **Adaptee:** Existing incompatible class.

### Must-Remember Differences

| Pattern | Main Purpose |
| --- | --- |
| Adapter | Makes incompatible interfaces compatible |
| Facade | Simplifies a complex subsystem |
| Decorator | Adds behavior without changing interface |
| Proxy | Controls access to an object |
| Bridge | Separates abstraction from implementation |

### Important Rules

- Prefer composition in C#.
- Keep adapters thin.
- Do not leak external models.
- Translate exceptions.
- Use adapters for external boundaries.
- Do not put business logic in adapters.

### Common Interview Questions

- What is Adapter pattern?
- What problem does it solve?
- Adapter vs Facade?
- Object adapter vs Class adapter?
- How does Adapter support Open/Closed Principle?
- Where have you used Adapter in real projects?
- What are the disadvantages?

---

## 15. Rapid-Fire Interview Questions

### 1. What type of pattern is Adapter?

Adapter is a **structural design pattern**.

### 2. What problem does Adapter solve?

It solves **interface incompatibility**.

### 3. What is the target interface?

The interface expected by the client.

### 4. What is the adaptee?

The existing incompatible class being adapted.

### 5. What is the adapter?

The class that converts the target call into an adaptee call.

### 6. Does Adapter change the adaptee?

No, it wraps or uses the adaptee.

### 7. Which is preferred in C#: object adapter or class adapter?

Object adapter is usually preferred.

### 8. Why is object adapter preferred?

Because it uses composition and is more flexible.

### 9. Adapter vs Facade?

Adapter changes an interface; Facade simplifies a subsystem.

### 10. Adapter vs Decorator?

Adapter changes interface; Decorator adds behavior while keeping the same interface.

### 11. Can Adapter improve testability?

Yes, because client code can depend on interfaces and mocks.

### 12. Should Adapter contain business rules?

Usually no. It should mainly contain translation logic.

### 13. Can Adapter be used with dependency injection?

Yes, and it commonly is.

### 14. Can Adapter call external APIs?

Yes, adapters often wrap third-party APIs.

### 15. Can Adapter convert data formats?

Yes, such as XML to object or external DTO to internal model.

### 16. What is a class adapter?

An adapter that uses inheritance.

### 17. What is an object adapter?

An adapter that uses composition.

### 18. What is a two-way adapter?

An adapter that supports both old and new interfaces.

### 19. What is the biggest risk of Adapter?

Adding unnecessary complexity or hiding poor design.

### 20. When should Adapter not be used?

When interfaces are already compatible or simple redesign is possible.

---

## 16. Mock Interview

This section is designed to be interactive.

I will ask one question at a time. After you answer, I will:

1. Rate your answer from **1–10**
2. Tell you what you got right
3. Tell you what you missed
4. Correct mistakes
5. Give an ideal interview answer
6. Ask the next question

### Mock Interview Question 1

**What is the Adapter Design Pattern, and what problem does it solve?**

> Reply with your answer, and I will evaluate it before asking Question 2.

---

## 17. Final Interview Revision

### 25 Most Important Things to Remember

1. Adapter is a **structural design pattern**.
2. It makes **incompatible interfaces work together**.
3. It acts as a **translator** between client and existing code.
4. Main components are **Client, Target Interface, Adapter, and Adaptee**.
5. The client depends on the **target interface**.
6. The adaptee is the existing incompatible class.
7. The adapter implements the target interface.
8. The adapter internally calls the adaptee.
9. Adapter helps reuse existing or legacy code.
10. Adapter is common with third-party SDKs.
11. Adapter supports the **Open/Closed Principle**.
12. Adapter supports **Dependency Inversion**.
13. Object adapter uses **composition**.
14. Class adapter uses **inheritance**.
15. In C#, object adapter is usually preferred.
16. Adapter is different from Facade.
17. Adapter changes an interface; Facade simplifies a subsystem.
18. Adapter is different from Decorator.
19. Adapter changes interface; Decorator adds behavior.
20. Keep adapters thin and focused.
21. Do not put core business rules inside adapters.
22. Do not leak external SDK types into internal code.
23. Translate external exceptions into application-level errors.
24. Test mapping, conversion, and error handling logic.
25. Use Adapter when integrating legacy systems, external services, or incompatible APIs.

### Final Interview Tip

> If asked to explain Adapter, use this structure:  
> **“Adapter is a structural pattern used when an existing class has useful behavior but an incompatible interface. We create an adapter that implements the interface expected by the client and internally delegates to the existing class, converting inputs, outputs, or method calls as needed.”**