Q1. Controller aur Service mein kya difference hai?
A: Controller HTTP layer handle karta hai — request receive, validate, response return. Service business logic rakhta hai — database calls, LLM calls, calculations. Ye separation of concerns hai — testability, maintainability aur reusability ke liye zaroori. Controller thin rakho, service thick.

Q2. Dependency Injection kya hai aur kyun use karte hain?
A: DI ek design pattern hai jisme class apni dependencies khud create nahi karti, balki bahar se receive karti hai (usually constructor ke through). .NET ka built-in DI container Program.cs mein registration ke baad automatically inject karta hai. Fayde: loose coupling, testability (mock inject kar sakte ho), lifecycle management.

Q3. Service lifetimes — Singleton, Scoped, Transient mein difference?
A:

Singleton: Ek instance poore app lifetime ke liye (stateless services, cache)

Scoped: Har HTTP request ke liye ek instance (DB context, most services)

Transient: Har injection pe naya instance (lightweight helpers)

JARVIS mein ChatService Scoped hai kyunki har request ka apna state hoga (future mein DbContext bhi Scoped hoga).

Q4. DTO kya hai aur Entity se kaise different hai?
A: DTO (Data Transfer Object) sirf client-server communication ke liye data carry karta hai — koi logic nahi, koi database mapping nahi. Entity database table ko represent karta hai (EF Core mein). Rule: Entity ko direct API pe expose mat karo — DTO use karo. Reason: security (internal fields leak na ho), flexibility (API contract stable rahe), validation control.

Q5. HTTP status codes — 200, 400, 401, 403, 404, 500 kab use karte hain?
A:

200: Success

400: Client ne invalid data bheja (validation fail)

401: Authentication missing/invalid

403: Authenticated but not authorized

404: Resource not found

500: Server-side unhandled error

Q6. Middleware kya hai aur order kyun matter karta hai?
A: Middleware pipeline ka component hai jo har request/response ko process karta hai. Request pehle middleware 1, phir 2, phir 3 se guzarta hai, controller pe jaata hai, phir reverse order mein response wapas aata hai. Order important hai: Exception handling sabse pehle (sab catch kare), phir HTTPS redirect, phir auth, phir authorization, phir routing.

Q7. Global exception handling kaise implement karte hain?
A: Custom middleware banate hain jo try/catch ke saath _next(context) call karta hai. Exception catch hone pe:

ILogger se log karo (stack trace ke saath)

500 Internal Server Error set karo

Clean JSON response bhejo ({ error, requestId, timestamp })

Stack trace client ko MAT bhejo (security risk)
Program.cs mein app.UseMiddleware<ExceptionMiddleware>() sabse pehle register karo.

Q8. Structured logging kya hai? Normal logging se better kyun hai?
A: Structured logging mein data key-value pairs ke roop mein log hota hai (_logger.LogInformation("User {UserId} sent {Message}", userId, message)) — string concatenation nahi. Fayde: searchable/queryable (Application Insights mein "find all logs where UserId = gaurav"), performance (jab log level disabled ho to string banti hi nahi), dashboards aur alerts aasan.

Q9. CancellationToken kya karta hai?
A: Client agar request cancel kar de (browser band, timeout), to CancellationToken ke through ASP.NET Core signal bhejta hai. Isse long-running operations (DB queries, LLM calls) abort ho jaate hain — resources bachte hain. Har async method mein CancellationToken parameter le kar aage pass karo.

Q10. [ApiController] attribute kya karta hai?
A: Ye 3 cheezein enable karta hai:

Automatic model validation — invalid model pe automatically 400 return

Binding source inference — [FromBody], [FromQuery] automatically infer

ProblemDetails responses — standard error format
Enterprise APIs mein hamesha use karo.

Q11. record aur class mein difference?
A: record immutable reference type hai — value-based equality, with expression se non-destructive mutation, aur built-in ToString(). DTOs ke liye perfect. class mutable hai, reference equality. Entities/behaviour-heavy classes ke liye class use karo.

Q12. async Task<IActionResult> vs IActionResult — kaunsa kab?
A: async Task<IActionResult> jab method ke andar await ho (DB call, API call, file I/O). IActionResult jab synchronous ho (simple computation, in-memory). JARVIS mein chat endpoint async hai kyunki LLM call aayega.