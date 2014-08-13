MessageBird's REST API for C#
===============================
This repository contains the open source C# client for MessageBird's REST API. Documentation can be found at: https://www.messagebird.com/developers

Requirements
-----

- [Sign up](https://www.messagebird.com/en/signup) for a free MessageBird account
- Create a new access_key in the developers sections
- MessageBird API client for C# requires **.NET Framework >= 4** and **JSON.NET >= 4.5.6**

Installation
-----

#### JSON.NET

The *MessageBird* project depends on JSON.NET, which can be installed using the *NuGet* package manager.
For more information on how to achieve this, see ['Managing NuGet packages using the dialog'](http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog).

Usage
-----

We have put some self-explanatory examples in the *Examples* project, but here is a quick breakdown on how it works. First, you need to set up a **MessageBird.Client**. Be sure to replace **YOUR_ACCESS_KEY** with something real.

```csharp
using System;
using MessageBird;
using MessageBird.Objects;

class Example
{
  static void Main(string[] args)
  {
    Client client = Client.CreateDefault("YOUR_ACCESS_KEY");
  }
}
```

That's easy enough. Now we can query the server for information. Lets use getting your balance overview as an example:

```csharp
// Get your balance
Balance balance = client.Balance();
```

#### Error handling
In case of an error the *client* throws an *ErrorException*.
The *ErrorException* objects contains either:
- an array of *Error* objects that encapsulates the errors in the response (see [API errors](https://www.messagebird.com/developers#api-errors) for more info),
- a *best effort* description when no error information from the endpoint is available.

See the provided examples on how to access error information from the *ErrorException*.

Documentation
----
Complete documentation, instructions, and examples are available at:
[https://www.messagebird.com/developers](https://www.messagebird.com/developers)


License
----
The MessageBird REST Client for C# is licensed under [The ISC License](http://choosealicense.com/licenses/isc/). Copyright (c) 2014, Remco Vermeulen
